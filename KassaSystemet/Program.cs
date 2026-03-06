using KassaSystemet.Interfaces;
using KassaSystemet.Services;
using System;
using System.Security.Cryptography.X509Certificates;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileHandler fileHandler = new FileHandler();
            List<IProduct> allProducts = fileHandler.GetProducts();
            Checkout checkout = new Checkout(fileHandler, allProducts);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("KASSA");
                Console.WriteLine("1. Ny kund");
                Console.WriteLine("0. Avsluta");
                Console.Write("Val: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                    NewCostumerSession(checkout, allProducts);

                else if (choice == "0")
                    break;
            }
        }

        static void NewCostumerSession(Checkout checkout, List<IProduct> products)
        {
            checkout.NewCostumer();
            string statusMessage = "";

            while (true)
            {
                Console.Clear();
                DisplayProducts(products);

                Console.WriteLine("VARUKORG:");
                foreach (var item in checkout.CurrentReceipt.Items)
                    Console.WriteLine($"{item.Product.Name} - {item.Quantity}x{item.Product.PriceType}");
                
                Console.WriteLine($"TOTALT: {checkout.CurrentReceipt.Total}kr");
                Console.WriteLine("-----------------------");

                if (!string.IsNullOrEmpty(statusMessage))
                {
                    Console.WriteLine(statusMessage);
                    statusMessage = "";
                }

                Console.WriteLine("Kommandon: \n<ProduktID> <Antal> \nPAY");
                Console.Write("Kommando: ");
                string input = Console.ReadLine();

                if (input.ToUpper() == "PAY")
                {
                    checkout.Pay();
                    Console.ReadKey();
                    break;
                }

                string result = checkout.AddProduct(input);

                if (!string.IsNullOrEmpty(result))
                    statusMessage = result;
            }
        }

        static void DisplayProducts(List<IProduct> products)
        {
            Console.WriteLine("Tillgängliga produkter");
            Console.WriteLine("-----------------------");

            foreach (IProduct product in products)          
                Console.WriteLine($"{product.ID}: {product.Name} - {product.Price}kr/{product.PriceType}");
            Console.WriteLine("-----------------------");
        }
      
    }
}