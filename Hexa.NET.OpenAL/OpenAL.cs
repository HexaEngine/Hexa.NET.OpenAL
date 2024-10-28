namespace Hexa.NET.OpenAL
{
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
    }
}