using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary;
using UnitLabrary.Category;
using UnitLabrary.Item;
using UnitLabrary.SaleReceipts;
using UnitLabrary.Transaction;

namespace WebFormUnit.Form.Transactions.SaleInvoices
{
    public partial class FormSaleInvoiceAddItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string invoiceNo = Request.QueryString["InvoiceNoSaleInvoice"];

                ViewState["InvoiceNo"] = invoiceNo;

                lbDisplayInvoiceNo.Text = invoiceNo;

                GridBind(ViewState["InvoiceNo"].ToString());

                LoadFeilds();

                LoadInvoiceDetailInfo();
            }
        }
        private void GridBind(string InvoiceNo)
        {
            SaleReceiptInvoiceDetail srd = new SaleReceiptInvoiceDetail();

            var load = srd.SaleReceiptInvoiceDetailSelect(InvoiceNo);

            if (load != null)
            {
                gvSaleInvoiceDetail.DataSource = load;
                gvSaleInvoiceDetail.DataBind();
                lbDisplayAmountDue.Text = GrandTotalAmount.ToString("F2") + "$";
            }
        }

        private void LoadInvoiceDetailInfo()
        {
            lbDisplayAmountDue.Text = GrandTotalAmount.ToString("F2") + "$";
        }
        private void LoadFeilds()
        {
            LoadCategory();
            LoadItem(ddlItemCode.SelectedValue,ddlCategoryCode.SelectedValue);
        }

        private void LoadCategory()
        {
            Category category = new Category();
            var load = category.CategorySelects("");
            ddlCategoryCode.DataSource = load;
            ddlCategoryCode.DataTextField = "CategoryName";
            ddlCategoryCode.DataValueField = "CategoryCode";
            ddlCategoryCode.DataBind();
        }
        private void LoadItem(string itemCode, string categoryCode)
        {
            //Item
            ItemList itemList = new ItemList();
            var load = itemList.ItemListSelects("", "ALL", categoryCode).Select(item => new
            {
                ItemCode = item.ItemCode,
                SalePrice = item.SalePrice,
                ItemName = item.ItemCode + "-" + item.PurDescription
            });

            ddlItemCode.DataSource = load;
            ddlItemCode.DataTextField = "ItemName";
            ddlItemCode.DataValueField = "ItemCode";
            ddlItemCode.DataBind();

            if (ddlItemCode.Items.Count > 0)
            {
                var edit = load.FirstOrDefault(item => item.ItemCode == ddlItemCode.SelectedValue);

                if (edit != null)
                {
                    txtSalePrice.Text = edit.SalePrice.Value.ToString("F2");
                }
            }
            LoadUnitSale(itemCode);
        }
        private void LoadUnitSale(string itemCode)
        {
            UnitMeasurement unit = new UnitMeasurement();

            var loadUnit = unit.SelectUnitMeasurement(itemCode);

            ddlUnitSale.DataSource = loadUnit;
            ddlUnitSale.DataTextField = "UnitToName";
            ddlUnitSale.DataValueField = "UnitTo";
            ddlUnitSale.DataBind();
        }
        protected void ddlCategoryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItem(ddlItemCode.SelectedValue, ddlCategoryCode.SelectedValue);
        }
        private void LoadSalePrice(string itemCode)
        {
            ItemList itemList = new ItemList();

            var load = itemList.ItemListSelects("", "ALL", ddlCategoryCode.SelectedValue);

            var edit = load.FirstOrDefault(item => item.ItemCode == itemCode);

            if (edit != null)
            {
                txtSalePrice.Text = edit.SalePrice.Value.ToString("F2");
            }
        }
        protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalePrice(ddlItemCode.SelectedValue);
            LoadUnitSale(ddlItemCode.SelectedValue);
        }
        private void LoadItemInventory(string invoiceCode)
        {
            SaleReceiptInvoiceDetail srd = new SaleReceiptInvoiceDetail();

            var check = srd.SaleReceiptInvoiceDetailSelectEdit(invoiceCode);

            if (check != null)
            {
                ddlCategoryCode.SelectedValue = check.CategoryCode.ToString();
                ddlItemCode.SelectedValue = check.ItemCode.ToString();
                txtSalePrice.Text = check.SalePrice.ToString("F2");
                txtQuantity.Text = check.Quantity.ToString("F0");
                ddlUnitSale.SelectedValue = check.SaleUnit.ToString();
                txtTotal.Text = check.Total.Value.ToString("F2");
                txtDiscountPercent.Text = check.DiscountPercent.Value.ToString("F2");
                txtDiscountAmount.Text = check.DiscountAmount.Value.ToString("F2");
                txtTotalDiscount.Text = check.TotalDiscount.Value.ToString("F2");
                txtGrandTotal.Text = check.Total.Value.ToString("F2");
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
        private void ClearField()
        {
            ddlCategoryCode.SelectedIndex = 0;
            ddlCategoryCode.SelectedIndex = 0;
            txtQuantity.Text = string.Empty;
            ddlUnitSale.SelectedIndex = 0;
            txtTotal.Text = string.Empty;
            txtDiscountPercent.Text = string.Empty;
            txtDiscountAmount.Text = string.Empty;
            txtTotalDiscount.Text = string.Empty;
            txtGrandTotal.Text = string.Empty;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaleReceiptInvoiceDetailModel m = new SaleReceiptInvoiceDetailModel()
            {
                InvoiceCode = DateTime.Now.Ticks.ToString(),
                InvoiceNo = ViewState["InvoiceNo"].ToString(),
                ItemCode = ddlItemCode.SelectedValue,
                Quantity = txtQuantity.Text.KinalDecimal(),
                SaleUnit = ddlUnitSale.SelectedValue,
                SalePrice = txtSalePrice.Text.KinalDecimal(),
                DiscountAmount = txtDiscountAmount.Text.KinalDecimal(),
                DiscountPercent = txtDiscountPercent.Text.KinalDecimal(),
                locationCode = new ItemList().ItemListSelectEdits(ddlItemCode.SelectedValue).LocationCode.ToString()
            };
            
            SaleReceiptInvoiceDetail srvd = new SaleReceiptInvoiceDetail();

            if (string.IsNullOrEmpty(hdfInvoiceCode.Value))
            {
                bool isInsert = srvd.SaleReceiptInvoiceDetailInsert(m);

                if (isInsert)
                {
                    ShowAlert("Insert Item is successfully.", "success");

                    GridBind(ViewState["InvoiceNo"].ToString());

                    ClearField();
                }
                else
                {
                    ShowAlert("Insert Item is failed.", "danger");
                }
            }
            else
            {
                m.InvoiceCode = hdfInvoiceCode.Value;   

                bool isUpdate = srvd.SaleReceiptInvoiceDetailUpate(m);

                if (isUpdate)
                {
                    ShowAlert("Update Item is successfully.", "success");

                    hdfInvoiceCode.Value = null;

                    GridBind(ViewState["InvoiceNo"].ToString());

                    ClearField();
                }
                else
                {
                    ShowAlert("Update Item is failed.", "danger");
                }
            }
           
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Transactions/SaleInvoices/FormSaleInvoice");
        }

        private decimal GrandTotalAmount;
        protected void gvSaleInvoiceDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string totalColumn = e.Row.Cells[9].Text;

                GrandTotalAmount += Convert.ToDecimal(totalColumn);
            }
        }

        protected void gvSaleInvoiceDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem" || e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string invoiceCode = gvSaleInvoiceDetail.DataKeys[index].Value.ToString();

                hdfInvoiceCode.Value = invoiceCode; 

                if (e.CommandName == "EditItem")
                {
                    LoadItemInventory(invoiceCode);
                }
            }
        }
    }
}