using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Purchases.CompanyExpenses
{
    public class BillHeader
    {
        private DataLinqDataContext context;
       
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
        public decimal TotalDiscount { get; set; }       
        public decimal Total { get; set; }                    
        public decimal GrandTotal { get; set; }

        public BillHeader()
        {
            context = new DataLinqDataContext();
        }

        public BillHeader(
            string billNumber,
            DateTime dateBill,
            string venderCode,
            string refereceNo,
            string memo,
            decimal vatPercent,
            decimal discountPercent,
            decimal discountAmount,
            decimal totalDiscount,
            decimal total,
            decimal cost)
        {
            BillNumber = billNumber;
            DateBill = dateBill;
            VenderCode = venderCode;
            RefereceNo = refereceNo;
            Memo = memo;
            VatPercent = vatPercent;
            DiscountPercent = discountPercent;
            DiscountAmount = discountAmount;
            TotalDiscount = totalDiscount;
            Total = total;
            GrandTotal = cost;
        }
        
        public bool BillHeaderInserts(BillHeader c)
        {
            try
            {
                context.BillHeaderInsert(
                       c.BillNumber,
                       c.DateBill,
                       c.VenderCode,
                       c.RefereceNo,
                       c.Memo,
                       c.VatPercent,
                       c.DiscountPercent,
                       c.DiscountAmount,
                       c.GrandTotal
                    );

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public IEnumerable<BillHeaderSelectResult> BillHeaderSelects(string search)
        {
            return context.BillHeaderSelect(search).ToList();
        }
        public BillHeaderSelectEditResult BillHeaderSelectEdits(BillHeader c)
        {
            return context.BillHeaderSelectEdit(c.BillNumber).SingleOrDefault();
        }
        public bool CompanyExpenseUpdate(BillHeader c)
        {
            try
            {
                context.BillHeaderUpdate(
                    c.BillNumber,
                    c.DateBill,
                    c.VenderCode,
                    c.RefereceNo,
                    c.LocationCode,
                    c.Memo,
                    c.VatPercent,
                    c.DiscountPercent,
                    c.DiscountAmount,
                    c.GrandTotal
                     );
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool BillHeaderDeletes(BillHeader c)
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
