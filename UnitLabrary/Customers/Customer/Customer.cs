using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Customers.Customer
{
    public class Customer
    {
        public string CustomerCode { get; set; } // nvarchar(30) - Checked
        public string CustomerNumber { get; set; } // nvarchar(30) - Unchecked
        public string CustomerName { get; set; } // nvarchar(50) - Unchecked
        public string CustomerDesc { get; set; } // nvarchar(50) - Unchecked
        public string CustomerAddress { get; set; } // nvarchar(1000) - Unchecked
        public string CustomerPhone { get; set; } // nvarchar(50) - Unchecked
        public string CustomerFax { get; set; } // nvarchar(50) - Unchecked
        public string CustomerEmail { get; set; } // nvarchar(150) - Unchecked
        public string CustomerWeb { get; set; } // nvarchar(150) - Unchecked
        public string CustomerContact { get; set; } // nvarchar(50) - Unchecked
        public char CustomerStatus { get; set; } // char(1) - Unchecked
        public char CustomerType { get; set; } // char(1) - Unchecked
        public string CreateBy { get; set; } // nvarchar(30) - Unchecked
        public DateTime CreateDate { get; set; } // datetime - Unchecked
        public string UpdateBy { get; set; } // nvarchar(30) - Checked
        public DateTime? UpdateDate { get; set; } // datetime - Checked (nullable)
        public string Ana4 { get; set; } // MemberTypeCode
    }
}
