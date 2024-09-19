using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Item;

namespace WebFormUnit.Form.ItemsForm
{
    public partial class FormItemProperty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridBindProperty("",null,null);
            }
        }
        private void GridBindProperty(string searchName ,string sortExpression,string sortDirection)
        {
            ItemProperty itemProperty = new ItemProperty();
            var load = itemProperty.ItemPropertySelects(searchName.Trim());
            if (load.Any() || load != null)
            {
                if (sortExpression != null)
                {
                    load = sortDirection == "ASC" ?
                         load.OrderBy(x =>
                         {
                             var value = x.GetType().GetProperty(sortExpression).GetValue(x);
                             return value ?? ""; 
                         }).ToList() :
                         load.OrderByDescending(x =>
                         {
                             var value = x.GetType().GetProperty(sortExpression).GetValue(x);
                             return value ?? ""; 
                         }).ToList();
                }
                gvItemProperty.DataSource = load;
                gvItemProperty.DataBind();
                gvItemProperty.PageIndex = 0;
            }
            else
            {
                gvItemProperty.DataSource = null;
                gvItemProperty.DataBind();
            }
        }
        private string GetSortDirection(string sortExpression)
        {
            if (ViewState["SortExpression"] != null && ViewState["SortExpression"].ToString() == sortExpression)
            {
                // Toggle sorting direction
                ViewState["SortDirection"] = ViewState["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
            }
            else
            {
                // Default sort direction for a new column
                ViewState["SortDirection"] = "ASC";
            }

            ViewState["SortExpression"] = sortExpression;
            return ViewState["SortDirection"].ToString();
        }
        protected void gvItemProperty_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = GetSortDirection(sortExpression);
            GridBindProperty("", sortExpression, sortDirection);
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
            txtSearch.Text = string.Empty;
            txtSearch.Focus();
            txtName.Text = string.Empty;
            txtOrder.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ItemProperty itemProperty = new ItemProperty();

            // Fetch form data
            string propertyId = hdfPropertyID.Value; 
            string propertyName = txtName.Text;
            int propertyOrder = int.Parse(txtOrder.Text);
            string locationCode = string.IsNullOrEmpty(hdfLocationCode.Value) ? Guid.NewGuid().ToString() : hdfLocationCode.Value; // Use hidden field or new GUID
            char propertyStatus = char.Parse(ddlStatus.SelectedValue);
            string createBy = "admin";
            DateTime createDate = DateTime.Now;
            DateTime dateModify = DateTime.Now;
            bool IsAsync = false;

            // Check if record exists by PropertyId
            bool recordExist = false;

            foreach (GridViewRow row in gvItemProperty.Rows)
            {
                // Assuming PropertyId is stored in the hidden field inside the GridView
                HiddenField hdfExistingId = (HiddenField)row.FindControl("hfPropertyId");

                // If the same PropertyId exists, set flag
                if (hdfExistingId != null && hdfExistingId.Value == propertyId)
                {
                    recordExist = true;
                    break;
                }
            }

            // After checking the GridView, decide what to do
            if (recordExist && !string.IsNullOrEmpty(propertyId))
            {
                // Update existing record
                bool isUpdate = itemProperty.ItemPropertyUpToDate(
                    propertyId,
                    propertyName,
                    propertyOrder,
                    locationCode,
                    propertyStatus,
                    createBy,
                    createDate,
                    "admin",
                    dateModify,
                    IsAsync
                );

                if (isUpdate)
                {
                    ShowAlert("Item property updated successfully.", "info");
                    GridBindProperty("", null, "");
                    ClearFields(); 
                }
                else
                {
                    ShowAlert("Failed to update item property.", "error");
                }
            }
            else
            {
                propertyId = DateTime.Now.Ticks.ToString();

                bool isInsert = itemProperty.ItemPropertyInserts(
                    propertyId,
                    propertyName,
                    propertyOrder,
                    locationCode,
                    propertyStatus,
                    createBy,
                    createDate,
                    dateModify
                );

                if (isInsert)
                {
                    ShowAlert("Item property inserted successfully.", "success");
                    GridBindProperty("", null, null);
                    ClearFields();
                }
                else
                {
                    ShowAlert("Failed to insert item property.", "error");
                }
            }
        }


        protected void gvItemProperty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItemProperty" || e.CommandName == "DeleteItemProperty")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //string propertyId = gvItemProperty.DataKeys[index].Value.ToString();
                //way1
                string propertyId = gvItemProperty.DataKeys[index]["PropertyId"].ToString();
                string locationCode = gvItemProperty.DataKeys[index]["LocationCode"].ToString();
                //way2
                GridViewRow row = gvItemProperty.Rows[index];
                HiddenField hdfProID = (HiddenField)row.FindControl("hfPropertyId");
                HiddenField hdfLoc = (HiddenField)row.FindControl("hfLocationCode");
                string propertyId1 = hdfProID.Value;
                string locationCode1 = hdfLoc.Value;

                hdfPropertyID.Value = propertyId;
                hdfLocationCode.Value = locationCode;

                if (e.CommandName == "EditItemProperty")
                {
                    ItemProperty itemProperty = new ItemProperty();
                    var load = itemProperty.ItemPropertySelectToEdit(propertyId);
                    if (load != null)
                    {
                        txtName.Text = load.PropertyName;
                        txtOrder.Text = load.PropertyOrder.ToString();
                        ddlStatus.SelectedValue = load.PropertyStatus.ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "loadAddModal", "loadAddModal();", true);

                }
                else if (e.CommandName == "DeleteItemProperty")
                {
                    // ItemProperty itemProperty = new ItemProperty();
                    // var load = itemProperty.ItemPropertySelectToEdit(propertyId);

                    ScriptManager.RegisterStartupScript(this, GetType(), "loadDeleteModal", "loadDeleteModal();", true);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBindProperty(txtSearch.Text,null,null);
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            ItemProperty itemProperty = new ItemProperty();

            // Get the property ID from the hidden field
            string propertyId = hdfPropertyID.Value;

            if (string.IsNullOrEmpty(propertyId))
            {
                ShowAlert("No property ID specified.", "danger");
                return;
            }

            // Check if the record exists in the GridView
            bool recordExist = false;

            foreach (GridViewRow row in gvItemProperty.Rows)
            {
                HiddenField hdfExistingId = (HiddenField)row.FindControl("hfPropertyId");

                if (hdfExistingId != null && hdfExistingId.Value == propertyId)
                {
                    recordExist = true;
                    break;
                }
            }

            if (recordExist)
            {
                bool isDelete = itemProperty.ItemPropertyDeletes(propertyId);
                if (isDelete)
                {
                    ShowAlert("Item Property was successfully deleted.", "success");
                    GridBindProperty("", null, null);
                }
                else
                {
                    ShowAlert("There was a problem deleting the item property. Contact the developer.", "danger");
                }
            }
            else
            {
                ShowAlert("Item Property not found.", "warning");
            }
        }

        protected void gvItemProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemProperty.PageIndex = e.NewPageIndex;
            GridBindProperty("", null, null);
        }

       
    }
}