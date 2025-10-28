using System;
using System.IO;
using System.Text;

namespace MemoryManagerDemo
{
    class BufferExample2
    {
        public static void Run()
        {
            string filePath = "projecttext.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File '{filePath}' not found.");
                return;
            }

            Console.WriteLine($"Reading data from '{filePath}' using a buffer:\n");

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BufferedStream bufferedStream = new BufferedStream(fileStream))
            using (StreamReader reader = new StreamReader(bufferedStream))
            {
                string? line;
                int lineNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"Line {lineNumber++}: {line}");
                }
            }

            Console.WriteLine("\n=== END OF FILE ===");
        }
    }
}