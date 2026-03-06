using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace KassaSystemet.Models
{
    public class Receipt
    {
        public List<SaleItem> Items { get; private set; }
        public double Total
        {
            get
            {
                double sum = 0;
                foreach (var item in Items)
                {
                    sum += item.GetTotal();
                }
                return sum;
            }
        }

        public Receipt()
        {          
            Items = new List<SaleItem>();
        }

        public void WriteReceipt()
        {
            Console.WriteLine("\n-------------- KVITTO --------------");
            Console.WriteLine($" Datum: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            Console.WriteLine("------------------------------------");

            foreach (var item in Items)
            {
                Console.WriteLine($"{item.Product.Name} - {item.Quantity}x{item.Product.PriceType} ({item.Product.Price}kr)  = {item.GetTotal()}kr");
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine($" TOTALT: {Total}kr");
            Console.WriteLine("------------------------------------");
        }
    }
}
