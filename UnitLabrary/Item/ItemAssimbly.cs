using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Item
{
    public class ItemAssimbly
    {
        private DataLinqDataContext context = new DataLinqDataContext();
        public bool InsertItemAssemblies(string assemblyCode,string locationCode,string itemCode,string unit,decimal quantity)
        {
            try
            {
                context.ItemAssemblyInsert(assemblyCode, locationCode, itemCode, unit, quantity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ItemAssemblySelectEditResult itemAssemblySelectEdits(string assemblyCode,string locationCode,string itemCode)
        {
            return context.ItemAssemblySelectEdit(assemblyCode,locationCode,itemCode).SingleOrDefault();
        }
       
        public List<ItemAssemblySelectResult> ItemAssemblySelects(string assemblyCode,string locationCode)
        {
            return context.ItemAssemblySelect(assemblyCode,locationCode).ToList();
        }
        public List<ItemAssemblySelectPartResult> GetAssemblyPartsByLocation(string locationCode,string categoryCode)
        {
            return context.ItemAssemblySelectPart(locationCode,categoryCode).ToList();
        }
        public ItemAssemblySelectInfoResult ItemAssemblySelectInfos(string assemblyCode, string locationCode, string itemCode)
        {
            return context.ItemAssemblySelectInfo(assemblyCode, locationCode, itemCode).SingleOrDefault();
        }
        public bool ItemAssemblyDeletes(string assemblyCode,string locationCode, string itemCode)
        {
            try
            {
                context.ItemAssemblyDelete(assemblyCode, locationCode, itemCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ItemAssemblyUpdates(string assemblyCode,string locationCode,string oldItemCode,string newItemCode,string unit,decimal quantity)
        {
            try
            {
                context.ItemAssemblyUpdate(assemblyCode, locationCode, oldItemCode, newItemCode, unit, quantity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
