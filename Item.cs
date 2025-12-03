using System;

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
            this.name = name;
            this.description = description;
            this.price = price;
            this.quantity = quantity;
        }

        public Item(Item other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            this.name = other.name;
            this.description = other.description;
            this.price = other.price;
            this.quantity = other.quantity;
        }

        public bool Equals(Item other)
        {
            if (other == null) return false;

            // “Same item” if name and description match
            return other.name == this.name &&
                   other.description == this.description;
        }

        public bool IsOutOfStock()
        {
            return quantity <= 0;
        }

        public override string ToString()
        {
            if (IsOutOfStock())
            {
                return $"{name} - OUT OF STOCK";
            }
            else
            {
                return $"{name} - ${price:F2}";
            }
        }

        public string GetDetails()
        {
            return $"Name: {name}\n" +
                   $"Description: {description}\n" +
                   $"Price: ${price:F2}\n" +
                   $"Quantity: {quantity}";
        }

        public string GetCSV()
        {
            return $"{name},{description},{price},{quantity}";
        }

        public void AddToInventory(int amount)
        {
            if (amount <= 0) return;
            this.quantity += amount;
        }

        public void RemoveFromInventory(int amount)
        {
            if (amount <= 0) return;

            this.quantity -= amount;
            if (this.quantity < 0)
            {
                this.quantity = 0;
            }
        }
    }
}
