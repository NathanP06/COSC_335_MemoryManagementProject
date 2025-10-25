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

        // STACK EXAMPLE (LIFO and Stack Overflow demo).
        static void StackExample()
        {
            Console.WriteLine("Imagine a stack of plates — Last In, First Out (LIFO).\n");

            // C#’s built-in library 
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
                // StackOverflowException can’t be caught in C#
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
            Console.WriteLine("Restaurant Memory Management Demo:\n");

            // Objects created with 'new' are stored on the heap
            Table table1 = new Table(1, "Window");
            Table table2 = new Table(2, "Patio");

            // Create and assign orders to tables
            table1.AssignOrder(new Order("Pasta", "Tiramisu"));
            table2.AssignOrder(new Order("Steak", "Cheesecake"));

            // Show current restaurant state
            table1.DisplayStatus();
            table2.DisplayStatus();

            Console.WriteLine("\nCustomers from Table 1 have paid and left...");
            // When we clear the reference, the Order object becomes eligible for garbage collection
            table1.CloseOrder();
            
            Console.WriteLine("Customers from Table 2 have paid and left...");
            // Same for table 2's order
            table2.CloseOrder();

            // Now the tables are being cleaned for the night
            Console.WriteLine("\nRestaurant is closing for the night...");
            // Remove references to the tables, making them eligible for garbage collection
            table1 = default!;
            table2 = default!;

            Console.WriteLine("Tables and their orders are now unreferenced and eligible for garbage collection.");
        }

        class Table
        {
            public int Number { get; private set; }
            public string Location { get; private set; }
            private Order? CurrentOrder; // stored on the heap

            public Table(int number, string location)
            {
                Number = number;
                Location = location;
                CurrentOrder = null;
            }

            public void AssignOrder(Order order)
            {
                CurrentOrder = order;
            }

            public void CloseOrder()
            {
                Console.WriteLine($"Clearing order for Table {Number}...");
                CurrentOrder = null; // Order object becomes eligible for garbage collection
            }

            public void DisplayStatus()
            {
                Console.WriteLine($"\nTable {Number} ({Location}):");
                if (CurrentOrder != null)
                {
                    CurrentOrder.DisplayItems();
                }
                else
                {
                    Console.WriteLine("No current order");
                }
            }
        }

        class Order
        {
            private string MainCourse { get; set; }
            private string Dessert { get; set; }

            public Order(string mainCourse, string dessert)
            {
                MainCourse = mainCourse;
                Dessert = dessert;
            }

            public void DisplayItems()
            {
                Console.WriteLine($"  Main Course: {MainCourse}");
                Console.WriteLine($"  Dessert: {Dessert}");
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