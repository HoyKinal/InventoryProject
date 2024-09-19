using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Item.ItemCommissions
{
    public class ItemCommissions
    {
        public string ItemCode { get; set; }
        public string LocationCode { get; set; }
        public string CommissionTypeCode { get; set; }
        public decimal ItemCommission { get; set; }
        public bool ItemCommissionPercent { get; set; }
        public bool IsSync { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
