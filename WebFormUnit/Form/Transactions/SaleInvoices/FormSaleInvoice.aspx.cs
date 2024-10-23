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
                string invoiceNo = Request.QueryString["InvoiceNoFromSaleInvoiceAddItem"] ?? Request.QueryString["InvoiceNoFromInvoiceList"] ?? Request.QueryString["InvoiceNoFromInvoicePayment"] ?? Request.QueryString["InvoiceNoFromPaymentList"] ??"";

                if (!string.IsNullOrEmpty(invoiceNo))
                {
                    ViewState["InvoiceNo"] = invoiceNo;
                }

                LoadFieldInvoiceHeader();

                LoadInvoiceHeaderSelectEdit(invoiceNo);

                GridBind(invoiceNo);
            }
        }

        //Load gridview invoice detail Item
        private void GridBind(string invoiceNo)
        {
            SaleReceiptInvoiceDetail srd = new SaleReceiptInvoiceDetail();

            var load = srd.SaleReceiptInvoiceDetailSelect(invoiceNo);

            if (load != null)
            {
                gvSaleInvoice.DataSource = load;
                gvSaleInvoice.DataBind();
            }
        }
        //Load Invoice HeaderInfo
        private void LoadInvoiceHeaderSelectEdit(string invoiceNo)
        {
            if (!string.IsNullOrEmpty(invoiceNo))
            {
                SaleReceiptInvoice sr = new SaleReceiptInvoice();

                var load = sr.ReceiptInvoiceHeaderSelectEdits(invoiceNo);

                if (load != null)
                {
                    txtInvoiceNo.Text = load.InvoiceNo;
                    ddlCustomerCode.SelectedValue = load.CustomerCode;
                    txtInvoiceDate.Text = load.InvoiceDate.ToString("dd/MM/yyyy");
                    txtInvoiceDueDate.Text = load.InvoiceDueDate?.ToString("dd/MM/yyyy");
                    txtMemoInvoice.Text = load.Memo;
                    txtVatePercent.Text = load.VatPercent.Value.ToString("F2");
                    txtDiscountPercent.Text = load.DiscountPercent.Value.ToString("F2");
                    txtDiscountAmount.Text = load.DiscountAmount.Value.ToString("F2");
                    txtVatAmount.Text = (load.VatAmount??0.00m).ToString("F2");
                    txtTotalDiscountPercent.Text = (load.TotalDiscountPercent??0.00m).ToString("F2");
                    txtTotalDiscount.Text = (load.TotalDiscount ?? 0.00m).ToString("F2");
                    lbDisplayGrandTotal.Text = (load.GrandTotal ?? 0.00m).ToString("F2") + "$";
                    lbDisplayTotalInvoiceDetail.Text = (load.Total ?? 0.00m).ToString("F2");
                }
            }
        }
        private void LoadFieldInvoiceHeader()
        {
            txtInvoiceNo.Text = GenerateInvoiceNo();
            LoadCustomerCode();
            txtInvoiceDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
            txtInvoiceDueDate.Text = DateTime.UtcNow.AddHours(7).AddDays(10).ToString("dd/MM/yyyy");
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

        //Insert Invoice Header
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            SaleReceiptInvoiceModel srm = new SaleReceiptInvoiceModel()
            {
                InvoiceNo = txtInvoiceNo.Text,
                CustomerCode = ddlCustomerCode.SelectedValue,
                InvoiceDate = txtInvoiceDate.Text.ConvertDateTime(),
                InvoiceDueDate = txtInvoiceDueDate.Text.ConvertDateTime(),
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
            if (e.CommandName == "EditItem" | e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string invoiceCode = gvSaleInvoice.DataKeys[index]["InvoiceCode"].ToString();

                string invoiceNo = gvSaleInvoice.DataKeys[index]["InvoiceNo"].ToString();

                ViewState["InvoiceCode"] = invoiceCode;

                if (e.CommandName == "DeleteItem")
                {
                    SaleReceiptInvoiceDetail srid = new SaleReceiptInvoiceDetail();

                    var check = srid.SaleReceiptInvoiceDetailSelectEdit(invoiceCode);

                    if (check != null)
                    {
                        lbDiscription.Text = check.SaleDescription;

                        ScriptManager.RegisterStartupScript(this, GetType(), "DeleteModal", "DeleteModal();", true);
                    }
                }
                else if (e.CommandName == "EditItem")
                {
                     Response.Redirect($"/Form/Transactions/SaleInvoices/FormSaleInvoiceAddItem?InvoiceCodeFromEditSaleInvoice={Server.UrlEncode(invoiceCode)}&InvoiceNoFromEditSaleInvoice={invoiceNo}");
                }
            }
        }

        //Delete Invoice Item
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewState["InvoiceCode"].ToString()))
            {
                SaleReceiptInvoiceDetail ri = new SaleReceiptInvoiceDetail();

                int countRow = gvSaleInvoice.Rows.Count;

                if (countRow>1)
                {
                    bool isDelete = ri.SaleReceiptInvoiceDetailDelete(ViewState["InvoiceCode"].ToString());

                    if (isDelete)
                    {
                        ShowAlert("Delete item is successfully.", "success");

                        GridBind(ViewState["InvoiceNo"].ToString());

                        ViewState["InvoiceCode"] = null;

                        LoadInvoiceHeaderSelectEdit(ViewState["InvoiceNo"].ToString());
                    }
                    else
                    {
                        ShowAlert("Delete item is failed.", "danger");
                    }
                }
                else
                {
                    ShowAlert("Can not delete this item because it has only one.","danger");

                    ViewState["InvoiceCode"] = null;
                }
            }
        }

        //Update Invoice Header
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["InvoiceNo"]?.ToString() != null)
            {
                SaleReceiptInvoice sr = new SaleReceiptInvoice();

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

                bool isUpdate = sr.SaleReceiptInvoiceUpdates(srm);

                if (isUpdate)
                {
                    ShowAlert("Update Invoice Header is successfully.", "success");

                    LoadInvoiceHeaderSelectEdit(ViewState["InvoiceNo"].ToString());
                }
                else
                {
                    ShowAlert("Update Invoice Header is failed.", "danger");
                }
            }
            else
            {
                ShowAlert("Not found Item for update.", "danger");
            }
        }
        private void ShowAlertAndRedirect(string message, string type, string redirectUrl, int delay)
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
                window.location.href = '{redirectUrl}';
            }}, {delay});";

            ClientScript.RegisterStartupScript(this.GetType(), "showAlertAndRedirect", script, true);
        }
        
        //Delete Invoice Header
        protected void btnDeleteHeader_Click(object sender, EventArgs e)
        {
            if (ViewState["InvoiceNo"]?.ToString() != null)
            {
                SaleReceiptInvoice sr = new SaleReceiptInvoice();


                bool isDelete = sr.SaleReceiptInvoiceDeletes(ViewState["InvoiceNo"].ToString());

                if (isDelete)
                {
                    ShowAlertAndRedirect("Delete Invoice is successfully.", "success", ResolveUrl("~/Form/Transactions/SaleInvoices/FormSaleInvoice"), 1000);
                }
                else
                {
                    ShowAlert("Update Invoice Header is failed.", "danger");
                }
            }
            else
            {
                ShowAlert("Not found InvoiceNo for Delete.", "danger");
            }
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoiceList");
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoicePayment");
        }
    }
}