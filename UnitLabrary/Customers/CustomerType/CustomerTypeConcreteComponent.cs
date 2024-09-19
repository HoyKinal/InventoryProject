using System.Collections.Generic;
using System.Linq;

namespace UnitLabrary.Customers.CustomerType
{
    public class CustomerTypeConcreteComponent : ICustomerTypeComponent
    {
        private DataLinqDataContext context;

        public CustomerTypeConcreteComponent()
        {
            context = new DataLinqDataContext();
        }

        public bool CustomerTypeDeletes(CustomerType ct)
        {
            try
            {
                context.MemberTypeDelete(ct.MemberTypeCode);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CustomerTypeInserts(CustomerType ct)
        {
            try
            {
                context.MemberTypeInsert(
                    ct.MemberTypeCode, ct.MemberTypeName,
                    ct.MemberTypePrice, ct.MemberTypeStatus,
                    ct.MemberTypeIsSync, ct.MemberTypeDiscount,
                    ct.ModifiedBy,
                    ct.ModifiedDate
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<MemberTypeSelectResult> CustomerTypeSelect(string search)
        {
            return context.MemberTypeSelect(search).ToList();
        }

        public MemberTypeSelectEditResult CustomerTypeSelectEdit(CustomerType ct)
        {
            return context.MemberTypeSelectEdit(ct.MemberTypeCode).SingleOrDefault();
        }

        public bool CustomerTypeUpdate(CustomerType ct)
        {
            try
            {
                context.MemberTypeUpdate(
                    ct.MemberTypeCode, ct.MemberTypeName,
                    ct.MemberTypePrice, ct.MemberTypeStatus,
                    ct.MemberTypeIsSync, ct.MemberTypeDiscount,
                    ct.ModifiedBy, ct.ModifiedDate
                );
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
