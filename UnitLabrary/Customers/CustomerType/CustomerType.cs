using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Customers.CustomerType
{
    public class CustomerType
    {
        public string MemberTypeCode { get; set; }
        public string MemberTypeName { get; set; }
        public decimal MemberTypePrice { get; set; }
        public bool MemberTypeStatus { get; set; }
        public bool MemberTypeIsSync { get; set; }
        public decimal MemberTypeDiscount { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
