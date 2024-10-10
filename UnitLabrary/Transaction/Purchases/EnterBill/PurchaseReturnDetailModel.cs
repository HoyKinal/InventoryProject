using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.EnterBill
{
    public class PurchaseReturnDetailModel
    {
        public string PurchaseReturnNo {  get; set; }   
        public string BillNo { get; set; }  
        public DateTime DatePaid {  get; set; }
        public decimal PaidAmount {  get; set; }
        public string MemoReturnPaid {  get; set; } 
    }
}
