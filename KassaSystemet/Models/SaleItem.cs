using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystemet.Models
{
    public class SaleItem
    {
        public IProduct Product { get; private set; }
        public double Quantity { get; private set; }

        public SaleItem(IProduct product, double quantity) 
        {
            Product = product;
            Quantity = quantity;
        }

        public double GetTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
