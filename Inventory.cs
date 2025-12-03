using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStoneProject
{
    internal class Inventory
    {
        private List<Item> stuffForSale;

        public Inventory()
        {
            stuffForSale = new List<Item>();
        }

        public void DisplayInventory()
        {
            for (int index = 0; index < stuffForSale.Count; index++)
            {
                Console.WriteLine($"{index}: {stuffForSale[index]}");
            }
        }

        public void DisplayDetails(int index)
        {
            Item thing = stuffForSale[index];
            Console.WriteLine($"{thing.GetDetails}");
        }

        public void AddInventory(int index, int amount)
        {
            Item thing = stuffForSale[index];
            thing.AddToInventory(amount);
        }

        public void RemoveInventory(int index, int amount)
        {
            Item thing = stuffForSale[index];
            thing.RemoveFromInventory(amount);
        }

        public void AddItem(Item thing)
        {
            stuffForSale.Add(thing);
        }

        public void RemoveItem(Item thing)
        {
            stuffForSale.Remove(thing);
        }

        public void PurchaseItem(Item cartItem)
        {
            for (int index = 0; index < stuffForSale.Count; index++)
            {
                if (cartItem.Equals(stuffForSale[index]))
                {
                    Item originalItem = stuffForSale[index];
                    originalItem.RemoveFromInventory(cartItem.Quantity);
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
            if (File.Exists(filePath))
            {
                string[] arrayOfAllStuff = File.ReadAllLines(filePath);
                stuffForSale.Clear();
                foreach (string csv in arrayOfAllStuff)
                {
                    string[] parts = csv.Split(',');
                    string name = parts[0];
                    string description = parts[1];
                    decimal price = decimal.Parse(parts[2]);
                    int quantity = int.Parse(parts[3]);
                    Item item = new Item(name, description, price, quantity);
                    stuffForSale.Add(item);
                }
            }
        }

        public List<Item> GetStuffForSale()
        {
            return stuffForSale;
        }
    }
}
