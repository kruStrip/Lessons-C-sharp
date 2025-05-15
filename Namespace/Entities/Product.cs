using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namespace.Entities
{
    internal class Product
    {
        private string _name;
        private int _amount;
        private int _price;

        public Product(string product, int amount, int price)
        {
            _name = product;
            _amount = amount;
            _price = price;
        }

        public string Name => _name;
        public int Price => _price;
        public int Amount => _amount;

        public void AddPrice(int price)
        {
            _price = price;
        }

        public void RemoveAmount(int difference)
        {
            _amount -= difference;
        }

    }
}
