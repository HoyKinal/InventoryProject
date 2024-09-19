using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Item
{
    public class ItemProperty
    {
        private DataLinqDataContext context = new DataLinqDataContext();
        public bool ItemPropertyInserts(string propertyId,string propertyName,int? propertyOrder,string locationCode,char? propertyStatus,string createBy,DateTime createDate,DateTime dateModify)
        {
            context.ItemPropertyInsert(propertyId, propertyName, propertyOrder, locationCode, propertyStatus, createBy,createDate, dateModify);
            return true;
        }
        public List<ItemPropertySelectResult> ItemPropertySelects(string search)
        {
            return context.ItemPropertySelect(search).ToList(); 
        }
        public ItemPropertySelectEditResult ItemPropertySelectToEdit(string propertyId)
        {
            return context.ItemPropertySelectEdit(propertyId).SingleOrDefault();
        }
        public bool ItemPropertyDeletes(string propertyId)
        {
            try
            {
                context.ItemPropertyDelete(propertyId);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ItemPropertyUpToDate(string propertyId,string propertyName,int propertyOrder,
            string locationCode,char propertyStatus,string createBy,DateTime createDate,string modifyBy,DateTime modifyDate,bool isSync)
        {
            try
            {
                context.ItemPropertyUpdate(propertyId, propertyName, propertyOrder, locationCode, propertyStatus, createBy, createDate, modifyBy, modifyDate, isSync);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
