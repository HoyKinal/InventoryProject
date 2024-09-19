using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Customers.Customer
{
    public class CustomerRepo : ICustomerRepo
    {
        private DataLinqDataContext context;
        public CustomerRepo()
        {
            context = new DataLinqDataContext();
        }

        public bool CustomerDeletes(Customer c)
        {
            context.CustomersDelete(c.CustomerCode);
            return true;
        }

        public bool CustomerInsert(Customer c)
        {

            try
            {
                context.CustomersInsert(c.CustomerCode,c.CustomerNumber,c.CustomerName,c.CustomerDesc
                    ,c.CustomerAddress,c.CustomerPhone,c.CustomerFax,c.CustomerEmail,c.CustomerWeb,c.CustomerContact,
                    c.CustomerStatus,c.CustomerType,c.CreateBy,c.CreateDate);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public CustomersSelectEditResult CustomersSelectEdits(Customer c)
        {
            return context.CustomersSelectEdit(c.CustomerCode).SingleOrDefault();
        }

        public IEnumerable<CustomersSelectResult> CustomersSelects(string search)
        {
            return context.CustomersSelect(search).ToList();
        }

        public bool CustomerUpdates(Customer c)
        {
            context.CustomersUpdate(c.CustomerCode, c.CustomerNumber, c.CustomerName, c.CustomerDesc
                    , c.CustomerAddress, c.CustomerPhone, c.CustomerFax, c.CustomerEmail, c.CustomerWeb, c.CustomerContact,
                    c.CustomerStatus, c.CustomerType, c.CreateBy, c.CreateDate,c.UpdateBy,c.UpdateDate,c.Ana4);
            return true;
        }
    }
}
