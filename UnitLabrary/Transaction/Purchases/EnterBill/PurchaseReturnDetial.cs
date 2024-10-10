using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.EnterBill
{
    public class PurchaseReturnDetial
    {
        private DataLinqDataContext context;

        public PurchaseReturnDetial()
        {
            context = new DataLinqDataContext();
        }

        public bool PurchaseReturnDetailInserts(PurchaseReturnDetailModel m)
        {
            try
            {
                context.PurchaseReturnDetailInsert(m.PurchaseReturnNo, m.BillNo, m.DatePaid, m.PaidAmount, m.MemoReturnPaid);
                
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public List<PurchaseReturnDetailSelectResult> PurchaseReturnDetailResultSelect(string search,string startDate, string endDate)
        {
            return context.PurchaseReturnDetailSelect(search,startDate,endDate).ToList();
        }

        public PurchaseReturnDetailSelectEditResult PurchaseReturnDetailSelectEdits(string purchaseReturnNo)
        {
            return context.PurchaseReturnDetailSelectEdit(purchaseReturnNo).SingleOrDefault();
        }

        public bool PurchaseReturnDetailDeletes(string purchaseReturnNo,string billNo)
        {
            try
            {
                context.PurchaseReturnDetailDelete(purchaseReturnNo,billNo);
               
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool PurchaseReturnDetailUpdate(PurchaseReturnDetailModel m)
        {
            try
            {
                context.PurchaseReturnDetailUpdate(m.PurchaseReturnNo,m.BillNo,m.DatePaid,m.PaidAmount,m.MemoReturnPaid);
                
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
