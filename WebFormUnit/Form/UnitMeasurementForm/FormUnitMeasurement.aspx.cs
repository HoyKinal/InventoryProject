using System;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary;

namespace WebFormUnit.Form.UnitMeasurementForm
{
    public partial class FormUnitMeasurement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUnitMain();
                GridBind(string.Empty);
            }
        }

        private void GridBind(string searchQuery)
        {
            UnitMeasurement unitMeasurement = new UnitMeasurement();
            var unit = unitMeasurement.SelectUnitMeasurement(searchQuery);
            gvUnitMeasurement.DataSource = unit;
            gvUnitMeasurement.DataBind();
        }

        private void ClearField()
        {
            ddlUnitFrom.SelectedIndex = 0;
            ddlUnitTo.SelectedIndex = 0;
            txtUnitFromDescription.Text = string.Empty;
            ddlOperator.SelectedIndex = 0;
            txtUnitFactor.Text = string.Empty;
            txtSearch.Text = string.Empty;
        }

        private void LoadUnitMain()
        {
            var unitMain = new UnitMain();
            var unit = unitMain.SelectUnits("");

            // Binding dropdowns for unit selection
            ddlUnitFrom.DataSource = unit;
            ddlUnitFrom.DataTextField = "UnitName";
            ddlUnitFrom.DataValueField = "UnitCode";
            ddlUnitFrom.DataBind();

            ddlUnitTo.DataSource = unit;
            ddlUnitTo.DataTextField = "UnitName";
            ddlUnitTo.DataValueField = "UnitCode";
            ddlUnitTo.DataBind();

            ddlEditUnitFromName.DataSource = unit;
            ddlEditUnitFromName.DataTextField = "UnitName";
            ddlEditUnitFromName.DataValueField = "UnitCode";
            ddlEditUnitFromName.DataBind();

            ddlEditUnitToName.DataSource = unit;
            ddlEditUnitToName.DataTextField = "UnitName";
            ddlEditUnitToName.DataValueField = "UnitCode";
            ddlEditUnitToName.DataBind();
        }

        protected void InsertUnitMeasurement()
        {
            var unitMeasurement = new UnitMeasurement();

            string unitFrom = ddlUnitFrom.SelectedValue;
            string unitTo = ddlUnitTo.SelectedValue;
            string unitFromDesc = txtUnitFromDescription.Text;

            if (!decimal.TryParse(txtUnitFactor.Text, out decimal unitFactor))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid Conversion Factor');", true);
                return;
            }

            if (!char.TryParse(ddlOperator.SelectedValue, out char unitOperator))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid Operator');", true);
                return;
            }

            try
            {
                var existingMeasurement = unitMeasurement.SelectUnitMeasurementEdit(unitFrom, unitTo);

                if (existingMeasurement == null)
                {
                    bool isInserted = unitMeasurement.InsertUnitMeasurement(unitFrom, unitFromDesc, unitTo, "", unitOperator, unitFactor, User.Identity.Name, DateTime.Now, User.Identity.Name, DateTime.Now);

                    if (isInserted)
                    {
                      //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Insert Successfully');", true);
                        ClearField();
                        GridBind(string.Empty);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Your insert is duplicate, please choose another.');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }

        protected void UpdateUnitMeasurement()
        {
            var unitMeasurement = new UnitMeasurement();

            string unitFrom = ddlEditUnitFromName.SelectedValue;
            string unitTo = ddlEditUnitToName.SelectedValue;
           
            //convert dropdown value to text
            //string unitFromName = ddlEditUnitFromName.SelectedItem.Text;
            //string unitToName = ddlEditUnitToName.SelectedItem.Text;

            string unitFromDesc = txtEditUnitFromDesc.Text;
            char unitOperator = char.Parse(ddlEditOperator.SelectedValue);
            decimal unitFactor = decimal.Parse(txtEditUnitFactor.Text);

            try
            {
                // Update the UnitMeasurement table with the potentially swapped values
                bool isUpdated = unitMeasurement.UnitMeasurementUpdate(unitFrom,unitFromDesc,unitTo,"",unitOperator,unitFactor,"admin",DateTime.Now);

                if (isUpdated)
                {
                    GridBind(string.Empty);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update Successfully');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update Failed');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        } 


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text.Trim());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUnitMeasurement();
            }
        }

        protected void gvUnitMeasurement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArg = e.CommandArgument.ToString().Split('|');
            string unitFrom = commandArg[0];
            string unitTo = commandArg[1];

            if (e.CommandName == "EditMeasurement")
            {
                var unitMeasurement = new UnitMeasurement();

                var checkUnitMeasurement = unitMeasurement.SelectUnitMeasurementEdit(unitFrom, unitTo);

                if (checkUnitMeasurement != null)
                {
                    ddlEditUnitFromName.SelectedValue = checkUnitMeasurement.UnitFrom;
                    ddlEditUnitToName.SelectedValue = checkUnitMeasurement.UnitTo;
                    txtEditUnitFromDesc.Text = checkUnitMeasurement.UnitFromDesc;
                    txtEditUnitFactor.Text = Convert.ToString(checkUnitMeasurement.UnitFactor);
                    ddlEditOperator.SelectedValue = Convert.ToString(checkUnitMeasurement.UnitOperator);

                    ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", "showEditModal();", true);
                }
            }
            else if (e.CommandName == "DeleteMeasurement")
            {
                hiddenFieldUnitFrom.Value = unitFrom;
                hiddenFieldUnitTo.Value = unitTo;

                var unitMeasurement = new UnitMeasurement();
                var unitMain = new UnitMain();
                var selectUnit = unitMeasurement.SelectUnitMeasurementEdit(unitFrom, unitTo);

                if (selectUnit != null)
                {
                    var unitFromUnitMain = unitMain.SelectUnitForEdit(unitFrom);
                    var unitToUnitMain = unitMain.SelectUnitForEdit(unitTo);

                    txtDeleteUnitFrom.Text = unitFromUnitMain.UnitName;
                    txtDeleteUnitTo.Text = unitToUnitMain.UnitName;
                    txtDeleteDesc.Text = selectUnit.UnitFromDesc;
                    txtDeleteFactor.Text = Convert.ToString(selectUnit.UnitFactor);
                    ddlDeleteOperator.SelectedValue = Convert.ToString(selectUnit.UnitOperator);

                    ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteModal", "showDeleteModal();", true);
                }
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateUnitMeasurement();
            ClearField();
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlertEditModal", "showAlertEditModal();", true);
            GridBind(string.Empty);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var unitMeasurement = new UnitMeasurement();
            unitMeasurement.DeleteMeasurement(hiddenFieldUnitFrom.Value, hiddenFieldUnitTo.Value);
            ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteAlert", "showDeleteAlert();", true);
            GridBind(string.Empty);
        }

        protected void cvUnitFactor_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = args.Value.Length == 9;
        }
    }
}
