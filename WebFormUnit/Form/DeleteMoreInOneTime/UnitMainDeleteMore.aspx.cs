using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary;

namespace WebFormUnit.Form.DeleteMoreInOneTime
{
    public partial class UnitMainDeleteMore : System.Web.UI.Page
    {
        private string CurrentUserID => "admin"; // Replace with actual user ID retrieval logic

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridBind("");
            }
        }

        private void ClearFields()
        {
            txtNameTextBox.Text = string.Empty;
            ddlStatusDropdown.SelectedIndex = 0;
        }

        private void GridBind(string search)
        {
            UnitMain unitMain = new UnitMain();
            var displayUnit = unitMain.SelectUnits(search);

            if (displayUnit != null && displayUnit.Any())
            {
                gvUnitMain.DataSource = displayUnit;
                gvUnitMain.DataBind();
            }
            else
            {
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

            bool isSuccess = false;

            if (!string.IsNullOrEmpty(unitCode))
            {
                isSuccess = unitMain.UpdateUnit(unitCode, unitName, unitStatus, true, CurrentUserID, DateTime.Now, CurrentUserID, DateTime.Now);
            }
            else
            {
                isSuccess = unitMain.InsertUnit(DateTime.Now.Ticks.ToString(), unitName, unitStatus, true, CurrentUserID, DateTime.Now, CurrentUserID, DateTime.Now);
            }

            if (isSuccess)
            {
                GridBind("");
                ClearFields();
                ShowAlert("Record saved successfully.", "success");
            }
            else
            {
                ShowAlert("Record not saved, please contact the developer.", "danger");
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
                }}, 1000);
            ";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text.Trim());
        }

        protected void gvUnitMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UnitMain unitMain = new UnitMain();
            string unitCode = e.CommandArgument.ToString();

            if (e.CommandName == "EditUnit")
            {
                var unit = unitMain.SelectUnitForEdit(unitCode);

                if (unit != null)
                {
                    txtUnitCode.Value = unit.UnitCode;
                    txtNameTextBox.Text = unit.UnitName;
                    ddlStatusDropdown.SelectedValue = unit.UnitSatus.ToString();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", "showEditModal();", true);
                }
            }
            else if (e.CommandName == "DeleteUnit")
            {
                hidenCodeFeild.Value = unitCode;
                var unit = unitMain.SelectUnitForEdit(unitCode);

                if (unit != null)
                {
                    labelUnitID.Text = unit.UnitCode;
                    labelName.Text = unit.UnitName;
                    labelStatus.Text = unit.UnitSatus.ToString();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteModal", "showDeleteModal();", true);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string unitCode = hidenCodeFeild.Value;
            UnitMain unitMain = new UnitMain();

            bool isDeleted = unitMain.DeleteUnit(unitCode);

            if (isDeleted)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertDeleteModal", "alertDeleteModal();", true);
                GridBind("");
            }
            else
            {
                ShowAlert("Record not yet deleted", "danger");
            }
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            UnitMain unitMain = new UnitMain();
            List<string> selectedUnitCodes = new List<string>();

            foreach (GridViewRow row in gvUnitMain.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect"); //Find CheckBox for each ch
                if (chkSelect != null && chkSelect.Checked)
                {
                    string unitCode = gvUnitMain.DataKeys[row.RowIndex].Value.ToString();
                    selectedUnitCodes.Add(unitCode);
                }
            }

            if (selectedUnitCodes.Any())
            {
                bool isSuccess = unitMain.DeleteUnits(selectedUnitCodes, CurrentUserID);

                if (isSuccess)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertDeleteModal", "alertDeleteModal();", true);
                    GridBind("");
                }
                else
                {
                    ShowAlert("Some records could not be deleted, please contact the developer.", "danger");
                }
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            foreach (GridViewRow row in gvUnitMain.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    chkSelect.Checked = chkSelectAll.Checked;
                }
            }
        }
    }
}
