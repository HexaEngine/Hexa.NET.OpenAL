// See https://aka.ms/new-console-template for more information
using HexaGen;
using HexaGen.Patching;

CsCodeGeneratorConfig config = CsCodeGeneratorConfig.Load("generator.json");
CsCodeGenerator generator = new(config);
generator.PatchEngine.RegisterPrePatch(new NamingPatch(["Alc", "Al"], NamingPatchOptions.None));
generator.LogToConsole();
generator.Generate(["include/main.h"], "../../../../Hexa.NET.OpenAL/Generated");