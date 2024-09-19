using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary;
using UnitLabrary.Category;
using UnitLabrary.Item;

namespace WebFormUnit.Form.ItemsForm
{
    public partial class FormAddPartToAssembly : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadInventoryAssembly();
                loadCategoryCode();
                loadUnitFromForSale();
                GridBindItemAssembly();
                lbDisplaySalePrice.Text = totalAmount.ToString("C");   
                string itemCode = Request.QueryString["ItemCode"];
                Session["ItemCode"] = itemCode; 
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
        private void loadCategoryCode()
        {
            Category category = new Category();
            var load = category.CategorySelects("");
            ddlProductCategory.DataSource = load;
            
            ddlProductCategory.DataTextField = "CategoryName";
            ddlProductCategory.DataValueField = "CategoryCode";    
            ddlProductCategory.DataBind();

            ddlProductCategory.Items.Insert(0, new ListItem("-- Select Category --", ""));
        }


        private void GridBindItemAssembly()
        {
            string itemCode = Request.QueryString["ItemCode"];
            string assemblyCode = itemCode;
            ItemList itemList = new ItemList(); 
            var loc = itemList.ItemListSelectEdits(itemCode);
            string locationCode = loc.LocationCode.ToString();
            ItemAssimbly itemAssimbly = new ItemAssimbly();
            var load = itemAssimbly.ItemAssemblySelects(assemblyCode, locationCode);
            if (load.Any() || load != null)
            {
                gvItemAssembly.DataSource = load;
                gvItemAssembly.DataBind();
                lbDisplaySalePrice.Text = totalAmount.ToString("C");
            }

        }
        private void loadInventoryAssembly()
        {
            ItemList itemList = new ItemList();
            string itemCode = Request.QueryString["ItemCode"];
            var loadId = itemList.ItemListSelectEdits(itemCode);
            if (loadId != null)
            {
                lbName.Text = $"{loadId.ItemCode}-{loadId.PurDescription}";
                lbCategory.Text = loadId.CategoryCode;
                lbSalePrice.Text = loadId.SalePrice.Value.ToString("F2");
                string account = loadId.IncomeAccount;
                switch (account)
                {
                    case "4000":
                        lbIncomeAccount.Text = "4000-Revenue";
                        break;
                    case "40001":
                        lbIncomeAccount.Text = "40001-SR-White-Platinum";
                        break;
                    case "40002":
                        lbIncomeAccount.Text = "40002-SR-Red-Platinum";
                        break;
                    case "40003":
                        lbIncomeAccount.Text = "40003-SR-Diamonds";
                        break;
                    case "40004":
                        lbIncomeAccount.Text = "40004-SR-Gold";
                        break;
                    case "40005":
                        lbIncomeAccount.Text = "40005-SR-Order";
                        break;
                    case "40006":
                        lbIncomeAccount.Text = "40006-SR-Shell(VLJ)";
                        break;
                    case "40007":
                        lbIncomeAccount.Text = "40007-Stock Income";
                        break;
                    case "40008":
                        lbIncomeAccount.Text = "40008-Tax Income";
                        break;
                    default:
                        break;
                }
            }
        }
        private void loadAccount()
        {
            ItemList itemList = new ItemList();
            string itemCode = ddlItemCode.SelectedValue;
            if (itemCode != null)
            {
                var load = itemList.ItemListSelectEdits(itemCode);
                if(load != null)
                {
                    string accountIncome = load.IncomeAccount;
                    string soldAccount = load.CoGSAccount;
                    string assetAccount = load.AssetAccount;

                    // Define the dictionary with all account codes and their descriptions
                    var accountDesc = new Dictionary<string, string>
                    {
                        { "4000", "4000-Revenue" },
                        { "40001", "40001-SR-White-Platinum" },
                        { "40002", "40002-SR-Red-Platinum" },
                        { "40003", "40003-SR-Diamonds" },
                        { "40004", "40004-SR-Gold" },
                        { "40005", "40005-SR-Order" },
                        { "40006", "40006-SR-Shell(VLJ)" },
                        { "40007", "40007-Stock Income" },
                        { "40008", "40008-Tax Income" },
                        { "5000", "5000-Cost of Goods Sold" },
                        { "5001", "5001-COGS-White-Platinum" },
                        { "5002", "5002-COGS-Red-Platinum" },
                        { "5003", "5003-COGS-Diamonds" },
                        { "5004", "5004-COGS-Gold" },
                        { "5005", "5005-COGS-Order" },
                        { "5006", "5005-COGS-Shell(VLJ)" },
                        { "1410", "1410-Inventory" },
                        { "14101", "14101-WH-White-Plactinum" },
                        { "14102", "14102-WH-Red-Platinum" },
                        { "14103", "14103-WH-Diamonds" },
                        { "14104", "14104-WH-Gold" },
                        { "14105", "14105-WH-Order" },
                        { "14106", "14105-WH-Shell(VLJ)" }
                    };

                    // Update the revenue account label
                    if (accountDesc.TryGetValue(accountIncome, out string incomeDescription))
                    {
                        LabelRevenueAccount.Text = incomeDescription;
                    }
                    else
                    {
                        LabelRevenueAccount.Text = "Unknown Revenue Account";
                    }

                    // Update the COGS account label
                    if (accountDesc.TryGetValue(soldAccount, out string soldDescription))
                    {
                        LabelCOGSAccount.Text = soldDescription;
                    }
                    else
                    {
                        LabelCOGSAccount.Text = "Unknown COGS Account";
                    }

                    // Update the asset account label
                    if (accountDesc.TryGetValue(assetAccount, out string assetDescription))
                    {
                        LabelInventoryAsset.Text = assetDescription;
                    }
                    else
                    {
                        LabelInventoryAsset.Text = "Unknown Asset Account";
                    }
                }
            }
        }
        private void loadCost()
        {
            ItemList itemList = new ItemList();
            string itemCode = ddlItemCode.SelectedValue;

            if (!string.IsNullOrEmpty(itemCode))
            {
                var load = itemList.ItemListSelectEdits(itemCode);

                if (load != null)
                {
                    txtCost.Text = load.Cost.ToString("F2");
                    LabelType.Text = load.ItemType.ToString();
                    LabelCategory.Text = load.CategoryName.ToString();
                    LabelName.Text = load.PurDescription.ToString();
                    LabelSalePrice.Text = load.SalePrice.Value.ToString("F2");
                   // LabelStockQuantity.Text = load.Quantity.ToString(); // do it letter 
                    LabelAverageCost.Text = load.Cost.ToString("F2");
                    LabelUnitSale.Text = load.UnitSaleName.ToString();
                    LabelUnitStock.Text = load.UnitStockName.ToString();
                    //LabelRevenueAccount.Text = load.IncomeAccount.ToString();
                    //LabelCOGSAccount.Text = load.CoGSAccount.ToString();
                    //LabelInventoryAsset.Text = load.AssetAccount.ToString();
                    loadAccount();
                }
                else
                {
                    txtCost.Text = "Cost not available";
                }
            }
            else
            {
                txtCost.Text = "Select an item";
            }
        }

        protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCost();
            txtQuantity.Text = string.Empty;
        }
        protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadItemCode1();
            //loadItemCode();
            loadCost();
        }

        //way1
        //private void loadItemCode()
        //{
        //    ItemAssimbly itemAssembly = new ItemAssimbly();
        //    ItemList itemList = new ItemList();

        //    string itemCode = Request.QueryString["ItemCode"];
        //    string categoryCode = ddlProductCategory.SelectedValue;
        //    var itemDetails = itemList.ItemListSelectEdits(itemCode);

        //    if (itemDetails != null)
        //    {
        //        string locationCode = itemDetails.LocationCode;
        //        var inventoryPart = itemAssembly.GetAssemblyPartsByLocation(locationCode, categoryCode);


        //        ddlItemCode.Items.Clear();
        //        ddlItemCode.Items.Insert(0, new ListItem("-- Select Item --", ""));

        //        if (inventoryPart.Any())
        //        {
        //            ddlItemCode.DataSource = inventoryPart;
        //            ddlItemCode.DataTextField = "ItemName";
        //            ddlItemCode.DataValueField = "ItemCode";
        //            ddlItemCode.DataBind();

        //            ddlItemCode.Items.Insert(0,new ListItem("-- Select Item --",""));
        //        }
        //        else
        //        {

        //            ddlItemCode.Items.Clear();
        //            ddlItemCode.Items.Insert(0, new ListItem("-- No items available --", ""));
        //        }
        //    }
        //}

        //way2
        private void loadItemCode1()
        {
            ItemList itemList = new ItemList();
            string categoryCode = ddlProductCategory.SelectedValue;


            if (!string.IsNullOrEmpty(categoryCode))
            {
                string itemCode = Session["ItemCode"].ToString();
                var record = itemList.ItemListSelectEdits(itemCode);

                var load = itemList.ItemListSelects("", record.LocationCode, categoryCode)
                    .Where(item => item.ItemType == "Inventory Part" && item.ItemStatus.Equals("Active"))
                    .Select(item => new
                    {
                        itemCode = item.ItemCode,
                        DisplayText = item.ItemCode + " - " + item.PurDescription
                    })
                    .ToList();

                if (load.Any())
                {
                    ddlItemCode.DataSource = load;
                    ddlItemCode.DataTextField = "DisplayText";
                    ddlItemCode.DataValueField = "itemCode";                  
                    ddlItemCode.DataBind();
                    //ddlItemCode.Items.Insert(0, new ListItem("-- Insert Item --", ""));
                }
                else
                {
                    ddlItemCode.Items.Clear();
                    
                }
            }
            else
            {
                ddlItemCode.Items.Clear();
                ddlItemCode.Items.Insert(0, new ListItem("-- Select Category First --", ""));
            }
        }

        private void loadUnitFromForSale()
        {
            UnitMeasurement unitMeasurement = new UnitMeasurement();
            var load = unitMeasurement.SelectUnitMeasurement("");
            ddlUnit.DataSource = load;
            ddlUnit.DataTextField = "UnitFromName";
            ddlUnit.DataValueField = "UnitFrom";
            ddlUnit.DataBind(); 
        }
        protected void ddlBackNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choice = ddlBackNew.SelectedValue;
            switch (choice)
            {
                case "0":
                    Response.Redirect("~/Form/ItemsForm/FormItems");
                        break;
                case "1":
                    ClearFields();
                    break;
                default:
                    break;

            }
        }
       
        private void ClearFields()
        {
            ddlProductCategory.SelectedIndex = 0;   
            ddlItemCode.SelectedIndex = 0;
            txtQuantity.Text = string.Empty;
            txtCost.Text = string.Empty;
            txtTotal.Text = string.Empty;
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ItemAssimbly itemAssimbly = new ItemAssimbly();
            ItemList itemList = new ItemList();

            string itemCode = Request.QueryString["ItemCode"];

            string assemblyCode = itemCode;
            string locationCode = itemList.ItemListSelectEdits(itemCode).LocationCode;
            string unit = ddlUnit.SelectedValue;
            string NewitemCodePK = ddlItemCode.SelectedValue; 
            string oldItemCodePK = Session["ItemCode"] as string;

            if (!decimal.TryParse(txtQuantity.Text, out decimal quantity))
            {
                ShowAlert("Invalid quantity format.", "danger");
                return;
            }

            var recordExists = itemAssimbly.itemAssemblySelectEdits(assemblyCode, locationCode, oldItemCodePK);

            if (recordExists != null)
            {
                bool isUpdate = itemAssimbly.ItemAssemblyUpdates(assemblyCode,locationCode, oldItemCodePK, NewitemCodePK, unit,quantity);
                if (isUpdate)
                {
                    ShowAlert("Inventory Part Item updated successfully", "info");
                    ClearFields();
                    GridBindItemAssembly();
                }   
                else
                {
                    ShowAlert("Having something wrong with updating Inventory part Items", "danger");
                }
            }
            else
            {
                bool isInsert = itemAssimbly.InsertItemAssemblies(assemblyCode, locationCode, NewitemCodePK, unit, quantity);

                if (isInsert)
                {
                    ShowAlert("Insert Inventory part to Assembly is successful", "success");
                    ClearFields();
                    GridBindItemAssembly();
                }
                else
                {
                    ShowAlert("Record Inventory part to Assembly is already exist, please try another.", "danger");
                    txtQuantity.Text = string.Empty;
                }
            }
        }

        protected void gvItemAssembly_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string itemCode = gvItemAssembly.DataKeys[index]["ItemCode"].ToString();
            string assemblyCode = gvItemAssembly.DataKeys[index]["AssemblyCode"].ToString();
            string locationCode = gvItemAssembly.DataKeys[index]["LocationCode"].ToString();
            if (e.CommandName == "EditItem")
            {
                ItemAssimbly itemAssimbly = new ItemAssimbly();
               
                Session["ItemCode"] = itemCode;
                Session["AssemblyCode"] = assemblyCode;
                Session["LocationCode"] = locationCode;
               
                var load = itemAssimbly.itemAssemblySelectEdits(assemblyCode,locationCode,itemCode);
                
                if (load != null)
                {
                    ddlProductCategory.SelectedValue = load.CategoryCode;
                    loadItemCode1();
                    if (ddlItemCode.Items.FindByValue(load.ItemCode) != null)
                    {

                     ddlItemCode.SelectedValue = load.ItemCode.ToString();
                    }
                    txtQuantity.Text = load.Quantity.ToString("F0");
                    txtCost.Text = load.Cost.ToString("F2");    
                    txtTotal.Text = load.Total.Value.ToString("F2");
                }
                GridBindItemAssembly();
                loadCost();
            }
            else if (e.CommandName == "DeleteItem")
            {
                ViewState["ItemCode"] = itemCode;
                ViewState["AssemblyCode"] = assemblyCode;
                ViewState["LocationCode"] = locationCode;

                ScriptManager.RegisterStartupScript(this, GetType(), "modalDeleteAlert", "modalDeleteAlert();", true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string asstemblyCode = ViewState["AssemblyCode"] as string;
            string locationCode = ViewState["LocationCode"].ToString();
            string itemCode = ViewState["ItemCode"].ToString();

            ItemAssimbly itemAssimbly = new ItemAssimbly(); 

            bool isDelete = itemAssimbly.ItemAssemblyDeletes(asstemblyCode, locationCode,itemCode);
            if (isDelete)
            {
                ShowAlert("Delete Inventory Part is successfully", "success");
                GridBindItemAssembly();
            }
            else
            {
                ShowAlert("Delete Inventory Part not complete, please contact to developer.", "danger");
            }
        }

        private decimal totalAmount = 0;
        protected void gvItemAssembly_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming the 'Total' column is the 7th column (index 6)
                string totalText = e.Row.Cells[6].Text;

                decimal totalValue;
                if (Decimal.TryParse(totalText, System.Globalization.NumberStyles.Currency, null, out totalValue))
                {
                    totalAmount += totalValue; // Add the parsed value to the running total
                }
            }
            else if(e.Row.RowType == DataControlRowType.Footer) // If it's the footer row, display the total
            {
                e.Row.Cells[6].Text = "Total: " + totalAmount.ToString("C"); // Display total formatted as currency
            }
        }

        protected void gvItemAssembly_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemAssembly.PageIndex = e.NewPageIndex;
            GridBindItemAssembly();
        }
    }
}