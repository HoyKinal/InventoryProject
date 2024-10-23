using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.SaleReceipts
{
    public class SaleInvoiceReturnModel
    {
        public string InvoiceReturnNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime ReceiveDate { get; set; }
        public decimal ReceiveAmount { get; set; }
        public string ReceiveMemo { get; set; } 
    }
}
