using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WebFormUnit.Form.UnitForm
{
    public partial class FormUnitMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridBind("");
            }
        }

        //Clear Fileds
        private void ClearFields()
        {
            // txtUnitCode.Value = string.Empty;
            txtNameTextBox.Text = string.Empty;
            ddlStatusDropdown.SelectedIndex = 0;

        }
        private void GridBind(string search)
        {
            UnitMain unitMain = new UnitMain();
            var displayUnit = unitMain.SelectUnits(search);

            // Check if the list is empty
            if (displayUnit != null && displayUnit.Any())
            {
                gvUnitMain.DataSource = displayUnit;
                gvUnitMain.DataBind();
            }
            else
            {
                // Optionally, clear the GridView or handle the empty state
                gvUnitMain.DataSource = null;
                gvUnitMain.DataBind();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UnitMain unitMain = new UnitMain();

            string unitCode = txtUnitCode.Value;
            string unitName = txtNameTextBox.Text;
            bool unitStatus = ddlStatusDropdown.SelectedValue == "True";

            // Check if we are updating an existing unit
            if (!string.IsNullOrEmpty(unitCode))
            {
                // Update the existing unit
                bool isUpdate = unitMain.UpdateUnit(unitCode, unitName, unitStatus, true, "admin", DateTime.Now, "admin", DateTime.Now);

                if (isUpdate)
                {
                    GridBind("");
                    ClearFields();

                    // Alert success message for update
                    string script = @"
                        var alertDiv = document.createElement('div');
                        alertDiv.className = 'alert alert-success';
                        alertDiv.role = 'alert';
                        alertDiv.innerHTML = 'Record updated successfully.';
                        document.body.insertBefore(alertDiv, document.body.firstChild);

                        // Hide the alert after 1 seconds
                        setTimeout(function() {
                            alertDiv.style.display = 'none';
                        }, 1000);
                    ";

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
                else
                {
                    string script = @"
                        var Div = document.createElement('div');
                        Div.className = 'alert alert-danger';
                        Div.role = 'alert';
                        Div.innerHTML = 'Record is not updating, please to developer';
                        document.body.insertBefore(Div, document.body.firstChild);

                        //Hide alert after 1 second
                        setTimeout(function(){Div.remove();},1000);
                    ";
                    ClientScript.RegisterStartupScript(this.GetType(),"alert",script,true);
                }
            }
            else
            {
                // Insert a new unit
                bool isInserted = unitMain.InsertUnit(DateTime.Now.Ticks.ToString(), unitName, unitStatus, true, "admin", DateTime.Now, "admin", DateTime.Now);

                if (isInserted)
                {
                    GridBind("");
                    ClearFields();

                    // Show success message for insert
                    string script = @"
                        var alertDiv = document.createElement('div');
                        alertDiv.className = 'alert alert-success';
                        alertDiv.role = 'alert';
                        alertDiv.innerHTML = 'Record inserted successfully.';
                        document.body.insertBefore(alertDiv, document.body.firstChild);

                        // Hide the alert after 1 seconds
                        setTimeout(function() {
                            alertDiv.style.display = 'none';
                            alertDiv.remove();
                        }, 1000);
                    ";

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    /*
                      <div class="alert alert-success" role="alert">
                           Record inserted successfully.
                     </div>

                    <script>
                        // Automatically hide the alert after 1 second
                        setTimeout(function() {
                            var alertDiv = document.querySelector('.alert');
                            if (alertDiv) {
                                alertDiv.style.display = 'none';
                            }
                        }, 1000);
                    </script>
                     */
                }
                else
                {

                    string script = @"
                        var Div = document.createElement('div');
                        Div.className = 'alert alert-danger';
                        Div.role = 'alert';
                        Div.innerHTML = 'Record is not Inserting, please to developer';
                        document.body.insertBefore(Div, document.body.firstChild);

                        //Hide alert after 1 second
                        setTimeout(function(){Div.remove();},1000);
                    ";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text.Trim());
        }

        protected void gvUnitMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvUnitMain.SelectedRow;
           
            if (row != null)
            {
                
            }
        }

        protected void gvUnitMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UnitMain unitMain = new UnitMain();

            string unitCode = e.CommandArgument.ToString();

            if (e.CommandName == "EditUnit")
            {
                var unit = unitMain.SelectUnitForEdit(unitCode); // Fetch unit details by unitCode

                if (unit != null)
                {
                    // Populate modal fields with unit details
                    txtUnitCode.Value = unit.UnitCode;
                    txtNameTextBox.Text = unit.UnitName;
                    ddlStatusDropdown.SelectedValue = unit.UnitSatus.ToString();

                    // Show the modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", "showEditModal();", true);
                }
            }
            else if (e.CommandName == "DeleteUnit")
            {
                hidenCodeFeild.Value = unitCode;
                var unit = unitMain.SelectUnitForEdit(unitCode); // Fetch unit details for the selected unit

                if (unit != null)
                {
                    // Populate the deletion modal fields
                    labelUnitID.Text = unit.UnitCode;
                    labelName.Text = unit.UnitName;
                    labelStatus.Text = unit.UnitSatus.ToString();

                    // Show the deletion modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteModal", "showDeleteModal();", true);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            string UnitCode = hidenCodeFeild.Value;
            var unitMain = new UnitMain();
            bool isDelete = unitMain.DeleteUnit(UnitCode);
          
            if (isDelete)
            {   
                ScriptManager.RegisterStartupScript(this, GetType(), "alertDeleteModal", "alertDeleteModal();", true);
                
                GridBind("");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(),"alert", "Record not yet for delete",true);
            }
        }
    }
}