using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.SaleReceipts
{
    public class SaleReceiptInvoiceDetailModel
    {
        public string InvoiceCode { get; set; }
        public string InvoiceNo { get;set; }
        public string ItemCode { get; set; }
        public decimal Quantity {  get; set; }  
        public string SaleUnit {  get; set; }
        public decimal SalePrice { get; set; }  
        public decimal DiscountAmount { get; set;}
        public decimal DiscountPercent { get; set;}

        public decimal TotalItem => Quantity * SalePrice;
        public decimal TotalDiscount => DiscountAmount +  (DiscountPercent * TotalItem / 100);
        public decimal Total => TotalItem - TotalDiscount;
        public string locationCode { get; set; }   
    }
}
