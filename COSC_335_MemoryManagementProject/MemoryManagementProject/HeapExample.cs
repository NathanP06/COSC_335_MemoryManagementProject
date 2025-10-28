using System;

namespace MemoryManagerDemo
{
    class HeapExample
    {
        public static void Run()
        {
            Console.WriteLine("Restaurant Memory Management Demo:\n");

            Table? table1 = new(1, "Window");
            Table? table2 = new(2, "Patio");

            table1.AssignOrder(new Order("Pasta", "Tiramisu"));
            table2.AssignOrder(new Order("Steak", "Cheesecake"));

            table1.DisplayStatus();
            table2.DisplayStatus();

            Console.WriteLine("\nCustomers from Table 1 have paid and left...");
            table1.CloseOrder();

            Console.WriteLine("Customers from Table 2 have paid and left...");
            table2.CloseOrder();

            Console.WriteLine("\nRestaurant is closing for the night...");
            table1 = null;
            table2 = null;

            Console.WriteLine("Tables and their orders are now unreferenced and eligible for garbage collection.");
        }
    }

    class Table
    {
        public int Number { get; private set; }
        public string Location { get; private set; }
        private Order? CurrentOrder;

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
            CurrentOrder = null;
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"\nTable {Number} ({Location}):");
            if (CurrentOrder != null)
                CurrentOrder.DisplayItems();
            else
                Console.WriteLine("No current order");
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
}
