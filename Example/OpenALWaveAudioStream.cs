﻿namespace OpenALTest
{
    using Hexa.NET.OpenAL;
    using System.Runtime.InteropServices;

    public unsafe class OpenALWaveAudioStream : IDisposable
    {
        private readonly Stream stream;
        private readonly uint* buffers;
        private readonly int bufferCount;
        private readonly int bufferSize;
        private readonly byte[] buffer;
        public readonly uint SampleOffset;
        public readonly uint ByteOffset;
        public readonly ALEnum Type;
        public readonly WaveHeader Header;
        public readonly ALFormat Format;
        private int position;
        private bool looping;
        private bool reachedEnd;
        private bool disposedValue;

        public OpenALWaveAudioStream(Stream stream, int bufferCount = 4, int bufferSize = 65536)
        {
            Type = ALEnum.Streaming;
            Header = new(stream);
            Format = Header.GetBufferFormat();
            if (Header.AudioFormat != WaveFormatEncoding.Pcm)
            {
                throw new NotSupportedException("Wav PCM only");
            }

            this.stream = stream;
            this.bufferCount = bufferCount;
            this.bufferSize = bufferSize;
            buffer = new byte[bufferSize];
            buffers = Alloc<uint>(bufferCount);
            OpenAL.GenBuffers(bufferCount, buffers);
        }

        private static T* Alloc<T>(int count) where T : unmanaged
        {
            return (T*)Marshal.AllocHGlobal(count * sizeof(T));
        }

        private static void Free(void* ptr)
        {
            Marshal.FreeHGlobal((nint)ptr);
        }

        public int Position => position;

        public bool ReachedEnd => reachedEnd;

        public bool Looping { get => looping; set => looping = value; }

        public event Action? EndOfStream;

        public void Reset()
        {
            reachedEnd = false;
            position = 0;
        }

        public void FullCommit(uint source)
        {
            stream.Position = Header.DataBegin;
            byte[] data = new byte[Header.DataSize];
            stream.Read(data);
            fixed (byte* buffer = data)
            {
                OpenAL.BufferData(buffers[0], (ALEnum)Format, buffer, Header.DataSize, Header.SampleRate);
            }

            OpenAL.SetSourceProperty(source, ALEnum.Buffer, (int)buffers[0]);
        }

        public void Initialize(uint source)
        {
            for (int i = 0; i < bufferCount; i++)
            {
                if (reachedEnd)
                {
                    return;
                }

                var absPosition = Header.DataBegin + position;

                long dataSizeToCopy = bufferSize;
                if (absPosition + bufferSize > stream.Length)
                {
                    dataSizeToCopy = stream.Length - absPosition;
                }

                stream.Position = absPosition;
                stream.Read(buffer, 0, (int)dataSizeToCopy);
                position += (int)dataSizeToCopy;

                if (dataSizeToCopy < bufferSize)
                {
                    if (!looping)
                    {
                        position = 0;
                        EndOfStream?.Invoke();
                        reachedEnd = true;
                        fixed (byte* pData = buffer)
                        {
                            OpenAL.BufferData(buffers[i], (ALEnum)Format, pData, (int)dataSizeToCopy, Header.SampleRate);
                        }
                        return;
                    }
                    stream.Position = Header.DataBegin;
                    stream.Read(buffer, (int)dataSizeToCopy, (int)(bufferSize - dataSizeToCopy));
                    position = (int)(bufferSize - dataSizeToCopy);
                }

                fixed (byte* pData = buffer)
                {
                    OpenAL.BufferData(buffers[i], (ALEnum)Format, pData, bufferSize, Header.SampleRate);
                }
                OpenAL.SourceQueueBuffers(source, 1, &buffers[i]);
            }
        }

        public void Update(uint source)
        {
            if (reachedEnd)
            {
                return;
            }

            int buffersProcessed;
            OpenAL.GetSourceProperty(source, ALEnum.BuffersProcessed, &buffersProcessed);

            if (buffersProcessed <= 0)
            {
                return;
            }

            while (buffersProcessed-- != 0)
            {
                uint bufferId;
                OpenAL.SourceUnqueueBuffers(source, 1, &bufferId);

                var absPosition = Header.DataBegin + position;

                long dataSizeToCopy = bufferSize;
                if (absPosition + bufferSize > stream.Length)
                {
                    dataSizeToCopy = stream.Length - absPosition;
                }

                stream.Position = absPosition;
                stream.Read(buffer, 0, (int)dataSizeToCopy);
                position += (int)dataSizeToCopy;

                if (dataSizeToCopy < bufferSize)
                {
                    if (!looping)
                    {
                        position = 0;
                        EndOfStream?.Invoke();
                        reachedEnd = true;
                        fixed (byte* pData = buffer)
                        {
                            OpenAL.BufferData(bufferId, (ALEnum)Format, pData, (int)dataSizeToCopy, Header.SampleRate);
                        }
                        return;
                    }
                    stream.Position = Header.DataBegin;
                    stream.Read(buffer, (int)dataSizeToCopy, (int)(bufferSize - dataSizeToCopy));
                    position = (int)(bufferSize - dataSizeToCopy);
                }

                fixed (byte* pData = buffer)
                {
                    OpenAL.BufferData(bufferId, (ALEnum)Format, pData, bufferSize, Header.SampleRate);
                }
                OpenAL.SourceQueueBuffers(source, 1, &bufferId);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                stream.Dispose();
                OpenAL.DeleteBuffers(bufferCount, buffers);
                Free(buffers);
                disposedValue = true;
            }
        }

        ~OpenALWaveAudioStream()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}