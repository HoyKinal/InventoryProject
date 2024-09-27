using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Transaction;
using UnitLabrary.Transaction.Purchases.CompanyExpense;
using UnitLabrary.Transaction.Purchases.CompanyExpenses;
using UnitLabrary.Transaction.Suppliers;

namespace WebFormUnit.Form.Transactions.EnterBill
{
    public partial class FormCompanyEnterBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtBillNumberNo.Text =  GenerateRandomBarcode();
              
                LoadSupplier();


                string billNumber = Session["BillNumberFormEdit"] != null
                     ? Request.QueryString["billNumberFromEdit"]
                     : Request.QueryString["BillNumberNoBack"];

                if (!string.IsNullOrEmpty(billNumber))
                {
                    Session["BillNumberNoBack"] = billNumber;
                    hdfBillNumber.Value = billNumber;

                    GridBind(billNumber);
                    LoadBillHeader();
                    LoadItemInfo();
                }
                else
                {
                    GridBind("");
                }

            }
        }
        private void GridBind(string BillNumberCode)
        {
            BillItem billItem = new BillItem();
            var load = billItem.BillItemSelects(BillNumberCode.Trim());
            if (load != null)
            {
                gvEnterBill.DataSource = load;
                gvEnterBill.DataBind();
            }
        }
        protected void btnNewItem_Click(object sender, EventArgs e)
        {
            string randomBarcode = GenerateRandomBarcode();
            txtBillNumberNo.Text = randomBarcode;
            Thread.Sleep(2000); 
            Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyEnterBill");
        }
        private static string GenerateRandomString(int length)
        {
            const string chars = "0123456789";
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
            Session["BillNumberNoFrom"] = txtBillNumberNo.Text;
            Session["SupplierCode"] = ddlSupplier.SelectedValue;
            Session["StartDate"] = txtStartDate.Text;
            Session["ExpireDate"] = txtExpireDate.Text;
            Session["Reference"] = txtReference.Text;
            Session["Memo"] = txtMemo.Text;
            Session["VatPercent"] = txtVATPercent.Text;
            Session["VatAmount"] = txtVatAmount.Text;
            Session["DiscountPercent"] = txtDiscountPercent.Text;
            Session["DiscountAmount"] = txtDiscountAmount.Text;

            Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyAddBill");
        }

        private void LoadBillHeader()
        {
            if (Session["BillNumberNoBack"] == null)
            {
                return;
            }
           
            BillHeaderModel billHeaderModal = new BillHeaderModel() { BillNumber = hdfBillNumber.Value };

            BillHeader billHeader = new BillHeader();

            var load = billHeader.BillHeaderSelectEdits(billHeaderModal);

            if (load != null)
            {
                txtBillNumberNo.Text = load.BillNumber;
                ddlSupplier.SelectedValue = load.VenderCode;
                txtStartDate.Text = load.DateBill.ToString("dd/MM/yyyy");
                txtExpireDate.Text = load.DueDateBill?.ToString("dd/MM/yyyy");
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

        protected void gvEnterBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem" || e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string billItemCode = gvEnterBill.DataKeys[index].Value.ToString();

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

                            GridBind(Session["BillNumberNoBack"].ToString());

                        }
                        else
                        {
                            ShowAlert("Delete Inventory is failed.", "danger");
                        }
                    }
                }
                else if (e.CommandName == "EditItem")
                {
                    Session["BillItemCodeFromEdit"] = billItemCode;

                    Response.Redirect($"~/Form/Transactions/EnterBill/FormCompanyAddBill?BillItemCodeFrom={Server.UrlEncode(billItemCode)}");
                }
            }
        }
        protected void btnOpenItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyBillList");
        }

        protected void btnDeleteItem_Click(object sender, EventArgs e)
        {
            string billNumber = hdfBillNumber.Value;

            bool option = false;

            BillTransactionPurchase purchase = new BillTransactionPurchase();

            BillHeaderModel bh = new BillHeaderModel { BillNumber = billNumber };

            if (billNumber == "")
            {
                return;
            }

            bool isDelete = purchase.PurchaseItemDelete(billNumber, option, bh);

            if (isDelete)
            {
                GridBind(hdfBillNumber.Value);

                string message = "Delete bill number is successful.";

                string type = "success";

                string redirectUrl = ResolveUrl("~/Form/Transactions/EnterBill/FormCompanyEnterBill");

                int delay = 1000;

                ShowAlertAndRedirect(message, type, redirectUrl, delay);

            }
            else
            {
                ShowAlert("Delete billnumber is failed.", "danger");
            }
        }

        protected void btnSaveItem_Click(object sender, EventArgs e)
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
           
            if (check != null)
            {
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
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyPayBill");
        }
    }
}