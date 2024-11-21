namespace Hexa.NET.OpenAL
{
    using System.Numerics;

    public static unsafe partial class OpenAL
    {
        /// <summary>
        /// Opens the named playback device. <br/>
        /// </summary>
        public static unsafe ALCdevicePtr OpenDevice()
        {
            ALCdevicePtr ret = OpenDeviceNative(null);
            return ret;
        }

        /// <summary>
        /// Create and attach a context to the given device. <br/>
        /// </summary>
        public static unsafe ALCcontextPtr CreateContext(ALCdevicePtr device)
        {
            ALCcontextPtr ret = CreateContextNative(device, null);
            return ret;
        }

        /// <summary>
        /// Set source parameters. <br/>
        /// </summary>
        public static void SetSourceProperty(uint source, ALEnum param, Vector3 vector)
        {
            SetSourcePropertyNative(source, param, vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Get source parameters. <br/>
        /// </summary>
        public static void GetSourceProperty(uint source, ALEnum param, out Vector3 vector)
        {
            fixed (Vector3* pvalue = &vector)
            {
                GetSourcePropertyNative(source, param, &pvalue->X, &pvalue->Y, &pvalue->Z);
            }
        }

        /// <summary>
        /// Set listener parameters. <br/>
        /// </summary>
        public static void SetListenerProperty(ALEnum param, Vector3 vector)
        {
            SetListenerPropertyNative(param, vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Get listener parameters. <br/>
        /// </summary>
        public static void GetListenerProperty(ALEnum param, out Vector3 vector)
        {
            fixed (Vector3* pvalue = &vector)
            {
                GetListenerPropertyNative(param, &pvalue->X, &pvalue->Y, &pvalue->Z);
            }
        }

        /// <summary>
        /// Set source parameters. <br/>
        /// </summary>
        public static void SetBufferProperty(uint buffer, ALEnum param, Vector3 vector)
        {
            SetBufferPropertyNative(buffer, param, vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Get source parameters. <br/>
        /// </summary>
        public static void GetBufferProperty(uint buffer, ALEnum param, out Vector3 vector)
        {
            fixed (Vector3* pvalue = &vector)
            {
                GetBufferPropertyNative(buffer, param, &pvalue->X, &pvalue->Y, &pvalue->Z);
            }
        }
    }
}