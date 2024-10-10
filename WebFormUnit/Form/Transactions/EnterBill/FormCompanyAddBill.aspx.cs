using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Category;
using UnitLabrary.Item;
using UnitLabrary.Transaction.Purchases.CompanyExpense;
using UnitLabrary.Transaction;
using UnitLabrary.CustomFunction;
using System.Runtime.Remoting.Messaging;
using Microsoft.Ajax.Utilities;
using UnitLabrary.Transaction.Purchases.EnterBill;
using UnitLabrary.Transaction.Purchases.CompanyExpenses;

namespace WebFormUnit.Form.Transactions.EnterBill
{
    public partial class FormCompanyAddBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hdfDisplayTotalAmount.Value = totalAmount.ToString();

            lbTotalAmountDisplay.Text = hdfDisplayTotalAmount.Value;

            if (!IsPostBack)
            {
                LoadFieldsFromEnterBill();
                LoadInventory();
                GridBind(hdfNumberNo.Value);
                LoadBillItem();
            }
        }

        private void LoadFieldsFromEnterBill()
        {
            if (Session["BillNumberNoFromEnterBill"] != null)
            {
                hdfNumberNo.Value = Session["BillNumberNoFromEnterBill"].ToString();
            }

            if (Session["SupplierCode"] != null)
            {
                hdfSupplierCode.Value = Session["SupplierCode"].ToString();
            }

            if (Session["StartDate"] != null)
            {
                hdfStartDate.Value = Session["StartDate"].ToString();
            }
            if (Session["ExpireDate"] != null)
            {
                hdfExpireDate.Value = Session["ExpireDate"].ToString();
            }

            if (Session["Reference"] != null)
            {
                hdfReference.Value = Session["Reference"].ToString();
            }

            if (Session["Memo"] != null)
            {
                hdfMemo.Value = Session["Memo"].ToString();
            }

            if (string.IsNullOrEmpty(Session["VatPercent"]?.ToString()))
            {
                hdfVatPercent.Value = "0.00";
            }
            else
            {
                hdfVatPercent.Value = Session["VatPercent"].ToString();
            }

            hdfDiscountPercent.Value = !string.IsNullOrEmpty(Session["DiscountPercent"]?.ToString()) ? Session["DiscountPercent"].ToString() : "0.00";
            hdfDiscountAmount.Value = !string.IsNullOrEmpty(Session["DiscountAmount"]?.ToString()) ? Session["DiscountAmount"].ToString() : "0.00";
        }
        private void LoadInventory()
        {
            LoadCategory();
            LoadItem(ddlCategory.SelectedValue);
        }
        private void LoadBillItem()
        {
            string billItemCode = Request.QueryString["BillItemCodeFromEnterBill"]; 

            if (string.IsNullOrEmpty(billItemCode))
            {
                return;
            }
            else
            {
                BillItem billItem = new BillItem();

                var load = billItem.BillItemSelectEdits(billItemCode);

                if (load != null)
                {
                    ddlCategory.SelectedValue = load.CategoryCode;

                    LoadItem(load.CategoryCode);

                    ddlItemCode.SelectedValue = load.ItemCode;

                    txtQuantity.Text = load.OrderQty.ToString("F0");
                    txtTotal.Text = (load.Cost * load.OrderQty).ToString("F2");
                    txtDiscountPercent.Text = load.DiscountPercent.Value.ToString("F2");
                    txtDiscountAmount.Text = load.Discount.Value.ToString("F2");
                    txtTotalDiscount.Text = load.TotalDiscount.Value.ToString("F2");
                    txtSubTotal.Text = load.Total.Value.ToString("F2");
                }
            }
        }
        private void LoadCategory()
        {
            Category category = new Category();
            var load = category.CategorySelects("");
            ddlCategory.DataSource = load;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryCode";
            ddlCategory.DataBind();
        }
        private void LoadItem(string categoryCode)
        {
            ItemList itemList = new ItemList();

            var load = itemList.ItemListSelects("", "ALL", categoryCode).Select(item => new
            {
                ItemCode = item.ItemCode,
                Name = item.ItemCode + "-" + item.PurDescription,
                Cost = item.Cost
            }).ToList();

            ddlItemCode.DataSource = load;
            ddlItemCode.DataTextField = "Name";
            ddlItemCode.DataValueField = "ItemCode";
            ddlItemCode.DataBind();

            if (ddlItemCode.Items.Count > 0)
            {
                string itemCode = ddlItemCode.SelectedValue;

                var selectedItem = load.FirstOrDefault(item => item.ItemCode == itemCode);

                if (selectedItem != null)
                {
                    txtCost.Text = selectedItem.Cost.ToString("F2");
                }

                var loadId = itemList.ItemListSelectEdits(itemCode);
                if (loadId != null)
                {
                    txtUnitStock.Text = loadId.UnitStockName;
                }
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItem(ddlCategory.SelectedValue);
        }
       
        private void GridBind(string BillNumberCode)
        {
            BillItem billItem = new BillItem();
            var load = billItem.BillItemSelects(BillNumberCode.Trim());
            if (load != null)
            {
                gvAddEnterBill.DataSource = load;
                gvAddEnterBill.DataBind();
                lbTotalAmountDisplay.Text = totalAmount.ToString();
                lbExpenseNoDisplay.Text = hdfNumberNo.Value;
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
            txtQuantity.Text = string.Empty;
            txtDiscountPercent.Text = string.Empty;
            txtDiscountAmount.Text = string.Empty;
            txtTotal.Text = string.Empty;
        }
        protected void btnSave_Click(object sender, EventArgs e)    
        {
            Session["IsAddButtonClicked"] = true;

            BillHeaderModel h = new BillHeaderModel()
            {
                BillNumber = hdfNumberNo.Value,
                DateBill = hdfStartDate.Value.ConvertDateTime(), //DateTime.UtcNow.AddHours(7),
                DueDateBill = hdfExpireDate.Value.ConvertDateTime(),
                VenderCode = hdfSupplierCode.Value,
                RefereceNo = hdfReference.Value,
                Memo = hdfMemo.Value,
                VatPercent = hdfVatPercent.Value.KinalDecimal(),
                VatAmount = hdfVatAmount.Value.KinalDecimal(),
                DiscountPercent = hdfDiscountPercent.Value.KinalDecimal(),
                DiscountAmount = hdfDiscountAmount.Value.KinalDecimal(),
                TotalDiscoutPercent = hdfTotalDiscountPercent.Value.KinalDecimal(),
                TotalDiscount = hdfTotalDiscount.Value.KinalDecimal(),
                SubTotal = hdfTotal.Value.KinalDecimal(),
                Indedted = true
            };

            BillItemModel i = new BillItemModel
            {
                BillItemCode = DateTime.Now.Ticks.ToString(),
                BillNumber = hdfNumberNo.Value,
                PurDescription = new ItemList().ItemListSelectEdits(ddlItemCode.SelectedValue).PurDescription.ToString(),
                OrderQty = txtQuantity.Text.KinalDecimal(),
                UnitBill = txtUnitStock.Text,
                Cost = txtCost.Text.KinalDecimal(),
                LocationCode = new ItemList().ItemListSelectEdits(ddlItemCode.SelectedValue).LocationCode.ToString(),
                DiscountPercent = txtDiscountPercent.Text.KinalDecimal(),
                DiscountAmount = txtDiscountAmount.Text.KinalDecimal(),
                TotalDiscount = 0m,
                CategoryCode = ddlCategory.SelectedValue,
                ItemCode = ddlItemCode.SelectedValue
            };


            BillTransactionPurchase purchase = new BillTransactionPurchase();

            //string billItemCode = string.Empty;

            //if (!string.IsNullOrEmpty(Request.QueryString["BillItemCodeFrom"]))
            //{
            //    billItemCode = Request.QueryString["BillItemCodeFrom"];
            //}
            //else if (ViewState["BillItemCode"] != null)
            //{
            //    billItemCode = ViewState["BillItemCode"].ToString();
            //}


            string billItemCode = Request.QueryString["BillItemCodeFromEnterBill"]
                      ?? ViewState["BillItemCode"]?.ToString()
                      ?? string.Empty;

            BillItem item = new BillItem();

            var check = item.BillItemSelectEdits(billItemCode);

            if ( check == null)
            {
                BillTransactionPurchase bill = new BillTransactionPurchase();

                bool isInsert = bill.PurchaseItemInsert(h, i);

                if (isInsert)
                {
                    ShowAlert("Insert purchase is successfully.", "success");

                    GridBind(hdfNumberNo.Value);

                    ClearFields();

                    //In this use when I add more Item to BillNo will update Unpaid and Paid

                    PurchaseReturnHeaderModel prm = new PurchaseReturnHeaderModel
                    {
                        BillNo = hdfNumberNo.Value,
                        PurchaseDateBill = hdfStartDate.Value.ConvertDateTime(),//DateTime.UtcNow.AddHours(7),
                        Unpaid = new BillHeader().BillHeaderSelectEdits(h)?.TotalHeadWithVat

                    };

                    PurchaseReturnTransaction prt = new PurchaseReturnTransaction();

                    bool isPrmInsert = prt.PurchaseReturnInsert(prm);

                    if (isPrmInsert) { }
                }
                else
                {
                    ShowAlert("Insert purchase is failed.", "danger");
                }
            }
            else
            {

                i.BillItemCode = billItemCode;

                bool isUpdate = purchase.PurchaseItemUpdate(i, h);

                if (isUpdate)
                {
                    //In this use when I add more Item to BillNo will update Unpaid and Paid

                    PurchaseReturnHeaderModel prm = new PurchaseReturnHeaderModel
                    {
                        BillNo = hdfNumberNo.Value,
                        PurchaseDateBill = DateTime.UtcNow.AddHours(7),
                        Unpaid = new BillHeader().BillHeaderSelectEdits(h)?.TotalHeadWithVat

                    };

                    PurchaseReturnTransaction prt = new PurchaseReturnTransaction();

                    bool isPrmInsert = prt.PurchaseReturnInsert(prm);

                    if (isPrmInsert) { } else return;

                    ShowAlert("Update purchase is successfully.", "success");
                    GridBind(hdfNumberNo.Value);
                    ClearFields();

                    ViewState["BillItemCode"] = null;

                    Response.Redirect($"~/Form/Transactions/EnterBill/FormCompanyAddBill?BillItemCodeFromEnterBill=");
                }
                else
                {
                    ShowAlert("Update purchase is failed.", "danger");
                }
               
               

            }
        }

        decimal totalAmount = 0;
        protected void gvAddEnterBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string totalColumn = e.Row.Cells[9].Text;

                decimal totalValue;

                if (Decimal.TryParse(totalColumn, System.Globalization.NumberStyles.Currency, null, out totalValue))
                {
                    totalAmount += totalValue;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "Total: " + totalAmount.ToString("C");
            }
        }

        protected void gvAddEnterBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {     
            if (e.CommandName == "EditItem" || e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                if (index < 0) { return; }
                else
                {
                    string billItemCode = gvAddEnterBill.DataKeys[index].Value.ToString();

                    ViewState["BillItemCode"] = billItemCode;

                    BillItem billItem = new BillItem();

                    if (e.CommandName == "EditItem")
                    {

                        var load = billItem.BillItemSelectEdits(billItemCode);

                        if (load != null)
                        {
                            ddlCategory.SelectedValue = load.CategoryCode;

                            LoadItem(load.CategoryCode);

                            ddlItemCode.SelectedValue = load.ItemCode;

                            txtQuantity.Text = load.OrderQty.ToString("F0");
                            txtTotal.Text = (load.Cost * load.OrderQty).ToString("F2");
                            txtDiscountPercent.Text = load.DiscountPercent.Value.ToString("F2");
                            txtDiscountAmount.Text = load.Discount.Value.ToString("F2");
                            txtTotalDiscount.Text = load.TotalDiscount.Value.ToString("F2");
                            txtSubTotal.Text = load.Total.Value.ToString("F2");
                        }
                    }
                    else if (e.CommandName == "DeleteItem")
                    {

                        var load = billItem.BillItemSelectEdits(billItemCode);

                        if (load != null)
                        {
                            BillHeaderModel h = new BillHeaderModel()
                            {
                                BillNumber = hdfNumberNo.Value,
                                DateBill = hdfStartDate.Value.ConvertDateTime(), //DateTime.UtcNow.AddHours(7),
                                DueDateBill = hdfExpireDate.Value.ConvertDateTime(), //DateTime.UtcNow.AddHours(7),
                                VenderCode = hdfSupplierCode.Value,
                                RefereceNo = hdfReference.Value,
                                Memo = hdfMemo.Value,
                                VatPercent = hdfVatPercent.Value.KinalDecimal(),
                                VatAmount = hdfVatAmount.Value.KinalDecimal(),
                                DiscountPercent = hdfDiscountPercent.Value.KinalDecimal(),
                                DiscountAmount = hdfDiscountAmount.Value.KinalDecimal(),
                                TotalDiscoutPercent = hdfTotalDiscountPercent.Value.KinalDecimal(),
                                TotalDiscount = hdfTotalDiscount.Value.KinalDecimal(),
                                SubTotal = hdfTotal.Value.KinalDecimal(),
                                Indedted = false
                            };

                            PurchaseReturnTransaction prt = new PurchaseReturnTransaction();

                            bool isDelete = prt.PurchaseReturnDetailDelete(billItemCode, true, h);

                            if (isDelete)
                            {
                                ShowAlert("Delete Inventory is successfully.", "success");
                                GridBind(hdfNumberNo.Value);

                                PurchaseReturnHeaderModel prm = new PurchaseReturnHeaderModel
                                {
                                    BillNo = hdfNumberNo.Value,
                                    PurchaseDateBill = DateTime.UtcNow.AddHours(7),
                                    Unpaid = new BillHeader().BillHeaderSelectEdits(h)?.TotalHeadWithVat

                                };

                                bool isPrmInsert = prt.PurchaseReturnInsert(prm);

                                if (isPrmInsert) { } else return;
                            }
                            else
                            {
                                ShowAlert("Delete Inventory is failed.", "danger");
                            }
                        }
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Session["IsAddButtonClicked"] != null && (bool)Session["IsAddButtonClicked"])
            {
                if (Session["BillNumberNoFromEnterBill"] != null)
                {
                    string BillNumberNoFrom = Session["BillNumberNoFromEnterBill"].ToString();

                    Response.Redirect($"~/Form/Transactions/EnterBill/FormCompanyEnterBill?BillNumberNoBackFromAddBill={BillNumberNoFrom}");
                }
                else
                {
                    Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyEnterBill");
                }
            }
            else
            { 
                Response.Redirect("~/Form/Transactions/EnterBill/FormCompanyEnterBill");
            }
        }
    }
}