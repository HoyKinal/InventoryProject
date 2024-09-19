<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCustomerType.aspx.cs" Inherits="WebFormUnit.Form.CustomerForm.FormCustomerType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-straight/css/uicons-regular-straight.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-regular-straight/css/uicons-regular-straight.css'>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }

        .underline-header a {
            text-decoration: none;
        }
    </style>
    <div class="wrapper pt-3">
        <div class="row shadow-sm pb-2">
            <div class="col-6">
                <h5 class="text-primary">Customer Type List</h5>
                <p class="text-muted">Make change to your customer type information in the list below.</p>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <button id="btnAddPopup" type="button" class="btn btn-primary">Add</button>
            </div>
        </div>
        <div class="input-group w-25 mt-3 mb-3">
            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" PlaceHolder="Search..."></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="gvCustomerType" CssClass="table table-hover table-striped" runat="server" AutoGenerateColumns="false"
            DataKeyNames="MemberTypeCode"
            EmptyDataText="Empty Information CustomerType"
            HeaderStyle-CssClass="underline-header"
            RowStyle-CssClass="underline-header"
            AllowSorting="true"
            OnSorting="gvCustomerType_Sorting"
            AllowPaging="true"
            PageSize="5"
            OnPageIndexChanging="gvCustomerType_PageIndexChanging"
            OnRowDataBound="gvCustomerType_RowDataBound"
            OnRowCommand="gvCustomerType_RowCommand">
            <Columns>
                <asp:BoundField DataField="RowNo" HeaderText="#" SortExpression="RowNo" />
                <asp:BoundField DataField="MemberTypeName" HeaderText="Name" SortExpression="MemberTypeName" />
                <asp:BoundField DataField="MemberTypePrice" HeaderText="Price" SortExpression="MemberTypePrice" />
                <asp:TemplateField HeaderText="Discount" SortExpression="MemberTypeDiscount">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDiscount" runat="server"
                            CommandName="Select"
                            CommandArgument='<%# Eval("MemberTypeDiscount") %>'
                            Text='<%# Eval("MemberTypeDiscount") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MemberTypeStatus" HeaderText="Status" SortExpression="MemberTypeStatus" />
                <asp:BoundField DataField="ModifiedDate" HeaderText="Date Modified" SortExpression="ModifiedDate" />
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
                        <h5 class="modal-title">CustomerType</h5>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-6">
                                <label class="form-label">Name</label>
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
                            <div class="col-6">
                                <label class="form-label">Price</label>
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfPrice"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtPrice"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="revPrice"
                                    runat="server"
                                    ControlToValidate="txtPrice"
                                    ErrorMessage="*only numbers are allowed"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                    ValidationGroup="save"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Discount in percentage</label>
                                <asp:TextBox ID="txtDiscrountPercentage" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rqfDiscount"
                                    runat="server"
                                    ErrorMessage="*require"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ControlToValidate="txtDiscrountPercentage"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="revDiscount"
                                    runat="server"
                                    ControlToValidate="txtDiscrountPercentage"
                                    ErrorMessage="*only numbers are allowed"
                                    CssClass="text-danger"
                                    Display="Dynamic"
                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                    ValidationGroup="save"></asp:RegularExpressionValidator>
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
                        <asp:HiddenField ID="hdfMemberTypeCode" runat="server" />
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
