using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Item;

namespace WebFormUnit.Form.ItemsForm
{
    public partial class FormCommissionType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridBind("",null,null);
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
        private void GridBind(string search, string sortExpression, string sortDirection)
        {
            ItemCommissionType itemCommissionType = new ItemCommissionType();
            var load = itemCommissionType.itemCommissionTypeSelects(search.Trim());
            if (load != null && load.Any())
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
                gvItemCommissiontype.DataSource = load;
                gvItemCommissiontype.DataBind();
                gvItemCommissiontype.PageIndex = 0;
            }
        }
        protected void gvItemCommissiontype_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = GetSortDirection(sortExpression);
            GridBind("", sortExpression, sortDirection);
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
            txtSearch.Text = string.Empty;  
            txtSearch.Focus();
            txtName.Text = string.Empty;
            ddlExpenseAccount.SelectedIndex = 0;
            ddlExpenseAccount.SelectedIndex =0;
            ddlStatus.SelectedIndex = 0;
        }
        protected void btnAddEdit_Click(object sender, EventArgs e)
        {
            ItemCommissionType itemCommissionType = new ItemCommissionType();
            string commissionTypeCode = DateTime.Now.Ticks.ToString();
            string locationCode = (DateTime.Now.Ticks + 1).ToString(); 
            string commissionTypeName = txtName.Text;
            bool commissionTypeStatus = ddlStatus.SelectedValue == "true";
            string payableAccount = ddlPayabelAccount.SelectedValue;
            string expenseAccount = ddlExpenseAccount.SelectedValue;

            if (string.IsNullOrEmpty(payableAccount) || string.IsNullOrEmpty(expenseAccount))
            {
                ShowAlert("Please select valid Payable and Expense accounts.", "danger");
                return;
            }
            bool isSync = true;
            string createBy = "admin";
            string modifiedBy = "admin";
            DateTime modifiedDate = DateTime.Now;
            DateTime createDate = DateTime.Now;


            string commissionCodeFromSession = Session["CommissionCode"] as string;
            bool isEditMode = !string.IsNullOrEmpty(commissionCodeFromSession);
            
            if (isEditMode)
            {
                var chkExist = itemCommissionType.ItemCommissionTypeSelectEdits(commissionCodeFromSession);
                if (chkExist != null)
                {
                    bool isUpdate = itemCommissionType.ItemCommissionTypeUpdates(commissionCodeFromSession, locationCode, commissionTypeName,
                    commissionTypeStatus, payableAccount, expenseAccount, isSync, createBy, createDate, modifiedBy, modifiedDate);

                    if (isUpdate)
                    {
                        ShowAlert("Commission type updated successfully.", "success");
                        ClearField();
                    }
                    else
                    {
                        ShowAlert("There was a problem updating the commission type. Please contact the developer.", "danger");
                    }
                }
            }
            else
            {
                bool isInsert = itemCommissionType.ItemCommissionTypeInserts(commissionTypeCode, locationCode,
                    commissionTypeName, commissionTypeStatus, payableAccount, expenseAccount, isSync, createBy, modifiedDate);

                if (isInsert)
                {
                    ShowAlert("Item commission type inserted successfully.", "success");
                    ClearField();
                }
                else
                {
                    ShowAlert("There was a problem inserting the item commission. Please contact the developer.", "danger");
                }
            }
            // Clear the session after processing
            Session["CommissionCode"] = null;

            // Re-bind grid to reflect changes
            GridBind("", null, null);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text,null,null);
        }

        protected void gvItemCommissiontype_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewItem" || e.CommandName == "EditItem" || e.CommandName== "DeleteItem")
            {
                string commissionCode = e.CommandArgument.ToString();
               
                Session["CommissionCode"]= commissionCode;
                

                if (e.CommandName == "EditItem")
                {
                    ItemCommissionType itemCommissionType = new ItemCommissionType();   

                    var chkExist = itemCommissionType.ItemCommissionTypeSelectEdits(commissionCode);

                    if (chkExist != null)
                    {
                        txtName.Text = chkExist.CommissionTypeName;
                        ddlStatus.SelectedValue = chkExist.CommissionTypeStatus.ToString();
                        ddlPayabelAccount.SelectedValue = chkExist.PayableAccount;
                        ddlExpenseAccount.SelectedValue = chkExist.ExpenseAccount;
                        ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", "showEditModal();", true);
                    }
                }else if (e.CommandName == "DeleteItem")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteModal", "showDeleteModal();", true);
                }else if (e.CommandName == "ViewItem")
                {
                    Response.Redirect($"~/Form/ItemsForm/FormItemCommission.aspx?CommissionTypeCode={Server.UrlEncode(commissionCode)}");

                }
            }
        }

        protected void btnCancelClear_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            ItemCommissionType itemCommissionType = new ItemCommissionType();
            string commissionCode = Session["CommissionCode"].ToString();
            var chkExist = itemCommissionType.ItemCommissionTypeSelectEdits(commissionCode);

            if (chkExist != null)
            {
                bool isDelete = itemCommissionType.ItemCommissionTypeDeletes(commissionCode);
                if (isDelete)
                {
                    ShowAlert("Delete items is successfully." ,"success");
                    GridBind("", null, null);
                }
                else
                {
                    ShowAlert("Delete items with problem, please contact to developer.", "success");
                }
            }
        }

        private Dictionary<string, string> expenseAccountingMapping = new Dictionary<string, string>
        {
            { "6001", "6001-Stock Lose" },
            { "6100", "6100-Retal Expense" },
            { "6110", "6110-Avertising Expense" },
            { "6130", "6130-Office Supply Expense" },
            { "6140", "6140-Gasoline Expense" },
            { "6150", "6150-Repair & Maintenance" },
            { "6200", "6200-Transportation" }
        };

        private Dictionary<string, string> payableAccountMapping = new Dictionary<string, string>
        {
             { "5000", "5000-Account Payable" }
        };


        protected void gvItemCommissiontype_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Retrieve data from the current row
                var expenseAccountValue = DataBinder.Eval(e.Row.DataItem, "ExpenseAccount").ToString();
                var payableAccountValue = DataBinder.Eval(e.Row.DataItem, "PayableAccount").ToString();

                // Convert values using dictionaries
                var expenseAccountText = expenseAccountingMapping.ContainsKey(expenseAccountValue)
                    ? expenseAccountingMapping[expenseAccountValue]: "Unknown Expense Account";
               
                var payableAccountText = payableAccountMapping.ContainsKey(payableAccountValue)?
                    payableAccountMapping[payableAccountValue] : "Unknown Expense Account";

                e.Row.Cells[2].Text = payableAccountText; //column2
                e.Row.Cells[3].Text = expenseAccountText; //column3
                
            }
        }

        protected void gvItemCommissiontype_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemCommissiontype.PageIndex = e.NewPageIndex;
            GridBind("", null, null);
        } 
    }
}
