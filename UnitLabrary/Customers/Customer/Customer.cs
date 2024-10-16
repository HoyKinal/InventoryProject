using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Customers.Customer
{
    public class Customer
    {
        private DataLinqDataContext context;
        public Customer()
        {
            context = new DataLinqDataContext();
        }

        public bool CustomerInsert(CustomerModel m)
        {
            try
            {
                context.CustomersInsert(m.CustomerCode,m.CustomerNumberCard,m.CustomerName,m.CustomerDesc,
                    m.CustomerAddress,m.CustomerPhone,m.CustomerEmail,m.CustomerWeb,
                    m.CustomerContact,m.CustomerStatus,m.CustomerType);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public List<CustomersSelectResult> CustomerSelects(string search)
        {
            return context.CustomersSelect(search).ToList();
        }
        public CustomersSelectEditResult CustomerSelectEdits(string customerCode)
        {
            return context.CustomersSelectEdit(customerCode).SingleOrDefault();
        }
        public bool CustomerUpdate(CustomerModel m)
        {
            try
            {
                context.CustomersInsert(m.CustomerCode, m.CustomerNumberCard, m.CustomerName, m.CustomerDesc,
                    m.CustomerAddress, m.CustomerPhone, m.CustomerEmail, m.CustomerWeb,
                    m.CustomerContact, m.CustomerStatus, m.CustomerType);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool CustomerDeletes(string customerCode)
        {
            try
            {
                context.CustomersDelete(customerCode);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
