using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace UnitLabrary
{
    public class ExportFiles
    {
        // Method to export data based on file type
        public void ExportData(DataTable table, string fileName, string contentType, string extension, string title = "", bool rotate = false, string worksheetName = "Sheet1")
        {
            using (var stream = new MemoryStream())
            {
                switch (extension.ToLower())
                {
                    case "pdf":
                        ExportToPDF(table, stream, title, rotate);
                        break;

                    case "xlsx":
                        ExportToExcel(table, stream, worksheetName);
                        break;

                    default:
                        throw new ArgumentException("Unsupported file extension", nameof(extension));
                }

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename={fileName}_{DateTime.Now:yyyyMMddHHmmss}.{extension}");
                HttpContext.Current.Response.ContentType = contentType;
                HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                HttpContext.Current.Response.End();
            }
        }

        // Export data to PDF
        private void ExportToPDF(DataTable table, MemoryStream stream, string title, bool rotate)
        {
            // Initialize the Document object with appropriate orientation
            var document = rotate
                ? new Document(PageSize.A4.Rotate(), 20f, 20f, 30f, 30f)
                : new Document(PageSize.A4, 20f, 20f, 30f, 30f);

            PdfWriter.GetInstance(document, stream);
            document.Open();

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var titleParagraph = new Paragraph(title, titleFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(titleParagraph);
            document.Add(new Paragraph("\n"));

            var pdfTable = new PdfPTable(table.Columns.Count)
            {
                WidthPercentage = 100
            };
            pdfTable.SetWidths(table.Columns.Cast<DataColumn>().Select(c => 1f).ToArray());

            // Add header cells
            foreach (DataColumn column in table.Columns)
            {
                var headerCell = new PdfPCell(new Phrase(column.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                pdfTable.AddCell(headerCell);
            }

            // Add data rows
            foreach (DataRow row in table.Rows)
            {
                foreach (var cell in row.ItemArray)
                {
                    pdfTable.AddCell(new PdfPCell(new Phrase(cell.ToString(), normalFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });
                }
            }

            document.Add(pdfTable);
            document.Close();
        }

        // Export data to Excel
        private void ExportToExcel(DataTable table, MemoryStream stream, string worksheetName)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial; // Set license context
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(worksheetName);
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                package.SaveAs(stream);
            }
        }
    }
}


/*Example*/

/*
 
 private DataTable GetSampleData()
{
    DataTable table = new DataTable();
    table.Columns.Add("ID");
    table.Columns.Add("Name");
    table.Columns.Add("Quantity");
    table.Columns.Add("Price");

    table.Rows.Add("1", "Product A", "10", "20.00");
    table.Rows.Add("2", "Product B", "5", "15.00");
    table.Rows.Add("3", "Product C", "8", "25.00");

    return table;
}
protected void btnExport_Click(object sender, EventArgs e)
{
    DataTable table = GetSampleData(); // Replace this with your actual data retrieval

    string fileName = "ProductReport"; // Base file name
    string contentType = ""; // This will be set based on the file extension
    string extension = "pdf"; // Change to "xlsx" for Excel
    string title = "Product Report"; // Title for the PDF report
    bool rotate = true; // Set to true if you want landscape orientation

    // Set the content type based on file extension
    if (extension.Equals("pdf", StringComparison.OrdinalIgnoreCase))
    {
        contentType = "application/pdf";
    }
    else if (extension.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
    {
        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }
    else
    {
        throw new ArgumentException("Unsupported file extension", nameof(extension));
    }

    ExportFiles exporter = new ExportFiles();
    exporter.ExportData(table, fileName, contentType, extension, title, rotate);
}

 
 */
