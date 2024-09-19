<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormItemCommission.aspx.cs" Inherits="WebFormUnit.Form.ItemsForm.FormItemCommission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
   
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-straight/css/uicons-regular-straight.css'>

    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }
        .custom-header a{
            text-decoration:none;
        }
        .btn-icon {
            background-color: transparent;
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
        .custom-dropdown-wrapper{
            position:relative;
        }
        
    </style>

    <div class="wrapper pt-3">
        <div class="row shadow-sm p-3 mb-4">
            <div class="col-6">
                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="Back" OnClick="btnBack_Click" />
                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-primary" Text="New" />
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center custom-dropdown-wrapper">
                <asp:DropDownList ID="ddlExports" runat="server" CssClass="form-select w-auto" OnSelectedIndexChanged="ddlExports_SelectedIndexChanged">
                    <asp:ListItem Value="" Text="Export Fille" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Excel" Text="Excel" ></asp:ListItem>
                    <asp:ListItem Value="PDF" Text="PDF"></asp:ListItem>
                    <asp:ListItem Value="PrintCurrentPriview" Text="Print current preview" ></asp:ListItem>
                    <asp:ListItem Value="PrintAllData" Text="Print all data"></asp:ListItem>
                </asp:DropDownList>
                <button type="button" class="btn-icon" data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="fi fi-br-menu-dots-vertical icon"></i>
                </button>
                <ul class="dropdown-menu">
                    <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('Excel')" >Export Excel</a></li>
                    <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('PDF')" >Export PDF</a></li>
                    <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('PrintCurrentPriview')" >Current Preview</a></li>
                    <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('PrintAllData')" >Preview All Data</a></li>
                </ul>
            </div>
        </div>

        <!-- Main row -->
        <div class="row">
            <!-- Left Column: Form -->
            <div class="col-md-2">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h5 class="text-primary">Inventory Item</h5>
                        <p class="text-muted">Please select an item to add a commission</p>
                        <div class="form-group mb-3">
                            <label class="form-label">Product Category</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                               
                            </asp:DropDownList>
                        </div>
                        <div class="form-group mb-4">
                            <label class="form-label">Item</label>
                            <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                               
                            </asp:DropDownList>
                        </div>
                        <div class="row mb-3">
                            <div class="col-6">
                                <label class="form-label">Sale Price</label>
                                <asp:TextBox ID="txtSalePrice" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-6">
                                <label class="form-label">Unit</label>
                                <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group mb-3">
                            <label class="form-label">Commission</label>
                            <asp:TextBox ID="txtCommission" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rqfCommission"
                                runat="server"
                                ControlToValidate="txtCommission"
                                ErrorMessage="*require commission as num 0,1 ..."
                                Display="Dynamic"
                                CssClass="text-danger"
                                ValidationGroup="InsertValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="revCommission"
                                runat="server"
                                ControlToValidate="txtCommission"
                                ErrorMessage="*only numbers are allowed"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationExpression="^\d+(\.\d{1,2})?$"
                                ValidationGroup="InsertValidation"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-check mb-4 ps-0">
                            <asp:CheckBox ID="chkPercentage" runat="server" />
                            <label class="form-check-label" for="chkPercentage">Is Percentage?</label>
                        </div>
                        <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary" Text="Insert" OnClick="btnInsert_Click" ValidationGroup="InsertValidation" />
                    </div>
                </div>
            </div>

            <!-- Right Column: Grid List -->
            <div class="col-md-7">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h5 class="text-primary">Inventory Item List</h5>
                        <p class="text-muted">List of inventory items with commission set</p>
                        <asp:GridView ID="gvItemCommission" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="false"
                            EmptyDataText="No items with commissions available."
                            DataKeyNames="CommissionTypeCode,ItemCode,LocationCode"
                            AllowSorting="true"
                            OnSorting="gvItemCommission_Sorting"
                            AllowPaging="true"
                            PageSize="3"
                            OnPageIndexChanging="gvItemCommission_PageIndexChanging"
                            OnRowCommand="gvItemCommission_RowCommand"
                            HeaderStyle-CssClass="custom-header"
                            >
                            <Columns>
                                <asp:BoundField DataField="RowNo" HeaderText="#" SortExpression="RowNo" />
                                <asp:BoundField DataField="CategoryName" HeaderText="Product Category" SortExpression="CategoryName" />
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" />
                                <asp:BoundField DataField="SaleDescription" HeaderText="Description" SortExpression="SaleDescription" />
                                <asp:BoundField DataField="UnitSale" HeaderText="Unit" SortExpression="UnitSale"/>
                                <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" SortExpression="SalePrice" DataFormatString="{0:#,##0.00}$"/>
                                <asp:BoundField DataField="ItemCommission" HeaderText="Commission" SortExpression="ItemCommission" DataFormatString="{0:#,##0.00}$" />
                                <asp:BoundField DataField="ItemCommissionPecent" HeaderText="Is Percentage?" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-decoration-none" CommandName="EditItemCommission" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'>
                                            <i class="fi fi-rr-edit text-primary"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-decoration-none" CommandName="DeleteItemCommission" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'>
                                            <i class="fi fi-rr-trash text-danger"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="Numeric" Position="Bottom"  PageButtonCount="5"/>
                            <PagerStyle BackColor="WhiteSmoke" VerticalAlign="Bottom" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h5 class="text-primary">Commission Type Info</h5>
                        <p class="text-muted">Detailed information of selected commission type</p>
                        <div class="row">
                            <div class="col-5 vstack">
                                <label class="form-label">Name:</label>
                                <label class="form-label">Payable Account:</label>
                                <label class="form-label">Expense Account:</label>
                                <label class="form-label">Status:</label>
                            </div>
                            <div class="col-7 vstack">
                                <asp:Label ID="LabelName" runat="server" CssClass="form-label text-success"></asp:Label>
                                <asp:Label ID="LabelPayableAccount" runat="server" CssClass="form-label text-success"></asp:Label>
                                <asp:Label ID="LabelExpenseAccount" runat="server" CssClass="form-label text-success"></asp:Label>
                                <asp:Label ID="LabelStatus" runat="server" CssClass="form-label text-success"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- Modal Delete --%>
        <div id="modalDelete" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Delete ItemCommission</h5>
                    </div>
                    <div class="modal-body">
                        <p class="text-muted">Are you sure want to delete this item.</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Confirm" OnClick="btnDelete_Click" />
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
     
    </div>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function removeNonNumeric(input) {
            //input.value = input.value.replace(/\D/g,''); // \D : if non-num will remove
            input.value = input.value.replace(/[^0-9.]/g, ''); // Allow only numbers and dot
            if (input.value.split('.').length > 2) {
                input.value = input.value.replace(/\.+$/, ""); // Remove any extra dots
            }
        }
        function DeleteModal() {
            var alert = new bootstrap.Modal(document.getElementById('modalDelete'));
            alert.show();
        }
        function selectDropdownItem(value) {
            var dropdown = document.getElementById('<%= ddlExports.ClientID %>');
            dropdown.value = value;
            // Trigger a postback to ensure the server-side event is fired
            __doPostBack('<%= ddlExports.ClientID %>', '');
        }
    </script>
</asp:Content>
