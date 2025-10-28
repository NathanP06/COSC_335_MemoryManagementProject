using System;
using System.IO;
using System.Text;

namespace MemoryManagerDemo
{
    class BufferExample2
    {
        public static void Run()
        {
            // Try to find `projecttext.txt` by searching upwards from the current
            // working directory first (when run from the project root) and then
            // from the assembly base directory (which is often the `bin` folder).
            // This prevents hard-coding the file into `bin` and works when the
            // project is run from different locations.
            string fileName = "projecttext.txt";

            string? filePath = FindFileUpwards(fileName, Directory.GetCurrentDirectory(), 6)
                               ?? FindFileUpwards(fileName, AppContext.BaseDirectory, 6);

            if (filePath == null)
            {
                Console.WriteLine($"File '{fileName}' not found.");
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

        // Helper: search for a file by walking up parent directories up to `maxLevels`
        static string? FindFileUpwards(string fileName, string startDirectory, int maxLevels)
        {
            try
            {
                var dir = new DirectoryInfo(startDirectory);
                for (int i = 0; i < maxLevels && dir != null; i++)
                {
                    string candidate = Path.Combine(dir.FullName, fileName);
                    if (File.Exists(candidate))
                        return candidate;

                    dir = dir.Parent;
                }
            }
            catch
            {
                // Ignore and fall through to return null
            }

            return null;
        }
    }
}