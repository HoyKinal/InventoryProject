using System;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Transaction;
using UnitLabrary.Transaction.Purchases.CompanyExpense;
using UnitLabrary.Transaction.Purchases.CompanyExpenses;
using UnitLabrary.Transaction.Suppliers;

namespace WebFormUnit.Form.Transactions.CompanyExpenses
{
    public partial class FormCompanyExpense : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSupplier();
                txtExpenseNo.Text = GenerateRandomBarcode();

                //if (Session["BillExpenseCode"] != null)
                //{
                //    hdfBillNumber.Value = Session["BillExpenseCode"].ToString();
                //}
                if (Session["BillNumberFormEdit"] != null)
                {
                    hdfBillNumber.Value = Request.QueryString["billNumberFromEdit"];
                }
                else
                {
                    if (Request.QueryString["BillNumber"] != null)
                    {
                        hdfBillNumber.Value = Request.QueryString["BillNumber"];
                    }
                }

                LoadBillHeader();

                GridBind(hdfBillNumber.Value);

                LoadItemInfo();
            }
        }

        private void LoadBillHeader()
        {
            BillHeaderModel billHeaderModal = new BillHeaderModel() { BillNumber = hdfBillNumber.Value};

            BillHeader billHeader = new BillHeader();

            var load = billHeader.BillHeaderSelectEdits(billHeaderModal);

            if (load != null)
            {
                txtExpenseNo.Text = load.BillNumber;
                ddlSupplier.SelectedValue = load.VenderCode;
                txtDate.Text = load.DateBill.ToString("dd/MM/yyyy");
                txtReference.Text = load.RefereceNo.ToString();
                txtMemo.Text = load.Memo;
                txtVATPercent.Text = load.VatPercent.Value.ToString("F2");
                txtVatAmount.Text = load.VATAmount.Value.ToString("F2");
                txtDiscountPercent.Text = load.DiscountPercent.Value.ToString("F2");
                txtDiscountAmount.Text = load.DiscountAmount.Value.ToString("F2");
                txtTotalDiscountPercent.Text = load.TotalDiscount.Value.ToString("F2");
                txtTotalDiscount.Text = load.TotalDiscount.Value.ToString("F2");
                lbDisplayGrandTotal.Text = load.TotalHeadWithVat.Value.ToString("F2");
                lbIncreaseItem.Text = load.TotalItem.Value.ToString("F2");
            }
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

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static string GenerateRandomBarcode()
        {
            string prefix = "HQ-";
            int randomPartLength = 8;
            string randomPart = GenerateRandomString(randomPartLength);
            return prefix + randomPart;
        }
        protected void btnNewItem_Click(object sender, EventArgs e)
        {
            string randomBarcode = GenerateRandomBarcode();
            txtExpenseNo.Text = randomBarcode;
            Session["BillNumberFormEdit"] = null;
            Response.Redirect("~/Form/Transactions/CompanyExpenses/FormCompanyExpense");
        }
       
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            // Store values in session
            Session["ExpenseNo"] = txtExpenseNo.Text;
            Session["SupplierCode"] = ddlSupplier.SelectedValue;
            Session["Date"] = txtDate.Text; 
            Session["Reference"] = txtReference.Text; 
            Session["Memo"] = txtMemo.Text;
            Session["VatPercent"] = txtVATPercent.Text;
            Session["VatAmount"] = txtVatAmount.Text;
            Session["DiscountPercent"] = txtDiscountPercent.Text;
            Session["DiscountAmount"] = txtDiscountAmount.Text;

            Response.Redirect("~/Form/Transactions/CompanyExpenses/FormAddExpense");
        }
        private void GridBind(string BillNumberCode)
        {
            BillItem billItem = new BillItem();
            var load = billItem.BillItemSelects(BillNumberCode.Trim());
            if (load != null)
            {
                gvAddExpense.DataSource = load;
                gvAddExpense.DataBind();
            }
        }
        private decimal totalAmount = 0;
        protected void gvAddExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string totalText = e.Row.Cells[9].Text;
                decimal totalValue;
                if (Decimal.TryParse(totalText, System.Globalization.NumberStyles.Currency, null, out totalValue))
                {
                    totalAmount += totalValue;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "Total: " + totalAmount.ToString("C");
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
            }}, {delay});"; // Delay in milliseconds

            ClientScript.RegisterStartupScript(this.GetType(), "showAlertAndRedirect", script, true);
        }

        protected void gvAddExpense_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem" || e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string billItemCode = gvAddExpense.DataKeys[index].Value.ToString(); 

                if (e.CommandName == "DeleteItem")
                {
                    BillItem billItem = new BillItem();

                    var load = billItem.BillItemSelectEdits(billItemCode);

                    bool option = true;

                    if (load != null)
                    {
                        bool isDelete = billItem.BillItemDeletes(billItemCode, option);

                        if (isDelete)
                        {
                            ShowAlert("Delete Inventory is successfully.", "success");
                            GridBind(hdfBillNumber.Value);
                        }
                        else
                        {
                            ShowAlert("Delete Inventory is failed.", "danger");
                        }
                    }
                }
                else if(e.CommandName == "EditItem")
                {
                    
                    Session["billItemCode"] = billItemCode;

                    Response.Redirect($"~/Form/Transactions/CompanyExpenses/FormAddExpense.aspx?billItemCode={Server.UrlEncode(billItemCode)}");
                    
                }
            }
        }

        protected void gvAddExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAddExpense.PageIndex = e.NewPageIndex;
            GridBind(hdfBillNumber.Value);
        }

        private void SaveItem()
        {
            BillHeaderModel h = new BillHeaderModel()
            {
                BillNumber = hdfBillNumber.Value,
                DateBill = DateTime.UtcNow.AddHours(7),
                DueDateBill = DateTime.UtcNow.AddHours(7),  
                VenderCode = ddlSupplier.SelectedValue,
                RefereceNo = txtReference.Text,
                Memo = txtMemo.Text,
                VatPercent = txtVATPercent.Text.KinalDecimal(),
                DiscountPercent = txtDiscountPercent.Text.KinalDecimal(),
                DiscountAmount = txtDiscountAmount.Text.KinalDecimal(),
            };

            BillHeader billHeader = new BillHeader();

            var check = billHeader.BillHeaderSelectEdits(h);

            bool isUpdate = billHeader.BillHeaderUpdate(h);

            if (isUpdate)
            {
                ShowAlert("Update BillHeader is successfully.", "success");
                LoadBillHeader();
                LoadItemInfo();
            }
            else
            {
                ShowAlert("Update BillHeader is failed.", "danger");
            }
        }
        protected void btnSaveItem_Click(object sender, EventArgs e)
        {
            SaveItem();
        }
        private void DeletePurchase()
        {
            string billNumber = hdfBillNumber.Value;

            bool option = false;

            BillTransactionPurchase purchase = new BillTransactionPurchase();

            BillHeaderModel bh = new BillHeaderModel { BillNumber =  billNumber };

            bool isDelete =  purchase.PurchaseItemDelete(billNumber, option, bh);

            if (isDelete)
            {
                
                string message = "Delete bill number is successful.";
               
                string type = "success";
               
                string redirectUrl = ResolveUrl("~/Form/Transactions/CompanyExpenses/FormCompanyExpense");

                int delay = 2000; 

                ShowAlertAndRedirect(message, type, redirectUrl, delay);
            }
            else
            {
                ShowAlert("Delete billnumber is failed.", "danger");
            }
        }
        protected void btnDeleteItem_Click(object sender, EventArgs e)
        {
            DeletePurchase();
            GridBind(hdfBillNumber.Value);
        }
        private void LoadItemInfo()
        {
            BillHeaderModel model = new BillHeaderModel() { BillNumber = hdfBillNumber.Value };
            BillHeader header = new BillHeader();

            var load = header.BillHeaderSelectEdits(model);

            if (load != null)
            {
                lbDiscount.Text = load.TotalDiscountItem.Value.ToString("F2");
                lbGrandTotalHeader.Text = load.GrandTotalHeader.Value.ToString("F2");
                lbAmount.Text = load.TotalItem.Value.ToString("F2");
                lbTotalDicount.Text = load.TotalDiscount.Value.ToString("F2");
                lbTotalVat.Text = load.VATAmount.Value.ToString("F2");
                lbGrandTotalVat.Text = load.TotalHeadWithVat.Value.ToString("F2");
            }
        }

        protected void btnOpenItem_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("~/Form/Transactions/CompanyExpenses/FormOpenExpense");
        }
    }
}