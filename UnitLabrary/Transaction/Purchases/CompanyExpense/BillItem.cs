using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.CompanyExpense
{
    public class BillItem
    {
        //Default Constructor
        private DataLinqDataContext context;
        public BillItem()
        {
            context = new DataLinqDataContext();
        }

        public bool BillItemInserts(BillItemModel b)
        {
            try
            {
                context.BillItemInsert(b.BillItemCode, b.BillNumber, b.PurDescription, b.OrderQty,
                        b.UnitBill, b.Cost, b.DiscountPercent, b.DiscountAmount, b.TotalDiscount, 
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
        public bool BillItemUpdates(BillItemModel b)
        {
            try
            {
                context.BillItemUpdate(b.BillItemCode, b.BillNumber, b.PurDescription, b.OrderQty,
                        b.UnitBill, b.Cost, b.DiscountPercent, b.DiscountAmount, 
                        b.TotalDiscount, b.Total,b.CategoryCode, b.ItemCode, b.LocationCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool BillItemDeletes(string billItemCode)
        {
            try
            {
                context.BillItemDelete(billItemCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }

}
