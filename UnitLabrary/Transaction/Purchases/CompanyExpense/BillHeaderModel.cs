using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.CompanyExpense
{
    public class BillHeaderModel
    {
        public string BillNumber { get; set; }
        public DateTime DateBill { get; set; }
        public string VenderCode { get; set; }
        public string RefereceNo { get; set; }
        public string LocationCode { get; set; }
        public string Memo { get; set; }
        public decimal VatPercent { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalDiscoutPercent { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
