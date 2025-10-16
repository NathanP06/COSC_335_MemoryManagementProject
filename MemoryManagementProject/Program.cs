using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

class MemoryDemo
{
    static void StackExample(int x, int y)
    {
        int localVar = x; // Stored in stack memory
        int localVar2 = y; // Stored in stack memory
        Console.WriteLine($"[STACK] localVar = {localVar}");
    }

    static void HeapExample()
    {
        int[] heapArray = new int[3]; // Heap memory
        heapArray[0] = 42;
        Console.WriteLine($"[HEAP] heapArray[0] = {heapArray[0]}");
    }

    static void BufferExample()
    {
        byte[] buffer = Encoding.UTF8.GetBytes("Hello Buffers!");
        using (MemoryStream stream = new MemoryStream())
        {
            stream.Write(buffer, 0, buffer.Length);
            Console.WriteLine($"[BUFFER] Wrote {stream.Length} bytes to stream");
        }
    }

    static void Main()
    {
        Console.WriteLine("=== Memory Demo in C# ===\n");

        // Starts method "StackExample" which stores two variables in stack memory, then creates a third value
        // by combining these two values. The method then prints the value of the third variable.
        // After the method completes, all three variables are removed from stack memory.
        StackExample(2, 3);

        HeapExample();
        BufferExample();

        Console.WriteLine("\nTriggering garbage collection...");
        GC.Collect(); // Force GC (for demonstration only)
        Console.WriteLine("Garbage collection complete.");
    }
}
