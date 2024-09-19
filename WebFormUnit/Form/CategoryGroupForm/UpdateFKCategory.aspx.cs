using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Category;

namespace WebFormUnit.Form.ItemListForm
{
    public partial class FormItemList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategoryCode();
                LoadCategoryGroupName();
                GridBind();
            }
        }

        private void GridBind()
        {
            var categoryGroup = new CategoryGroup();
            string categoryGroupId = Request.QueryString["categoryGroupID"];
           
            var loadCategoryGroup = categoryGroup.CategoryGroupItemsSelects(categoryGroupId);
            gvUpdateCategoryGroupID.DataSource = loadCategoryGroup;
            gvUpdateCategoryGroupID.DataBind();
        }

        private void LoadCategoryGroupName()
        {
            var categoryGroup = new CategoryGroup();

            string categoryGroupId = Request.QueryString["categoryGroupID"];

            if (string.IsNullOrEmpty(categoryGroupId))
            {
                ShowAlert("Category Group ID is missing.", "danger");
                return;
            }

            var category = categoryGroup.CategoryGroupSelectEdits(categoryGroupId);

            if (category != null)
            {
                labelNameGroupCategory.Text = category.CategoryGroupName;
                labelLink.Text = category.CategoryGroupName;
            }
            else
            {
                labelNameGroupCategory.Text = "Category not found";
            }
        }

        private void LoadCategoryCode()
        {
            var category = new Category();

            var categoryItems = category.CategorySelects(string.Empty);

            ddlCategoryItem.DataSource = categoryItems;
            ddlCategoryItem.DataTextField = "CategoryName";
            ddlCategoryItem.DataValueField = "CategoryCode";
            ddlCategoryItem.DataBind();

            //ddlCategoryItem.Items.Insert(0, new ListItem("Select CategoryType", ""));
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"
                var alertDiv = document.createElement('div');
                alertDiv.className = 'alert alert-{type}';
                alertDiv.role = 'alert';
                alertDiv.innerHTML = '{HttpUtility.JavaScriptStringEncode(message)}';
                document.body.insertBefore(alertDiv, document.body.firstChild);

                setTimeout(function() {{
                    alertDiv.style.display = 'none';
                    alertDiv.remove();
                }}, 2000);
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", script, true);
        }

        protected void InsertCategoryItem_Click(object sender, EventArgs e)
        {
            string categoryCode = ddlCategoryItem.SelectedValue;
            string categoryGroupId = Request.QueryString["categoryGroupID"];
            Category category = new Category();
            var load = category.CategoryTSelectEdits(categoryCode);
            string locationCode = load.Ana2.ToString();
            bool isUpdate = false;
            if (string.IsNullOrEmpty(categoryGroupId) || string.IsNullOrEmpty(categoryCode))
            {
                ShowAlert("Invalid selection or missing category group ID.", "danger");
                return;
            }

            var categoryGroup = new CategoryGroup();
            bool isUpdated = categoryGroup.CategoryGroupUpdateAtCategoryType(categoryCode, locationCode, categoryGroupId, isUpdate);

            if (isUpdated)
            {
                ShowAlert("Category Type has been successfully updated.", "success");
                GridBind();
            }
            else
            {
                ShowAlert("Error updating category type. Please contact the developer.", "danger");
            }
        }

        protected void gvUpdateCategoryGroupID_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string categoryCode = e.CommandArgument.ToString();
            string categoryGroupId = Request.QueryString["categoryGroupID"];
            Category category = new Category();    
      
            string locationCode = category.CategoryTSelectEdits(categoryCode).Ana2;
            bool isDelete = true;

            if (e.CommandName == "DeleteCategory")
            {
                if (string.IsNullOrEmpty(categoryGroupId) || string.IsNullOrEmpty(categoryCode))
                {
                    ShowAlert("Invalid selection or missing category group ID.", "danger");
                    return;
                }
                else
                {

                    var isSelect = category.CategoryTSelectEdits(categoryCode);

                    if (isSelect != null)
                    {
                        var categoryGroup = new CategoryGroup();

                        bool isDeletes = categoryGroup.CategoryGroupUpdateAtCategoryType(categoryCode, locationCode, null, isDelete);

                        if (isDeletes)
                        {
                            ShowAlert("Category Type has been delete successfully deleted.", "success");
                            GridBind();
                        }
                        else
                        {
                            ShowAlert("Error deleting category type. Please contact the developer.", "danger");
                        }
                    }
                }
            }
        }
    }
}
