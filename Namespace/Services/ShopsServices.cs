using Namespace.Entities;
using Namespace.Services.Shops.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Namespace.Services.ShopsService;

namespace Namespace.Services
{
    internal class ShopsServices
    {
        public class ShopsService : IShopsService
        {
            private List<Shop> _shops;

            public ShopsService()
            {
                _shops = new List<Shop>();
            }

            public Product AddProduct(string name, int amount, int price, Shop shop)
            {
                var product = new Product(name, amount, price);
                shop.AddProductToShop(product);
                return product;
            }

            public Shop AddShop(string name, string address)
            {
                var shop = new Shop(name, address);
                _shops.Add(shop);
                return shop;
            }

            public Product FindProduct(string name, Shop shop)
            {
                Product result = null;
                foreach (Product product1 in shop.Products)
                {
                    if (product1.Name == name)
                    {
                        result = product1;
                        break;
                    }
                }

                if (result == null)
                {
                    throw new ShopsException("No such product in this shop");
                }

                return result;
            }

            public bool IsProductInShop(Product product, Shop shop)
            {
                Product result = null;
                foreach (Product product1 in shop.Products)
                {
                    if (product1 == product)
                    {
                        result = product1;
                        break;
                    }
                }

                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public Shop FindCheapestProduct(string productName, int productAmount)
            {
                Shop result = null;
                int minPrice = int.MaxValue;
                foreach (Shop shop in _shops)
                {
                    foreach (Product product in shop.Products)
                    {
                        if (product.Name == productName
                            & product.Amount >= productAmount
                            & product.Price < minPrice)
                        {
                            result = shop;
                            minPrice = product.Price;
                        }
                    }
                }

                if (result == null)
                {
                    throw new ShopsException("No such product in shops");
                }

                return result;
            }

            public int Buy(Dictionary<string, int> shopList, Buyer buyer, Shop shop)
            {
                int checkAmount = 0;
                foreach (KeyValuePair<string, int> keyValue in shopList)
                {
                    string productName = keyValue.Key;
                    int amount = keyValue.Value;
                    foreach (Product product in shop.Products)
                    {
                        int cost = amount * product.Price;
                        if (product.Name == productName
                            & product.Amount >= amount
                            & buyer.Money >= cost)
                        {
                            checkAmount += cost;
                            buyer.Paying(cost);
                            product.RemoveAmount(amount);
                        }
                    }
                }

                return checkAmount;
            }
        }
    }
}

