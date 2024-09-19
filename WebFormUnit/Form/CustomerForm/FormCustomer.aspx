<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCustomer.aspx.cs" Inherits="WebFormUnit.Form.CustomerForm.FormCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-straight/css/uicons-regular-straight.css'>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }
    </style>

    <div class="wrapper pt-3">
        <div class="row shadow-sm pb-2">
            <div class="col-6">
                <h5 class="text-primary">Customer List</h5>
                <p class="text-muted">Make change to your customer information in the list below.</p>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <button id="btnAddPopup" type="button" class="btn btn-primary">Add</button>
            </div>
        </div>
        <div class="input-group w-25 mt-3 mb-3">
            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" PlaceHolder="Search..."></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" />
        </div>
        <div class="row">
            <div class="col-3">
                <div class="card shadow-sm">
                    <div class="card-header">
                        <h5 class="text-primary pt-1">Customer Type</h5>
                        <p class="text-muted">Select a customer type to show your customers.</p>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvCustomerType" CssClass="table table-hover table-striped" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="MemberTypeCode"
                            EmptyDataText="Empty Information CustomerType"
                            HeaderStyle-CssClass="text-center underline-header"
                            RowStyle-CssClass="text-center underline-header">
                            <Columns>
                                <asp:BoundField DataField="MemberTypeName" HeaderText="Customer Type" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
            <div class="col-9">
                <div class="card">
                    <div class="card-body">
                        <asp:GridView ID="GridView1" CssClass="table table-hover table-striped" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="MemberTypeCode"
                            EmptyDataText="Empty Information CustomerType"
                            HeaderStyle-CssClass="underline-header"
                            RowStyle-CssClass="underline-header">
                            <Columns>
                                <asp:BoundField DataField="RowNo" HeaderText="#" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnView" runat="server" CssClass="text-success text-decoration-none" CommandName="ViewItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-br-chart-mixed-up-circle-dollar"></i></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary text-decoration-none" CommandName="EditItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-danger text-decoration-none" CommandName="DeleteItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="5" />
                            <PagerStyle BackColor="WhiteSmoke" HorizontalAlign="Right" VerticalAlign="Bottom" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <nav aria-label="Page navigation example">
            <ul class="pagination" id="pagination">
                <li class="page-item" id="prevPage">
                    <a class="page-link" href="#" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                    </a>
                </li>
                <!-- Page links will be dynamically added here -->
                <li class="page-item" id="nextPage">
                    <a class="page-link" href="#" aria-label="Next">
                        <span aria-hidden="true">&raquo;</span>
                    </a>
                </li>
            </ul>
        </nav>
        <%-- Modal Add Edit --%>
        <div id="addEditModal" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Customer</h5>
                        <p class="modal-title">Make change customer info.</p>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class=" d-flex align-items-center">
                                <small class="text-muted me-3">General information:</small>
                                <div class="flex-grow-1 border-bottom"></div>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Customer Code*</label>
                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfCustomerCode"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtCustomerCode"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Name*</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfName"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtName"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Tax Identification Number</label>
                                <asp:TextBox ID="txtTaxIdNum" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfTaxIdNum"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtTaxIdNum"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="revTaxIDNum"
                                    runat="server"
                                    ControlToValidate="txtTaxIdNum"
                                    ErrorMessage="*only numbers are allowed"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                    ValidationGroup="save"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Description</label>
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfDesc"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtName"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mt-3 d-flex align-items-center">
                                <small class="text-muted me-3">Contact information:</small>
                                <div class="flex-grow-1 border-bottom"></div>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Address</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfAddress"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtAddress"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Phone Number *</label>
                                <asp:TextBox ID="txtPhoneNum" runat="server" CssClass="form-control" onkeyup=" removeNonNumeric(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfPhone"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtPhoneNum"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="revPhone"
                                    runat="server"
                                    ControlToValidate="txtPhoneNum"
                                    ErrorMessage="*only numbers are allowed"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                    ValidationGroup="save"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfEmail"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtEmail"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="revEmail"
                                    runat="server"
                                    ErrorMessage="*Invalid email()"
                                    CssClass="text-danger"
                                    ControlToValidate="txtEmail"
                                    Display="Dynamic"
                                    ValidationExpression="^[\w\.\-]+@([\w\-]+\.)+[a-zA-Z]{2,4}$"
                                    ValidationGroup="save"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Website (If any)</label>
                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfWebsite"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtWebsite"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Contact Person</label>
                                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfContactPerson"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtContactPerson"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Status</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="True" Text="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="False" Text="Disable"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hdfCustomerCode" runat="server" />
                        <asp:Button ID="btnAddEdit" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="save" />
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
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Confirm" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
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
