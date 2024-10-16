using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Customers.Customer
{
    public class CustomerModel
    {
        public string CustomerCode { get; set; } 
        public string CustomerNumberCard { get; set; } 
        public string CustomerName { get; set; } 
        public string CustomerDesc { get; set; } 
        public string CustomerAddress { get; set; } 
        public string CustomerPhone { get; set; } 
        public string CustomerEmail { get; set; } 
        public string CustomerWeb { get; set; } 
        public string CustomerContact { get; set; } 
        public char CustomerStatus { get; set; } 
        public char CustomerType { get; set; } 
    }
}
