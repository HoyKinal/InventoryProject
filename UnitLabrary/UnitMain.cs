using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitLabrary
{
    public class UnitMain
    {
        private DataLinqDataContext context = new DataLinqDataContext();

        public bool InsertUnit(string unitCode, string unitName, bool? unitStatus, bool? unitIsSync, string createBy, DateTime? createDate, string updateBy, DateTime? updateDate)
        {
            try
            {
                context.UnitMainInsert(unitCode, unitName, unitStatus, unitIsSync, createBy, createDate, updateBy, updateDate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateUnit(string unitCode, string unitName, bool? unitStatus, bool? unitIsSync, string createBy, DateTime? createDate, string updateBy, DateTime? updateDate)
        {
            try
            {
                context.UnitMainUpdate(unitCode, unitName, unitStatus, unitIsSync, createBy, createDate, updateBy, updateDate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUnit(string unitCode)
        {
            try
            {
                context.UnitMainDelete(unitCode);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<UnitMainSelectResult> SelectUnits(string search)
        {
            return context.UnitMainSelect(search).ToList();
        }

        public UnitMainSelectEditResult SelectUnitForEdit(string search)
        {
            return context.UnitMainSelectEdit(search).SingleOrDefault();
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

        public bool DeleteUnits(List<string> unitCodes, string userID)
        {
            try
            {
                foreach (var unitCode in unitCodes) //var = string
                {
                    context.UnitMainDelete(unitCode);
                }
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}
