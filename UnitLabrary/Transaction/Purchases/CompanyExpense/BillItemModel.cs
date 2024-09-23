using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.CompanyExpense
{
    public class BillItemModel
    {
        public string BillItemCode { get; set; }
        public string BillNumber { get; set; }
        public string PurDescription { get; set; }
        public decimal OrderQty { get; set; }
        public string UnitBill { get; set; }
        public decimal Cost { get; set; }
        public string LocationCode { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal Total => Cost * OrderQty;
        public string CategoryCode { get; set; }
        public string ItemCode { get; set; }

    }
}
