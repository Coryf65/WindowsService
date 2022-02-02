using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEPFiles
{
    internal class OrderInfo
    {
        public Guid OrderID { get; set;}
        public string ItemName { get; set;}
        public int QuantityOrdered { get; set; }
    }
}
