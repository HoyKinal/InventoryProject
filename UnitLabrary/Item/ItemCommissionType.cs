using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Item
{
    public class ItemCommissionType
    {
        private DataLinqDataContext context;
        public ItemCommissionType()
        {
            context = new DataLinqDataContext();
        }
        public bool ItemCommissionTypeInserts(string commissionTypeCode,string locationCode,
            string commissionTypeName,bool commissionTypeStatus,string payableAccount,
            string expenseAccount,bool isSync,string createBy,DateTime modifiedDate)
        {
            context.ItemCommissionTypeInsert(commissionTypeCode,locationCode,commissionTypeName,commissionTypeStatus
                ,payableAccount,expenseAccount,isSync,createBy,modifiedDate);
            return true;
        }
        public ItemCommissionTypeSelectEditResult ItemCommissionTypeSelectEdits(string commissionTypeCode)
        {
            return context.ItemCommissionTypeSelectEdit(commissionTypeCode).SingleOrDefault();
        }
        public List<ItemCommissionTypeSelectResult> itemCommissionTypeSelects(string search)
        {
            return context.ItemCommissionTypeSelect(search).ToList();
        }

        public bool ItemCommissionTypeUpdates(string commissionTypeCode,string locationCode,string commissionTypeName,
            bool commissionTypeStatus,string payableAccount,string expenseAccount, bool isSync,string createBy,
            DateTime createDate,string modifiedBy, DateTime modifiedDate)
        {
            context.ItemCommissionTypeUpdate(commissionTypeCode,locationCode,commissionTypeName,commissionTypeStatus,
                payableAccount,expenseAccount,isSync,createBy,createDate,modifiedBy,modifiedDate);
            return true;    
        }
        public bool ItemCommissionTypeDeletes(string commissionTypeCode)
        {
            try
            {
                context.ItemCommissionTypeDelete(commissionTypeCode);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
