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

namespace WebFormUnit.Form.Transactions.SaleReceipts
{
    public partial class FormSaleReceiptAddItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string invoiceCode = Request.QueryString["InvoiceCodeFromEditSaleReceipt"];
                
                string invoiceNo = Request.QueryString["InvoiceNoSaleReceipt"] 
                    ?? Request.QueryString["InvoiceNoFromEditSaleReceipt"] ??"";
                
                if (invoiceNo != null)
                {
                    ViewState["InvoiceNo"] = invoiceNo;
                }

                if(invoiceCode != null)
                {
                    ViewState["InvoiceCode"] = invoiceCode;
                   
                    LoadItemInventory(invoiceCode);
                }

                lbDisplayInvoiceNo.Text = invoiceNo;
                
                LoadInventory();

            }
        }
        private void LoadInventory()
        {
            LoadCategory();
            LoadItem(ddlItemCode.SelectedValue,ddlCategoryCode.SelectedValue);
            GridBind(ViewState["InvoiceNo"].ToString());          
        }
        private void LoadCategory()
        {
            //Category
            Category category = new Category();
            var load = category.CategorySelects("");
            ddlCategoryCode.DataSource = load;
            ddlCategoryCode.DataTextField = "CategoryName";
            ddlCategoryCode.DataValueField = "CategoryCode";
            ddlCategoryCode.DataBind(); 
        }
        private void LoadItem(string itemCode,string categoryCode)
        {
            //Item
            ItemList itemList = new ItemList();
            var load = itemList.ItemListSelects("", "ALL", categoryCode).Select(item => new
            {
                ItemCode = item.ItemCode,   
                SalePrice = item.SalePrice,                  
                ItemName = item.ItemCode +"-"+ item.PurDescription
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
        private void LoadUnitSale(string itemCode)
        {
            UnitMeasurement unit = new UnitMeasurement();

            var loadUnit = unit.SelectUnitMeasurement(itemCode);

            ddlUnitSale.DataSource = loadUnit;
            ddlUnitSale.DataTextField = "UnitToName";
            ddlUnitSale.DataValueField = "UnitTo";
            ddlUnitSale.DataBind();
        }
        
        private void GridBind(string InvoiceNo)
        {
            SaleReceiptInvoiceDetail srd = new SaleReceiptInvoiceDetail();
            
            var load = srd.SaleReceiptInvoiceDetailSelect(InvoiceNo);
           
            if (load != null)
            {
                gvSaleReceiptDetail.DataSource = load;  
                gvSaleReceiptDetail.DataBind();
                lbDisplayAmountDue.Text = GrandTotal.ToString() + "$";
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect($"~/Form/Transactions/SaleReceipts/FormSaleReceipt?InvoiceNoFromAddSaleReceipt={Server.UrlEncode(ViewState["InvoiceNo"].ToString())}");
        }

        protected void ddlCategoryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItem(ddlItemCode.SelectedValue,ddlCategoryCode.SelectedValue);
        }

        protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalePrice(ddlItemCode.SelectedValue);
            LoadUnitSale(ddlItemCode.SelectedValue);
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
            txtSubTotal.Text = string.Empty;    
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaleReceiptInvoiceDetailModel m = new SaleReceiptInvoiceDetailModel()
            {
                InvoiceCode = DateTime.Now.Ticks.ToString(),
                InvoiceNo = ViewState["InvoiceNo"].ToString(),    
                ItemCode = ddlItemCode.SelectedValue?? "drink-001",
                Quantity = txtQuantity.Text.KinalDecimal(),
                SaleUnit = ddlUnitSale.SelectedValue,  
                SalePrice = txtSalePrice.Text.KinalDecimal(),
                DiscountAmount = txtDiscountAmount.Text.KinalDecimal(),
                DiscountPercent = txtDiscountPercent.Text.KinalDecimal(),   
                locationCode = new ItemList().ItemListSelectEdits(ddlItemCode.SelectedValue?? "drink-001").LocationCode.ToString()
            };
           
            SaleReceiptInvoiceDetail srd = new SaleReceiptInvoiceDetail();

            //Take code from row command InvoiceCode
            string invoiceCode = ViewState["InvoiceCode"]?.ToString()??hdfInvoiceCode.Value??"";

            if (string.IsNullOrEmpty(invoiceCode))
            {
                bool isInsert = srd.SaleReceiptInvoiceDetailInsert(m);

                if (isInsert)
                {
                    ShowAlert("Insert Item is successfully.", "success");

                    GridBind(ViewState["InvoiceNo"].ToString());

                    ClearField();
                }
                else
                {
                    ShowAlert("Insert item is failed.", "danger");
                }
            }
            else
            {
                m.InvoiceCode = ViewState["InvoiceCode"]?.ToString() ?? hdfInvoiceCode.Value??"";

                bool isUpdate = srd.SaleReceiptInvoiceDetailUpate(m);

                if (isUpdate)
                {
                    ShowAlert("Update Item is successfully.", "success");

                    GridBind(ViewState["InvoiceNo"].ToString());

                    hdfInvoiceCode.Value = null;

                    ViewState["InvoiceCode"] = null;    

                    ClearField();
                }
                else
                {
                    ShowAlert("Update item is failed.", "danger");
                }
            }

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
                txtSubTotal.Text = check.Total.Value.ToString("F2");
            }
        }
        protected void gvSaleReceiptDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem" || e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string invoiceCode = gvSaleReceiptDetail.DataKeys[index]["InvoiceCode"].ToString();
                
               hdfInvoiceCode.Value = invoiceCode;

                if (e.CommandName == "EditItem")
                {
                    LoadItemInventory(invoiceCode);
                }
                else if (e.CommandName == "DeleteItem")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "DeleteModal", "DeleteModal();", true) ;
                }

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SaleReceiptInvoiceDetail srd = new SaleReceiptInvoiceDetail();    

            if (hdfInvoiceCode.Value != null)
            {
                bool isDelete = srd.SaleReceiptInvoiceDetailDelete(hdfInvoiceCode.Value);
                if (isDelete)
                {
                    ShowAlert("Delete item is successfully.","success");

                    GridBind(ViewState["InvoiceNo"].ToString());

                    hdfInvoiceCode.Value = null;    
                }
                else
                {
                    ShowAlert("Failed for delete this item.","danger");
                }
            }
        }

        private decimal GrandTotal;
        protected void gvSaleReceiptDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string totalColumn = e.Row.Cells[9].Text;

                GrandTotal += Convert.ToDecimal(totalColumn);  
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearField();
        }
    }
}