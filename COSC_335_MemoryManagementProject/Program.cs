using System;
using System.IO;
using System.Text;

namespace MemoryManagerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🧠 Memory Management Demo in C#\n");

            // 1️⃣ STACK DEMO
            Console.WriteLine("=== STACK DEMO ===");
            StackExample(3);

            // 2️⃣ HEAP DEMO
            Console.WriteLine("\n=== HEAP DEMO ===");
            HeapExample();

            // 3️⃣ BUFFER DEMO
            Console.WriteLine("\n=== BUFFER DEMO ===");
            BufferExample();

            Console.WriteLine("\n✅ Demo complete!");
        }

        // ----------------------------------------------------------
        // 1️⃣ STACK EXAMPLE
        // ----------------------------------------------------------
        static void StackExample(int depth)
        {
            // Each recursive call creates a new stack frame
            if (depth == 0)
            {
                Console.WriteLine("Reached the bottom of the stack!");
                return;
            }

            int localNumber = depth; // stored on the stack
            Console.WriteLine($"Stack depth: {localNumber}");

            // Recursive call (creates a new stack frame)
            StackExample(depth - 1);

            // When the function ends, this stack frame is popped off
            Console.WriteLine($"Returning from depth {localNumber}");
        }

        // ----------------------------------------------------------
        // 2️⃣ HEAP EXAMPLE
        // ----------------------------------------------------------
        static void HeapExample()
        {
            // Objects created with 'new' are stored on the heap
            Dog rover = new Dog("Rover");
            Dog bella = new Dog("Bella");

            rover.Speak();
            bella.Speak();

            // The garbage collector will clean these up later
            rover = null;
            bella = null;

            Console.WriteLine("Dogs are now unreferenced and eligible for garbage collection.");
        }

        class Dog
        {
            public string Name { get; set; } // stored on the heap

            public Dog(string name)
            {
                Name = name;
            }

            public void Speak()
            {
                Console.WriteLine($"{Name} says: Woof!");
            }
        }

        // ----------------------------------------------------------
        // 3️⃣ BUFFER EXAMPLE
        // ----------------------------------------------------------
        static void BufferExample()
        {
            // Imagine we’re streaming data in chunks (like reading from a file)
            string data = "This is a stream of data that will be read using a buffer.";
            byte[] allBytes = Encoding.UTF8.GetBytes(data);

            // A small 10-byte buffer
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