using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Category;
using UnitLabrary.Item;
using UnitLabrary.Item.ItemCommissions;
using ListItem = System.Web.UI.WebControls.ListItem;
using OfficeOpenXml;

namespace WebFormUnit.Form.ItemsForm
{
    public partial class FormItemCommission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string commissionTypeCode = Request.QueryString["CommissionTypeCode"];

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(commissionTypeCode))
                {
                    Session["CommissionTypeCode"] = commissionTypeCode;
                }
                LoadCommissionTypeInfo();
                InventoryItem();
                GridBind("", null, null);
            }
        }
        private void GridBind(string search, string sortExpression, string sortDirection)
        {
            IItemCommissionRepository item = new ItemCommissionRepository();

            string commissionTypeCode = Session["CommissionTypeCode"] as string;

            var load = item.ItemCommissionSelects(search.Trim(), commissionTypeCode);
            if (load !=null && load.Any())
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
                gvItemCommission.DataSource = load;
                gvItemCommission.DataBind();
                gvItemCommission.PageIndex= 0;
            }
            else
            {
                gvItemCommission.DataSource = load;
                gvItemCommission.DataBind();
                gvItemCommission.PageIndex = 0;
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
        protected void gvItemCommission_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = GetSortDirection(sortExpression);
            GridBind("", sortExpression, sortDirection);
        }
        protected void gvItemCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemCommission.PageIndex = e.NewPageIndex;
            GridBind("", null, null);
        }
        private void LoadCommissionTypeInfo()
        {
            string commissionTypeCode = Session["CommissionTypeCode"] as string;
            ItemCommissionType itemCommissionType = new ItemCommissionType();
            var load = itemCommissionType.ItemCommissionTypeSelectEdits(commissionTypeCode);
            if (load != null)
            {
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string> {
                  { "6001", "6001-Stock Lose" },
                  { "6100", "6100-Retal Expense" },
                  { "6110", "6110-Avertising Expense" },
                  { "6130", "6130-Office Supply Expense" },
                  { "6140", "6140-Gasoline Expense" },
                  { "6150", "6150-Repair & Maintenance" },
                  { "6200", "6200-Transportation" },
                  { "5000", "5000-Account Payable" },
                  {"True" , "Active" },
                  {"False", "Disable" }
                };

                LabelName.Text = load.CommissionTypeName;

                if (keyValuePairs.TryGetValue(load.PayableAccount, out string PayableAccount))
                {
                    LabelPayableAccount.Text = PayableAccount;
                } 
                if (keyValuePairs.TryGetValue(load.ExpenseAccount,out string ExpenseAccount))
                {
                    LabelExpenseAccount.Text = ExpenseAccount;
                }
                if (keyValuePairs.TryGetValue(load.CommissionTypeStatus.ToString(), out string Status))
                {
                    LabelStatus.Text = Status;
                }
            }
        }
        private void InventoryItem()
        {
            LoadCategory();

            LoadItem();

            LoadSalePriceUnit();

        }
        private void ClearFields()
        {
            ddlCategory.SelectedIndex = 0;  
            ddlItem.SelectedIndex = 0;   
            txtCommission.Text = string.Empty;  
            chkPercentage.Checked = false;
        }
        private void LoadCategory()
        {
            Category category = new Category();
            var loadCategory = category.CategorySelects("");
            ddlCategory.DataSource = loadCategory;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryCode";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("ALL", "ALL"));
        }
        private void LoadSalePriceUnit()
        {
            ItemList itemList = new ItemList();
            var load = itemList.ItemListSelectEdits(ddlItem.SelectedValue);
            if(load != null)
            {
                txtSalePrice.Text = load.SalePrice.Value.ToString("#,##0.00");
                txtUnit.Text = load.UnitSaleName.ToString();
            }
        }
        private void LoadItem()
        {
            ItemList itemList = new ItemList();
            var loadItem = itemList.ItemListSelects("", "ALL", ddlCategory.SelectedValue).Select(item => new
            {
                itemCode = item.ItemCode,
                itemName = item.ItemCode +"-"+ item.PurDescription
            });
            ddlItem.DataSource = loadItem;
            ddlItem.DataTextField = "itemName";
            ddlItem.DataValueField = "ItemCode";
            ddlItem.DataBind();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/ItemsForm/FormCommissionType.aspx");
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItem();
            LoadSalePriceUnit();
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalePriceUnit();
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
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            
            IItemCommissionRepository itemRepository = new ItemCommissionRepository();
            ItemList itemList = new ItemList();

            ItemCommissions itemCommissions = new ItemCommissions
            {
                ItemCode = ddlItem.SelectedValue,
                LocationCode = itemList.ItemListSelectEdits(ddlItem.SelectedValue)?.LocationCode.ToString(),
                CommissionTypeCode = Session["CommissionTypeCode"].ToString(),
                ItemCommission = decimal.Parse(txtCommission.Text),
                ItemCommissionPercent = chkPercentage.Checked,
                IsSync = true,
                CreateBy = "admin", 
                CreateDate = DateTime.Now
            };

            var existingRecord = itemRepository.ItemCommissionSelectEdit(itemCommissions);

            if (existingRecord == null)
            {
                bool isInsert = itemRepository.ItemCommissionInserts(itemCommissions);

                if (isInsert)
                {
                    ShowAlert("Insert Item commission successfully.", "success");
                    GridBind("", null, null); 
                    ClearFields(); 
                }
                else
                {
                    ShowAlert("Insert Item commission failed, please contact the developer.", "danger");
                }
            }
            else
            {
                if (existingRecord.ItemCode == ViewState["ItemCode"]?.ToString())
                {
                    bool isUpdate = itemRepository.ItemCommissionUpdates(itemCommissions);

                    if (isUpdate)
                    {
                        ShowAlert("Update Item commission successfully.", "info");
                        GridBind("", null, null); 
                        ClearFields(); 
                        ViewState["ItemCode"] = null; 
                    }
                    else
                    {
                        ShowAlert("Update Item commission failed, please contact the developer.", "danger");
                    }
                } 
                else
                {
                    ShowAlert("Duplicated Record item commission, please try again.", "danger");
                }
            }
        }


        protected void gvItemCommission_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName== "EditItemCommission" || e.CommandName == "DeleteItemCommission")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string commissionTypeCode = gvItemCommission.DataKeys[index]["CommissionTypeCode"].ToString();
                string itemCode = gvItemCommission.DataKeys[index]["ItemCode"].ToString();
                string locationCode = gvItemCommission.DataKeys[index]["LocationCode"].ToString();

                ViewState["CommissionTypeCode"] = commissionTypeCode;
                ViewState["ItemCode"] = itemCode;
                ViewState["LocationCode"] = locationCode;

                if (e.CommandName== "EditItemCommission")
                {
                    var itemCommission = new ItemCommissions
                    {
                        ItemCode = itemCode,
                        LocationCode = locationCode,
                        CommissionTypeCode = commissionTypeCode
                    };
                   
                    IItemCommissionRepository item = new ItemCommissionRepository();

                    var load = item.ItemCommissionSelectEdit(itemCommission);

                    if (load != null)
                    {
                        ddlCategory.SelectedValue = load.CategoryCode;
                        ddlItem.SelectedValue = load.ItemCode;
                        txtSalePrice.Text = load.SalePrice.Value.ToString("F2");
                        txtUnit.Text = load.UnitSaleName;
                        txtCommission.Text = load.ItemCommission.ToString("F2");
                        string ischeck = load.ItemCommissionPecent.ToString();
                        if (ischeck=="True")
                        {
                            chkPercentage.Checked = true;
                        }
                        else
                        {
                            chkPercentage.Checked = false;
                        }
                    }
                }
                else if (e.CommandName == "DeleteItemCommission")
                {
                    var itemCommission = new ItemCommissions
                    {
                        ItemCode = itemCode,
                        LocationCode = locationCode,
                        CommissionTypeCode = commissionTypeCode
                    };

                    IItemCommissionRepository item = new ItemCommissionRepository();

                    var load = item.ItemCommissionSelectEdit(itemCommission);
                    if (load != null)
                    {
                        ScriptManager.RegisterStartupScript(this,GetType(), "DeleteModal", "DeleteModal();", true);
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string itemCode = ViewState["ItemCode"] as string;
            string locationCode = ViewState["LocationCode"].ToString();
            string commissionTypeCode = (string)ViewState["CommissionTypeCode"];

            IItemCommissionRepository item = new ItemCommissionRepository();

            var itemC = new ItemCommissions {
                ItemCode = itemCode,
                LocationCode = locationCode,
                CommissionTypeCode = commissionTypeCode
            };

            bool isDelete = item.ItemCommissionDeletes(itemC);

            if (isDelete)
            {
                ShowAlert("Delete item commission is successfully.","success");
                GridBind("", null, null);
            }
            else
            {
                ShowAlert("Delete item commission is failed, please contact to developer.", "danger");
            }
           
        }

        protected void ddlExports_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choices = ddlExports.SelectedValue;
            switch(choices)
            {
                case "Excel":
                    ExportExcel();
                    break;
                case "PDF":
                    ExportPDF();
                    break;
                case "PrintCurrentPriview":
                    CurrentPreview();
                    break;
                case "PrintAllData":
                    PreviewAllData();
                    break;
                default:
                    ShowAlert("Your choosion is not found.","warnning");
                    break;
            }
        }

        private void PreviewAllData()
        {
            IItemCommissionRepository itemCommission = new ItemCommissionRepository();
            string commissionTypeCode = Session["CommissionTypeCode"].ToString();
            var items = itemCommission.ItemCommissionSelects("", commissionTypeCode);

            // Create a DataTable to store the data
            DataTable table = new DataTable();

            // Add table columns (header)
            table.Columns.Add("#");
            table.Columns.Add("Product Category");
            table.Columns.Add("Item Code");
            table.Columns.Add("Description");
            table.Columns.Add("Unit");
            table.Columns.Add("Sale Price");
            table.Columns.Add("Commission");
            table.Columns.Add("Is Percentage?");

            // Add table rows (body)
            foreach (var item in items)
            {
                table.Rows.Add(item.RowNo,
                    item.CategoryName,
                    item.ItemCode,
                    item.SaleDescription,
                    item.UnitSale,
                    item.SalePrice,
                    item.ItemCommission,
                    item.ItemCommissionPecent ? "Yes" : "No");
            }

            // Define the HTML structure and inline CSS
            string htmlContent = @"
            <html><head><title>Print All Data</title>
            <style>
                @page { size: landscape; size: A4; margin: 20mm; }
                body { font-family: Arial, sans-serif; font-size: 14px; color: #333; }
                table { width: 100%; border-collapse: collapse; margin: 20px 0; }
                th, td { padding: 12px; text-align: left; border: 1px dotted #495057; }
                th { background-color: #f2f2f2; color: #333; }
                tr:nth-child(even) { background-color: #f9f9f9; }
                tr:hover { background-color: #f1f1f1; }
                caption { font-size: 18px; font-weight: bold; margin-bottom: 10px; }
            </style></head>
            <body>
                <table>
                    <caption>Item Commission Report</caption>
                    <tr>
                        <th>#</th>
                        <th>Product Category</th>
                        <th>Item Code</th>
                        <th>Description</th>
                        <th>Unit</th>
                        <th>Sale Price</th>
                        <th>Commission</th>
                        <th>Is Percentage?</th>
                    </tr>";

            // Loop through DataTable rows and build HTML table body
            foreach (DataRow row in table.Rows)
            {
                htmlContent += "<tr>";
                foreach (var cell in row.ItemArray)
                {
                    htmlContent += $"<td>{HttpUtility.HtmlEncode(cell.ToString())}</td>";
                }
                htmlContent += "</tr>";
            }

                // Close the table and HTML structure
            htmlContent += "</table></body></html>";

                // Define JavaScript for opening and printing the window
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
            printWindow.print();";

            // Register the script to execute on the client-side
            ScriptManager.RegisterStartupScript(this, GetType(), "PrintAllData", script, true);
        }

        private void CurrentPreview()
        {
            string script = @"
                var printWindow = window.open('', '', 'width=800,height=600');
                var content = document.getElementById('" + gvItemCommission.ClientID + @"').outerHTML;

                var parser = new DOMParser();
                var doc = parser.parseFromString(content, 'text/html');
                var table = doc.querySelector('table');

                // Customize columns (show/hide specific columns)
                var headers = table.querySelectorAll('th');
                var rows = table.querySelectorAll('tr');

                // Example: Hide the ninth column (index starts from 0)
                var columnIndex = 8;

                if (headers.length > columnIndex) {
                    headers[columnIndex].style.display = 'none';  
                }

                rows.forEach(function(row) {
                    var cells = row.querySelectorAll('td');
                    if (cells.length > columnIndex) {
                        cells[columnIndex].style.display = 'none';  
                    }
                });

                // Center the print window
                var width = 800;
                var height = 600;
                var left = (window.innerWidth / 2) - (width / 2);
                var top = (window.innerHeight / 2) - (height / 2);

                printWindow.resizeTo(width, height);
                printWindow.moveTo(left, top);

                // Prepare the custom content for printing
                printWindow.document.open();
                printWindow.document.write('<html><head><title>Print View</title>');
                printWindow.document.write('<style>');
                printWindow.document.write('body { font-family: Arial, sans-serif; font-size: 14px; color: #333; margin: 20px; }');
                printWindow.document.write('table { width: 100%; border-collapse: collapse; margin: 20px 0; border: 1px solid #ddd; }');
                printWindow.document.write('caption { font-size: 18px; font-weight: bold; margin-bottom: 10px; text-align: center; }');
                printWindow.document.write('th, td { padding: 10px; text-align: left; border: 1px solid #ddd; }');
                printWindow.document.write('th { background-color: #f4f4f4; color: #333; font-weight: bold; }');
                printWindow.document.write('th a { text-decoration: none; color: inherit; }');
                printWindow.document.write('tr:nth-child(even) { background-color: #f9f9f9; }');
                printWindow.document.write('tr:hover { background-color: #f1f1f1; }');
                printWindow.document.write('</style>');
                printWindow.document.write('</head><body>');
                printWindow.document.write('<caption>Item Commission Report</caption>');
                printWindow.document.write(table.outerHTML);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
            ";

            // Register the script to be executed on the client-side
            ScriptManager.RegisterStartupScript(this, GetType(), "CurrentPreview", script, true);
        }

        private void ExportPDF()
        {
            IItemCommissionRepository itemCommission = new ItemCommissionRepository();
            string commissionTypeCode = Session["CommissionTypeCode"].ToString();
            var items = itemCommission.ItemCommissionSelects("", commissionTypeCode);

            // Set document to landscape orientation
            var document = new Document(PageSize.A4.Rotate(), 20f, 20f, 30f, 30f);
            //PageSize.A4.Rotate() : landscape
            //PageSize.A4: default is portrait 
            using (var stream = new System.IO.MemoryStream())
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Title and metadata
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                var titleParagraph = new Paragraph("Item Commission Report", titleFont);
                titleParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(titleParagraph);
                document.Add(new Paragraph("\n"));

                // Create table with column widths
                var table = new PdfPTable(8); // Adjust the number of columns
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 2f, 2f, 3f, 1f, 2f, 2f, 2f });

                // Add header cells
                var headers = new[] { "#", "Product Category", "Item Code", "Description", "Unit", "Sale Price", "Commission", "Is Percentage?" };
                foreach (var header in headers)
                {
                    var headerCell = new PdfPCell(new Phrase(header, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(headerCell);
                }

                // Add data rows
                foreach (var item in items)
                {
                    table.AddCell(new PdfPCell(new Phrase(item.RowNo.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(item.CategoryName, normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(item.ItemCode, normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(item.SaleDescription, normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(item.UnitSale, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(item.SalePrice.HasValue ? item.SalePrice.Value.ToString("F2") : "N/A", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    table.AddCell(new PdfPCell(new Phrase(item.ItemCommission.ToString("F2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    table.AddCell(new PdfPCell(new Phrase(item.ItemCommissionPecent ? "Yes" : "No", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                // Add table to document
                document.Add(table);
                document.Close();

                // Return the PDF as a file download
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", $"attachment;filename=ItemCommission_{DateTime.Now:yyyyMMddHHmmss}.pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
        }



        private void ExportExcel()
        {
            //Install-Package EPPlus
            //Install-Package iTextSharp

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            IItemCommissionRepository itemCommission = new ItemCommissionRepository();
            string commissionTypeCode = Session["CommissionTypeCode"].ToString();
            var items = itemCommission.ItemCommissionSelects("", commissionTypeCode);

            var table = new DataTable();
            table.Columns.Add("#");
            table.Columns.Add("Product Category");
            table.Columns.Add("Item Code");
            table.Columns.Add("Description");
            table.Columns.Add("Unit");
            table.Columns.Add("Sale Price");
            table.Columns.Add("Commission");
            table.Columns.Add("Is Percentage?");

            foreach (var item in items)
            {
                table.Rows.Add(item.RowNo,
                     item.CategoryName,
                     item.ItemCode,
                     item.SaleDescription,
                     item.UnitSale,
                     item.SalePrice,
                     item.ItemCommission,
                     item.ItemCommissionPecent ? "Yes" : "No");
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ItemCommission");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (var stream = new System.IO.MemoryStream())
                {
                    package.SaveAs(stream);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", $"attachment;filename=ItemCommission_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
        }
    }
}