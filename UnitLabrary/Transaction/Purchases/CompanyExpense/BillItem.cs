using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.CompanyExpense
{
    public class BillItem
    {
        public string BillItemCode { get; set; }  
        public string BillNumber { get; set; }    
        public string PurDescription { get; set; } 
        public decimal OrderQty { get; set; }    
        public string UnitBill { get; set; }      
        public decimal Cost { get; set; }
        public string LocationCode { get; set; }

        // Nullable fields (Checked in DB schema)
        public decimal? DiscountPercent { get; set; } 
        public decimal? Discount { get; set; }        
        public decimal? TotalDiscount { get; set; }   
        public decimal? Total { get; set; }
        public string CategoryCode { get; set; }
        public string ItemCode { get; set; }

        //Default Constructor
        private DataLinqDataContext context;
        public BillItem()
        {
            context = new DataLinqDataContext();
        }

        public bool BillItemInserts(BillItem b)
        {
            try
            {
                context.BillItemInsert(b.BillItemCode, b.BillNumber, b.PurDescription, b.OrderQty,
                        b.UnitBill, b.Cost, b.DiscountPercent, b.Discount, b.TotalDiscount, 
                        b.Total,b.CategoryCode, b.ItemCode, b.LocationCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public IEnumerable<BillItemSelectResult> BillItemSelects(string billNumber)
        {
            return context.BillItemSelect(billNumber).ToList();
        }
        public BillItemSelectEditResult BillItemSelectEdits(string BillItemCode)
        {
            try
            {
                return context.BillItemSelectEdit(BillItemCode).FirstOrDefault();

            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool BillItemUpdates(BillItem b)
        {
            try
            {
                context.BillItemUpdate(b.BillItemCode, b.BillNumber, b.PurDescription, b.OrderQty,
                        b.UnitBill, b.Cost, b.DiscountPercent, b.Discount, 
                        b.TotalDiscount, b.Total,b.CategoryCode, b.ItemCode, b.LocationCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool BillItemDeletes(BillItem b)
        {
            try
            {
                context.BillItemDelete(b.BillItemCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }

}
