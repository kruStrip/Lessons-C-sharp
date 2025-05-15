using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namespace.Entities
{
    internal class Shop
    {
        private static int id = 1;

        private int _id;
        private string _name;
        private string _address;
        private List<Product> _products;

        public Shop(string name, string address)
        {
            _id = id++;
            _name = name;
            _address = address;
            _products = new List<Product>();
        }

        public List<Product> Products => _products;
        public int ID => _id;
        public string Name => _name;
        public string Address => _address;

        public void AddProductToShop(Product product)
        {
            _products.Add(product);
        }

    }
}
