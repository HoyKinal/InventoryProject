using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Customers.Customer;
using UnitLabrary.CustomFunction;
using UnitLabrary.SaleReceipts;
using UnitLabrary.Transaction;

namespace WebFormUnit.Form.Transactions.SaleInvoices
{
    public partial class FormSaleInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFieldInvoiceHeader();
            }
        }

        private void LoadFieldInvoiceHeader()
        {
            txtInvoiceNo.Text = GenerateInvoiceNo();
            LoadCustomerCode();
            txtInvoiceDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy"); 
        }

        private string GenerateInvoiceNo()
        {
            string header = "IN-";           
            const string characters = "1234567890";
            int length = 8;
            Random random = new Random();
            string randomPart = new string(Enumerable.Repeat(characters, length)
                .Select(r => r[random.Next(r.Length)]).ToArray());
            return header + randomPart;
        }
        private void LoadCustomerCode()
        {
            Customer c = new Customer();
            var load = c.CustomerSelects("");
            ddlCustomerCode.DataSource = load;
            ddlCustomerCode.DataTextField = "CustomerName";
            ddlCustomerCode.DataValueField = "CustomerCode";
            ddlCustomerCode.DataBind(); 
        }
        private void ClearField()
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoice");
        }
        protected void btnNewInvoice_Click(object sender, EventArgs e)
        {
            ClearField();
            txtInvoiceNo.Text = GenerateInvoiceNo();         
        }
        private void ShowAlert(string message, string type)
        {
            string script = $@"
                var alertDiv = document.createElement('div');
                alertDiv.className = 'alert alert-{type}';
                alertDiv.role = 'alert';
                alertDiv.innerHTML = '{message}';
                document.body.insertBefore(alertDiv, document.body.firstChild);

                setTimeout(function() {{
                    alertDiv.style.display = 'none';
                    alertDiv.remove();
                }}, 2000); 
            ";

            ClientScript.RegisterStartupScript(this.GetType(), "showAlert", script, true);
        }
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            SaleReceiptInvoiceModel srm = new SaleReceiptInvoiceModel()
            {
                InvoiceNo = txtInvoiceNo.Text,
                CustomerCode = ddlCustomerCode.SelectedValue,
                InvoiceDate = txtInvoiceDate.Text.ConvertDateTime(),
                Memo = txtMemoInvoice.Text,
                InvoiceStatus = true,
                VatPercent = txtVatePercent.Text.KinalDecimal(),
                DiscountPercent = txtDiscountPercent.Text.KinalDecimal(),
                DiscountAmount = txtDiscountAmount.Text.KinalDecimal(),
            };

            SaleReceiptInvoice saleReceipt = new SaleReceiptInvoice();

            bool isInsert = saleReceipt.SaleReceiptInvoiceInserts(srm);

            if (isInsert)
            {
                ShowAlert("Insert ReceiptHeader is successfully", "success");

                Response.Redirect($"~/Form/Transactions/SaleInvoices/FormSaleInvoiceAddItem?InvoiceNoSaleInvoice={Server.UrlEncode(txtInvoiceNo.Text)}");

                ClearField();
            }
            else
            {
                Response.Redirect($"~/Form/Transactions/SaleInvoices/FormSaleInvoiceAddItem?InvoiceNoSaleInvoice={Server.UrlEncode(txtInvoiceNo.Text)}");

            }
        }
        protected void gvSaleInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}