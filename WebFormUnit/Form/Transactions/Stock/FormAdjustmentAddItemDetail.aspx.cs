using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Category;
using UnitLabrary.Item;

namespace WebFormUnit.Form.Transactions.Stock
{
    public partial class FormAdjustmentItemDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInventory();
            }
        }

        private void LoadInventory()
        {
            LoadCategoryCode();
            ItemCode(ddlCategoryCode.SelectedValue);
        }
        private void LoadCategoryCode()
        {
            Category c = new Category();
            var load = c.CategorySelects("");
            ddlCategoryCode.DataSource = load;
            ddlCategoryCode.DataTextField = "CategoryName";
            ddlCategoryCode.DataValueField = "CategoryCode";
            ddlCategoryCode.DataBind(); 
        }
        private void ItemCode(string categoryCode)
        {
            ItemList itemList = new ItemList();
            var load = itemList.ItemListSelects("","ALL", categoryCode).Select(item => new
            {
                ItemCode = item.ItemCode,   
                ItemName = item.ItemCode + "-" + item.PurDescription
            });
            ddlItemCode.DataSource = load;
            ddlItemCode.DataTextField = "ItemName";
            ddlItemCode.DataValueField = "ItemCode";
            ddlItemCode.DataBind();
        }

        protected void ddlCategoryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemCode(ddlCategoryCode.SelectedValue);
        }
    }
}