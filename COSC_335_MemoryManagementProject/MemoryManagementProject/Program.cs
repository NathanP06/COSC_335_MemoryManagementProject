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
            StackExample.Run();

            // HEAP DEMO
            Console.WriteLine("\n=== HEAP DEMO ===");
            HeapExample.Run();

            // BUFFER DEMO
            Console.WriteLine("\n=== BUFFER DEMO ===");
            BufferExample.Run();

            Console.WriteLine("\n Demo complete!");
        }
    }
}