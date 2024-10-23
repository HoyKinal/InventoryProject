using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Customers.Customer;
using UnitLabrary.CustomFunction;
using UnitLabrary.SaleReceipts;
using UnitLabrary.Transaction;

namespace WebFormUnit.Form.Transactions.SaleReceipts
{
    public partial class FormSaleReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInvoiceHeader();
                LoadInvoiceCode();
            }
        }
        private void LoadInvoiceCode()
        {
            string invoiceNo = Request.QueryString["InvoiceNoFromAddSaleReceipt"] ?? Request.QueryString["InvoiceNoFromReceiptList"] ??"";

            if (invoiceNo != null)
            {
                ViewState["InvoiceNo"] = invoiceNo; 
            }

            LoadInvoiceHeaderSelectEdit(invoiceNo);
            GridBind(invoiceNo);
        }
        private void GridBind(string InvoiceNo)
        {
            SaleReceiptInvoiceDetail srd = new SaleReceiptInvoiceDetail();

            var load = srd.SaleReceiptInvoiceDetailSelect(InvoiceNo);

            if (load != null)
            {
                gvSaleReceipt.DataSource = load;
                gvSaleReceipt.DataBind();
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
                    txtReciptNo.Text = load.InvoiceNo;
                    ddlCustomerCode.SelectedValue = load.CustomerCode;
                    txtReceiptDate.Text = load.InvoiceDate.ToString("dd/MM/yyyy");
                    txtMemoReceipt.Text = load.Memo;
                    txtVatePercent.Text = (load.VatPercent ?? 0.00m).ToString("F2");
                    txtDiscountPercent.Text = (load.DiscountPercent ?? 0.00m).ToString("F2");
                    txtDiscountAmount.Text = (load.DiscountAmount ?? 0.00m).ToString("F2");
                    txtVatAmount.Text = (load.VatAmount ?? 0.00m)   .ToString("F2");
                    txtTotalDiscountPercent.Text = (load.TotalDiscountPercent ?? 0.00m).ToString("F2");
                    txtTotalDiscount.Text = (load.TotalDiscount ?? 0.00m)   .ToString("F2");
                    lbDisplayGrandTotal.Text = (load.GrandTotal ?? 0.00m).ToString("F2") + "$";
                    lbDisplayTotalInvoiceDetail.Text = (load.Total ?? 0.00m).ToString("F2");
                }
            }
        }
        private void LoadInvoiceHeader()
        {
            LoadCustomer();
            txtReciptNo.Text = GenerateRandomBarcode();
            txtReceiptDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
        }
        private void LoadCustomer()
        {
            Customer customer = new Customer();
            var load = customer.CustomerSelects("");
            if (load != null)
            {
                ddlCustomerCode.DataSource = load;
                ddlCustomerCode.DataTextField = "CustomerName";
                ddlCustomerCode.DataValueField = "CustomerCode";    
                ddlCustomerCode.DataBind();   
            }
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
        private static string GenerateRandomBarcode()
        {
            string prefixPart = "RN-";
            int randomLengthPart = 8;

            const string chars = "0123456789";
            Random random = new Random();
            string radomPart = new string(Enumerable.Repeat(chars, randomLengthPart)
                .Select(r => r[random.Next(r.Length)]).ToArray());
            return prefixPart + radomPart;
        }
        protected void btnNewReceipt_Click(object sender, EventArgs e)
        {
            txtReciptNo.Text = GenerateRandomBarcode();
            ClearField();

        }
        private void ClearField()
        {
            Response.Redirect("~/Form/Transactions/SaleReceipts/FormSaleReceipt");
        }
      
        //Save SaleReceipt
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            SaleReceiptInvoiceModel srm = new SaleReceiptInvoiceModel()
            {
                InvoiceNo = txtReciptNo.Text,
                CustomerCode = ddlCustomerCode.SelectedValue,
                InvoiceDate = txtReceiptDate.Text.ConvertDateTime(),
                Memo = txtMemoReceipt.Text,
                InvoiceStatus = false,
                VatPercent = txtVatePercent.Text.KinalDecimal(),
                DiscountPercent = txtDiscountPercent.Text.KinalDecimal(),
                DiscountAmount = txtDiscountAmount.Text.KinalDecimal(),
            };
            
            SaleReceiptInvoice saleReceipt = new SaleReceiptInvoice();
           
            bool isInsert = saleReceipt.SaleReceiptInvoiceInserts(srm);

            if (isInsert)
            {
                ShowAlert("Insert ReceiptHeader is successfully", "success");

                Response.Redirect($"~/Form/Transactions/SaleReceipts/FormSaleReceiptAddItem?InvoiceNoSaleReceipt={Server.UrlEncode(txtReciptNo.Text)}");

                ClearField();                  
            }
            else
            {
                Response.Redirect($"~/Form/Transactions/SaleReceipts/FormSaleReceiptAddItem?InvoiceNoSaleReceipt={Server.UrlEncode(txtReciptNo.Text)}");
            }          
        }
        
        //Update SaleReceipt
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Check InvoiceNo exist or not

            if (ViewState["InvoiceNo"]?.ToString() != "")
            {
                SaleReceiptInvoice sr = new SaleReceiptInvoice();

                SaleReceiptInvoiceModel srm = new SaleReceiptInvoiceModel()
                {
                    InvoiceNo = txtReciptNo.Text,
                    CustomerCode = ddlCustomerCode.SelectedValue,
                    InvoiceDate = txtReceiptDate.Text.ConvertDateTime(),
                    InvoiceDueDate = null,
                    Memo = txtMemoReceipt.Text,
                    InvoiceStatus = false,
                    VatPercent = txtVatePercent.Text.KinalDecimal(),
                    DiscountPercent = txtDiscountPercent.Text.KinalDecimal(),
                    DiscountAmount = txtDiscountAmount.Text.KinalDecimal(),
                };
                
                bool isUpdate = sr.SaleReceiptInvoiceUpdates(srm);
                
                if (isUpdate)
                {
                    ShowAlert("Update Invoice Header is successfully.","success");
                   
                    LoadInvoiceHeaderSelectEdit(ViewState["InvoiceNo"].ToString());
                }
                else
                {
                    ShowAlert("Update Invoice Header is failed.","danger");
                }
            }
            else
            {
                ShowAlert("Not found InvoiceNo for update.","danger");
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
        protected void btnDeleteHeader_Click(object sender, EventArgs e)
        {
            if (ViewState["InvoiceNo"]?.ToString() != "")
            {
                SaleReceiptInvoice sr = new SaleReceiptInvoice();

               
                bool isDelete = sr.SaleReceiptInvoiceDeletes(ViewState["InvoiceNo"].ToString());

                if (isDelete)
                {
                    ShowAlertAndRedirect("Delete Invoice is successfully.","success", ResolveUrl("~/Form/Transactions/SaleReceipts/FormSaleReceipt"),1000);
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
        protected void gvSaleReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem" | e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string invoiceCode = gvSaleReceipt.DataKeys[index]["InvoiceCode"].ToString();

                string invoiceNo = gvSaleReceipt.DataKeys[index]["InvoiceNo"].ToString();
               
                ViewState["InvoiceCode"] = invoiceCode; 

                if (e.CommandName == "DeleteItem")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "DeleteModal", "DeleteModal();", true) ;
                }
                else if (e.CommandName == "EditItem")
                {
                    Response.Redirect($"/Form/Transactions/SaleReceipts/FormSaleReceiptAddItem?InvoiceCodeFromEditSaleReceipt={Server.UrlEncode(invoiceCode)}&InvoiceNoFromEditSaleReceipt={invoiceNo}");
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //check code exist or not
            SaleReceiptInvoiceDetail srd = new SaleReceiptInvoiceDetail();

            string invoiceCode = ViewState["InvoiceCode"].ToString();

            var check = srd.SaleReceiptInvoiceDetailSelectEdit(invoiceCode);

            if (check != null)
            {
                bool isDelete = srd.SaleReceiptInvoiceDetailDelete(invoiceCode);
                
                if (isDelete)
                {
                    ShowAlert("Delete Item is successfully.", "success");

                    GridBind(ViewState["InvoiceNo"].ToString());

                    LoadInvoiceHeaderSelectEdit(ViewState["InvoiceNo"].ToString());
                }
                else
                {
                    ShowAlert("Delete Item is failed.","danger");
                }
            }
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleReceipts/FormSaleReceiptList");
        }
    }
}