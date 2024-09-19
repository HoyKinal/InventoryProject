using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Customers.Customer
{
    public interface ICustomerRepo
    {
        bool CustomerInsert(Customer c);
        IEnumerable<CustomersSelectResult> CustomersSelects(string search);
        CustomersSelectEditResult CustomersSelectEdits(Customer c);
        bool CustomerDeletes(Customer c);
        bool CustomerUpdates(Customer c);
    }
}
