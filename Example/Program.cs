// See https://aka.ms/new-console-template for more information

using Hexa.NET.OpenAL;
using OpenALTest;
using System.Diagnostics;
using System.Runtime.CompilerServices;

ALCdevicePtr device = OpenAL.OpenDevice();

ALCcontextPtr context = OpenAL.CreateContext(device);
CheckError(device);
OpenAL.DistanceModel(ALEnum.InverseDistanceClamped);
CheckError(device);

OpenAL.MakeContextCurrent(context);
CheckError(device);

var fs = File.OpenRead("CantinaBand60.wav");

OpenALWaveAudioStream stream = new(fs)
{
    Looping = true
};

uint source = 0;
OpenAL.GenSources(1, ref source);

stream.Initialize(source);

OpenAL.SourcePlay(source);

bool running = true;
while (running)
{
    stream.Update(source);
    running = !stream.ReachedEnd;
    Thread.Sleep(10);
}

stream.Dispose();

OpenAL.DeleteSources(1, ref source);
OpenAL.DestroyContext(context);
OpenAL.CloseDevice(device);

static bool CheckError(ALCdevicePtr device, [CallerFilePath] string filename = "", [CallerLineNumber] int line = 0, [CallerMemberName] string name = "")
{
    int error = OpenAL.GetError(device);
    if (error != OpenAL.ALC_NO_ERROR)
    {
        Debug.WriteLine($"***OpenAL ERROR*** ({filename}: {line}, {name})");
        throw error switch
        {
            OpenAL.ALC_INVALID_VALUE => new Exception("ALC_INVALID_VALUE: an invalid value was passed to an OpenAL function"),
            OpenAL.ALC_INVALID_DEVICE => new Exception("ALC_INVALID_DEVICE: a bad device was passed to an OpenAL function"),
            OpenAL.ALC_INVALID_CONTEXT => new Exception("ALC_INVALID_CONTEXT: a bad context was passed to an OpenAL function"),
            OpenAL.ALC_INVALID_ENUM => new Exception("ALC_INVALID_ENUM: an unknown enum value was passed to an OpenAL function"),
            OpenAL.ALC_OUT_OF_MEMORY => new Exception("ALC_OUT_OF_MEMORY: an unknown enum value was passed to an OpenAL function"),
            _ => new Exception($"UNKNOWN ALC ERROR: {error}"),
        };
    }
    return true;
}