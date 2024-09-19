using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Customers.CustomerType;

namespace WebFormUnit.Form.CustomerForm
{
    public partial class FormCustomerType : System.Web.UI.Page
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
        protected void gvCustomerType_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = GetSortDirection(sortExpression);
            GridBind("", sortExpression, sortDirection);
        }
        private void GridBind(string search, string sortExpression, string sortDirection)
        {
            ICustomerTypeComponent component = new CustomerTypeConcreteComponent();
            ICustomerTypeComponent decoratedComponent = new CustomerTypeConcreteDecorator(component);
            var load = decoratedComponent.CustomerTypeSelect(search);
            if (load != null)
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
                gvCustomerType.DataSource = load;
                gvCustomerType.DataBind();
            }
        }
        protected void gvCustomerType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerType.PageIndex = e.NewPageIndex;
            GridBind("", null, null);
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
            txtName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtDiscrountPercentage.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
        }
        protected void btnAddEdit_Click(object sender, EventArgs e)
        {
            ICustomerTypeComponent component = new CustomerTypeConcreteComponent();
            ICustomerTypeComponent decoratedComponent = new CustomerTypeConcreteDecorator(component);
            CustomerType ct = new CustomerType
            {
                MemberTypeCode = DateTime.Now.Ticks.ToString(),
                MemberTypeName = txtName.Text,
                MemberTypePrice = decimal.Parse(txtPrice.Text),
                MemberTypeStatus = bool.Parse(ddlStatus.SelectedValue),
                MemberTypeIsSync = true,
                MemberTypeDiscount = decimal.Parse(txtDiscrountPercentage.Text),
                ModifiedBy = "Admin",
                ModifiedDate = DateTime.Now
            };
            string check = hdfMemberTypeCode.Value;
            if (check == "")
            {
                bool isInsert = decoratedComponent.CustomerTypeInserts(ct);
                if (isInsert)
                {
                    ShowAlert("Insert CustomerType successfully", "success");
                    GridBind("", null, null);
                    ClearFields();
                }
                else
                {
                    ShowAlert("Insert Item failed", "danger");
                }
            }
            else
            {
                ct.MemberTypeCode = hdfMemberTypeCode.Value;
                bool isUpdate = decoratedComponent.CustomerTypeUpdate(ct);
                if (isUpdate)
                {
                    ShowAlert("Update CustomerType successfully", "info");
                    GridBind("", null, null);
                    ClearFields();
                    hdfMemberTypeCode.Value = "";
                }
                else
                {
                    ShowAlert("Update CustomerType failed", "warning");
                }
            }
            
        }
        protected Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
        {
            { "True", "Active" },
            { "False", "Disable" }
        };

        protected void gvCustomerType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var statuseMemberValue = DataBinder.Eval(e.Row.DataItem, "MemberTypeStatus").ToString();

                var statuseMemberText = keyValuePairs.ContainsKey(statuseMemberValue) ?
                   keyValuePairs[statuseMemberValue] : "Unknown Expense Account";

                e.Row.Cells[4].Text = statuseMemberText;

                //highlight color
                double price = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MemberTypePrice"));
                if (price > 50)
                {
                    //e.Row.BackColor = System.Drawing.Color.LightGreen;
                    TableCell tableCell = e.Row.Cells[2];
                    tableCell.BackColor = System.Drawing.Color.LightGreen; 
                    tableCell.Text = $"<span style='background-color: yellow; padding:5px; border-radius:5px;'>{price:C}</span>";


                }
                else if (price < 20)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text,null,null);
        }

        protected void gvCustomerType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewItem" || e.CommandName== "EditItem" || e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string memberTypeCode = gvCustomerType.DataKeys[index].Value.ToString();

                ViewState["MemberTypeCode"] = memberTypeCode;
                hdfMemberTypeCode.Value = memberTypeCode;

                if (e.CommandName == "EditItem")
                {
                    ICustomerTypeComponent component = new CustomerTypeConcreteComponent();
                    ICustomerTypeComponent componentDecorated = new CustomerTypeConcreteDecorator(component);
                    CustomerType ct = new CustomerType
                    {
                        MemberTypeCode = memberTypeCode,
                    };
                    var check =  componentDecorated.CustomerTypeSelectEdit(ct);
                    if (check != null)
                    {
                        txtName.Text = check.MemberTypeName;
                        txtPrice.Text = check.MemberTypePrice.ToString("F2");
                        txtDiscrountPercentage.Text = check.MemberTypeDiscount.ToString("F2");
                        ddlStatus.SelectedValue = check.MemberTypeStatus.ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal()", "showEditModal();", true);  
                    }
                }
                else if (e.CommandName == "DeleteItem")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDeleteModal()", "showDeleteModal();", true);
                }
                else if (e.CommandName == "ViewItem")
                {
                    Response.Redirect($"~/Form/CustomerForm/FormCustomer.aspx?MemberTypeCode={Server.UrlEncode(memberTypeCode)}");
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string memberTypeCode = ViewState["MemberTypeCode"].ToString();
            ICustomerTypeComponent component = new CustomerTypeConcreteComponent();
            ICustomerTypeComponent componentDecorated = new CustomerTypeConcreteDecorator(component);
            CustomerType ct = new CustomerType
            {
                MemberTypeCode = memberTypeCode,
            };
            bool isDelete = componentDecorated.CustomerTypeDeletes(ct);
            if (isDelete)
            {
                ShowAlert("Delete CustomerType successfully", "success");
                GridBind("", null, null);
                ViewState["MemberTypeCode"] = "";
                hdfMemberTypeCode.Value = "";
            }
            else
            {
                ShowAlert("Delete CustomerType failed", "warning");
            }
        }
    }
}