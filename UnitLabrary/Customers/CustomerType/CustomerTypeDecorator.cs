using System.Collections.Generic;

namespace UnitLabrary.Customers.CustomerType
{
    public abstract class CustomerTypeDecorator : ICustomerTypeComponent
    {
        protected ICustomerTypeComponent _customerTypeComponent;

        protected CustomerTypeDecorator(ICustomerTypeComponent customerTypeComponent)
        {
            _customerTypeComponent = customerTypeComponent;
        }

        public virtual bool CustomerTypeDeletes(CustomerType ct)
        {
            return _customerTypeComponent.CustomerTypeDeletes(ct);
        }

        public virtual bool CustomerTypeInserts(CustomerType ct)
        {
            return _customerTypeComponent.CustomerTypeInserts(ct);
        }

        public virtual IEnumerable<MemberTypeSelectResult> CustomerTypeSelect(string search)
        {
            return _customerTypeComponent.CustomerTypeSelect(search);
        }

        public virtual MemberTypeSelectEditResult CustomerTypeSelectEdit(CustomerType ct)
        {
            return _customerTypeComponent.CustomerTypeSelectEdit(ct);
        }

        public virtual bool CustomerTypeUpdate(CustomerType ct)
        {
            return _customerTypeComponent.CustomerTypeUpdate(ct);
        }
    }
}
