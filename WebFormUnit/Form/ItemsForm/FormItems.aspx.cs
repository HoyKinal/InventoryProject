using iTextSharp.text.pdf;
using iTextSharp.text;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary;
using UnitLabrary.Category;
using UnitLabrary.Item;
using System.Drawing.Drawing2D;
using Org.BouncyCastle.Asn1.X500;
using System.IO;

namespace WebFormUnit.Form.ItemsForm
{
    public partial class FormItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridBind("", "ALL", "ALL", null,null);
                GridBindCategoryT("");
            }
        }

        private void GridBindCategoryT(string search)
        {
            Category category = new Category();
            var load = category.CategorySelects(search.Trim())
                .Select(item => new
                {
                    CategoryCode = item.CategoryCode,
                    DisplayText = item.CategoryCode + " - " + item.CategoryName
                }).ToList();  // Convert to list to avoid multiple enumerations

            if (load.Any())
            {
                gvCategoryT.DataSource = load;
            }
            else
            {
                gvCategoryT.DataSource = null;
            }
            gvCategoryT.DataBind();
        }


        private void GridBind(string search, string locationCode, string categoryCode, string sortExpression, string sortDirection)
        {
            ItemList itemList = new ItemList();
            var load = itemList.ItemListSelects(search.Trim(), locationCode.Trim(), categoryCode.Trim());

            if (load != null && load.Any())
            {
                // Apply sorting if a sort expression is provided
                if (!string.IsNullOrEmpty(sortExpression))
                {
                    load = sortDirection == "ASC" ?
                        load.OrderBy(x =>
                        {
                            var value = x.GetType().GetProperty(sortExpression).GetValue(x);
                            return value ?? ""; // Handle null values
                        }).ToList() :
                        load.OrderByDescending(x =>
                        {
                            var value = x.GetType().GetProperty(sortExpression).GetValue(x);
                            return value ?? ""; // Handle null values
                        }).ToList();
                }

                // Set total record count in ViewState or any other way if needed
                ViewState["TotalRecordCount"] = load.Count;

                // Bind data to GridView and let GridView handle paging
                gvItemList.DataSource = load;
                gvItemList.DataBind();
            }
            else
            {
                gvItemList.DataSource = null;
                gvItemList.DataBind();
            }
        }

        protected void gvItemList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = GetSortDirection(sortExpression);

            string categoryCode = Session["SelectedCategoryCode"] as string ?? "ALL";
            GridBind(txtSearch.Text, "ALL", categoryCode, sortExpression, sortDirection);
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


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/ItemsForm/FormAddItems");
        }

        protected void gvItemList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string itemCode = e.CommandArgument.ToString();

            if (e.CommandName == "EditItem")
            {
                Response.Redirect($"~/Form/ItemsForm/FormAddItems.aspx?ItemCode={HttpUtility.UrlEncode(itemCode)}");
            }
            else if (e.CommandName == "DeleteItem")
            {
                hdfItemCode.Value = itemCode;
                Session["ItemCode"] = itemCode;
                ScriptManager.RegisterStartupScript(this, GetType(), "ItemListDeleteAlert", "ItemListDeleteAlert();", true);
            }else if (e.CommandName == "ViewItem")
            {
                Response.Redirect($"~/Form/ItemsForm/FormAddPartToAssembly.aspx?ItemCode={Server.UrlEncode(itemCode)}");
                //Response.Redirect($"~/Form/ItemsForm/FormAddPartToAssembly.aspx?ItemCode={Server.UrlEncode(itemCode)}&LocationCode={Server.UrlEncode(locationCode)}");
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
        private void ItemListDelete()
        {
            string itemCode = Request.QueryString["ItemCode"];

            if (string.IsNullOrEmpty(itemCode))
            {
                ShowAlert("Invalid Item Code. Please try again.", "danger");
                return;
            }

            ItemList itemList = new ItemList();
            ItemFile itemFile = new ItemFile();
            string folderPath = Server.MapPath("~/Images/");

            // Retrieve item details
            var itemDetails = itemList.ItemListSelectEdits(itemCode);

            if (itemDetails != null)
            {
                // Attempt to delete the item record
                bool isDeleted = itemList.ItemListDeletes(itemCode, itemDetails.LocationCode);

                if (isDeleted)
                {
                    // Retrieve file details
                    var fileDetails = itemFile.ItemFileSelectEdits(itemCode);

                    if (fileDetails != null && !string.IsNullOrEmpty(fileDetails.ItemFileName))
                    {
                        string filePath = Path.Combine(folderPath, fileDetails.ItemFileName);

                        // Delete the associated image file
                        if (File.Exists(filePath))
                        {
                            try
                            {
                                File.Delete(filePath);
                            }
                            catch (Exception ex)
                            {
                                ShowAlert("Failed to delete the associated image file: " + ex.Message, "warning");
                            }
                        }
                    }

                    ShowAlert("Item record and associated image deleted successfully.", "success");
                    string categoryCode = Session["SelectedCategoryCode"] as string ?? "ALL";
                    GridBind("", "ALL", categoryCode, null, null);
                }
                else
                {
                    ShowAlert("Item record deletion failed, please contact the developer.", "danger");
                }
            }
            else
            {
                ShowAlert("Item not found for deletion.", "danger");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ItemList itemList = new ItemList();

            if (Session["ItemCode"] == null)
            {
                ShowAlert("No item selected for deletion.", "danger");
                return;
            }

            string itemCode = Session["ItemCode"].ToString();

            var itemDetails = itemList.ItemListSelectEdits(itemCode);

            if (itemDetails == null)
            {
                ShowAlert("Item not found.", "danger");
                return;
            }
            string folderPath = Server.MapPath("~/Images/");
            ItemFile itemFile = new ItemFile();
            var fileDetails = itemFile.ItemFileSelectEdits(itemCode);

            if (fileDetails != null && !string.IsNullOrEmpty(fileDetails.ItemFileName) && fileDetails.ItemFileName != "Placeholder.png")
            {
                string oldFilePath = Path.Combine(folderPath, fileDetails.ItemFileName);
                if (File.Exists(oldFilePath))
                {
                    try
                    {
                        File.Delete(oldFilePath);
                    }
                    catch (Exception ex)
                    {
                        ShowAlert("Failed to delete the associated image file: " + ex.Message, "warning");
                    }
                }
            }

            string locationCode = itemDetails.LocationCode.ToString();

            bool isDeleted = itemList.ItemListDeletes(itemCode, locationCode);

            if (isDeleted)
            {
                // Show success message and refresh grid or data view
                ShowAlert("Item record has been deleted successfully.", "success");

                // Refresh the grid
                string categoryCode = Session["SelectedCategoryCode"] as string ?? "ALL";
                GridBind("", "ALL", categoryCode, null, null);
            }
            else
            {
                ShowAlert("Item record deletion failed, please contact the developer.", "danger");
            }
        }


        protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectValue = ddlExport.SelectedValue;

            switch (selectValue)
            {
                case "Excel":
                    ExportToExcel();
                    break;
                case "PDF":
                    ExportToPDF();
                    break;
                case "PrintView":
                    PrintCurrentView();
                    break;
                case "PrintAll":
                    PrintAllData();
                    break;
                default:
                    ShowAlert("Invalid selection", "danger");
                    break;
            }
        }

        private void PrintAllData()
        {
            // Retrieve all items from the database
            ItemList itemList = new ItemList();
            var allItems = itemList.ItemListSelects("", "ALL", "ALL");

            if (allItems == null || !allItems.Any())
            {
                ShowAlert("No data available for printing.", "warning");
                return;
            }

            // Create a DataTable to hold all items
            var dataTable = new DataTable();
            dataTable.Columns.Add("#");
            dataTable.Columns.Add("ItemCode");
            dataTable.Columns.Add("ItemType");
            dataTable.Columns.Add("CategoryCode");
            dataTable.Columns.Add("BarCode");
            dataTable.Columns.Add("PurDescription");
            dataTable.Columns.Add("SaleDescription");
            dataTable.Columns.Add("Cost");
            dataTable.Columns.Add("SalePrice");
            dataTable.Columns.Add("ItemStatus");
            dataTable.Columns.Add("CreatedBy");
            dataTable.Columns.Add("DateModified");

            foreach (var item in allItems)
            {
                dataTable.Rows.Add(item.RowNo, item.ItemCode, item.ItemType, item.CategoryCode, item.BarCode,
                    item.PurDescription, item.SaleDescription, item.Cost, item.SalePrice, item.ItemStatus, item.CreatedBy, item.DateModified);
            }

           string htmlContent = "<html><head><title>Print All Data</title>" +
                      "<style>" +
                      "@page { size: landscape; margin: 20mm; }" +
                      "body { font-family: Arial, sans-serif; margin: 20px; }" +
                      "table { width: 100%; border-collapse: collapse; margin: 20px 0; }" +
                      "th, td { padding: 12px; text-align: left; border: 1px solid #ddd; }" +
                      "th { background-color: #f2f2f2; color: #333; }" +
                      "tr:nth-child(even) { background-color: #f9f9f9; }" +
                      "tr:hover { background-color: #f1f1f1; }" +
                      "</style></head><body>";

            htmlContent += "<table><tr><th>#</th><th>ItemCode</th><th>ItemType</th><th>CategoryCode</th><th>BarCode</th>" +
                           "<th>PurDescription</th><th>SaleDescription</th><th>Cost</th><th>SalePrice</th><th>ItemStatus</th>" +
                           "<th>CreatedBy</th><th>DateModified</th></tr>";

            //int rowIndex = 1;
            foreach (DataRow row in dataTable.Rows)
            {
                htmlContent += "<tr>";
                //htmlContent += $"<td>{rowIndex++}</td>"; // Add row number
                foreach (var cell in row.ItemArray)
                {
                    htmlContent += $"<td>{HttpUtility.HtmlEncode(cell.ToString())}</td>";
                }
                htmlContent += "</tr>";
            }

            htmlContent += "</table></body></html>";

            // Calculate the center position with a medium height
            int width = 800;
            int height = 600; 
            string script = $@"
                var width = {width};
                var height = {height};
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var printWindow = window.open('', '', 'width=' + width + ',height=' + height + ',top=' + top + ',left=' + left);
                printWindow.document.open();
                printWindow.document.write('{HttpUtility.JavaScriptStringEncode(htmlContent)}');
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
            ";

            // Register the script to be executed on the client-side
            ScriptManager.RegisterStartupScript(this, GetType(), "PrintAllData", script, true);


            // Register the script to be executed on the client-side
            ScriptManager.RegisterStartupScript(this, GetType(), "PrintAllData", script, true);


            // Register the script to be executed on the client-side
            ScriptManager.RegisterStartupScript(this, GetType(), "PrintAllData", script, true);
        }

        private void PrintCurrentView()
        {
            string script = @"
                var printWindow = window.open('', '', 'width=800,height=600');
                var content = document.getElementById('" + gvItemList.ClientID + @"').outerHTML;
                printWindow.document.open();
                printWindow.document.write('<html><head><title>Print View</title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(content);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
            ";

            // Register the script to be executed on the client-side
            ScriptManager.RegisterStartupScript(this, GetType(), "PrintCurrentView", script, true);
        }

        private void ExportToPDF()
        {
            try
            {
                var itemList = new ItemList();
                var dataItem = itemList.ItemListSelects("", "ALL", "ALL");

                var document = new Document();
                using (var stream = new System.IO.MemoryStream())
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    var table = new PdfPTable(12); // Number of columns in the table
                    table.AddCell("No");
                    table.AddCell("ItemCode");
                    table.AddCell("ItemType");
                    table.AddCell("CategoryCode");
                    table.AddCell("BarCode");
                    table.AddCell("PurDescription");
                    table.AddCell("SaleDescription");
                    table.AddCell("Cost");
                    table.AddCell("SalePrice");
                    table.AddCell("ItemStatus");
                    table.AddCell("CreatedBy");
                    table.AddCell("DateModified");

                    foreach (var item in dataItem)
                    {
                        table.AddCell(item.RowNo.ToString());
                        table.AddCell(item.ItemCode);
                        table.AddCell(item.ItemType);
                        table.AddCell(item.CategoryCode);
                        table.AddCell(item.BarCode);
                        table.AddCell(item.PurDescription);
                        table.AddCell(item.SaleDescription);
                        table.AddCell(item.Cost.ToString("F2"));
                        table.AddCell(item.SalePrice.ToString());
                        table.AddCell(item.ItemStatus);
                        table.AddCell(item.CreatedBy);
                        table.AddCell(item.DateModified.ToString("yyyy-MM-dd"));
                    }

                    document.Add(table);
                    document.Close();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", $"attachment;filename=ItemList_{DateTime.Now:yyyyMMddHHmmss}.pdf");
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ExportToPDF: {ex.Message}");
                ShowAlert("An error occurred while exporting to PDF. Please try again.", "danger");
            }
        }

        private void ExportToExcel()
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;

                ItemList itemList = new ItemList();
                var dataItem = itemList.ItemListSelects("", "ALL", "ALL");

                var dataTable = new DataTable();
                dataTable.Columns.Add("#");
                dataTable.Columns.Add("ItemCode");
                dataTable.Columns.Add("ItemType");
                dataTable.Columns.Add("CategoryCode");
                dataTable.Columns.Add("BarCode");
                dataTable.Columns.Add("PurDescription");
                dataTable.Columns.Add("SaleDescription");
                dataTable.Columns.Add("Cost");
                dataTable.Columns.Add("SalePrice");
                dataTable.Columns.Add("ItemStatus");
                dataTable.Columns.Add("CreatedBy");
                dataTable.Columns.Add("DateModified");

                foreach (var item in dataItem)
                {
                    dataTable.Rows.Add(item.RowNo, item.ItemCode, item.ItemType, item.CategoryCode, item.BarCode,
                        item.PurDescription, item.SaleDescription, item.Cost, item.SalePrice, item.ItemStatus, item.CreatedBy, item.DateModified);
                }

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("ItemList");
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                    using (var stream = new System.IO.MemoryStream())
                    {
                        package.SaveAs(stream);
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", $"attachment;filename=ItemList_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.BinaryWrite(stream.ToArray());
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ExportToExcel: {ex.Message}");
                ShowAlert("An error occurred while exporting to Excel. Please try again.", "danger");
            }
        }


        protected void gvCategoryT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string categoryCode = e.CommandArgument.ToString();
                Session["SelectedCategoryCode"] = categoryCode;
                //Reset the page index to 0 when a new category is selected 
                gvItemList.PageIndex = 0;

                GridBind("", "ALL", categoryCode, null, null);
                //ShowAlert($"Category with code {categoryCode} has been selected.", "info");
            }
        }
        protected void gvCategoryT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvCategoryT,$"Select${e.Row.RowIndex}"); // index-> 0,1,2 ...
               
                //e.Row.Style.Add("cursor","pointer");
                e.Row.Attributes["style"] = "cursor:pointer"; 

            }
        }
        protected void gvCategoryT_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = gvCategoryT.SelectedIndex;
           
            string categoryCode = gvCategoryT.DataKeys[selectedIndex].Value.ToString();
            
            ShowAlert($"categoryCode is : {categoryCode}", "info");

            Session["SelectedCategoryCode"] = categoryCode;

            // Reset the page index to 0 when a new category is selected
            gvItemList.PageIndex = 0;

            GridBind("", "ALL", categoryCode, null, null);

        }

        protected void gvItemList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemList.PageIndex = e.NewPageIndex;
            string categoryCode = Session["SelectedCategoryCode"] as string;

            if (string.IsNullOrEmpty(categoryCode))
            {
                categoryCode = "ALL";
            }
            else {
                GridBind("", "ALL", categoryCode, null, null);
            }

            // string categoryCode = Session["SelectedCategoryCode"] as string ?? "all";

        }

        protected void gvItemList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnView = (LinkButton)e.Row.FindControl("btnView");

                // Make sure btnView is not null
                if (btnView != null)
                {
                    // Get the item type from the DataRow
                    string itemType = DataBinder.Eval(e.Row.DataItem, "ItemType") as string;

                    if (itemType == "Inventory Assembly" || itemType == "Group Assembly")
                    {
                        btnView.Visible = true;
                    }
                    else
                    {
                        btnView.Visible = false;
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvItemList.PageIndex = 0; // Reset the page index on search

            GridBind(txtSearch.Text, "ALL", "ALL", null,null);
        }

        protected void gvItemList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string itemCode = gvItemList.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = gvItemList.Rows[e.RowIndex];
            //reset the to rebind the gridview 
            gvItemList.EditIndex = -1;
            GridBind("","ALL","ALL",null,null);
        }
    }
}
