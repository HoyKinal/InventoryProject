<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormSupplier.aspx.cs" Inherits="WebFormUnit.Form.Transactions.Suppliers.FormSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-straight/css/uicons-regular-straight.css'>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }

        .underline-header a {
            text-decoration: none;
        }
        /* General focus-ring class */
        .focus-ring:focus {
            outline: none;
            box-shadow: 0 0 0 0.25rem rgba(0, 123, 255, 0.25); /* Example for a default focus */
        }

        /* Warning focus class */
        .focus-ring-warning:focus {
            outline: none;
            box-shadow: 0 0 0 0.25rem rgba(255, 193, 7, 0.5); /* Warning yellow shadow */
        }
    </style>
    <div class="wrapper pt-3">
        <div class="row shadow-sm pb-2">
            <div class="col-6">
                <h5 class="text-primary">Supplier List</h5>
                <p class="text-muted">Make change to your supplier information in the list below.</p>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <button id="btnAddPopup" type="button" class="btn btn-primary">Add</button>
            </div>
        </div>
        <div class="input-group w-25 mt-3 mb-3">
            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" PlaceHolder="Search..."></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="gvSupplier" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="false"
            DataKeyNames="SupplierCode"
            OnRowCommand="gvSupplier_RowCommand"
            OnRowDataBound="gvSupplier_RowDataBound"
            >
            <Columns>
                <asp:BoundField DataField="RowNo" HeaderText="#"/>
                <asp:BoundField DataField="SupplierName" HeaderText="Name"/>
                <asp:BoundField DataField="SupplierDesc" HeaderText="SupplierDesc" />
                <asp:BoundField DataField="SupplierAddress" HeaderText="SupplierAddress" />
                <asp:BoundField DataField="SupplierPhone" HeaderText="Phone" />
                <asp:BoundField DataField="SupplierFax" HeaderText="Fax" />
                <asp:BoundField DataField="SupplierEmail" HeaderText="Email" />
                <asp:BoundField DataField="SupplierWeb" HeaderText="Website" />
                <asp:BoundField DataField="SupplierContact" HeaderText="Contact" />
                <asp:BoundField DataField="SupplierStatus" HeaderText="Status" />
                <asp:BoundField DataField="CreateDate" HeaderText="Date Modified" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-decoration-none" CommandName="EditItemSupplier" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'>
                 <i class="fi fi-rr-edit text-primary"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-decoration-none" CommandName="DeleteItemSupplier" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'>
                 <i class="fi fi-rr-trash text-danger"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <%-- Modal Add Edit --%>
        <div id="addEditModal" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Supplier</h5>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="d-flex align-items-center">
                                <small class="text-muted me-3">General information:</small>
                                <div class="flex-grow-1 border-bottom"></div>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Supplier Code </label>
                                <asp:TextBox ID="txtSupplierCode" runat="server" CssClass="form-control focus-ring focus-ring-warning"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Name</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control focus-ring focus-ring-warning"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Description</label>
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control focus-ring focus-ring-warning"></asp:TextBox>
                            </div>
                            <div class="mt-3 d-flex align-items-center">
                                <small class="text-muted me-3">Contact information:</small>
                                <div class="flex-grow-1 border-bottom"></div>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Address</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control focus-ring focus-ring-warning"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Phone Number</label>
                                <asp:TextBox ID="txtPhoneNum" runat="server" CssClass="form-control focus-ring focus-ring-warning" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Fax</label>
                                <asp:TextBox ID="txtFax" runat="server" CssClass="form-control focus-ring focus-ring-warning"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control focus-ring focus-ring-warning"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Website</label>
                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control focus-ring focus-ring-warning"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Contact</label>
                                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control focus-ring focus-ring-warning"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Status</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select focus-ring focus-ring-warning">
                                    <asp:ListItem Value="A" Selected="True" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="D" Text="Disable"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hdfSupplierCode" runat="server" />
                        <asp:Button ID="btnAddEdit" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="save" OnClick="btnAddEdit_Click" />
                        <asp:Button ID="btnCancelClear" runat="server" CssClass="btn btn-secondary" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>
        <%-- Modal Delete --%>
        <div id="deleteModal" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Confirm Delete Item</h5>
                    </div>
                    <div class="modal-body">
                        <p class="text-muted">Are you sure want to delete this items.</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Confirm" OnClick="btnDelete_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../../Scripts/bootstrap.min.js"></script>
    <script src="../../../Scripts/bootstrap.bundle.min.js"></script>
    <script>
        function removeNonNumeric(input) {
            input.value = input.value.replace(/[^0-9.]/g, '');
            if (input.value.split('.').length > 2) {
                input.value = input.value.replace(/\.+$/, "");
            }
        }
        function showEditModal() {
            var editModal = new bootstrap.Modal(document.getElementById('addEditModal'));
            editModal.show();
        }
        function showDeleteModal() {
            var deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            deleteModal.show();
        }
        $(document).ready(function () {
            $('#btnAddPopup').click(function () {
                $('#addEditModal').modal('show');
            });
        });

    </script>
</asp:Content>
