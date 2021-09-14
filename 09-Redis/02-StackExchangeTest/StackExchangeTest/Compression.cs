using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zlib;

namespace StackExchangeTest
{
    public static class Compression
    {
        /// <summary>
        /// GZip compress.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static byte[] GZipCompress(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                System.IO.Compression.GZipStream stream = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress);
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// GZip decompress.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static byte[] GZipDecompress(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream stream =
                    new System.IO.Compression.GZipStream(new MemoryStream(buffer), System.IO.Compression.CompressionMode.Decompress))
                {
                    byte[] temp = new byte[4096];
                    int numRead;
                    while ((numRead = stream.Read(temp, 0, temp.Length)) != 0)
                    {
                        ms.Write(temp, 0, numRead);
                    }
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Deflates the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static byte[] Deflate(string data)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(data);
            return DeflateRaw(buffer, buffer.Length);
        }

        /// <summary>
        /// Deflates the specified data.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="length">The length. It could be smaller than buffer.Length.</param>
        /// <returns></returns>
        public static byte[] DeflateRaw(byte[] buffer, int length)
        {
#if true
            // zlib.net
            using (MemoryStream ms = new MemoryStream())
            {
                ZOutputStream outZStream = new ZOutputStream(ms, zlibConst.Z_DEFAULT_COMPRESSION);
                outZStream.Write(buffer, 0, length);
                outZStream.Close();
                return ms.ToArray();
            }
#else
			// DeflateStream
			MemoryStream ms = new MemoryStream();
			DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Compress, true);
			deflateStream.Write(buffer, 0, buffer.Length);
			deflateStream.Close();
			return ms.ToArray();
#endif
        }

        /// <summary>
        /// Inflates the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Inflate(byte[] data)
        {
            byte[] buffer = InflateRaw(data, data.Length);
            return Encoding.Unicode.GetString(buffer);
        }
        /// <summary>
        /// Inflates the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="length">The length.  It could be smaller than buffer.Length.</param>
        /// <returns></returns>
        public static byte[] InflateRaw(byte[] data, int length)
        {
            byte[] buffer;

#if true
            // zlib.net
            using (System.IO.MemoryStream outStream = new System.IO.MemoryStream())
            {
                ZOutputStream outZStream = new ZOutputStream(outStream);
                outZStream.Write(data, 0, length);
                outZStream.Close();
                buffer = outStream.ToArray();
            }
#else
			// DeflateStream
			MemoryStream ms = new MemoryStream(data);
			int bufferSize = data.Length * 10;
			while (true)
			{
				ms.Position = 0;
				DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Decompress, true);
				buffer = new byte[bufferSize];
				int length = deflateStream.Read(buffer, 0, bufferSize);
				if (length < bufferSize)
				{
					break;
				}

				// Just in case the buffer it's not big enough
				bufferSize *= 10;
			}
			ms.Close();
#endif
            return buffer;
        }

        /// <summary>
        /// Inflates the specified data, built in rety logic for padded zeros at the end
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static byte[] InflateRawWithRetryForPaddedZeros(byte[] data, int length)
        {
            bool continueToRetry = true;
            int retryAttempts = 0;
            int lengthToUse = length;
            byte[] buffer = null;

            while (continueToRetry)
            {
                try
                {
                    buffer = InflateRaw(data, lengthToUse);
                    continueToRetry = false;
                }
                catch (Exception ex)
                {
                    if (ex.Message == "inflating: ")
                    {
                        retryAttempts++;
                        lengthToUse++;
                        if (lengthToUse > data.Length || retryAttempts > 129)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return buffer;
        }
    }
}
