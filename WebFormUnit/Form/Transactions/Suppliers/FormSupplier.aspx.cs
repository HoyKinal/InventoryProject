using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Transaction.Suppliers;


namespace WebFormUnit.Form.Transactions.Suppliers
{
    public partial class FormSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridBind("");
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
        private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
        {
            {"A","Active" },
            {"D","Disable" }
        };
        protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //value
                var statusKey = DataBinder.Eval(e.Row.DataItem, "SupplierStatus").ToString();

                //text
                var statusValue = keyValuePairs.ContainsKey(statusKey)
                    ? keyValuePairs[statusKey] : "Unknown Expense Account";
                //display
                e.Row.Cells[9].Text = statusValue;
            }
        }
        private void GridBind(string search)
        {
            Supplier s = new Supplier();
            var load = s.SuppliersSelects(search.Trim());
            gvSupplier.DataSource = load;
            gvSupplier.DataBind();
        }
        private void ClearFields()
        {
            txtSupplierCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNum.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtContact.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
        }

        protected void btnAddEdit_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier
            {
                SupplierCode = txtSupplierCode.Text,
                SupplierName = txtName.Text,
                SupplierDesc = txtDesc.Text,
                SupplierAddress = txtAddress.Text,
                SupplierPhone = txtPhoneNum.Text,
                SupplierFax = txtFax.Text,
                SupplierEmail = txtEmail.Text,
                SupplierWeb = txtWebsite.Text,
                SupplierContact = txtContact.Text,
                SupplierStatus = char.Parse(ddlStatus.SelectedValue),
                CreateBy = "admin",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            if (hdfSupplierCode.Value == "")
            {
                bool isInsert = supplier.SupplierInserts(supplier);

                if (isInsert)
                {
                    ShowAlert("Insert supplier info successfully.", "success");
                    GridBind("");
                    ClearFields();
                }
                else
                {
                    ShowAlert("Insert supplier info is failed.", "danger");
                }
            }
            else
            {
                bool isUpdate = supplier.SupplierUpdates(supplier);

                if (isUpdate)
                {
                    ShowAlert("Update supplier info successfully.", "success");
                    GridBind("");
                    ClearFields();
                    hdfSupplierCode.Value = "";
                }
                else
                {
                    ShowAlert("Update supplier info is failed.", "danger");
                }
            }    
        }

        protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItemSupplier" || e.CommandName == "DeleteItemSupplier")
            {
                int index = Convert.ToInt32(e.CommandArgument); 
                
                string supplierCode = gvSupplier.DataKeys[index].Value.ToString();
                
                hdfSupplierCode.Value = supplierCode;

                if (e.CommandName == "EditItemSupplier")
                {
                    Supplier s = new Supplier() { SupplierCode = supplierCode };

                    var loadById = s.SuppliersSelectEdits(s);
                    
                    if (loadById != null)
                    {
                        txtSupplierCode.Text = loadById.SupplierCode;
                        txtName.Text = loadById.SupplierName;
                        txtDesc.Text = loadById.SupplierDesc;
                        txtAddress.Text = loadById.SupplierAddress;
                        txtPhoneNum.Text = loadById.SupplierPhone;
                        txtFax.Text = loadById.SupplierFax;
                        txtEmail.Text = loadById.SupplierEmail;
                        txtWebsite.Text = loadById.SupplierWeb;  
                        txtContact.Text = loadById.SupplierContact;
                        ddlStatus.SelectedValue = loadById.SupplierStatus.ToString();
                    }
                    ScriptManager.RegisterStartupScript(this,GetType(), "showEditModal", "showEditModal();", true);
                }
                else if (e.CommandName == "DeleteItemSupplier")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteModal", "showDeleteModal();", true);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();

            supplier.SupplierCode = hdfSupplierCode.Value;

            bool isDelete = supplier.SupplierDeletes(supplier);

            if (isDelete)
            {
                ShowAlert("Delete supplier info successfully.", "info");
                GridBind("");
            }
            else
            {
                ShowAlert("Delete supplier info failed.", "warning");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text);
        }
    }
}