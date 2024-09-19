using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Customers.CustomerType
{
    public interface ICustomerTypeComponent
    {
        bool CustomerTypeInserts(CustomerType ct);
        IEnumerable<MemberTypeSelectResult> CustomerTypeSelect(string search);
        MemberTypeSelectEditResult CustomerTypeSelectEdit(CustomerType ct);
        bool CustomerTypeDeletes(CustomerType ct);
        bool CustomerTypeUpdate(CustomerType ct);
    }
}
