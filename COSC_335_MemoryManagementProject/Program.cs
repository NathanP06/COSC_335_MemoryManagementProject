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

            //Creates a new stack to hold "plates". Each plate is represented by a string.
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

        // HEAP EXAMPLE
        static void HeapExample()
        {
            Console.WriteLine("Restaurant Memory Management Demo:\n");

            // Objects created with 'new' are stored on the heap
            Table? table1 = new(1, "Window");
            Table? table2 = new(2, "Patio");

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
            table1 = null;
            table2 = null;

            Console.WriteLine("Tables and their orders are now unreferenced and eligible for garbage collection.");
        }

        class Table
        {
            // Properties of the Table class
            public int Number { get; private set; }
            public string Location { get; private set; }
            private Order? CurrentOrder; // stored on the heap

            // Constructor
            public Table(int number, string location)
            {
                Number = number;
                Location = location;
                CurrentOrder = null;
            }

            // Assign an order to the table
            public void AssignOrder(Order order)
            {
                CurrentOrder = order;
            }

            // Clear the current order when customers leave
            public void CloseOrder()
            {
                Console.WriteLine($"Clearing order for Table {Number}...");
                CurrentOrder = null; // Order object becomes eligible for garbage collection
            }

            // Display current status of the table
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
            // Properties of the Order class
            private string MainCourse { get; set; }
            private string Dessert { get; set; }

            // Constructor
            public Order(string mainCourse, string dessert)
            {
                MainCourse = mainCourse;
                Dessert = dessert;
            }

            // Display ordered items
            public void DisplayItems()
            {
                Console.WriteLine($"  Main Course: {MainCourse}");
                Console.WriteLine($"  Dessert: {Dessert}");
            }
        }

        //BUFFER EXAMPLE
        static void BufferExample()
        {
            // String we will read data from
            string data = "This is a string of data that will be read using a buffer. Buffers help manage memory efficiently by reading data in chunks. While this is a simple example, buffers are crucial in real-world applications for performance optimization and memory management.";

            // Convert the string data to bytes using UTF8 encoding (Standard encoding method)
            byte[] allBytes = Encoding.UTF8.GetBytes(data);

            // A small 10-byte "buffer" to read data in chunks
            // In actuality, an array of bytes is created, storing 10 bytes
            byte[] buffer = new byte[10];

            // Using MemoryStream to simulate reading data in chunks
            using (MemoryStream stream = new MemoryStream(allBytes))
            {
                int bytesRead;
                int chunkNumber = 1;

                // Read data into the buffer in chunks
                // bytesRead is equal to the number of bytes actually read into the buffer
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Creates the string "chunk" and takes the bytesRead integer returned by 
                    // the Read method to know how many bytes were actually read
                    // Takes the bytes that were read and converts them to a string for display
                    string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Display the chunk read
                    Console.WriteLine($"Buffer #{chunkNumber++}: {chunk}");
                }
            }

            Console.WriteLine("All data has been read using the buffer.");
        }
    }
}