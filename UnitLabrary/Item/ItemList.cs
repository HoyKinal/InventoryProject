using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Item
{
    public class ItemList
    {
        private DataLinqDataContext context = new DataLinqDataContext();
        public bool ItemListInserts(string itemCode, string locationCode, char itemType, 
            string categoryCode, string barCode, string purDesciption, decimal cost, string coGSAccount, 
            string saleDescription, string unitSale, decimal salePrice, string incomeAccount,
            string assetAccount,string unitStock,decimal pointRecorder, char stockType,
            char itemStatus,string createBy,DateTime dateCreate,string modifiedBy, DateTime dateModified,
            decimal ana5,string isProperty
            )
        {
            try
            {
                context.ItemListInsert(itemCode, locationCode, itemType, categoryCode, barCode, purDesciption, cost,
                       coGSAccount, saleDescription, unitSale, salePrice, incomeAccount, assetAccount, unitStock,
                       pointRecorder, stockType, itemStatus, createBy, dateCreate, modifiedBy, dateModified, ana5, isProperty);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<ItemListSelectResult> ItemListSelects(string search, string locationCode,string categoryCode)
        {
            return context.ItemListSelect(search,locationCode,categoryCode).ToList();
        }
        public ItemListSelectEditResult ItemListSelectEdits(string itemCode)
        {
            return context.ItemListSelectEdit(itemCode).SingleOrDefault();
        }
        public bool ItemListDeletes(string itemCode, string locationCode)
        {
            SqlConnection conn = (SqlConnection)context.Connection; 
            conn.Open();

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    context.Transaction = transaction;

                    ItemFile itemFile = new ItemFile();

                    itemFile.DeleteItemFiles(itemCode, locationCode);

                    context.ItemListDelete(itemCode);

                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }  
        }
        public bool ItemListUpdate(string itemCode, string locationCode,char itemType,string 
            categoryCode,string barCode, string purDescription,decimal cost, string coGSAccount, 
            string saleDescription, string unitSale,decimal salePrice,string incomeAccount,
            string assetAccount,string unitStock,decimal recorderPoint,char stockType,
            char itemStatus,string createBy,DateTime dateCreated,string modifiedBy,DateTime dateModified
            )
        {
           
            try
            {       
                context.ItemListUpdate(itemCode,locationCode,itemType,categoryCode,barCode,purDescription,cost,coGSAccount,saleDescription,unitSale,salePrice,incomeAccount,assetAccount,unitStock,recorderPoint,stockType,itemStatus,createBy,dateCreated,modifiedBy,dateModified);

                return true;
            }
            catch (Exception)
            {
                  

                return false;
            }
        }
    }
}
