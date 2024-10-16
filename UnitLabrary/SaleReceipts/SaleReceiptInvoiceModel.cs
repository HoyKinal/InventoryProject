using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.SaleReceipts
{
    public class SaleReceiptInvoiceModel
    {
        public string InvoiceNo { get; set; }
        public string CustomerCode { get; set;}
        public DateTime InvoiceDate { get; set; }   
        public string Memo { get; set;}
        public bool InvoiceStatus { get; set; } 
        public decimal VatPercent { get; set; } 
        public decimal DiscountPercent { get; set;}
        public decimal DiscountAmount { get; set;}  

    
    }
}
