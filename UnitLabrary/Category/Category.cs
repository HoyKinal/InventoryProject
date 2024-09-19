using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.Category
{
    public class Category
    {
        private DataLinqDataContext context = new DataLinqDataContext();
        public List<CategoryTSelectResult> CategorySelects(string search)
        {
            return context.CategoryTSelect(search).ToList();
        }

        public CategoryTSelectEditResult CategoryTSelectEdits (string CategoryCode)
        {
            return context.CategoryTSelectEdit(CategoryCode).SingleOrDefault();    
        }

        public bool CategoryInserts(string categoryCode,string categoryName, char categoryStatus,string createBy,DateTime createDate, string modifyBy,DateTime modifyDate,string ana1, string ana2, string ana3,string ana4, decimal ana5, decimal ana6, decimal ana7, DateTime ana8, DateTime ana9, DateTime ana10)
        {
            try
            {
                context.CategoryTInsert(categoryCode,categoryName,categoryStatus,createBy,createDate,modifyBy,modifyDate,ana1,ana2,ana3,ana4,ana5,ana6,ana7,ana8,ana9,ana10);
               
                return true;

            }catch (Exception) {
               
                return false;   
            }
        }

        public bool CategoryUpdates(string categoryCode, string categoryName, char categoryStatus, string createBy, DateTime createDate, string modifyBy, DateTime modifyDate, string ana1, string ana2, string ana3, string ana4, decimal ana5, decimal ana6, decimal ana7, DateTime ana8, DateTime ana9, DateTime ana10)
        {
            try
            {
                context.CategoryTUpdate(categoryCode, categoryName, categoryStatus, createBy, createDate, modifyBy, modifyDate, ana1, ana2, ana3, ana4, ana5, ana6, ana7, ana8, ana9, ana10);

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool CategoryDelete(string categoryCode)
        {
            try
            {
                context.CategoryTDelete(categoryCode);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool CateogoryDeletes(List<string> categoryCodes)
        {
            //Use exist connection in DataLinqDataContext
            SqlConnection conn = (SqlConnection)context.Connection; 

            conn.Open();

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    //Attach transaction with context
                    context.Transaction = transaction;

                    foreach (var categoryCode in categoryCodes)
                    {
                        context.CategoryTDelete(categoryCode); 
                    }

                    transaction.Commit();
                  
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                   
                    return false;
                }finally { 
                    
                    conn.Close();
                }  

            }
               
        }
    }
}
