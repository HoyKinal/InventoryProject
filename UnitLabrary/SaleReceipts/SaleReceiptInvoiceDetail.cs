using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.SaleReceipts
{
    public class SaleReceiptInvoiceDetail
    {
        private DataLinqDataContext context;
        public SaleReceiptInvoiceDetail()
        {
            context = new DataLinqDataContext();
        }
        public bool SaleReceiptInvoiceDetailInsert(SaleReceiptInvoiceDetailModel m)
        {
            try
            {
                context.InvoiceHeaderDetailInsert(m.InvoiceCode, m.InvoiceNo, m.ItemCode, m.Quantity,
                       m.SaleUnit, m.SalePrice, m.DiscountAmount, m.DiscountPercent, m.TotalDiscount, m.Total, m.locationCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool SaleReceiptInvoiceDetailUpate(SaleReceiptInvoiceDetailModel m)
        {
            try
            {
                context.InvoiceHeaderDetailUpdate(m.InvoiceCode, m.InvoiceNo, m.ItemCode, m.Quantity,
                       m.SaleUnit, m.SalePrice, m.DiscountAmount, m.DiscountPercent, m.TotalDiscount, m.Total, m.locationCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool SaleReceiptInvoiceDetailDelete(string invoiceCode)
        {
            try
            {
                context.InvoiceHeaderDetailDelete(invoiceCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public InvoiceHeaderDetailSelectEditResult SaleReceiptInvoiceDetailSelectEdit(string invoiceCode)
        {
            return context.InvoiceHeaderDetailSelectEdit(invoiceCode).SingleOrDefault() ;
        }
        public List<InvoiceHeaderDetailSelectResult> SaleReceiptInvoiceDetailSelect(string InvoiceNo)
        {
            return context.InvoiceHeaderDetailSelect(InvoiceNo).ToList();
        }
    }
}
