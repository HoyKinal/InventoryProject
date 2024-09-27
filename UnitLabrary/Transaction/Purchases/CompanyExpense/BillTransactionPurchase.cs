using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitLabrary.Transaction.Purchases.CompanyExpenses;

namespace UnitLabrary.Transaction.Purchases.CompanyExpense
{
    public class BillTransactionPurchase
    {
        private DataLinqDataContext context;
        private SqlConnection conn;

        public BillTransactionPurchase()
        {
            context = new DataLinqDataContext();
            conn = (SqlConnection)context.Connection;
            conn.Open();
        }

        public bool PurchaseItemInsert(BillHeaderModel h, BillItemModel i)
        {
            conn = (SqlConnection)context.Connection;
           
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    context.Transaction = transaction;

                    //Insert BillHeader
                    BillHeader bill = new BillHeader();
                    bill.BillHeaderInserts(h);

                    //Insert BillItem
                    BillItem billItem = new BillItem();

                    billItem.BillItemInserts(i);

                   
                    bill.BillHeaderUpdate(h);

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
        public bool PurchaseItemUpdate(BillItemModel i,BillHeaderModel h)
        {
            SqlConnection conn = (SqlConnection)context.Connection;

            //Connection State: check whether the connection is already open.

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using (SqlTransaction transaction = conn.BeginTransaction()) 
            {
                try
                {
                    context.Transaction = transaction;

                    //Update BillItem
                    BillItem item = new BillItem();

                    item.BillItemUpdates(i);

                    //Update BillHeader

                    BillHeader bill = new BillHeader();

                    bill.BillHeaderUpdate(h);

                    transaction.Commit();

                    return true;

                }catch (Exception)
                {
                    transaction.Rollback();

                    return false;
                }
                finally
                {
                    conn.Close ();  
                }
            }
        }
        public bool PurchaseItemDelete(string billNumberBillItemCode, bool option,BillHeaderModel h)
        {
            SqlConnection conn = (SqlConnection)context.Connection;

            //Connection State: check whether the connection is already open.

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    context.Transaction = transaction;

                    //Delete BillItem
                    BillItem item = new BillItem();

                    item.BillItemDeletes(billNumberBillItemCode,option);

                    //Delete BillHeader

                    BillHeader bill = new BillHeader();

                    bill.BillHeaderDeletes(h);
                   

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
