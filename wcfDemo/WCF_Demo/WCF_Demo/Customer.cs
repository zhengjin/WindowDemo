using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF_Demo
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public decimal CurrentDue { get; set; }
        public decimal TotalDue { get; set; }
        public string LastName { get; set; }
        public string Zip { get; set; }
    }
}