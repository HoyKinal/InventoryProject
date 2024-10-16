using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Animation;
using UnitLabrary;
using UnitLabrary.Category;
using UnitLabrary.Item;


namespace WebFormUnit.Form.ItemsForm
{
    public partial class FormAddItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProduCategory();
                LoadUnitMeasurement();

                string itemCode = Request.QueryString["ItemCode"];

                if (!string.IsNullOrEmpty(itemCode))
                {
                    LoadItemDetails(itemCode);
                }
            }
        }
        private void LoadProduCategory()
        {
            Category category = new Category();
            var selectCategory = category.CategorySelects("");

            if (selectCategory != null) 
            { 
                ddlProductCategory.DataSource = selectCategory;
                ddlProductCategory.DataTextField = "CategoryName";
                ddlProductCategory.DataValueField = "CategoryCode"; 
                ddlProductCategory.DataBind();  
            }
        }
        private void LoadUnitMeasurement()
        {
            UnitMeasurement measurement = new UnitMeasurement();
            var load = measurement.SelectUnitMeasurement("");
           
            ddlUnitStock.DataSource = load;
            ddlUnitStock.DataTextField = "UnitFromName";
            ddlUnitStock.DataValueField = "UnitTo";
            ddlUnitStock.DataBind();

            ddlUnitSale.DataSource = load;
            ddlUnitSale.DataTextField = "UnitToName";
            ddlUnitSale.DataValueField = "UnitFrom";
            ddlUnitSale.DataBind();
           
        }

        protected void btnGenerateCode_Click(object sender, EventArgs e)
        {
            long x = 0;
            int i = 0;
            Random rnd = new Random();

            while (i<12)
            {
                x += (long)(Math.Pow(10, i)) * rnd.Next(1, 10);
                i++;
            }

            txtBarCode.Text= x.ToString();  
        }
        public static string CalculateMarkup(decimal cost, decimal salePrice)
        {
            if (cost == 0 || salePrice==0)
            {
                return "0";
            }
            decimal markup = (salePrice - cost) * 100 / cost;
            return markup.ToString("F2"); // example 34.00
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
            txtItemCode.Text = string.Empty;
            ddlType.SelectedIndex = 0;
            ddlProductCategory.SelectedIndex = 0;
            txtBarCode.Text = string.Empty;
            txtPurDescrition.Text = string.Empty;   
            txtCost.Text = string.Empty;
            ddlSoldAccount.SelectedIndex = 0;
            txtSaleDescrition.Text = string.Empty;
            ddlUnitSale.SelectedIndex = 0;
            txtSalePrice.Text = string.Empty;   
            ddlIncomeAccount.SelectedIndex = 0; 
            ddlAssetAccount.SelectedIndex = 0;  
            ddlUnitStock.SelectedIndex = 0;
            txtReorderPoint.Text = "0.00";
            ddlStatus.SelectedIndex = 0;    
        }
        private void SaveItemList()
        {
            string itemCode = txtItemCode.Text;
            Category category = new Category();
            var Ana2LocationCode = category.CategoryTSelectEdits(ddlProductCategory.SelectedValue);
            string locationCode = Ana2LocationCode.Ana2.ToString();
            char itemType = char.Parse(ddlType.SelectedValue);
            string categoryCode = ddlProductCategory.SelectedValue;
            string barCode = txtBarCode.Text;
            string purDescription = txtPurDescrition.Text;
            decimal cost = decimal.Parse(txtCost.Text);
            string coGSAccount = ddlSoldAccount.SelectedValue;
            string saleDescription = txtSaleDescrition.Text;
            string unitSale = ddlUnitSale.SelectedValue;
            decimal salePrice = decimal.Parse(txtSalePrice.Text);
            string incomeAccount = ddlIncomeAccount.SelectedValue;
            string assetAccount = ddlAssetAccount.SelectedValue;
            string unitStock = ddlUnitStock.SelectedValue;
            decimal pointRecorder;
            bool isValidRecorder = decimal.TryParse(txtReorderPoint.Text, out pointRecorder);
            if (isValidRecorder == false)
            {
                ShowAlert("Unknown decimal type for pointRecorder", "danger");
            }
            char stockType = '1'; 
            char itemStatus = char.Parse(ddlStatus.SelectedValue);
            string createBy = "admin";
            DateTime dateCreate = DateTime.Now;
            string modifiedBy = "admin";
            DateTime dateModified = DateTime.Now;
            decimal ana5 = 0m;
            string isProperty = null;

            
            string itemFileCode = DateTime.Now.Ticks.ToString();
            long itemFileSize = fileUpload.PostedFile.ContentLength;
            string itemFileMIME = fileUpload.PostedFile.ContentType;

          
            ItemList itemList = new ItemList();
            ItemFile itemFile = new ItemFile();

          
            var check = itemList.ItemListSelectEdits(itemCode);

            if (check == null)
            {
                bool isInsert = itemList.ItemListInserts(
                    itemCode,                            // itemCode (nvarchar(30))
                    locationCode,             // locationCode (nvarchar(30))
                    itemType,                              // itemType (char(1))
                    categoryCode,                            // categoryCode (nvarchar(30))
                    barCode,                     // barCode (nvarchar(30))
                    purDescription,                         // purDescription (nvarchar(200))
                    100m,                             // cost (numeric(18, 5))
                    coGSAccount,                            // coGSAccount (nvarchar(7))
                    saleDescription,                         // saleDescription (nvarchar(200))
                    unitSale,             // unitSale (nvarchar(30))
                    1000m,                            // salePrice (numeric(18, 5))
                    incomeAccount,                          // incomeAccount (nvarchar(7))
                    assetAccount,                          // assetAccount (nvarchar(7))
                    unitStock,             // unitStock (nvarchar(30))
                    pointRecorder,                               // reorderPoint (numeric(18, 5))
                    stockType,                              // stockType (char(1))
                    itemStatus,                              // itemStatus (char(1))
                    createBy,                          // createdBy (nvarchar(30))
                    dateCreate,                     // dateCreated (datetime)
                    modifiedBy,                          // modifiedBy (nvarchar(30))
                    dateModified,                     // dateModified (datetime)
                    ana5,                               // ana5 (numeric(18, 5))
                    isProperty                             // isProperty (nvarchar(255))
                );


                if (isInsert)
                {
                    string fileName;
                    bool isInserted =false;

                    if (fileUpload.HasFile)
                    {
                        try
                        {
                            string[] validFileTypes = { "jpg", "jpeg", "png", "gif" };
                            string fileExtension = Path.GetExtension(fileUpload.PostedFile.FileName).ToLower();

                            if (Array.Exists(validFileTypes, ext => ext == fileExtension.Substring(1)))
                            {
                                string folderPath = Server.MapPath("~/Images/");

                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }

                                fileName = Guid.NewGuid().ToString() + fileExtension;
                                string filePath = Path.Combine(folderPath, fileName);

                                fileUpload.SaveAs(filePath);

                                isInserted = itemFile.ItemFileInserts(
                                    itemFileCode, itemCode, itemFileSize, fileName, itemFileMIME, createBy, dateCreate, locationCode);
                            }
                            else
                            {
                                lblMessage.Text = "Only Image (.jpg, .jpeg, .png, .gif) can be uploaded";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error: " + ex.Message;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        fileName = "Placeholder.png";
                        isInserted = itemFile.ItemFileInserts(
                            itemFileCode, itemCode, itemFileSize, fileName, itemFileMIME, createBy, dateCreate, locationCode);
                    }

                    if (isInserted)
                    {
                        ShowAlert("Insert ItemList is successfully", "success");
                        ClearFields();
                    }
                    else
                    {
                        ShowAlert("Insert Image is failed, please contact to developer", "danger");
                    }
                    
                }
                else
                {
                    ShowAlert("Insert ItemList has problem, please contact developer", "danger");
                }
            }
            else
            {
                ShowAlert("Insert ItemList is duplicated, try again.", "danger");
            }
        }

        private void LoadItemDetails(string itemCode)
        {
            ItemList itemList = new ItemList();
            var item = itemList.ItemListSelectEdits(itemCode);

            if (item != null)
            {
                // Populate the form fields with item data
                txtItemCode.Text = item.ItemCode;
                ddlType.SelectedValue = item.ItemType.ToString();
                ddlProductCategory.SelectedValue = item.CategoryCode;
                txtBarCode.Text = item.BarCode;
                txtPurDescrition.Text = item.PurDescription;
                txtCost.Text = item.Cost.ToString("F2");
                ddlSoldAccount.SelectedValue = item.CoGSAccount;
                txtSaleDescrition.Text = item.SaleDescription;
                ddlUnitSale.SelectedValue = item.UnitSale;
                txtSalePrice.Text = item.SalePrice?.ToString("F2");
                ddlIncomeAccount.SelectedValue = item.IncomeAccount;
                ddlAssetAccount.SelectedValue = item.AssetAccount;
                ddlUnitStock.SelectedValue = item.UnitStock;
                txtReorderPoint.Text = item.ReorderPoint.ToString("F2");
                ddlStatus.SelectedValue = item.ItemStatus.ToString();

                // Retrieve the image information
                ItemFile itemFile = new ItemFile();
                var fileDetails = itemFile.ItemFileSelectEdits(itemCode);

                // Check if there is a valid file name and update the image control accordingly
                if (fileDetails != null && !string.IsNullOrEmpty(fileDetails.ItemFileName))
                {
                    string imageUrl = "~/Images/" + fileDetails.ItemFileName;
                    imagePreview.ImageUrl = imageUrl;
                    imagePreview.Visible = true;
                }
                else
                {
                    //// If no image is found, use a placeholder image
                    imagePreview.ImageUrl = "~/Images/Placeholder.png";
                    imagePreview.Visible = false;
                }
            }
            else
            {
                ShowAlert("Item not found for editing.", "danger");
            }
        }

        private void ItemListUpdate()
        {
            string itemCode = Request.QueryString["ItemCode"];

            if (string.IsNullOrEmpty(itemCode))
            {
                ShowAlert("Invalid Item Code. Please try again.", "danger");
                return;
            }

            ItemList itemList = new ItemList();

            var check = itemList.ItemListSelectEdits(itemCode);

            if (check != null)
            {
                string locationCode = check.LocationCode; 
                char itemType = char.Parse(ddlType.SelectedValue);
                string categoryCode = ddlProductCategory.SelectedValue;
                string barCode = txtBarCode.Text;
                string purDescription = txtPurDescrition.Text;
                decimal cost;
                if (!decimal.TryParse(txtCost.Text, out cost))
                {
                    ShowAlert("Invalid cost value.", "danger");
                    return;
                }
                string coGSAccount = ddlSoldAccount.SelectedValue;
                string saleDescription = txtSaleDescrition.Text;
                string unitSale = ddlUnitSale.SelectedValue;
                decimal salePrice;
                if (!decimal.TryParse(txtSalePrice.Text, out salePrice))
                {
                    ShowAlert("Invalid sale price value.", "danger");
                    return;
                }
                string incomeAccount = ddlIncomeAccount.SelectedValue;
                string assetAccount = ddlAssetAccount.SelectedValue;
                string unitStock = ddlUnitStock.SelectedValue;
                decimal recorderPoint;
                if (!decimal.TryParse(txtReorderPoint.Text, out recorderPoint))
                {
                    ShowAlert("Invalid reorder point value.", "danger");
                    return;
                }
                char stockType = '1'; 
                char itemStatus = char.Parse(ddlStatus.SelectedValue);
                string createBy = "admin";
                string modifiedBy = "admin";
                DateTime dateModified = DateTime.Now;
                DateTime dateCreated = DateTime.Now;

                bool isUpdated = itemList.ItemListUpdate(itemCode,locationCode,itemType,categoryCode,barCode,purDescription,cost,
                    coGSAccount,saleDescription,unitSale,salePrice,incomeAccount,assetAccount,unitStock,recorderPoint,stockType,
                    itemStatus,createBy,dateCreated,modifiedBy,dateModified);

                if (isUpdated)
                {
                    if (fileUpload.HasFile)
                    {
                        try
                        {
                            string[] validFileType = { "jpg", "jpeg", "png", "gif" };
                            string fileExtension = Path.GetExtension(fileUpload.PostedFile.FileName).ToLower();

                            if (Array.Exists(validFileType, ext => ext == fileExtension.Substring(1)))
                            {
                                string folderPath = Server.MapPath("~/Images/");

                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }

                                string fileName = Guid.NewGuid().ToString() + fileExtension;
                                string filePath = Path.Combine(folderPath, fileName);

                                fileUpload.SaveAs(filePath);

                                ItemFile itemFile = new ItemFile();
                                var fileDetails = itemFile.ItemFileSelectEdits(itemCode);

                                if (!string.IsNullOrEmpty(fileDetails.ItemFileName))
                                {
                                    string oldFilePath = Path.Combine(folderPath, fileDetails.ItemFileName);
                                    if (File.Exists(oldFilePath))
                                    {
                                        File.Delete(oldFilePath);
                                    }
                                }

                                // Generate unique file code
                                string itemFileCode = DateTime.Now.Ticks.ToString();
                                long itemFileSize = fileUpload.PostedFile.ContentLength;
                                string itemFileMIME = fileUpload.PostedFile.ContentType;

                                //update file data
                                

                                bool isFileUpdated = itemFile.UpdateItemFiles(itemCode, itemFileCode, itemFileSize, fileName, itemFileMIME, createBy, dateCreated, locationCode);

                                // Or
                                //bool isFileUpdated = itemFile.UpdateItemFiles(
                                //   itemCode, itemFilecode: DateTime.Now.Ticks.ToString(),
                                //   itemFileSize: fileUpload.PostedFile.ContentLength,
                                //   itemFileName: fileName,
                                //   itemFileMIME: fileUpload.PostedFile.ContentType,
                                //   createBy: "admin",
                                //   dateCreate: DateTime.Now,
                                //   locationCode: "");

                                if (isFileUpdated)
                                {
                                    lblMessage.Text = "Item and image updated successfully!";
                                    lblMessage.ForeColor = System.Drawing.Color.Green;
                                }
                                else
                                {
                                    lblMessage.Text = "Item updated, but failed to update image in the database.";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                }

                            }
                            else
                            {
                                lblMessage.Text = "Only Image (.jpg, .jpeg, .png, .gif) can be uploaded";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }

                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error: " + ex.Message;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Item updated successfully, no image to update.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }

                    ShowAlert("Item updated successfully.", "success");
                    ClearFields();
                }
                else
                {
                    ShowAlert("Update failed, please contact the developer.", "danger");
                }
            }
            else
            {
                ShowAlert("Item not found, please try again.", "danger");
            }
        }

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            string itemCode = Request.QueryString["ItemCode"];
            ItemList itemList = new ItemList();
            var item = itemList.ItemListSelectEdits(itemCode);
            if (item != null)
            {
                ItemListUpdate();
            }
            else
            {
                SaveItemList();
            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            string itemCode = Request.QueryString["ItemCode"];
            ItemList itemList = new ItemList();
            var item = itemList.ItemListSelectEdits(itemCode);
            if (item != null)
            {
                ItemListUpdate();
            }
            else
            {
                SaveItemList();
            }
           
            Thread.Sleep(1000);
            Response.Redirect("~/Form/ItemsForm/FormItems");
        }
    }
}