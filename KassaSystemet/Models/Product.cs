using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystemet.Models
{
    public class Product : IProduct
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public PriceType PriceType { get; private set; }
    

        public Product(string id, string name, double price, PriceType priceType)
        {
            ID = id;
            Name = name;
            Price = price;
            PriceType = priceType;
        }
    }
}
