using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.CustomFunction;
using UnitLabrary.Transaction;
using UnitLabrary.Transaction.Purchases.CompanyExpense;
using UnitLabrary.Transaction.Purchases.CompanyExpenses;
using UnitLabrary.Transaction.Purchases.EnterBill;
using UnitLabrary.Transaction.Suppliers;

namespace WebFormUnit.Form.Transactions.EnterBill
{
    public partial class FormCompanyPayBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSupplier();
                GridBind(ddlSupplier.SelectedValue);
                txtDatePaid.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
            }
        }
        private void GridBind(string supplierCode)
        {
           PurchaseReturnHeader prh = new PurchaseReturnHeader();   

            var load = prh.PurchaseReturnHeaderResultSelect(supplierCode);

            if (load != null)
            {
                gvPayBillHeader.DataSource = load;  
                gvPayBillHeader.DataBind(); 
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyEnterBill");
        }
        private void LoadSupplier()
        {
            var supplier = new Supplier();
            var load = supplier.SuppliersSelects("");

            if (load != null)
            {
                ddlSupplier.DataSource = load;
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "SupplierCode";
                ddlSupplier.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(ddlSupplier.SelectedValue);
        }

        protected void gvPayBillHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);   
           
            string billNo = gvPayBillHeader.DataKeys[index].Value.ToString();  

            hdfBillNumber.Value = billNo;   
           
            if (e.CommandName == "PayItem")
            {
                PurchaseReturnHeader prh = new PurchaseReturnHeader();

                var load = prh.PurchaseReturnHeaderSelectResultEdit(billNo);
                 
                if (load != null)
                {
                    txtBillNo.Text = load.BillNo;
                    txtDateBill.Text = load.PurchaseDateBill.ToString("dd/MM/yyyy");
                    txtUnpaidAmount.Text = load.Unpaid.Value.ToString("F2"); ;
                    txtDatePaid.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");

                    ScriptManager.RegisterStartupScript(this, GetType(), "showAddModal", "showAddModal();", true);
                }
            }
            else if (e.CommandName == "OpenItem")
            {
                Response.Redirect($"~/Form/Transactions/EnterBill/FormCompanyEnterBill?BillNumberFromOpenPayBill={Server.UrlEncode(billNo)}");
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
        private void ClearFields()
        {
            txtDatePaid.Text = DateTime.UtcNow.AddHours(7).ToString();
            txtPayAmount.Text = string.Empty;   
            txtMemo.Text = string.Empty;    
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PurchaseReturnDetailModel prdm = new PurchaseReturnDetailModel()
            {
                PurchaseReturnNo = DateTime.Now.Ticks.ToString(),
                BillNo = hdfBillNumber.Value,
                DatePaid = txtDatePaid.Text.ConvertDateTime(), //DateTime.Now.AddDays(20),
                PaidAmount = txtPayAmount.Text.KinalDecimal(),
                MemoReturnPaid = txtMemo.Text
            };

            BillHeaderModel billHeader = new BillHeaderModel();

            billHeader.BillNumber = hdfBillNumber.Value;

            PurchaseReturnHeaderModel prhm = new PurchaseReturnHeaderModel
            {
                BillNo = hdfBillNumber.Value,
                PurchaseDateBill = txtDatePaid.Text.ConvertDateTime()
            };

            PurchaseReturnTransaction prt = new PurchaseReturnTransaction();

            if (hdfBillNumber.Value !=null)
            {
                //Use for Insert PaidAmount to ReturnDetail and Update Return Header Purchase

                bool isInsert = prt.PurchaseReturnDetailInsert(prdm, prhm);

                if (isInsert)
                {
                    ShowAlert("Insert Amount is successfully.", "success");
                    GridBind(ddlSupplier.SelectedValue);
                    ClearFields();
                }
            }
            else
            {
                return;
            }          
        }
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyPayBillList");
        }
    }
}