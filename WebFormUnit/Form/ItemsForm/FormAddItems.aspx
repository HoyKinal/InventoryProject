<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormAddItems.aspx.cs" Inherits="WebFormUnit.Form.ItemsForm.FormAddItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }

        .wrapper {
            padding-top: 10px;
        }

        .upload-container {
            text-align: center;
            margin-top: 0px;
        }


        .image-upload-box .preview-img {
            width: 200px;
            height: 200px;
            display: block;
            object-fit: cover;
            box-shadow: 5px 5px 10px rgba(0,0,0,0.3);
            border: 1px dotted #000;
            border-radius: 5px;
        }

        .image-upload-box {
            width: 200px;
            height: 200px;
            border: 2px dashed #ccc;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
        }

        .hideUpload {
            display: none;
        }
    </style>
    <div class="wrapper mt-1">
        <div class="row">
            <div class="col-6">
                <h5 class="text-primary">Item</h5>
                <small class="text-muted">Make change to your item information in the list below.</small>
                <nav aria-label="breadcrumb" class="hstack gap-2">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item"><a href="#">Warehouse</a></li>
                        <li class="breadcrumb-item active" aria-current="page">Items</li>
                    </ol>
                </nav>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <asp:Button ID="btnSaveNew" runat="server" CssClass="btn btn-primary me-2" Text="SaveNew" OnClick="btnSaveNew_Click" ValidationGroup="ItemList" />
                <asp:Button ID="btnSaveClose" runat="server" CssClass="btn btn-primary" Text="SaveClose" OnClick="btnSaveClose_Click" />
            </div>
        </div>
        <div class="container w-50 border border-warning-subtle shadow-sm p-3 border rounded">
            <div class="row">
                <div class="col-8">
                    <div class="row">
                        <div class="col-6 mb-2">
                            <label for="Type" class="form-label h6 text-muted">Type</label>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select">
                                <asp:ListItem Value="1" Text="Inventory Part" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="None Inventory Part"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Service"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Inventory Assembly"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Group Assembly"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-6 mb-2">
                            <label for="ProductCategory" class="form-label h6 text-muted">Product Category</label>
                            <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-select">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-6 mb-2">
                            <label for="ItemCode" class="form-label h6 text-muted">Item Code</label>
                            <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfItemCode" runat="server"
                                ControlToValidate="txtItemCode"
                                ErrorMessage="*ItemCode require"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationGroup="ItemList"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-6 mb-2">
                            <label for="BarCode" class="form-label h6 text-muted">Bar-Code</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtBarCode" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Button ID="btnGenerateCode" runat="server" CssClass="btn btn-primary" Text="Generate" OnClick="btnGenerateCode_Click" />
                            </div>
                            <asp:RequiredFieldValidator
                                ID="rqfBarCode"
                                runat="server"
                                ControlToValidate="txtBarCode"
                                ErrorMessage="*Require BarCode"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationGroup="ItemList"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-6 mb-2">
                            <label for="Status" class="form-label h6 text-muted">Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                                <asp:ListItem Value="A" Text="Active" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="D" Text="Disable"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <label for="fileUpload" class="form-label h6 text-muted">Item Photo</label>
                    <div class="upload-container">
                        <div class="image-upload-box">
                            <asp:Image ID="imagePreview" runat="server" CssClass="preview-img" ImageUrl="~/Images/Placeholder.png" />
                        </div>
                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control hideUpload mt-2" OnChange="PreviewImage(this)" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-2" Visible="false"></asp:Label>
                    </div>
                </div>

                <script type="text/javascript">
                    function PreviewImage(input) {
                        var file = input.files[0];
                        if (file) {
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                document.getElementById('<%= imagePreview.ClientID %>').src = e.target.result;
                            }
                            reader.readAsDataURL(file);
                        }
                    }
                </script>

            </div>
            <div class="row">
                <div class="col-12 mb-2">
                    <h5 class="text-primary">Purchase Info</h5>
                    <small class="text-muted">Please fill in information and assign cost of goods sold account</small>
                </div>
                <div class="col-4 mb-2">
                    <label for="PurchaseDesc" class="form-label h6 text-muted">Description on Purchase Transaction</label>
                    <asp:TextBox ID="txtPurDescrition" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-4 mb-2">
                    <label for="Cost" class="form-label h6 text-muted">Cost</label>
                    <asp:TextBox ID="txtCost" runat="server" CssClass="form-control"></asp:TextBox>
                    <%--  OnChange="markupCost()" --%>
                    <asp:RequiredFieldValidator
                        ID="rqfCost"
                        runat="server"
                        ControlToValidate="txtCost"
                        ErrorMessage="*Require Cost"
                        CssClass="text-danger"
                        Display="Dynamic"
                        ValidationGroup="ItemList"></asp:RequiredFieldValidator>
                </div>
                <div class="col-4 mb-2">
                    <label for="SoldAccount" class="form-label h6 text-muted">Cost of Goods Sold Account</label>
                    <asp:DropDownList ID="ddlSoldAccount" runat="server" CssClass="form-select">
                        <asp:ListItem Value="5000" Text="5000-Cost of Goods Sold" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="5001" Text="5001-COGS-White-Platinum"></asp:ListItem>
                        <asp:ListItem Value="5002" Text="5002-COGS-Red-Platinum"></asp:ListItem>
                        <asp:ListItem Value="5003" Text="5003-COGS-Diamonds"></asp:ListItem>
                        <asp:ListItem Value="5004" Text="5004-COGS-Gold"></asp:ListItem>
                        <asp:ListItem Value="5005" Text="5005-COGS-Order"></asp:ListItem>
                        <asp:ListItem Value="5006" Text="5005-COGS-Shell(VLJ)"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-4 mb-2">
                    <label for="ReorderPoint" class="form-label h6 text-muted">Reorder Point</label>
                    <asp:TextBox ID="txtReorderPoint" runat="server" CssClass="form-control" Text="0.00"></asp:TextBox>
                </div>
                <div class="col-4 mb-2">
                    <label for="UnitStock" class="form-label h6 text-muted">Unit Stock </label>
                    <asp:DropDownList ID="ddlUnitStock" runat="server" CssClass="form-select">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-4 mb-2">
                    <label for="AssetAccount" class="form-label h6 text-muted">Asset Account</label>
                    <asp:DropDownList ID="ddlAssetAccount" runat="server" CssClass="form-select">
                        <asp:ListItem Value="1410" Text="1410-Inventory" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="14101" Text="14101-WH-White-Plactinum"></asp:ListItem>
                        <asp:ListItem Value="14102" Text="14102-WH-Red-Platinum"></asp:ListItem>
                        <asp:ListItem Value="14103" Text="14103-WH-Diamonds"></asp:ListItem>
                        <asp:ListItem Value="14104" Text="14104-WH-Gold"></asp:ListItem>
                        <asp:ListItem Value="14105" Text="14105-WH-Order"></asp:ListItem>
                        <asp:ListItem Value="14106" Text="14105-WH-Shell(VLJ)"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-12 mb-2">
                    <h5 class="text-primary">Sale info</h5>
                    <small class="text-muted">Please fill in information and assign income account</small>
                </div>
                <div class="col-12 mb-2">
                    <div class="row">
                        <div class="col-4 mb-2">
                            <label for="SaleDescrition" class="form-label h6 text-muted">Description on Sale Transaction</label>
                            <asp:TextBox ID="txtSaleDescrition" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-4 mb-2">
                            <label for="UnitSale" class="form-label h6 text-muted">Unit Sale</label>
                            <asp:DropDownList ID="ddlUnitSale" runat="server" CssClass="form-select">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-4 mb-2">
                    <label for="SalePrice" class="form-label h6 text-muted">Sale Price</label>
                    <asp:TextBox ID="txtSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
                    <%-- OnChange="markupCost()" --%>
                    <asp:RequiredFieldValidator
                        ID="rqfSalePrice"
                        runat="server"
                        ControlToValidate="txtSalePrice"
                        ErrorMessage="*Require Sale Price"
                        CssClass="text-danger"
                        Display="Dynamic"
                        ValidationGroup="ItemList"></asp:RequiredFieldValidator>
                </div>
                <div class="col-4 mb-2">
                    <label for="Markup" class="form-label h6 text-muted">Markup %</label>
                    <asp:TextBox ID="txtMarkup" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-4 mb-2">
                    <label for="IncomeAccount" class="form-label h6 text-muted">Income Account</label>
                    <asp:DropDownList ID="ddlIncomeAccount" runat="server" CssClass="form-select">
                        <asp:ListItem Value="4000" Text="4000-Revenue" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="40001" Text="40001-SR-White-Plactinum"></asp:ListItem>
                        <asp:ListItem Value="40002" Text="40002-SR-Red-Platinum"></asp:ListItem>
                        <asp:ListItem Value="40003" Text="40003-SR-Diamonds"></asp:ListItem>
                        <asp:ListItem Value="40004" Text="40004-SR-Gold"></asp:ListItem>
                        <asp:ListItem Value="40005" Text="40005-SR-Order"></asp:ListItem>
                        <asp:ListItem Value="40006" Text="40006-SR-Shell(VLJ)"></asp:ListItem>
                        <asp:ListItem Value="40007" Text="40007-Stock Income"></asp:ListItem>
                        <asp:ListItem Value="40008" Text="40008-Tax Income"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <script>
       <%-- function markupCost() {
            var cost = parseFloat(document.getElementById("<%=txtCost.ClientID%>").value);
            var salePrice = parseFloat(document.getElementById("<%=txtSalePrice.ClientID%>").value);
            var markUp = document.getElementById("<%=txtMarkup.ClientID%>");

            // Check if cost or salePrice is not a number or is zero
            if (isNaN(cost) || cost === 0 || isNaN(salePrice) || salePrice === 0) {
                markUp.value = "";
                return;
            }

            // Calculate markup percentage
            var calculatedMarkUp = ((salePrice - cost) * 100) / cost;
            markUp.value = calculatedMarkUp.toFixed(2); // Round to 2 decimal places
        }--%>
        //Or
        function markupCost() {
            var cost = parseFloat(document.getElementById("<%=txtCost.ClientID%>").value);
            var salePrice = parseFloat(document.getElementById("<%=txtSalePrice.ClientID%>").value);
            var markUp = document.getElementById("<%=txtMarkup.ClientID%>");

            if (isNaN(cost) || cost === 0 || isNaN(salePrice) || salePrice === 0) {
                markUp.value = "";
                return;
            }

            var calculatedMarkUp = ((salePrice - cost) * 100) / cost;
            markUp.value = calculatedMarkUp.toFixed(2);
        }

        document.addEventListener("DOMContentLoaded", function () {
            var costField = document.getElementById("<%=txtCost.ClientID%>");
            var salePriceField = document.getElementById("<%=txtSalePrice.ClientID%>");

            costField.addEventListener("input", markupCost);
            salePriceField.addEventListener("input", markupCost);
        });

        $(document).ready(function () {
            $('#<%= imagePreview.ClientID %>').click(function () {
                $('#<%= fileUpload.ClientID %>').trigger('click');
            });
        });
    </script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.bundle.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
</asp:Content>
