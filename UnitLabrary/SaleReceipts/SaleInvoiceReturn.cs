using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.SaleReceipts
{
    public class SaleInvoiceReturn
    {
        private DataLinqDataContext context;
        public SaleInvoiceReturn()
        {
            context = new DataLinqDataContext(); 
        }
        public List<InvoiceReturnHeaderSelectResult> InvoiceReturnHeaderSelects(string customerCode)
        {
            return context.InvoiceReturnHeaderSelect(customerCode).ToList();
        }
        public bool InvoiceReturnDetailInserts(SaleInvoiceReturnModel m)
        {
            try
            {
                context.InvoiceReturnDetailInsert(m.InvoiceReturnNo,m.InvoiceNo,m.ReceiveDate,m.ReceiveAmount,m.ReceiveMemo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InvoiceReturnDetailUpdates(SaleInvoiceReturnModel m)
        {
            try
            {
                context.InvoiceReturnDetailUpdate(m.InvoiceReturnNo, m.InvoiceNo, m.ReceiveDate, m.ReceiveAmount, m.ReceiveMemo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InvoiceReturnDetailDeletes(string invoiceReturnNo)
        {
            try
            {
                context.InvoiceReturnDetailDelete(invoiceReturnNo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<InvoiceReturnDetailSelectResult> InvoiceReturnDetailSelects(string search, string fromDate,string toDate)
        {
            return context.InvoiceReturnDetailSelect(search,fromDate,toDate).ToList();   
        }
        public InvoiceReturnDetailSelectEditResult InvoiceReturnDetailSelectEdits(string invoiceReturnNo)
        {
            return context.InvoiceReturnDetailSelectEdit(invoiceReturnNo).SingleOrDefault();
        }
    }
}
