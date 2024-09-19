using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Item
{
    public class ItemFile
    {
        private DataLinqDataContext context = new DataLinqDataContext();
        public bool ItemFileInserts(string itemFileCode, string itemCode, long itemFileSize, string itemFileName, string itemFileMIME, string createBy, DateTime createDate, string locationCode)
        {
            try
            {
                context.ItemFileInsert(itemFileCode, itemCode, itemFileSize, itemFileName, itemFileMIME, createBy, createDate, locationCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
       public ItemFileSelectEditResult ItemFileSelectEdits(string itemCode)
       {
            return context.ItemFileSelectEdit(itemCode).SingleOrDefault();
       }
       public List<ItemFileSelectResult> ItemFileSelects(string search)
       {
            return context.ItemFileSelect(search).ToList();
       }
       public bool UpdateItemFiles(string itemCode,string itemFilecode,long itemFileSize, string itemFileName, string itemFileMIME, string createBy,DateTime dateCreate, string locationCode)
       {
            try
            {
                context.ItemFileUpdate(itemCode, itemFilecode, itemFileSize, itemFileName, itemFileMIME, createBy, dateCreate, locationCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
       }

       public bool DeleteItemFiles(string itemCode, string locationCode)
       {
            try
            {
                context.ItemFileDelete(itemCode,locationCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
       }
    }
}
