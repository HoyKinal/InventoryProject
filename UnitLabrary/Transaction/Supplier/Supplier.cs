using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Transaction.Suppliers
{
    public class Supplier
    {
        private DataLinqDataContext context;
        public Supplier() { 
            context = new DataLinqDataContext();
        }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string SupplierDesc { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierFax { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierWeb { get; set; }
        public string SupplierContact { get; set; }
        public char SupplierStatus { get; set; }
        public char SupplierType { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

        public bool SupplierInserts(Supplier s)
        {
            context.SuppliersInsert(s.SupplierCode,s.SupplierName,s.SupplierDesc,s.SupplierAddress,s.SupplierPhone,
                s.SupplierFax,s.SupplierEmail,s.SupplierWeb,s.SupplierContact,s.SupplierStatus,s.CreateBy,s.CreateDate);
            return true;
        }
        public IEnumerable<SuppliersSelectResult> SuppliersSelects(string search)
        {
            return context.SuppliersSelect(search).ToList();
        }
        public SuppliersSelectEditResult SuppliersSelectEdits(Supplier s)
        {
            return context.SuppliersSelectEdit(s.SupplierCode).SingleOrDefault();
        }
        public bool SupplierUpdates(Supplier s)
        {
            context.SuppliersUpdate(s.SupplierCode, s.SupplierName, s.SupplierDesc, s.SupplierAddress, s.SupplierPhone,
                s.SupplierFax, s.SupplierEmail, s.SupplierWeb, s.SupplierContact, s.SupplierStatus,s.SupplierType, s.CreateBy, 
                s.CreateDate,s.UpdateBy, s.UpdateDate);
            return true;
        }
        public bool SupplierDeletes(Supplier s)
        {
            context.SuppliersDelete(s.SupplierCode);
            return true;
        }
    }
}
