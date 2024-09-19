<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormAddPartToAssembly.aspx.cs" Inherits="WebFormUnit.Form.ItemsForm.FormAddPartToAssembly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-straight/css/uicons-regular-straight.css'>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }

        .btn-icon {
            background: transparent;
            border: none;
            font-size: 20px;
            color: #0094ff;
        }

        .dropdown-menu {
            min-width: 160px;
            padding: 0.5rem 0;
            border: 1px solid #ced4da;
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
        }

        .dropdown-item {
            padding: 0.5rem 1rem;
            font-size: 0.875rem;
            color: #495057;
            transition: background-color 0.15s, color 0.15s;
        }

        .dropdown-item:hover {
            background-color: #f8f9fa;
            color: #0094ff;
        }

        .icon {
            font-size: 1.25rem;
        }

        .custom-dropdown-wrapper {
            position: relative;
        }

        .d-none {
            display: none !important;
        }
    </style>

    <div class="wrapper pt-3">
        <div class="row">
            <div class="col-6">
                <div class="vstack gap-2">
                    <h4 class="text-primary mb-0">Inventory Part</h4>
                    <p class="text-muted">Please select inventory parts to assemble inventory assembly</p>
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="https://localhost:44351">Home</a></li>
                            <li class="breadcrumb-item"><a href="https://localhost:44351/Form/ItemsForm/FormItems">Warehouse</a></li>
                            <li class="breadcrumb-item"><a href="https://localhost:44351/Form/ItemsForm/FormItems">ItemList</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Add Part to Assembly</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <div class="custom-dropdown-wrapper">
                    <asp:DropDownList ID="ddlBackNew" runat="server" CssClass="form-select w-auto text-center d-none" AutoPostBack="true" OnSelectedIndexChanged="ddlBackNew_SelectedIndexChanged">
                        <asp:ListItem Selected="True">:</asp:ListItem>
                        <asp:ListItem Value="0" Text="Back"></asp:ListItem>
                        <asp:ListItem Value="1" Text="New"></asp:ListItem>
                    </asp:DropDownList>
                    <button type="button" class="btn-icon" data-bs-toggle="dropdown" aria-expanded="false">
                        <i class="fi fi-br-menu-dots-vertical icon"></i>
                    </button>
                    <ul class="dropdown-menu">
                        <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('0')">Back</a></li>
                        <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('1')">New</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="row">
            <%-- Card1 --%>
            <div class="col-3">
                <div class="card border-light">
                    <div class="card-body shadow-sm">
                        <h5 class="card-title text-primary">Assembly Information</h5>
                        <div class=" d-flex align-items-center">
                            <small class="text-muted me-3">Detailed information of inventory assembly</small>
                            <div class="flex-grow-1 border-bottom"></div>
                        </div>
                        <div class="input-group mb-1 mt-3">
                            <h6>Name:</h6>
                            <asp:Label ID="lbName" runat="server" class="text-muted h6"></asp:Label>
                        </div>
                        <div class="input-group mb-1">
                            <h6>Category:</h6>
                            <asp:Label ID="lbCategory" runat="server" class="text-muted h6"></asp:Label>
                        </div>
                        <div class="input-group mb-1">
                            <h6>Sale Price:</h6>
                            <asp:Label ID="lbSalePrice" runat="server" class="text-muted h6"></asp:Label>
                        </div>
                        <div class="input-group mb-1">
                            <h6>Income Account:</h6>
                            <asp:Label ID="lbIncomeAccount" runat="server" class="text-muted h6"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <%-- Card2 --%>
            <div class="col-6">
                <div class="card border-light">
                    <div class="card-body shadow-sm">
                        <div class="row">
                            <div class="col-6">
                                <h5 class="text-success">Invetory Part List</h5>
                                <div class=" d-flex align-items-center">
                                    <div class="small me-3">List of selected inventory parts</div>
                                    <div class="flex-grow-1 border-bottom"></div>
                                </div>
                            </div>
                            <div class="col-6 text-end">
                                <h5 class="text-primary">Total Bill of Meterial Cost</h5>
                                <%--<label class="fw-bold text-danger h3">Total: <span id="totalLabel">0.00</span></label><br />--%>
                                <asp:Label ID="lbDisplaySalePrice" runat="server" CssClass="fw-bold text-danger h3"></asp:Label>
                            </div>
                        </div>
                        <asp:GridView ID="gvItemAssembly" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped mt-3"
                            EmptyDataText="Empty Data information."
                            DataKeyNames="ItemCode,AssemblyCode,LocationCode"
                            OnRowCommand="gvItemAssembly_RowCommand"
                            OnRowDataBound="gvItemAssembly_RowDataBound"
                            AllowPaging="true"
                            PageSize="5"
                            OnPageIndexChanging="gvItemAssembly_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                <asp:BoundField DataField="PurchaseDesc" HeaderText="Description" />
                                <asp:BoundField DataField="ItemType" HeaderText="Type" />
                                <asp:BoundField DataField="Cost" HeaderText="Cost" DataFormatString="{0:#,##0.00;(#,##0.00)}" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="UnitName" HeaderText="Unit" />
                                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:F2}$" />
                                <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary text-decoration-none" CommandName="EditItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-danger text-decoration-none" CommandName="DeleteItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
            <%-- Card3 --%>
            <div class="col-3">
                <div class="card border-light">
                    <div class="card-body shadow-sm">
                        <h5 class="text-muted">Inventory Part</h5>
                        <div class="mt-3">
                            <label for="ProCategory" class="form-label">Product Category</label>
                            <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged">
                                <asp:ListItem Text="All" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="mt-3">
                            <label for="ProCategory" class="form-label">Item Code</label>
                            <asp:DropDownList ID="ddlItemCode" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged">
                                <asp:ListItem Text="Select Items" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <div class="mt-3">
                                    <label for="ProCategory" class="form-label">Quantity</label>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" oninput="calculateTotal()"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="rqfQuantity"
                                        runat="server"
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        ControlToValidate="txtQuantity"
                                        ErrorMessage="*Require quantity"
                                        ValidationGroup="catchWhenInsert"></asp:RequiredFieldValidator>
                                </div>
                                <div class="mt-3">
                                    <label for="Cost" class="form-label">Cost</label>
                                    <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary mt-3" Text="Insert" OnClick="btnInsert_Click" ValidationGroup="catchWhenInsert" />
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="mt-3">
                                    <label for="Unit" class="form-label">Unit</label>
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Select Unit Bill" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="mt-3">
                                    <label for="Total" class="form-label">Total</label>
                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card border-light mt-3">
                    <div class="card-body shadow-sm">
                        <h5 class="text-primary">Item Information</h5>
                        <div class="d-flex align-items-center">
                            <small class="me-3 text-muted">Detailed information of inventory assembly</small>
                            <div class="flex-grow-1 border-bottom"></div>
                        </div>
                        <div class="row">
                            <div class="col-6 pt-4 vstack">
                                <label class="h6">Type</label>
                                <label class="h6">Category</label>
                                <label class="h6">Name</label>
                                <label class="h6">Sale Price</label>
                                <label class="h6">Stock Quantity</label>
                                <label class="h6">Average Cost</label>
                                <label class="h6">Unit Sale</label>
                                <label class="h6">Unit Stock</label>
                                <label class="h6">Revenue Account</label>
                                <label class="h6">COGS Account</label>
                                <label class="h6">Inventory Asset</label>
                            </div>
                            <div class="col-6 pt-2 vstack g-3">
                                <asp:Label ID="LabelType" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelCategory" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelName" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelSalePrice" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelStockQuantity" runat="server" CssClass="text-success h6" Text="0"></asp:Label>
                                <asp:Label ID="LabelAverageCost" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelUnitSale" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelUnitStock" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelRevenueAccount" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelCOGSAccount" runat="server" CssClass="text-success h6"></asp:Label>
                                <asp:Label ID="LabelInventoryAsset" runat="server" CssClass="text-success h6"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- Modal delete alert --%>
            <div id="modalAlertDelete" class="modal fade">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Delete Modal</h5>
                        </div>
                        <div class="modal-body">
                            <p class="text-primary">Are you sure you want to delete this record?</p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Confirm" OnClick="btnDelete_Click" />
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        //dropdown list back and new 
        function selectDropdownItem(value) {
            var dropdown = document.getElementById('<%= ddlBackNew.ClientID %>');
            dropdown.value = value;
            // Trigger a postback to ensure the server-side event is fired
            __doPostBack('<%= ddlBackNew.ClientID %>', '');
        }
        function calculateTotal() {
            
            let qtyField = document.getElementById('<%= txtQuantity.ClientID %>');
            let costField = document.getElementById('<%= txtCost.ClientID %>');
            let totalField = document.getElementById('<%= txtTotal.ClientID %>');

         
            if (qtyField && costField && totalField) {
                
                let qty = parseFloat(qtyField.value) || 0;
                let cost = parseFloat(costField.value) || 0;

                let total = (qty * cost).toFixed(2);

                totalField.value = total;
            } else {
                console.error('One or more elements not found.');
            }
        }


        function modalDeleteAlert() {
            var deleteAlert = new bootstrap.Modal(document.getElementById('modalAlertDelete'));
            deleteAlert.show();
        }

       <%-- function calculateTotal() {
            var grid = document.getElementById('<%= gvItemAssembly.ClientID %>');
            var total = 0;

            // Start from row index 1 (assuming row 0 is the header)
            for (var i = 1; i < grid.rows.length; i++) {
                // Assuming 'Total' is in the last column (7th column, index 6)
                // Adjust the column index based on your GridView column structure
                var cellValue = grid.rows[i].cells[6].innerText || grid.rows[i].cells[6].textContent; // This is the 'Total' column
                total += parseFloat(cellValue.replace(/[^\d.-]/g, '')) || 0; // Parse the value and remove non-numeric characters
            }

            // Update the Total Label with the calculated total
            document.getElementById('totalLabel').innerText = total.toFixed(2);
        }

        // Automatically calculate total when the page is fully loaded
        window.onload = function () {
            calculateTotal();  // Call the function after the page loads
        };--%>

    </script>

    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
</asp:Content>
