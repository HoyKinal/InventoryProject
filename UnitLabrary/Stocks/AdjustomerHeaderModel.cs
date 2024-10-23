using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Stocks
{
    public class AdjustomerHeaderModel
    {
        public string AdjustmentNo { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Memo { get; set; }
        public bool AdjustmentStatus { get; set; }
    }
}
