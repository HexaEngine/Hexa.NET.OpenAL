namespace Hexa.NET.OpenAL
{
    using HexaGen.Runtime;
    using System.Runtime.InteropServices;

    public static partial class OpenAL
    {
        static OpenAL()
        {
            InitApi();
        }

        public static string GetLibraryName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "OpenAL32";
            }
            return "libopenal";
        }

        /// <summary>
        /// Opens the named playback device. <br/>
        /// </summary>
        [NativeName(NativeNameType.Func, "alcOpenDevice")]
        [return: NativeName(NativeNameType.Type, "ALCdevice*")]
        public static unsafe ALCdevicePtr OpenDevice()
        {
            ALCdevicePtr ret = OpenDeviceNative(null);
            return ret;
        }

        /// <summary>
        /// Create and attach a context to the given device. <br/>
        /// </summary>
        [NativeName(NativeNameType.Func, "alcCreateContext")]
        [return: NativeName(NativeNameType.Type, "ALCcontext*")]
        public static unsafe ALCcontextPtr CreateContext([NativeName(NativeNameType.Param, "device")][NativeName(NativeNameType.Type, "ALCdevice*")] ALCdevicePtr device)
        {
            ALCcontextPtr ret = CreateContextNative(device, null);
            return ret;
        }
    }
}