using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.SaleReceipts
{
    public class SaleReceiptInvoice
    {
        private DataLinqDataContext context;
        public SaleReceiptInvoice() 
        { 
            context = new DataLinqDataContext();    
        }
        public bool SaleReceiptInvoiceInserts(SaleReceiptInvoiceModel m)
        {
            try
            {
                context.InvoiceHeaderInsert(m.InvoiceNo, m.CustomerCode, m.InvoiceDate, m.Memo,m.InvoiceStatus, m.VatPercent, m.DiscountPercent, m.DiscountAmount);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool SaleReceiptInvoiceUpdates(SaleReceiptInvoiceModel m)
        {
            try
            {
                context.InvoiceHeaderUpdate(m.InvoiceNo, m.CustomerCode, m.InvoiceDate, m.Memo, m.InvoiceStatus, m.VatPercent, m.DiscountPercent, m.DiscountAmount);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool SaleReceiptInvoiceDeletes(string invoiceNo)
        {
            try
            {
                context.InvoiceHeaderDelete(invoiceNo);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public InvoiceHeaderSelectEditResult ReceiptInvoiceHeaderSelectEdits(string invoiceNo)
        {
            return context.InvoiceHeaderSelectEdit(invoiceNo).SingleOrDefault();
        }
        public List<InvoiceHeaderSelectResult> ReceiptInvoiceHeaderSelects(string search,string startDate, string toDate,bool invoiceStatus)
        {
            return context.InvoiceHeaderSelect(search,startDate,toDate, invoiceStatus).ToList();
        }
    }
}
