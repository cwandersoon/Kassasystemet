using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystemet.Services
{
    public class Checkout
    {
        public FileHandler FileHandler {  get; private set; }
        public List<IProduct> AllProducts {  get; private set; }
        public Receipt CurrentReceipt { get; private set; }

        public Checkout(FileHandler fileHandler, List<IProduct> allProducts)
        {
            FileHandler = fileHandler;
            AllProducts = allProducts;
        }

        public void NewCostumer()
        {
            CurrentReceipt = new Receipt();
        }

        public string AddProduct(string input)
        {
            string[] inputString = input.Split(' ');

            if (inputString.Length < 2) 
                return "Ange både ID och antal (t.ex. 100 5).";

            if (!double.TryParse(inputString[1], out double quantity)) 
                return "Antalet måste vara en siffra.";

            if (quantity <= 0) 
                return "Antalet måste vara större än 0.";

            IProduct existingProduct = null;

            foreach (var product in AllProducts)
            {
                if (product.ID == inputString[0])
                {
                    existingProduct = product;
                    break;
                }
            }

            if (existingProduct == null) 
                return $"Produkt med ID {inputString[0]} finns inte.";

            SaleItem newItem = new SaleItem(existingProduct, quantity);
            CurrentReceipt.Items.Add(newItem);

            return "";
        }

        public void Pay()
        {
            CurrentReceipt.WriteReceipt();

            FileHandler.SaveReceipt(CurrentReceipt);
        }

    }
}
