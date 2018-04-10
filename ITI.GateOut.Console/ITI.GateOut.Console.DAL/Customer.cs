using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public Customer()
        {
            CustomerID = 0;
            CustomerCode = string.Empty;
            Name = string.Empty;
        }
    }
}
