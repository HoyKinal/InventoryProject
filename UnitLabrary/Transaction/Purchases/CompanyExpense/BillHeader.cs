using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitLabrary.Transaction.Purchases.CompanyExpense;

namespace UnitLabrary.Transaction.Purchases.CompanyExpenses
{
    public class BillHeader
    {
        private DataLinqDataContext context;

        public BillHeader()
        {
            context = new DataLinqDataContext();
        }
      
        public bool BillHeaderInserts(BillHeaderModel c)
        {
            try
            {
                context.BillHeaderInsert(
                       c.BillNumber,
                       c.DateBill,
                       c.DueDateBill,
                       c.VenderCode,
                       c.RefereceNo,
                       c.Memo,
                       c.VatPercent,
                       c.DiscountPercent,
                       c.DiscountAmount,
                       c.Indedted
                    );

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public IEnumerable<BillHeaderSelectResult> BillHeaderSelects(string search,string fromDate, string toDate,bool indedted)
        {
            return context.BillHeaderSelect(search, fromDate,toDate, indedted).ToList();
        }
        public BillHeaderSelectEditResult BillHeaderSelectEdits(BillHeaderModel c)
        {
            return context.BillHeaderSelectEdit(c.BillNumber).FirstOrDefault();
        }
        public bool BillHeaderUpdate(BillHeaderModel c)
        {
            try
            {
                context.BillHeaderUpdate(
                    c.BillNumber,
                    c.DateBill,
                    c.DueDateBill,
                    c.VenderCode,
                    c.RefereceNo,
                    c.Memo,
                    c.VatPercent,
                    c.DiscountPercent,
                    c.DiscountAmount     
                     );
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool BillHeaderDeletes(BillHeaderModel c)
        {
            try
            {
                context.BillHeaderDelete(c.BillNumber);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        
    }
}
