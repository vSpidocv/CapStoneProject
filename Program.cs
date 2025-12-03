using System;

namespace CapStoneProject
{
    internal class Program
    {
        private const string DataFilePath = "inventory.csv";

        static void Main(string[] args)
        {
            Inventory ourInventory = new Inventory();

            // Optional: seed some sample data if inventory is empty
            if (ourInventory.GetStuffForSale().Count == 0)
            {
                SeedSampleInventory(ourInventory);
            }

            bool exit = false;

            while (!exit)
            {
                int selection = SplashScreen();
                Console.Clear();

                switch (selection)
                {
                    case 1:
                        HandleViewInventory(ourInventory);
                        break;

                    case 2:
                        Console.WriteLine("View Shopping Cart is not implemented in this version.");
                        Pause();
                        break;

                    case 3:
                        Console.WriteLine("Checkout is not implemented in this version.");
                        Pause();
                        break;

                    case 4:
                        HandleAddProduct(ourInventory);
                        break;

                    case 5:
                        HandleRemoveProduct(ourInventory);
                        break;

                    case 6:
                        ourInventory.Save(DataFilePath);
                        Console.WriteLine($"Inventory saved to {DataFilePath}.");
                        Pause();
                        break;

                    case 7:
                        ourInventory.Load(DataFilePath);
                        Console.WriteLine($"Inventory loaded from {DataFilePath}.");
                        Pause();
                        break;

                    case 8:
                        exit = true;
                        break;
                }
            }
        }

        // Main menu
        static int SplashScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===============================================");
                Console.WriteLine(" Welcome to Yangtze River Online Sales Company");
                Console.WriteLine("===============================================\n");
                Console.WriteLine(" Please choose an option:\n");
                Console.WriteLine(" 1. View Product Inventory");
                Console.WriteLine(" 2. View Shopping Cart");
                Console.WriteLine(" 3. Checkout");
                Console.WriteLine(" 4. Add Product to Inventory");
                Console.WriteLine(" 5. Remove Product from Inventory");
                Console.WriteLine(" 6. Save Data");
                Console.WriteLine(" 7. Load Data");
                Console.WriteLine(" 8. Exit\n");
                Console.Write(" Enter your selection (1-8): ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int selection) ||
                    selection < 1 || selection > 8)
                {
                    Console.WriteLine("\n Invalid input; please input a number (1-8).");
                    Pause();
                    continue;
                }

                return selection;
            }
        }

        static void HandleViewInventory(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("Current Inventory:\n");
            inventory.DisplayInventory();
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }

        static void HandleAddProduct(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("Add Product to Inventory\n");

            Console.Write(" Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write(" Description: ");
            string description = Console.ReadLine() ?? "";

            decimal price;
            while (true)
            {
                Console.Write(" Price: ");
                if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0)
                    break;

                Console.WriteLine(" Please enter a valid non-negative price.");
            }

            int quantity;
            while (true)
            {
                Console.Write(" Quantity: ");
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0)
                    break;

                Console.WriteLine(" Please enter a valid non-negative whole number.");
            }

            Item item = new Item(name, description, price, quantity);
            inventory.AddItem(item);

            Console.WriteLine("\n Item added to inventory.");
            Pause();
        }

        static void HandleRemoveProduct(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("Remove Product from Inventory\n");
            inventory.DisplayInventory();

            if (inventory.GetStuffForSale().Count == 0)
            {
                Console.WriteLine("\nInventory is empty.");
                Pause();
                return;
            }

            int index;
            while (true)
            {
                Console.Write("\n Enter index of item to remove: ");
                if (int.TryParse(Console.ReadLine(), out index) &&
                    index >= 0 && index < inventory.GetStuffForSale().Count)
                    break;

                Console.WriteLine(" Please enter a valid index.");
            }

            Item item = inventory.GetStuffForSale()[index];
            inventory.RemoveItem(item);

            Console.WriteLine("\n Item removed from inventory.");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\n Press Enter to continue...");
            Console.ReadLine();
        }

        static void SeedSampleInventory(Inventory inventory)
        {
            inventory.AddItem(new Item("USB Cable", "High speed USB-C cable", 7.99m, 25));
            inventory.AddItem(new Item("Wireless Mouse", "2.4 GHz wireless mouse", 19.99m, 10));
            inventory.AddItem(new Item("Keyboard", "Mechanical keyboard", 49.99m, 5));
        }
    }
}
