using System;
using System.IO;
using System.Text;

namespace MemoryManagerDemo
{
    class BufferExample
    {
        public static void Run()
        {
            string data = "This is a string of data that will be read using a buffer. Buffers help manage memory efficiently by reading data in chunks. While this is a simple example, buffers are crucial in real-world applications for performance optimization and memory management.";

            byte[] allBytes = Encoding.UTF8.GetBytes(data);
            byte[] buffer = new byte[10];

            using (MemoryStream stream = new MemoryStream(allBytes))
            {
                int bytesRead;
                int chunkNumber = 1;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Buffer #{chunkNumber++}: {chunk}");
                }
            }

            Console.WriteLine("All data has been read using the buffer.");
        }
    }
}
