using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitLabrary.Customers.CustomerType
{
    public class CustomerTypeConcreteDecorator : CustomerTypeDecorator
    {
        public CustomerTypeConcreteDecorator(ICustomerTypeComponent customerTypeComponent)
            : base(customerTypeComponent)
        {
        }

        public override bool CustomerTypeDeletes(CustomerType ct)
        {
            Debug.WriteLine($"[LOG] Deleting customer type: {ct.MemberTypeCode}");
            return base.CustomerTypeDeletes(ct);
        }

        public override bool CustomerTypeInserts(CustomerType ct)
        {
            Debug.WriteLine($"[LOG] Inserting customer type: {ct.MemberTypeName}");
            return base.CustomerTypeInserts(ct);
        }

        public override IEnumerable<MemberTypeSelectResult> CustomerTypeSelect(string search)
        {
            Debug.WriteLine($"[LOG] Selecting customer type: {search}");
            return base.CustomerTypeSelect(search);
        }

        public override MemberTypeSelectEditResult CustomerTypeSelectEdit(CustomerType ct)
        {
            Debug.WriteLine($"[LOG] Editing customer type: {ct.MemberTypeCode}");
            return base.CustomerTypeSelectEdit(ct);
        }

        public override bool CustomerTypeUpdate(CustomerType ct)
        {
            Debug.WriteLine($"[LOG] Updating customer type: {ct.MemberTypeName}");
            return base.CustomerTypeUpdate(ct);
        }
    }
}
