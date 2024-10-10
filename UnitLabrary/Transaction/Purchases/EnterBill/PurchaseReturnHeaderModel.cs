using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.EnterBill
{
    public class PurchaseReturnHeaderModel
    {
        public string BillNo { get; set; }
        public DateTime PurchaseDateBill { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Unpaid { get; set; }
        public decimal? GetCalculatedUnpaid()
        {
            return Unpaid - Paid; 
        }
    }
}
