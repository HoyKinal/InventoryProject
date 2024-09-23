using System;
using System.Collections.Generic;
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
    }
}
