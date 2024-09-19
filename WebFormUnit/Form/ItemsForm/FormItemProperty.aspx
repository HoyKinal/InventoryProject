<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormItemProperty.aspx.cs" Inherits="WebFormUnit.Form.ItemsForm.FormItemProperty" %>

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
    </style>

    <div class="wrapper pt-3">
        <div class="row shadow-sm">
            <div class="col-6">
                <h5 class="text-primary">Item Property List</h5>
                <p>Make change to item properties in the list below.</p>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <a id="btnAddPop" href="#" class="btn btn-primary">Add</a>
                <%-- OnClientClick="loadAddModal();return false" --%>
            </div>
        </div>

        <div class="input-group w-25 py-4">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" PlaceHolder="Search..."></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary text-light" Text="Search" OnClick="btnSearch_Click" />
        </div>

        <%-- GridView --%>
        <asp:GridView ID="gvItemProperty" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped"
            EmptyDataText="Empty Information Item Property"
            DataKeyNames="PropertyId,LocationCode" OnRowCommand="gvItemProperty_RowCommand"
            AllowPaging="true"
            PageSize="5"
            OnPageIndexChanging="gvItemProperty_PageIndexChanging"
            AllowSorting="true"
            OnSorting="gvItemProperty_Sorting"
            >
            <Columns>
                <asp:BoundField DataField="RowNo" HeaderText="#" SortExpression="RowNo" />
                <asp:BoundField DataField="PropertyName" HeaderText="Property Name" SortExpression="PropertyName" />
                <asp:BoundField DataField="PropertyOrder" HeaderText="Order" SortExpression="PropertyOrder" />
                <asp:BoundField DataField="PropertyStatus" HeaderText="Status" SortExpression="PropertyStatus" />
                <asp:BoundField DataField="ModifyDate" HeaderText="Modified Date" SortExpression="ModifyDate" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-decoration-none" CommandName="EditItemProperty" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'>
                            <i class="fi fi-rr-edit text-primary"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-decoration-none" CommandName="DeleteItemProperty" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'>
                            <i class="fi fi-rr-trash text-danger"></i>
                        </asp:LinkButton>
                        <asp:HiddenField ID="hfPropertyId" runat="server" Value='<%# Eval("PropertyId") %>' />
                        <asp:HiddenField ID="hfLocationCode" runat="server" Value='<%# Eval("LocationCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="5" />
            <PagerStyle BackColor="Window" Height="50px" VerticalAlign="Bottom" HorizontalAlign="Right" />
        </asp:GridView>
        <%-- Pagination --%>
        <nav aria-label="Page navigation example">
            <ul class="pagination d-flex justify-content-end">
                <li class="page-item">
                    <a class="page-link" href="#" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                    </a>
                </li>
                <li class="page-item"><a class="page-link" href="#">1</a></li>
                <li class="page-item"><a class="page-link" href="#">2</a></li>
                <li class="page-item"><a class="page-link" href="#">3</a></li>
                <li class="page-item">
                    <a class="page-link" href="#" aria-label="Next">
                        <span aria-hidden="true">&raquo;</span>
                    </a>
                </li>
            </ul>
        </nav>
        <%-- Modal --%>
        <div id="addModal" class="modal fade" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 id="myModalLabel" class="modal-title">Add Item Property</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-6">
                                <label class="form-label">Item Name</label>
                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1"
                                    runat="server"
                                    CssClass="text-danger"
                                    ErrorMessage="*Name required"
                                    ControlToValidate="txtName"
                                    ValidationGroup="Insert"
                                    Display="Dynamic" />
                            </div>
                            <div class="col-6">
                                <label class="form-label">Order Item</label>
                                <asp:TextBox ID="txtOrder" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator
                                    ID="rqfOrder"
                                    runat="server"
                                    CssClass="text-danger"
                                    ErrorMessage="*Order required"
                                    ControlToValidate="txtOrder"
                                    ValidationGroup="Insert"
                                    Display="Dynamic" />
                                <asp:RegularExpressionValidator
                                    ID="revOrder"
                                    runat="server"
                                    ControlToValidate="txtOrder"
                                    ErrorMessage="Please enter a valid number (up to 10 digits)."
                                    ValidationExpression="^\d{1,10}$"
                                    ForeColor="Red"
                                    Display="Dynamic" />
                                <asp:CustomValidator
                                    ID="cvOrder"
                                    runat="server"
                                    ControlToValidate="txtOrder"
                                    ClientValidationFunction="validateOrderInput"
                                    ErrorMessage="Number must be between 0 and 9999999999."
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    ValidationGroup="Insert" />

                            </div>
                            <div class="col-6">
                                <label class="form-label">Status</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="A" Text="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="D" Text="Disable"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" ValidationGroup="Insert" OnClick="btnAdd_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:HiddenField ID="hdfPropertyID" runat="server" />
                        <asp:HiddenField ID="hdfLocationCode" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <div id="deleteModal" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Delete Confirmation</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p>Are you sure you want to delete this item?</p>
                        <asp:Label ID="lblDeleteItemName" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnConfirmDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnConfirmDelete_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/bootstrap.bundle.js"></script>

    <script>
        $(document).ready(function () {
            $('#btnAddPop').click(function () {

                $('#addModal').modal('show');
            });
        });
        function loadAddModal() {
            var loadAdd = new bootstrap.Modal(document.getElementById('addModal'));
            loadAdd.show();
        }
        function loadDeleteModal() {
            var loadDelete = new bootstrap.Modal(document.getElementById('deleteModal'));
            loadDelete.show();
        }
       
    </script>
</asp:Content>
