using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace KassaSystemet.Services
{
    public class FileHandler
    {
        public List<IProduct> GetProducts()
        {
            string filePath = "../../../Data/products.txt";
            List<IProduct> products = new List<IProduct>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Inga produkter hittades.");
                return products;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] stringParts = line.Split(';');
                string id = stringParts[0];
                string name = stringParts[1];
                double price = double.Parse(stringParts[2]);
                PriceType priceType = Enum.Parse<PriceType>(stringParts[3]);

                Product newProduct = new Product(id, name, price, priceType);
                products.Add(newProduct);
            }

            return products;
        }

        public void SaveReceipt(Receipt receipt)
        {
            string filePath = "../../../Data/Receipts/";
            string fileDate = DateTime.Now.ToString("yyyyMMdd");
            string fileName = $"RECEIPT_{fileDate}.txt";
            string newPath = Path.Combine(filePath, fileName);

            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                using (StreamWriter writer = new StreamWriter(newPath, true))
                {
                    writer.WriteLine("\n-------------- KVITTO --------------");
                    writer.WriteLine($" Datum: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                    writer.WriteLine("------------------------------------");

                    foreach (var item in receipt.Items)
                    {
                        writer.WriteLine($"{item.Product.Name} - {item.Quantity}x{item.Product.PriceType} ({item.Product.Price}kr)  = {item.GetTotal()}kr");
                    }
                    writer.WriteLine("------------------------------------");
                    writer.WriteLine($" TOTALT: {receipt.Total}kr");
                    writer.WriteLine("------------------------------------");
                    writer.WriteLine("************************************");
                }
                Console.WriteLine($"\nKvittot sparat till {fileName}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Ett fel uppstod när kvittot skulle sparas: {ex.Message}");
            }
        }
    }
}
