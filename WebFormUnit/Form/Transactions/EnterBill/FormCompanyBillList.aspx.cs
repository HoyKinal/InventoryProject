using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Transaction.Purchases.CompanyExpenses;

namespace WebFormUnit.Form.Transactions.EnterBill
{
    public partial class FormCompanyBillList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.UtcNow.AddHours(7).AddDays(5).ToString("dd/MM/yyyy");

                GridBind("", txtFromDate.Text, txtToDate.Text,true);
            }
        }
        private void GridBind(string search, string fromDate, string toDate, bool indedted)
        {
            BillHeader billHeader = new BillHeader();

            var load = billHeader.BillHeaderSelects(search, fromDate, toDate, indedted);

            if (load != null)
            {
                gvExpenseHeader.DataSource = load;
                gvExpenseHeader.DataBind();
            }
        }

        protected void gvExpenseHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (index < 0) return;

            string billNumber = gvExpenseHeader.DataKeys[index].Value.ToString();

            if (e.CommandName == "OpenItem")
            {
                //Session["BillNumberFormBillListEdit"] = billNumber;

                Response.Redirect($"~/Form/Transactions/EnterBill/FormCompanyEnterBill?BillNumberFromBillListEdit={Server.UrlEncode(billNumber)}");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyEnterBill");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text,txtFromDate.Text,txtToDate.Text,true);
        }
    }
}