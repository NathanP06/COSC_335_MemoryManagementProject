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

            // BUFFER DEMOS
            Console.WriteLine("\n=== BUFFER DEMO - In Program/String ===");
            BufferExample.Run();

            Console.WriteLine("\n=== BUFFER DEMO 2 - In .txt file ===");
            BufferExample2.Run();

            Console.WriteLine("\n Demo complete!");
        }
    }
}