using System;
using System.IO;
using System.Text;

namespace MemoryManagerDemo
{
    class Program
    {
        //Main method
        static void Main(string[] args)
        {
            Console.WriteLine("Memory Management Demo in C#\n");

            // STACK DEMO
            Console.WriteLine("=== STACK DEMO ===");
            StackExample();

            // HEAP DEMO
            Console.WriteLine("\n=== HEAP DEMO ===");
            HeapExample();

            // BUFFER DEMO
            Console.WriteLine("\n=== BUFFER DEMO ===");
            BufferExample();

            Console.WriteLine("\n Demo complete!");
        }

        // STACK EXAMPLE (LIFO and Stack Overflow demo)
        static void StackExample()
        {
            Console.WriteLine("Imagine a stack of plates — Last In, First Out (LIFO).\n");

            Stack<string> plateStack = new Stack<string>();

            // Push plates onto the stack (LIFO: last plate added will be first removed)
            Console.WriteLine("Adding plates to the stack...");
            for (int i = 1; i <= 5; i++)
            {
                string plate = $"Plate #{i}";
                plateStack.Push(plate);
                Console.WriteLine($"Pushed {plate}");
            }

            Console.WriteLine("\nNow removing plates from the stack (LIFO order):");
            while (plateStack.Count > 0)
            {
                string plate = plateStack.Pop();
                Console.WriteLine($"Popped {plate}");
            }

            Console.WriteLine("\nAll plates have been removed from the stack.");

            // === STACK OVERFLOW DEMO ===
            Console.WriteLine("What happens when too many 'plates' are on the call stack:");

            try
            {
                CauseStackOverflow(1);
            }
            catch (StackOverflowException)
            {
                // This catch block won't trigger
                // .NET runtime immediately terminates the process to protect memory integrity
                Console.WriteLine("Stack overflow occurred!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }

        }

        // A recursive function that simulates an ever-growing call stack
        static void CauseStackOverflow(int level)
        {
            if (level > 10000)
            {
                Console.WriteLine(".NET Prevented actual crash — stack is overflowing!");
                return;
            }

            // Recursive call with no proper base case (would overflow in reality)
            CauseStackOverflow(level + 1);
        }

        // HEAP EXAMPLE
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

        //BUFFER EXAMPLE
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