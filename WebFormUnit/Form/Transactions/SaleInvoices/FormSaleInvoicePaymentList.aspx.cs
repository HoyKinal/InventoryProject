using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.CustomFunction;
using UnitLabrary.SaleReceipts;
using UnitLabrary.Transaction;

namespace WebFormUnit.Form.Transactions.SaleInvoices
{
    public partial class FormSaleInvoicePaymentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadField();
                GridBind("",txtFromDate.Text, txtToDate.Text);
            }
        }

        private void GridBind(string search,string fromDate,string toDate)
        {
            SaleInvoiceReturn sir = new SaleInvoiceReturn();

            var load = sir.InvoiceReturnDetailSelects(search.Trim(),fromDate,toDate);

            if (load != null)
            {
                gvReceiptPaymentList.DataSource = load; 
                gvReceiptPaymentList.DataBind();    
            }
        }
        private void LoadField()
        {
            txtFromDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.UtcNow.AddHours(7).AddDays(10).ToString("dd/MM/yyyy");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text, txtFromDate.Text, txtToDate.Text);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoicePayment");
        }
        protected void btnRecevePayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoice");
        }
        protected void gvReceiptPaymentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenItem" || e.CommandName == "EditItem" || e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string invoiceReturnNo = gvReceiptPaymentList.DataKeys[index]["InvoiceReturnNo"].ToString();
                string invoiceNo = gvReceiptPaymentList.DataKeys[index]["InvoiceNo"].ToString();

                ViewState["InvoiceNo"] = invoiceNo;

                ViewState["InvoiceReturnNo"] = invoiceReturnNo;

                if (e.CommandName == "EditItem")
                {
                    SaleInvoiceReturn sir = new SaleInvoiceReturn();
                    
                    var check = sir.InvoiceReturnDetailSelectEdits(invoiceReturnNo);
                   
                    if(check != null)
                    {
                        txtInvoiceNo.Text = check.InvoiceNo;
                        txtReceiptDate.Text = check.ReceiveDate.ToString("dd/MM/yyyy");
                        txtInvoiceDate.Text = check.InvoiceDate.ToString("dd/MM/yyyy");
                        txtRemainAmount.Text = check.RemainAmount.Value.ToString("F2");
                        txtReceiptAmount.Text = check.ReceiveAmount.ToString("F2");
                        txtMemo.Text = check.ReceiveMemo;

                        ScriptManager.RegisterStartupScript(this,GetType(), "showEditModal", "showEditModal();", true);
                    }
                }
                else if(e.CommandName == "DeleteItem")
                {
                    SaleInvoiceReturn sir = new SaleInvoiceReturn();

                    var check = sir.InvoiceReturnDetailSelectEdits(invoiceReturnNo);
                   
                    if (check != null)
                    {
                        lbDiscription.Text = check.InvoiceNo.ToString() + '-' + check.ReceiveDate.ToString("dd/MM/yyyy");    
                    }
                    ScriptManager.RegisterStartupScript(this,GetType(), "showDeleteModal", "showDeleteModal();", true);
                }
                else if(e.CommandName == "OpenItem")
                {
                    Response.Redirect($"~/Form/Transactions/SaleInvoices/FormSaleInvoice?InvoiceNoFromPaymentList{Server.UrlEncode(invoiceNo)}");
                }
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaleInvoiceReturn sir = new SaleInvoiceReturn();

            if (ViewState["InvoiceReturnNo"] != null)
            {
                SaleInvoiceReturnModel m = new SaleInvoiceReturnModel()
                {
                    InvoiceReturnNo = ViewState["InvoiceReturnNo"]?.ToString(),
                    InvoiceNo = txtInvoiceNo.Text,
                    ReceiveDate = txtReceiptDate.Text.ConvertDateTime(),
                    ReceiveAmount = txtReceiptAmount.Text.KinalDecimal(),
                    ReceiveMemo = txtMemo.Text
                };

                SaleReceiptInvoice sri = new SaleReceiptInvoice();  

                var check = sri.ReceiptInvoiceHeaderSelectEdits(ViewState["InvoiceNo"].ToString());

                if (txtReceiptAmount.Text.KinalDecimal()>check.GrandTotal || txtReceiptAmount.Text.KinalDecimal()<0)
                {
                    ShowAlert("Insert receive amount is incorrect, please try again.","danger");
                }
                else
                {
                    bool isUpdate = sir.InvoiceReturnDetailUpdates(m);

                    if (isUpdate)
                    {
                        ShowAlert("Update Receive Payment is successfully.", "success");

                        GridBind("", txtFromDate.Text, txtToDate.Text);


                        ViewState["InvoiceReturnNo"] = null;
                    }
                    else
                    {
                        ShowAlert("UPdate Receive Payment is failed.", "danger");
                    }
                }
            }   
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewState["InvoiceReturnNo"]?.ToString()))
            {
                SaleInvoiceReturn sir = new SaleInvoiceReturn();

                bool isDelete = sir.InvoiceReturnDetailDeletes(ViewState["InvoiceReturnNo"]?.ToString());

                if(isDelete) 
                {
                    ShowAlert("Delete received payment is successfully.", "success");

                    GridBind("", txtFromDate.Text, txtToDate.Text);

                    ViewState["InvoiceReturnNo"] = null;
                }
                else
                {
                    ShowAlert("Delete received payment is failed.","danger");
                }
            }
            else
            {
                return;
            }
        }
    }
}