using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStoneProject
{
    internal class Item
    {
        private string name;
        private string description;
        private decimal price;
        private int quantity;
        internal int Quantity
        {
            get { return quantity; }
        }

        public Item()
        {
            name = "Unknown";
            description = "No description";
            price = 0.0m;
            quantity = 0;
        }

        public Item(string name, string description, decimal price, int quantity)
        {
            name = this.name;
            description = this.description;
            price = this.price;
            quantity = this.quantity;
        }

        public Item(Item other)
        {
            this.name = other.name;
            this.description = other.description;
            this.price = other.price;
            this.quantity = other.quantity;
        }

        public bool Equals(Item other)
        {
            bool itemMatch = false;

            if (other.name == this.name && other.description == this.description)
            {
                itemMatch = true;
            }
            return itemMatch;
        }

        public bool IsOutOfStock()
        {
            bool IsOutOfStock = true;
            if (quantity > 0)
            {
                IsOutOfStock = false;
            }
            return IsOutOfStock;
        }

        override public string ToString()
        {
            if (IsOutOfStock())
            {
                return $"{name} - OUT OF STOCK";
            }
            else
            {
                return $"{name} - ${price}";
            }
        }

        public string GetDetails()
        {
            return $"Name: {name}\nDescription: {description}\nPrice: ${price}\nQuantity: {quantity}";
        }

        public string GetCSV()
        {
            return $"{name},{description},{price},{quantity}";
        }

        public void AddToInventory(int amount)
        {
            this.quantity += amount;
        }

        public void RemoveFromInventory(int amount)
        {
            this.quantity -= amount;
            if (this.quantity < 0)
            {
                this.quantity = 0;
            }
        }
    }
}


