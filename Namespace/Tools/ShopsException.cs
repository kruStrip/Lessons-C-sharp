using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namespace.Tools
{
    internal class ShopsException
    {
        public class ShopsException : Exception
        {
            public ShopsException()
            {
            }

            public ShopsException(string message)
                : base(message)
            {
            }

            public ShopsException(string message, Exception innerException)
                : base(message, innerException)
            {
            }

        }
    }
