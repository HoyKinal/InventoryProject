using OfficeOpenXml.Table.PivotTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.CustomFunction;
using UnitLabrary.Transaction;
using UnitLabrary.Transaction.Purchases.EnterBill;

namespace WebFormUnit.Form.Transactions.EnterBill
{
    public partial class FormCompanyPayBillList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IsPostBack type as bool: mean server process request from user at client when user click submit
            //Page load first time is false
            //As a result is true
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.UtcNow.AddHours(7).AddDays(10).ToString("dd/MM/yyyy");
                GridBind("",txtFromDate.Text,txtToDate.Text);
            }
        }
        private void GridBind(string search,string startDate, string endDate)
        {
            PurchaseReturnDetial prd = new PurchaseReturnDetial();
            var load = prd.PurchaseReturnDetailResultSelect(search.Trim(),startDate,endDate);
            if (load != null)
            {
                gvPayBill.DataSource = load;
                gvPayBill.DataBind();
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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyPayBill");
        }

        protected void gvPayBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string billNo = gvPayBill.DataKeys[index]["BillNo"].ToString();

            string purchaseReturnNo = gvPayBill.DataKeys[index]["PurchaseReturnNo"].ToString();

            GridViewRow row = gvPayBill.Rows[index];

            HiddenField hdfPaidDate = (HiddenField)row.FindControl("hdfPaidDate");
            HiddenField hdfPaidAmount = (HiddenField)row.FindControl("hdfPaidAmount");
            HiddenField hdfMemoReturnPaid = (HiddenField)row.FindControl("hdfMemoReturnPaid");

            string paidDate = hdfPaidDate.Value;
            string paidAmount = hdfPaidAmount.Value;
            string memoReturnPaid = hdfMemoReturnPaid.Value;

            ViewState["PurchaseReturnNo"] = purchaseReturnNo;

            if (e.CommandName == "OpenItem" || e.CommandName == "EditItem" || e.CommandName == "DeleteItem")
            {
                if (e.CommandName == "OpenItem")
                {
                    Response.Redirect($"~/Form/Transactions/EnterBill/FormCompanyEnterBill?BillNoFromPayBillList={Server.UrlEncode(billNo)}");
                }
                else if (e.CommandName == "EditItem")
                {
                    PurchaseReturnHeader prh = new PurchaseReturnHeader();

                    var load = prh.PurchaseReturnHeaderSelectResultEdit(billNo);

                    if (load !=null)
                    {
                        txtBillNo.Text = load.BillNo;
                        txtDateBill.Text = load.PurchaseDateBill.ToString("dd/MM/yyyy");
                        txtUnpaidAmount.Text = load.Unpaid.Value.ToString("F2");
                        if (DateTime.TryParse(paidDate, out DateTime parseDatePaid))
                        {
                            txtDatePaid.Text = parseDatePaid.ToString("dd/MM/yyyy");
                        }
                        txtPayAmount.Text = paidAmount;
                        txtMemo.Text = memoReturnPaid.ToString();   
                        ScriptManager.RegisterStartupScript(this, GetType(), " showAddModal", " showAddModal();", true);
                    }
                    
                }
                else if (e.CommandName == "DeleteItem")
                {
                    PurchaseReturnDetial prd = new PurchaseReturnDetial();  
                   
                    var check = prd.PurchaseReturnDetailSelectEdits(purchaseReturnNo);
                    
                    if (check != null) {

                        bool isDelete =  prd.PurchaseReturnDetailDeletes(purchaseReturnNo, billNo);
                        
                        if (isDelete)
                        {
                            ShowAlert("Delete purchase return is Successfully","success");

                            GridBind("", txtFromDate.Text, txtToDate.Text);
                        }
                        else
                        {
                            ShowAlert("Delete purchase return is failed.","danger");
                        }

                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string purchaseReturnNo = ViewState["PurchaseReturnNo"].ToString();

            PurchaseReturnDetial prd = new PurchaseReturnDetial();

            var load = prd.PurchaseReturnDetailSelectEdits(purchaseReturnNo);

            if (load != null)
            {
                PurchaseReturnDetailModel m = new PurchaseReturnDetailModel()
                {
                    BillNo = load.BillNo,
                    PurchaseReturnNo = purchaseReturnNo,
                    PaidAmount = txtPayAmount.Text.KinalDecimal(),   
                    DatePaid = txtDatePaid.Text.ConvertDateTime(),
                    MemoReturnPaid = txtMemo.Text,   
                };
               
                bool isUpdate =  prd.PurchaseReturnDetailUpdate(m);

                if (isUpdate)
                {
                    ShowAlert("Update payment is successfully.","success");

                    GridBind("", txtFromDate.Text, txtToDate.Text);

                    ViewState["PurchaseReturnNo"] = null;
                }
                {
                    ShowAlert("Failed for update payment.","danger");
                }             
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text, txtFromDate.Text, txtToDate.Text);
        }
    }
}