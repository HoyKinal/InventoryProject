using System;
using System.Web.UI;
using UnitLabrary;
using UnitLabrary.Category;


namespace WebFormUnit.Form.CategoryFrom
{
    public partial class FormAddCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDateModified.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }
        private void ClearFields()
        {
            txtCategoryCode.Text = string.Empty;
            txtCategoryName.Text = string.Empty;
            txtDateModified.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
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

        private void InsertCategory()
        {
            // Input validation
            if (!string.IsNullOrWhiteSpace(txtCategoryCode.Text) ||
                !string.IsNullOrWhiteSpace(txtCategoryName.Text) ||
                !string.IsNullOrWhiteSpace(ddlStatus.SelectedValue) ||
                !string.IsNullOrWhiteSpace(txtDateModified.Text))
            {
                // Validate category status
                if (!char.TryParse(ddlStatus.SelectedValue, out char categoryStatus))
                {
                    ShowAlert("Invalid category status", "danger");
                    return;
                }


                // Define an array of acceptable date formats
                string[] formats = new string[]
                {
                   "yyyy-MM-dd",     // ISO format (recommended for SQL Server)
                   "MM/dd/yyyy",     // Common US format
                   "dd/MM/yyyy",     // Common European format
                   "yyyyMMdd",       // Compact format
                   "yyyy-MM-dd HH:mm:ss", // ISO format with time
                   "MM/dd/yyyy HH:mm:ss", // US format with time
                   "dd/MM/yyyy HH:mm:ss", // European format with time
                };

                // Attempt to parse the date with any of the provided formats
                if (!DateTime.TryParseExact(txtDateModified.Text, formats,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None,
                                            out DateTime dateModified))
                {
                    ShowAlert("Invalid date format", "danger");
                    return;
                }

                string categoryCode = txtCategoryCode.Text;
                string categoryName = txtCategoryName.Text;
                string createBy = "admin";
                DateTime createDate = DateTime.Now;
                DateTime modifyDate = dateModified; 
                string modifyBy = "admin";
                string ana1 = "";
                string ana2 = DateTime.Now.Ticks.ToString();
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

                if (checkExisting == null)
                {
                    bool isInsert = category.CategoryInserts(categoryCode, categoryName, categoryStatus, createBy, createDate, modifyBy, modifyDate, ana1, ana2, ana3, ana4, ana5, ana6, ana7, ana8, ana9, ana10);

                    if (isInsert)
                    {
                        ShowAlert("Category item inserted successfully", "success");
                        ClearFields();
                    }
                    else
                    {
                        ShowAlert("Error inserting category. Please contact the developer", "danger");
                    }
                }
                else
                {
                    ShowAlert("Category already exists", "danger");
                }
            }
            else
            {
                ShowAlert("Please fill in all required fields", "danger");
                return;
            }            
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            InsertCategory();
            Response.Redirect("~/Form/CategoryFrom/FormCategory");
        }

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            InsertCategory();
            txtDateModified.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/CategoryFrom/FormCategory");
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "addGroupCategory", "$('#modalAddGroup').modal('show');", true);
            ScriptManager.RegisterStartupScript(this,GetType(), "showGroupCategory", "showGroupCategory();", true);
        }

        protected void btnAddCG_Click(object sender, EventArgs e)
        {

        }

    }
}
