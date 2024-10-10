using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitLabrary.Transaction.Purchases.CompanyExpense;
using UnitLabrary.Transaction.Purchases.CompanyExpenses;

namespace UnitLabrary.Transaction.Purchases.EnterBill
{
    public class PurchaseReturnTransaction
    {
        private DataLinqDataContext context;

        private SqlConnection conn; 
        public PurchaseReturnTransaction()
        {
            context = new DataLinqDataContext();    
            conn = (SqlConnection)context.Connection;
            conn.Open();
        }
        public bool PurchaseReturnInsert(PurchaseReturnHeaderModel prm)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {

                    context.Transaction = transaction;

                    PurchaseReturnHeader prh = new PurchaseReturnHeader();

                    //Can insert one time refer to BillNo is PK
                    prh.PurchaseReturnHeaderInsert(prm);    

                    //Second Time go To Update Header
                    prh.PurchaseReturnHeaderUpdateDetails(prm);    

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
        public bool PurchaseReturnDetailInsert(PurchaseReturnDetailModel prdm, PurchaseReturnHeaderModel prhm)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    context.Transaction = transaction;

                    PurchaseReturnDetial prd = new PurchaseReturnDetial();

                    prd.PurchaseReturnDetailInserts(prdm);

                    PurchaseReturnHeader prh = new PurchaseReturnHeader();

                    prh.PurchaseReturnHeaderUpdate(prhm);

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
       
        public bool PurchaseReturnDetailDelete(string billNumberBillItemCode, bool option, BillHeaderModel h)
        {
            if (conn.State != System.Data.ConnectionState.Open)
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

                    item.BillItemDeletes(billNumberBillItemCode, option);

                    //Update BillHeader

                    BillHeader bill = new BillHeader();

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
