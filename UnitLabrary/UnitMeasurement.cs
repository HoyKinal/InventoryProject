using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary
{
    public class UnitMeasurement
    {
       private DataLinqDataContext context = new DataLinqDataContext();
        
        //Function Add
        public bool InsertUnitMeasurement(string unitFrom, string UnitFromName, string unitTo, string UnitToName, char? unitOperator, decimal? unitFactor, string createBy, DateTime? createDate, string updateBy, DateTime? updateDate)
        {
            try
            {
                context.UnitMeasurementInsert(unitFrom, UnitFromName, unitTo, UnitToName, unitOperator,unitFactor,createBy,createDate,updateBy,updateDate);
                return true;
            }catch (Exception ex) {
            
                return false;
                throw new Exception($"Error Insert: {ex.Message}");
            }
        }
       
        //Function SelectAll and Filter Search
        public List<UnitMeasurementSelectResult> SelectUnitMeasurement(string search)
        {
            return context.UnitMeasurementSelect(search).ToList();
        }

        //Function SelectEdit (Select By ID)
        public UnitMeasurementSelectEditResult SelectUnitMeasurementEdit(string unitFrom,string unitTo)
        {
            return context.UnitMeasurementSelectEdit(unitFrom,unitTo).SingleOrDefault();
        }

        //Function Update
        public bool UnitMeasurementUpdate(string unitFrom, string unitFromDesc,string unitTo, string unitToDesc, char? unitOperator,decimal? unitFactor, string updateBy, DateTime? updateDate)
        {
            try
            {
                context.UnitMeasurementUpdate(unitFrom,unitFromDesc,unitTo,unitToDesc,unitOperator,unitFactor,updateBy,updateDate);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception($"Error Update: {ex.Message}");
            }
        }
        //Function Delete 
        public bool DeleteMeasurement(string unitFrom,string unitTo)
        {
            context.UnitMeasurementDelete(unitFrom,unitTo);
            return true;
        }

        public bool UpdateUnitMainName(string unitCode, string newUnitName)
        {
            try
            {
                context.UpdateUnitMainName(unitCode, newUnitName);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
