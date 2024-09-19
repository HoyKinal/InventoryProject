using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Category;

namespace WebFormUnit.Form.CategoryGroupForm
{
    public partial class FormCategoryGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;

                GridBind("");
            }
        }

        private void GridBind(string search)
        {
            CategoryGroup categoryGroup = new CategoryGroup();
            var group = categoryGroup.CategoryGroupSelects(search.Trim());

            gvCategoryGroupList.DataSource = group;
            gvCategoryGroupList.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridBind(txtSearch.Text);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showAdd", "showAdd();", true);
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"
                var alertDiv = document.createElement('div');
                alertDiv.className = 'alert alert-{type}';
                alertDiv.role = 'alert';
                alertDiv.innerHTML = '{HttpUtility.JavaScriptStringEncode(message)}';
                document.body.insertBefore(alertDiv, document.body.firstChild);

                setTimeout(function() {{
                    alertDiv.style.display = 'none';
                    alertDiv.remove();
                }}, 2000); 
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", script, true);
        }

        private void ClearField()
        {
            txtCategoryGroupName.Text = string.Empty;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string categoryGroupName = txtCategoryGroupName.Text.Trim();

            if (!string.IsNullOrWhiteSpace(categoryGroupName))
            {
                CategoryGroup categoryGroup = new CategoryGroup();

                bool isInsert = categoryGroup.CategoryGroupInserts(Guid.NewGuid().ToString(), categoryGroupName);

                if (isInsert)
                {
                    ShowAlert("Insert category group is successful", "success");
                    GridBind("");
                    ClearField();
                }
                else
                {
                    ShowAlert("Insert category group encountered a problem. Please contact the developer.", "danger");
                }
            }
        }

        protected void gvCategoryGroupList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string categoryGroupID = e.CommandArgument.ToString();

            if (e.CommandName == "UpdateCG")
            {
                hfCategoryGroupId.Value = categoryGroupID;

                CategoryGroup categoryGroup = new CategoryGroup();

                var check = categoryGroup.CategoryGroupSelectEdits(categoryGroupID);

                if (check != null)
                {
                    txtEditCategoryGroupName.Text = check.CategoryGroupName;

                    ScriptManager.RegisterStartupScript(this, GetType(), "showEdit", "showEdit();", true);
                }
            }
            else if (e.CommandName == "DeleteCG")
            {
                hidenFieldDelete.Value = categoryGroupID;
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlertDelete", "showAlertDelete();", true);
            }
            else if(e.CommandName == "InsertCG")
            {
                Response.Redirect("~/Form/CategoryGroupForm/UpdateFKCategory.aspx?categoryGroupID=" + Server.UrlEncode(categoryGroupID));
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            CategoryGroup category = new CategoryGroup();

            bool isUpdate = category.CategoryGroupUpdates(hfCategoryGroupId.Value, txtEditCategoryGroupName.Text);

            if (isUpdate)
            {
                GridBind("");
                ShowAlert("Update record is successfully", "success");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CategoryGroup categoryGroup = new CategoryGroup();

            bool isDeleted = categoryGroup.CategoryGroupDeletes(hidenFieldDelete.Value);

            if (isDeleted)
            {
                ShowAlert("Category group deleted successfully.", "success");
                GridBind("");
            }
            else
            {
                ShowAlert("Error deleting, please check item at CategoryItem", "danger");
            }
        }

        //Install-Package EPPlus
        //Install-Package iTextSharp

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string selectedValue = ddlExport.SelectedValue; 

            switch(selectedValue)
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

        private void ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            CategoryGroup categoryGroup = new CategoryGroup();
            var data = categoryGroup.CategoryGroupSelects("");

            //Data Table
            var dt = new DataTable();
            dt.Columns.Add("CategoryGroupID");
            dt.Columns.Add("CategoryGroupName");

            foreach (var item in data)
            {
                dt.Rows.Add(item.CategoryGroupId,item.CategoryGroupName);  
            }
            
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
               var worksheet = package.Workbook.Worksheets.Add("CategoryGroups");
               worksheet.Cells["A1"].LoadFromDataTable(dt,true);

                using (var stream = new System.IO.MemoryStream())
                {
                    package.SaveAs(stream);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=CategoryGroups.xlsx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
        }
        private void ExportToPDF()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            CategoryGroup categoryGroup = new CategoryGroup();
            var data = categoryGroup.CategoryGroupSelects("");

            //Data Table
            var dt = new DataTable();
            dt.Columns.Add("CategoryGroupID");
            dt.Columns.Add("CategoryGroupName");

            foreach (var item in data)
            {
                dt.Rows.Add(item.CategoryGroupId, item.CategoryGroupName);
            }
            using (var stream = new System.IO.MemoryStream())
            {
                var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                PdfWriter.GetInstance(document, stream);
                document.Open();

                var table = new PdfPTable(dt.Columns.Count);
                foreach (System.Data.DataColumn column in dt.Columns)
                {
                    table.AddCell(new Phrase(column.ColumnName));
                }
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    foreach (var cell in row.ItemArray)
                    {
                        table.AddCell(new Phrase(cell.ToString()));
                    }
                }

                document.Add(table);
                document.Close();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=CategoryGroups.pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
        }
        protected void PrintCurrentView()
        {
            string script = @"
                                function printCurrentView() {
                                    var width = 800;
                                    var height = 600;
            
                                    var left = (window.innerWidth / 2) - (width / 2);
                                    var top = (window.innerHeight / 2) - (height / 2);

                                    var printWindow = window.open('', '', 'width=' + width + ',height=' + height + ',left=' + left + ',top=' + top);
                                    printWindow.document.write('<html><head><title>Print View</title></head><body>');
                                    printWindow.document.write(document.getElementById('" + gvCategoryGroupList.ClientID + @"').outerHTML);
                                    printWindow.document.write('</body></html>');
                                    printWindow.document.close();
                                    printWindow.print();
                                }
                                printCurrentView();
                            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "printView", script, true);
           // ScriptManager.RegisterStartupScript(this, GetType(), "printCurrentView", "printCurrentView();", true);
        }
        private void PrintAllData()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            CategoryGroup categoryGroup = new CategoryGroup();
            var data = categoryGroup.CategoryGroupSelects("");

            var dt = new System.Data.DataTable();
            dt.Columns.Add("CategoryGroupID");
            dt.Columns.Add("CategoryGroupName");

            foreach (var item in data)
            {
                dt.Rows.Add(item.CategoryGroupId, item.CategoryGroupName);
            }

            StringBuilder html = new StringBuilder();
            html.Append("<html><head><title>Print All Data</title></head><body>");
            html.Append("<table border='1'><tr><th>CategoryGroupID</th><th>CategoryGroupName</th></tr>");

            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (var cell in row.ItemArray)
                {
                    html.Append($"<td>{cell}</td>");
                }
                html.Append("</tr>");
            }

            html.Append("</table></body></html>");

            string script = $@"
                            var printWindow = window.open('', '', 'width=800,height=600');
                            printWindow.document.open();
                            printWindow.document.write('{html.ToString().Replace("'", "\\'").Replace("\n", "")}');
                            printWindow.document.close();
                            printWindow.print();
                        ";

            ScriptManager.RegisterStartupScript(this, GetType(), "printAll", script, true);
        }
        
        //Handle the PageIndexChanging Event
        protected void gvCategoryGroupList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategoryGroupList.PageIndex = e.NewPageIndex;
            GridBindPagination(txtSearch.Text); // Rebind the data with the current search text
        }
      
        //Bind Data with Pagination
        private void GridBindPagination(string search)
        {
            CategoryGroup categoryGroup = new CategoryGroup();  
            var allData = categoryGroup.CategoryGroupSelects(search.Trim());

            //calculate the total number of pages
            int totalRecords = allData.Count;
            int pageSize = gvCategoryGroupList.PageSize;
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            if (gvCategoryGroupList.PageIndex<0 || gvCategoryGroupList.PageIndex>=totalPages)
            {
                gvCategoryGroupList.PageIndex = 0;
            }

            //fetch the data for the current page
            var pageData = allData.Skip(gvCategoryGroupList.PageIndex * pageSize).Take(pageSize).ToList();   

            gvCategoryGroupList.DataSource = pageData;
            gvCategoryGroupList.DataBind();
        }
    }
}
