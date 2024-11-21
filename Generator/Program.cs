// See https://aka.ms/new-console-template for more information
using CppAst;
using HexaGen;
using HexaGen.Core.Mapping;
using HexaGen.Metadata;
using HexaGen.Patching;
using System.Text;
using System.Text.RegularExpressions;

BatchGenerator batch = new();
batch.Start()
    .Setup<CsCodeGenerator>("generator.json")
    .AddPrePatch(new PatternNamingPatch("^AlSource(f|3F|fv|i|3I|iv)$", "SetSourceProperty"))
    .AddPrePatch(new PatternNamingPatch("^AlGetSource(f|3F|i|3I)$", "GetSourceProperty")
    {
        Transform = TransformFunc
    })
    .AddPrePatch(new PatternNamingPatch("^AlListener(f|3F|fv|i|3I|iv)$", "SetListenerProperty"))
    .AddPrePatch(new PatternNamingPatch("^AlGetListener(f|3F|i|3I)$", "GetListenerProperty")
    {
        Transform = TransformFunc
    })
    .AddPrePatch(new PatternNamingPatch("^AlBuffer(f|3F|fv|i|3I|iv)$", "SetBufferProperty"))
    .AddPrePatch(new PatternNamingPatch("^AlGetBuffer(f|3F|i|3I)$", "GetBufferProperty")
    {
        Transform = TransformFunc
    })

    .AddPrePatch(new NamingPatch(["Alc", "Al"], NamingPatchOptions.None))
    .AddPrePatch(new ConstantsToEnumPatch("AL_", "ALEnum", "int", ["AL_API", "AL_APIENTRY", "AL_MIN_METERS_PER_UNIT", "AL_MAX_METERS_PER_UNIT"]))
    .AddPrePatch(new ConstantsToEnumPatch("AL_FORMAT_", "ALFormat", "int"))
    .Generate(["include/main.h"], "../../../../Hexa.NET.OpenAL/Generated", [.. Directory.GetFiles("include")])
    .Finish();

void TransformFunc(CppFunction function, FunctionMapping mapping)
{
    mapping.CreateDefaultMappingParameters(function);
    foreach (var parameter in mapping.Parameters!)
    {
        parameter.UseOut = true;
    }
}

public class PatternNamingPatch : PrePatch
{
    private readonly Regex regex;
    private readonly List<NewNamePart> newNameParts = [];
    private readonly NamingPatchOptions patchOptions;

    private struct NewNamePart
    {
        public string Text;
        public int Start;
        public int Length;
        public bool IsLiteral;
        public int GroupIndex;

        public readonly ReadOnlySpan<char> AsSpan()
        {
            return Text.AsSpan(Start, Length);
        }

        public static implicit operator ReadOnlySpan<char>(NewNamePart part) => part.AsSpan();
    }

    public PatternNamingPatch(Regex regex, string newName, NamingPatchOptions patchOptions = NamingPatchOptions.None)
    {
        this.regex = regex;
        this.patchOptions = patchOptions;
        Prepare(newName);
    }

    public PatternNamingPatch(string pattern, string newName, RegexOptions options = RegexOptions.None, NamingPatchOptions patchOptions = NamingPatchOptions.None)
    {
        regex = new Regex(pattern, options | RegexOptions.Compiled);
        this.patchOptions = patchOptions;
        Prepare(newName);
    }

    public Action<CppFunction, FunctionMapping>? Transform { get; set; }

    private void Prepare(string name)
    {
        int s = 0;
        char last = '\0';
        for (int i = 0; i < name.Length; i++)
        {
            char c = name[i];
            if (c == '(' && last != '\\')
            {
                var span = name.AsSpan(i + 1);
                var end = span.IndexOf(')');

                if (end == -1)
                {
                    continue;
                }

                NewNamePart literalPart = new()
                {
                    Start = s,
                    Length = i - s,
                    Text = name,
                    IsLiteral = true
                };
                newNameParts.Add(literalPart);

                span = span[..end];
                int group = int.Parse(span);
                NewNamePart groupPart = new()
                {
                    Start = i,
                    Length = end + 1,
                    Text = name,
                    GroupIndex = group
                };
                newNameParts.Add(groupPart);
                i = s = end + 1;
            }
            last = c;
        }

        if (s != name.Length)
        {
            NewNamePart literalPart = new()
            {
                Start = s,
                Length = name.Length - s,
                Text = name,
                IsLiteral = true
            };
            newNameParts.Add(literalPart);
        }
    }

    private readonly StringBuilder sb = new();

    private bool Process(ref string name)
    {
        var match = regex.Match(name);
        if (match.Success)
        {
            sb.Clear();
            foreach (var part in newNameParts)
            {
                if (part.IsLiteral)
                {
                    sb.Append(part.AsSpan());
                }
                else
                {
                    sb.Append(match.Groups[part.GroupIndex].ValueSpan);
                }
            }
            name = sb.ToString();
            return true;
        }
        return false;
    }

    protected override void PatchFunction(CsCodeGeneratorConfig config, CppFunction cppFunction)
    {
        string csFunctionName = config.GetCsFunctionName(cppFunction.Name);
        if (Process(ref csFunctionName))
        {
            if (!config.TryGetFunctionMapping(csFunctionName, out FunctionMapping? mapping))
            {
                mapping = new FunctionMapping(cppFunction.Name, csFunctionName, null, new Dictionary<string, string>(), new List<Dictionary<string, string>>());
                config.FunctionMappings.Add(mapping);
            }

            FunctionMapping functionMapping = mapping;
            if (functionMapping.FriendlyName == null)
            {
                string text2 = (functionMapping.FriendlyName = csFunctionName);
            }

            Transform?.Invoke(cppFunction, mapping);
        }
    }
}

public class ConstantsToEnumPatch : PrePatch
{
    private readonly string macroPrefix;

    private readonly string csEnumName;

    private readonly string baseType;

    private readonly HashSet<string> ignored;

    private readonly HashSet<string> extra;

    public ConstantsToEnumPatch(string macroPrefix, string csEnumName, string baseType, HashSet<string>? ignored = null, HashSet<string>? extra = null)
    {
        this.macroPrefix = macroPrefix;
        this.csEnumName = csEnumName;
        this.baseType = baseType;
        this.ignored = ignored ?? new HashSet<string>();
        this.extra = extra ?? new HashSet<string>();
    }

    protected override void PatchCompilation(CsCodeGeneratorConfig settings, CppCompilation compilation)
    {
        List<CppMacro> list = new List<CppMacro>();
        HashSet<string> hashSet = new HashSet<string>();
        foreach (CppMacro macro in compilation.Macros)
        {
            if (!ignored.Contains(macro.Name) && (macro.Name.StartsWith(macroPrefix) || extra.Contains(macro.Name)))
            {
                list.Add(macro);
                hashSet.Add(macro.Name);
            }
        }

        CsEnumMetadata csEnumMetadata = new(macroPrefix, csEnumName, new List<string>(), null)
        {
            BaseType = baseType
        };
        EnumPrefix enumNamePrefixEx = settings.GetEnumNamePrefixEx(macroPrefix);
        foreach (CppMacro item2 in list)
        {
            string enumName = settings.GetEnumName(item2.Name, enumNamePrefixEx);
            string text = item2.Value;
            if (text.IsNumeric(out var numberType))
            {
                if ((numberType & NumberType.AnyFloat) != 0)
                {
                    continue;
                }
            }
            else
            {
                if (text.IsConstantExpression() || text.IsString())
                {
                    continue;
                }

                if (hashSet.Contains(text))
                {
                    text = settings.GetEnumName(text, enumNamePrefixEx);
                }
                else
                {
                    foreach (string item3 in hashSet)
                    {
                        int num = text.IndexOf(item3);
                        if (num != -1)
                        {
                            text = text.Remove(num, item3.Length);
                            text = text.Insert(num, settings.GetEnumName(item3, enumNamePrefixEx));
                        }
                    }
                }
            }

            CsEnumItemMetadata item = new CsEnumItemMetadata(item2.Name, item2.Value, enumName, text, new List<string>(), null);
            csEnumMetadata.Items.Add(item);
            settings.IgnoredConstants.Add(item2.Name);
        }

        settings.CustomEnums.Add(csEnumMetadata);
    }
}