using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Stocks
{
    public class AdjustmentHeader
    {
        private DataLinqDataContext context;
        public AdjustmentHeader()
        {
            context = new DataLinqDataContext();    
        }
        public bool AdjustmentHeaderInserts(AdjustomerHeaderModel m)
        {
            try
            {
                context.AdjustmentHeaderInsert(m.AdjustmentNo,m.AdjustmentDate,m.Memo,m.AdjustmentStatus);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool AdjustmentHeaderUpdates(AdjustomerHeaderModel m)
        {
            try
            {
                context.AdjustmentHeaderUpdate(m.AdjustmentNo, m.AdjustmentDate, m.Memo, m.AdjustmentStatus);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool AdjustmentHeaderDeletes(string adjustmentNo)
        {
            try
            {
                context.AdjustmentHeaderDelete(adjustmentNo);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<AdjustmentHeaderSelectResult> AdjustmentHeaderSelects(string search)
        {
            return context.AdjustmentHeaderSelect(search).ToList();
        }
        public AdjustmentHeaderSelectEditResult AdjustmentHeaderSelectEdits(string adjustmentNo)
        {
            return context.AdjustmentHeaderSelectEdit(adjustmentNo).SingleOrDefault();
        }
    }
}
