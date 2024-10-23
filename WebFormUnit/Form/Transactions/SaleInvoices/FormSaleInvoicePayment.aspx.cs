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

namespace WebFormUnit.Form.Transactions.SaleInvoices
{
    public partial class FormSaleInvoicePayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFields();
                GridBind(ddlCustomerCode.SelectedValue);
            }
        }
        private void LoadFields()
        {
            LoadCustomer();
            txtReceiveDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
        }
        private void LoadCustomer()
        {
            Customer cs = new Customer();
            var load = cs.CustomerSelects("");
            ddlCustomerCode.DataSource = load;
            ddlCustomerCode.DataTextField = "CustomerName";
            ddlCustomerCode.DataValueField = "CustomerCode";    
            ddlCustomerCode.DataBind(); 
        }

        private void GridBind(string cutomerCode)
        {
            SaleInvoiceReturn sir = new SaleInvoiceReturn();    
            var load = sir.InvoiceReturnHeaderSelects(cutomerCode);
            if (load != null)
            {
                gvSaleInvoicePayment.DataSource = load; 
                gvSaleInvoicePayment.DataBind();    
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoice");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(ddlCustomerCode.SelectedValue);
        }
        private void ClearField()
        {
            txtReceiveDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
            txtReceiveAmount.Text = string.Empty;
            txtMemo.Text = string.Empty;
        }
        protected void gvSaleInvoicePayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {                       
            int index = Convert.ToInt32(e.CommandArgument);
                
            GridViewRow row = gvSaleInvoicePayment.Rows[index];

            string totalRemainAmountStr = row.Cells[7].Text;

            hdfRemainAmount.Value = HttpUtility.HtmlEncode(totalRemainAmountStr);

            string invoiceNo = gvSaleInvoicePayment.DataKeys[index].Value.ToString();

            if (e.CommandName == "PaymentItem")
            {
                SaleReceiptInvoice ri = new SaleReceiptInvoice();
                    
                var check = ri.ReceiptInvoiceHeaderSelectEdits(invoiceNo);

                if (check != null)
                {
                    txtInvoiceNo.Text = check.InvoiceNo;
                    txtInvoiceDate.Text = check.InvoiceDate.ToString("dd/MM/yyyy");
                    decimal totalRemainAmount = totalRemainAmountStr.KinalDecimal();
                    txtRemainAmount.Text = totalRemainAmount.ToString();
                    txtReceiveAmount.Text = totalRemainAmount.ToString();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "showAddModal", "showAddModal();", true) ;
            }
            else if (e.CommandName == "OpenItem")
            {
                Response.Redirect($"/Form/Transactions/SaleInvoices/FormSaleInvoice?InvoiceNoFromInvoicePayment={HttpUtility.UrlEncode(invoiceNo)}");
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
            SaleInvoiceReturnModel m = new SaleInvoiceReturnModel()
            {
                InvoiceReturnNo = DateTime.Now.Ticks.ToString(),    
                InvoiceNo = txtInvoiceNo.Text,  
                ReceiveDate = txtReceiveDate.Text.ConvertDateTime(),
                ReceiveAmount = txtReceiveAmount.Text.KinalDecimal(),
                ReceiveMemo = txtMemo.Text
            };

            SaleInvoiceReturn sr = new SaleInvoiceReturn();



            if (hdfRemainAmount.Value.KinalDecimal()-txtReceiveAmount.Text.KinalDecimal()<0)
            {
                ShowAlert("Insert amount is wrong.","danger");
            }
            else
            {
                bool isInsert = sr.InvoiceReturnDetailInserts(m);

                if (isInsert)
                {
                    ShowAlert("Insert payment invoice is successfully.", "success");

                    GridBind(ddlCustomerCode.SelectedValue);

                    ClearField();
                }
                else
                {
                    ShowAlert("Insert payment invoice is failed.", "danger");
                }
            }
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoicePaymentList");
        }

        protected void gvSaleInvoicePayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //check it is data row (not header/footer)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfTotalInvoice = (HiddenField)e.Row.FindControl("hfTotalInvoice");

                HiddenField hdfTotalReceivedAmount = (HiddenField)e.Row.FindControl("hfTotalReceiptPayment");

                HiddenField hdfTotalRemainAmount = (HiddenField)e.Row.FindControl("hdfRemainAmount");

            

                if (hdfTotalInvoice != null)
                {
                    decimal GrandTotal = hdfTotalInvoice.Value.KinalDecimal();

                    decimal receivedAmount = string.IsNullOrEmpty(hdfReceivedAmount.Value) ? hdfTotalReceivedAmount.Value.KinalDecimal() : 0m;

                    decimal recivedAmountPercent = (receivedAmount/GrandTotal) * 100 ;


                    TableCell tableCell = e.Row.Cells[7];

                    if (recivedAmountPercent < 50)
                    {
                       // e.Row.BackColor = System.Drawing.Color.Red;

                       // tableCell.Text = $"<span style='background-color: gray; padding:5px; border-radius:5px;'></span>";
                    }
                    else if (recivedAmountPercent <= 70)
                    {
                        //tableCell.BackColor = System.Drawing.Color.Gray;
                      
                        //tableCell.Text = $"<span style='background-color: yellow; padding:5px; border-radius:5px;'>{hdfTotalRemainAmount.Value}</span>";

                    }
                    else
                    {
                        //e.Row.BackColor = System.Drawing.Color.Green;

                        //tableCell.Text = $"<span style='background-color: Green; padding:5px; border-radius:5px;'>{remainingAmount}</span>";
                    }
                }
            }
        }
    }
}