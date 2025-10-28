using System;
using System.IO;
using System.Text;

namespace MemoryManagerDemo
{

    // STACK EXAMPLE (LIFO and Stack Overflow demo).
    class StackExample
    {
        public static void Run()
        {
            Console.WriteLine("Imagine a stack of plates — Last In, First Out (LIFO).\n");

            //Creates a new stack using C# Stack Class
            Stack<string> plateStack = new Stack<string>();

            // Push plates onto the stack (LIFO: last plate added will be first removed)
            Console.WriteLine("Adding plates to the stack...");
            for (int i = 1; i <= 5; i++)
            {
                string plate = $"Plate #{i}";
                plateStack.Push(plate);
                Console.WriteLine($"Pushed {plate}");
            }

            // Printing the current stack shows us that the last plate added is on top
            Console.WriteLine("\nCurrent stack of plates:");
            foreach (string plate in plateStack)
            {
                Console.WriteLine(plate);
            }

            // Then we pop plates off the stack using the LIFO principle, removing the most recently added plate first
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
    }
}