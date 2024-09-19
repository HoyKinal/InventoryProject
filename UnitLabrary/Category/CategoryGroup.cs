using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Category
{
    public class CategoryGroup
    {
        private DataLinqDataContext context = new DataLinqDataContext();   

        public bool CategoryGroupInserts(string categoryGroupId, string categoryGroupName)
        {
            try
            {
                context.CategoryGroupInsert(categoryGroupId, categoryGroupName);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool CategoryGroupUpdates(string categoryGroupId, string categoryGroupName)
        {
            try
            {
                context.CategoryGroupUpdate(categoryGroupId, categoryGroupName);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool CategoryGroupDeletes(string categoryGroupId)
        {
            try
            {
                context.CategoryGroupDelete(categoryGroupId);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public List<CategoryGroupSelectResult> CategoryGroupSelects(string search)
        {
            return context.CategoryGroupSelect(search).ToList();
        }
        public CategoryGroupSelectEditResult CategoryGroupSelectEdits(string categoryGroupID)
        {
            return context.CategoryGroupSelectEdit(categoryGroupID).SingleOrDefault();
        }

        public bool CategoryGroupUpdateAtCategoryType(string categoryCode,string locationCode,string categoryGroupId, bool? isDelete)
        {
            try
            {
                context.ItemCategoryGroupUpdate(categoryCode, locationCode, categoryGroupId, isDelete);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        
        public List<ItemCategoryGroupSelectResult> CategoryGroupItemsSelects(string categoryGroupId)
        {
            return context.ItemCategoryGroupSelect(categoryGroupId).ToList();
        }
    }
}
