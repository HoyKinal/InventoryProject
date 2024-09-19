using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary
{
    public class UnitMainTransaction
    {
        private DataLinqDataContext context = new DataLinqDataContext();
        public bool InsertUnit(string unitCode, string unitName, bool? unitStatus, bool? unitIsSync, string createBy, DateTime? createDate, string updateBy, DateTime? updateDate)
        {
            // Use the existing context's connection
            var conn = (SqlConnection)context.Connection;
            
            conn.Open();

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    //Attach transaction to the context
                    context.Transaction = transaction;

                    // Perform the insert operation
                    context.UnitMainInsert(unitCode, unitName, unitStatus, unitIsSync, createBy, createDate, updateBy, updateDate);

                    // Commit the transaction if everything succeeds
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
        public bool UpdateUnit(string unitCode, string unitName, bool? unitStatus, bool? unitIsSync, string createBy, DateTime? createDate, string updateBy, DateTime? updateDate)
        {
            var conn = (SqlConnection)context.Connection;

            conn.Open();

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    context.Transaction = transaction;

                    context.UnitMainUpdate(unitCode, unitName, unitStatus, unitIsSync, createBy, createDate, updateBy, updateDate);

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
        public bool DeleteUnit(string unitCode)
        {
            
            SqlConnection conn = (SqlConnection)context.Connection;
           
            conn.Open();

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    context.Transaction = transaction;

                    context.UnitMainDelete(unitCode);

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
           SqlConnection conn = (SqlConnection)context.Connection;

           conn.Open();

           using (SqlTransaction transaction = conn.BeginTransaction())
           {
                try
                {
                    context.Transaction = transaction;

                    context.UpdateUnitMainName(unitCode, newUnitName);

                    transaction.Commit();   

                    return true;
                }
                catch
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

        public bool DeleteUnits(List<string> unitCodes, string userID)
        {
            SqlConnection conn = (SqlConnection)context.Connection;

            conn.Open();

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    context.Transaction = transaction;  

                    foreach (var unitCode in unitCodes) //var = string
                    {
                        context.UnitMainDelete(unitCode);
                    }

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
    }
}
