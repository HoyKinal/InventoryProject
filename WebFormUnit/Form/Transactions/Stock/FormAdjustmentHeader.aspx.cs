using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.CustomFunction;
using UnitLabrary.Stocks;

namespace WebFormUnit.Form.Transactions.Stock
{
    public partial class FormAdjustmentHeader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadField();
            }
        }
        private void LoadField()
        {
            txtAdjustmentNo.Text = GenerateAdjustmentNo();
            txtAdjustmentDate.Text = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
        }
        private string GenerateAdjustmentNo()
        {
            string header = "STOCK-";
            string chars = "1234567890";
            int length = 8;

            Random random = new Random();

            string randomPart = new string(Enumerable.Repeat(chars, length).Select(r => r[random.Next(r.Length)]).ToArray());

            return header + randomPart; 
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
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            AdjustomerHeaderModel m = new AdjustomerHeaderModel()
            {
                AdjustmentNo = txtAdjustmentNo.Text,
                AdjustmentDate = txtAdjustmentDate.Text.ConvertDateTime(),
                Memo = txtAdjustmentMemo.Text,
                AdjustmentStatus = true
            };

            AdjustmentHeader ah = new AdjustmentHeader();

            bool isInsert = ah.AdjustmentHeaderInserts(m);

            if (isInsert)
            {
                ShowAlert("Adjustment Item stock is recorded on system.","success");

                Response.Redirect($"~/Form/Transactions/Stock/FormAdjustmentAddItemDetail?AdjustmentNoFromHeader={Server.UrlEncode(txtAdjustmentNo.Text)}");
            }
            else
            {
                ShowAlert("Adjustment stock's item is failed.","danger");
            }
        }
    }
}