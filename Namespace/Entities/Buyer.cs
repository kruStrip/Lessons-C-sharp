using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namespace.Entities
{
    internal class Buyer
    {
        private string _name;
        private int _money;

        public Buyer(string name, int money)
        {
            _name = name;
            _money = money;
        }

        public string Name => _name;
        public int Money => _money;

        public void Paying(int sum)
        {
            _money -= sum;
        }

    }
}
