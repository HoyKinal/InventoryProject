using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary;
using UnitLabrary.Category;

namespace WebFormUnit.Form.CategoryFrom
{
    public partial class FormCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridBind("");
                txtModifyDate.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }
        private void GridBind(string search)
        {
            Category category = new Category(); 
            var ct = category.CategorySelects(search);
            gvCategoryList.DataSource = ct;
            gvCategoryList.DataBind();  
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
        protected void btnAdd_Click(object sender, EventArgs e)
        { 
            Response.Redirect("~/Form/CategoryFrom/FormAddCategory");
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            foreach (GridViewRow row in gvCategoryList.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    chkSelect.Checked = chkSelectAll.Checked;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text.Trim());
        }

        protected void gvCategoryList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Category category = new Category();

            string categoryCode = e.CommandArgument.ToString();

            hdFieldCategoryCode.Value = categoryCode;
            hdFieldCategoryCode.Value = categoryCode;

            if (e.CommandName == "EditCategory")
            {
                var selectedCategory = category.CategoryTSelectEdits(categoryCode);

                if (selectedCategory != null)
                {
                    txtCategoryCode.Text = selectedCategory.CategoryCode;
                    txtCategoryName.Text = selectedCategory.CategoryName;
                    ddlCategoryStatus.SelectedValue = selectedCategory.CategoryStatus.ToString();
                    txtModifyDate.Text = selectedCategory.ModifyDate.HasValue? selectedCategory.ModifyDate.Value.ToString("MM/dd/yyyy HH:mm:ss") : string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", "showEditModal();", true);
                }
            }
            else if (e.CommandName == "DeleteCategory")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteModal", "showDeleteModal();", true);
            }
        }

        protected void gvCategoryList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategoryList.EditIndex = e.NewEditIndex;
            GridBind(txtSearch.Text.Trim());
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string formats = "MM/dd/yyyy HH:mm:ss";
         
            if (!DateTime.TryParseExact(txtModifyDate.Text, formats,
                                        System.Globalization.CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.None,
                                        out DateTime dateModified))
            {
                ShowAlert("Invalid date format", "danger");
                return;
            }

            string categoryCode = hdFieldCategoryCode.Value;

            string categoryName = txtCategoryName.Text;

            char categoySatus = char.Parse(ddlCategoryStatus.SelectedValue);

            string createBy = "admin";

            DateTime createDate = DateTime.Now;

            string modifyBy = "admin";

            string ana1 = "";

            string ana2 = "";

            string ana3 = null;

            string ana4 = "";

            decimal ana5 = 0;

            decimal ana6 = 0;

            decimal ana7 = 0;

            DateTime ana8 = DateTime.Now;

            DateTime ana9 = DateTime.Now;

            DateTime ana10 = DateTime.Now;

            Category category = new Category();

     
            var checkExisting = category.CategoryTSelectEdits(categoryCode);

            if (checkExisting != null)
            {
                bool isUpdate = category.CategoryUpdates(categoryCode, categoryName, categoySatus, modifyBy, createDate, createBy, dateModified, ana1, ana2,ana3, ana4,ana5,ana6,ana7,ana8,ana9,ana10);

                if (isUpdate)
                {
                    GridBind("");
                    ShowAlert("Category item updated successfully", "success");
                    txtModifyDate.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                }
                else
                {
                    ShowAlert("Error updating category. Please contact the developer", "danger");
                }
            }
            else
            {
                ShowAlert("Problem with update: Category not found", "danger");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Category category = new Category(); 
            string categoryCode = hdFieldCategoryCode.Value;    

            bool isDelete = category.CategoryDelete(categoryCode); 

            if (isDelete)
            {
                GridBind("");
             
                ShowAlert("Delete Item Category is successfully","success");
            }
            else
            {
                ShowAlert("Delete is conflite somthing wrong","danger");
            }
        }

        protected void btnSelectDelete_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            List<string> categoryCodes = new List<string>();

            foreach (GridViewRow row in gvCategoryList.Rows)
            {
                CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;

                // Check if CheckBox is found and checked
                if (chkSelect != null && chkSelect.Checked)
                {
                    // Ensure DataKeys is correctly configured and accessible
                    if (row.RowIndex >= 0 && row.RowIndex < gvCategoryList.DataKeys.Count)
                    {
                        string categoryCode = gvCategoryList.DataKeys[row.RowIndex].Value.ToString();
                        categoryCodes.Add(categoryCode);
                    }
                    else
                    {
                        // Handle cases where the index is out of bounds
                        ShowAlert("Error: Data key index is out of range.", "danger");
                        return;
                    }
                }
            }

            if (categoryCodes.Any())
            {
                bool isDeleteSuccess = category.CateogoryDeletes(categoryCodes);
               
                if (isDeleteSuccess)
                {
                    GridBind(""); 
                    ShowAlert("All selected items deleted successfully.", "success");
                }
                else
                {
                    ShowAlert("An error occurred while deleting items.", "danger");
                }
            }
            else
            {
                ShowAlert("No items selected for deletion.", "warning");
            }
        }

    }
}