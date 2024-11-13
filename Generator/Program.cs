// See https://aka.ms/new-console-template for more information
using HexaGen;
using HexaGen.Patching;

BatchGenerator batch = new();
batch.Start()
    .Setup<CsCodeGenerator>("generator.json")
    .AddPrePatch(new NamingPatch(["Alc", "Al"], NamingPatchOptions.None))
    .Generate(["include/main.h"], "../../../../Hexa.NET.OpenAL/Generated", [.. Directory.GetFiles("include")])
    .Finish();
