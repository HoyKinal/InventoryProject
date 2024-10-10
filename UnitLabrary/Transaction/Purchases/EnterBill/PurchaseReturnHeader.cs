using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.EnterBill
{
    public class PurchaseReturnHeader
    {
		private DataLinqDataContext context;
		public PurchaseReturnHeader() { 

			context = new DataLinqDataContext();	
		}
        public bool PurchaseReturnHeaderInsert(PurchaseReturnHeaderModel model)
        {
			try
			{
				context.PurchaseReturnHeaderInsert(model.BillNo,model.PurchaseDateBill, model.Paid,model.Unpaid);
				
				return true;
			}
			catch (Exception)
			{
				return false;
			}
        }

		public bool PurchaseReturnHeaderUpdate(PurchaseReturnHeaderModel model)
		{
            try
            {
                context.PurchaseReturnHeaderUpdate(model.BillNo, model.PurchaseDateBill);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

		public bool PurchaseReturnHeaderUpdateDetails(PurchaseReturnHeaderModel m)
		{
			try
			{

				context.PurchaseReturnHeaderUpdateDetail(m.BillNo,m.Unpaid);

				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}
		public List<PurchaseReturnHeaderSelectResult> PurchaseReturnHeaderResultSelect(string supplier)
		{
			return context.PurchaseReturnHeaderSelect(supplier).ToList();
		}

		public PurchaseReturnHeaderSelectEditResult PurchaseReturnHeaderSelectResultEdit(string BillNo)
		{
			return context.PurchaseReturnHeaderSelectEdit(BillNo).SingleOrDefault();
		}
    }
}
