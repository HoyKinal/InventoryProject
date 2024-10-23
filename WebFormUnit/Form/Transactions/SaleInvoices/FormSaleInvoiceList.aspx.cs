using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.SaleReceipts;

namespace WebFormUnit.Form.Transactions.SaleInvoices
{
    public partial class FormSaleInvoiceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy") ;
                txtToDate.Text = DateTime.UtcNow.AddHours(7).AddDays(10).ToString("dd/MM/yyyy");
                GridBind("", txtFromDate.Text, txtToDate.Text, true) ;
            }
        }

        private void GridBind(string search, string startDate, string toDate, bool invoiceStatus)
        {
            SaleReceiptInvoice sr = new SaleReceiptInvoice();

            var load = sr.ReceiptInvoiceHeaderSelects(search.Trim(), startDate, toDate, invoiceStatus);

            if (load != null)
            {
                gvReceiptList.DataSource = load;
                gvReceiptList.DataBind();
            }
        }
        protected void gvReceiptList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            string invoiceNo = gvReceiptList.DataKeys[index].Value.ToString();

            if (e.CommandName == "OpenEdit")
            {
                Response.Redirect($"~/Form/Transactions/SaleInvoices/FormSaleInvoice?InvoiceNoFromInvoiceList={Server.UrlEncode(invoiceNo)}");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoice");

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text, txtFromDate.Text, txtToDate.Text, true);

        }
    }
}