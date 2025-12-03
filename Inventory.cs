using System;
using System.Collections.Generic;
using System.IO;

namespace CapStoneProject
{
    internal class Inventory
    {
        private readonly List<Item> stuffForSale;

        public Inventory()
        {
            stuffForSale = new List<Item>();
        }

        public void DisplayInventory()
        {
            if (stuffForSale.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }

            for (int index = 0; index < stuffForSale.Count; index++)
            {
                Console.WriteLine($"{index}: {stuffForSale[index]}");
            }
        }

        public void DisplayDetails(int index)
        {
            if (index < 0 || index >= stuffForSale.Count)
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            Item thing = stuffForSale[index];
            Console.WriteLine(thing.GetDetails());
        }

        public void AddInventory(int index, int amount)
        {
            if (index < 0 || index >= stuffForSale.Count) return;
            Item thing = stuffForSale[index];
            thing.AddToInventory(amount);
        }

        public void RemoveInventory(int index, int amount)
        {
            if (index < 0 || index >= stuffForSale.Count) return;
            Item thing = stuffForSale[index];
            thing.RemoveFromInventory(amount);
        }

        public void AddItem(Item thing)
        {
            if (thing == null) return;
            stuffForSale.Add(thing);
        }

        public void RemoveItem(Item thing)
        {
            if (thing == null) return;
            stuffForSale.Remove(thing);
        }

        public void PurchaseItem(Item cartItem)
        {
            if (cartItem == null) return;

            for (int index = 0; index < stuffForSale.Count; index++)
            {
                if (cartItem.Equals(stuffForSale[index]))
                {
                    Item originalItem = stuffForSale[index];
                    originalItem.RemoveFromInventory(cartItem.Quantity);
                    break;
                }
            }
        }

        public void Save(string filePath)
        {
            List<string> listOfAllStuff = new List<string>();

            foreach (Item item in stuffForSale)
            {
                string lineCSV = item.GetCSV();
                listOfAllStuff.Add(lineCSV);
            }

            string[] arrayOfAllStuff = listOfAllStuff.ToArray();
            File.WriteAllLines(filePath, arrayOfAllStuff);
        }

        public void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string[] arrayOfAllStuff = File.ReadAllLines(filePath);
            stuffForSale.Clear();

            foreach (string csv in arrayOfAllStuff)
            {
                if (string.IsNullOrWhiteSpace(csv)) continue;

                string[] parts = csv.Split(',');
                if (parts.Length != 4) continue;

                string name = parts[0];
                string description = parts[1];

                if (!decimal.TryParse(parts[2], out decimal price)) continue;
                if (!int.TryParse(parts[3], out int quantity)) continue;

                Item item = new Item(name, description, price, quantity);
                stuffForSale.Add(item);
            }
        }

        public List<Item> GetStuffForSale()
        {
            return stuffForSale;
        }
    }
}
