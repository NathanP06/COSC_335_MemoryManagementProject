using System;
using System.IO;
using System.Text;

class MemoryDemo
{
    static void StackExample(int x)
    {
        int localVar = x; // Stack memory
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

        StackExample(10);
        HeapExample();
        BufferExample();

        Console.WriteLine("\nTriggering garbage collection...");
        GC.Collect(); // Force GC (for demonstration only)
        Console.WriteLine("Garbage collection complete.");
    }
}
