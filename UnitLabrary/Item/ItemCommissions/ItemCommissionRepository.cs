using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Item.ItemCommissions
{
    public class ItemCommissionRepository : IItemCommissionRepository
    {
        private DataLinqDataContext context;
        public ItemCommissionRepository()
        {
            context = new DataLinqDataContext();
        }
        //public ItemCommissionRepository(DataLinqDataContext context)
        //{
        //    this.context = context;
        //}
        public bool ItemCommissionDeletes(ItemCommissions item)
        {
            try
            {
                context.ItemCommissionDelete(item.ItemCode, item.LocationCode, item.CommissionTypeCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ItemCommissionInserts(ItemCommissions item)
        {
            try
            {
                context.ItemCommissionInsert(item.ItemCode, item.LocationCode, item.CommissionTypeCode,
                       item.ItemCommission, item.ItemCommissionPercent, item.IsSync, item.CreateBy, item.CreateDate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // Implicit implementation
        public bool ItemCommissionUpdates(ItemCommissions item)
        {
            try
            {
                context.ItemCommissionUpdate(item.ItemCode, item.LocationCode, item.CommissionTypeCode,
                       item.ItemCommission, item.ItemCommissionPercent, item.IsSync, item.CreateBy,
                       item.CreateDate, item.ModifiedBy, item.ModifiedDate);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        // Implicit implementation
        public ItemCommissionSelectEditResult ItemCommissionSelectEdit(ItemCommissions item)
        {
            return context.ItemCommissionSelectEdit(item.ItemCode, item.LocationCode, item.CommissionTypeCode).SingleOrDefault();
        }
        // Explicit implementation
        IEnumerable<ItemCommissionSelectResult> IItemCommissionRepository.ItemCommissionSelects(string search, string commissionTypeCode)
        {
            return context.ItemCommissionSelect(search,commissionTypeCode).ToList();
        }
    }
}
