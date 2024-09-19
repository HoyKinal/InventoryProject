using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Item.ItemCommissions
{
    public interface IItemCommissionRepository
    {
        bool ItemCommissionInserts(ItemCommissions item);
        bool ItemCommissionUpdates(ItemCommissions item);
        bool ItemCommissionDeletes(ItemCommissions item);
        ItemCommissionSelectEditResult ItemCommissionSelectEdit(ItemCommissions item);
        IEnumerable<ItemCommissionSelectResult> ItemCommissionSelects(string search,string commissionTypeCode);
    }
}
