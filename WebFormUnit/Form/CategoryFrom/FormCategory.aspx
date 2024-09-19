<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCategory.aspx.cs" Inherits="WebFormUnit.Form.CategoryFrom.FormCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <!-- Include Bootstrap Datepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet">
    <!-- Include Timepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/css/bootstrap-timepicker.min.css" rel="stylesheet">
    <div class="container-fluid pt-3">
        <!-- Page Header -->
        <div class="row mb-2 shadow-sm mx-0">
            <div class="col-md-8">
                <h4 class="text-primary">Item Category List</h4>
                <p class="text-muted">Make changes to your category information in the list below.</p>
            </div>
            <div class="col-md-4 d-flex align-items-center justify-content-end">
                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary px-4" Text="Add" BorderStyle="None" OnClick="btnAdd_Click" />
                <asp:Button ID="btnSelectDelete" runat="Server" CssClass="btn btn-danger ms-2" Text="Delete Selected" OnClick="btnSelectDelete_Click" />
            </div>
        </div>

        <!-- Category List Table -->
        <div class="card">
            <div class="card-header">
                <div class="input-group w-25">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Search categories"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvCategoryList" runat="Server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" DataKeyNames="CategoryCode" OnRowCommand="gvCategoryList_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%--<asp:CheckBox ID="checkAll" runat="server" OnClick="SelectAllRows(this)" />--%>
                                <asp:CheckBox ID="chkSelectAll" runat="Server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="True" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="Server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RowNo" HeaderText="#" />
                        <asp:BoundField DataField="CategoryCode" HeaderText="Category Code" ReadOnly="True" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                        <asp:BoundField DataField="CategoryStatus" HeaderText="Status" />
                        <asp:BoundField DataField="ModifyDate" HeaderText="Date Modified" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-warning btn-sm" Text="Edit" CommandName="EditCategory" CommandArgument='<%# Eval("CategoryCode") %>' />
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete" CommandName="DeleteCategory" CommandArgument='<%# Eval("CategoryCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <%-- Modal Edit --%>
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editModalLabel">Edit Category</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="labelCategoryCode" class="form-label">Category Code</label>
                        <asp:TextBox ID="txtCategoryCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="labelCategoryCode" class="form-label">Category Name</label>
                        <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="LabelCategoryStatus" class="form-label">Status</label>
                        <asp:DropDownList ID="ddlCategoryStatus" runat="server" CssClass="form-select">
                            <asp:ListItem Selected="True" Text="Active" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                        </asp:DropDownList>
                        <div class="mb-3">
                            <label for="labelModifyDate" class="form-label">ModifyDate</label>
                            <asp:TextBox ID="txtModifyDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hdFieldCategoryCode" runat="server" />
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Modal Delete --%>
    <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteModalLabel">Delete popup</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <h5>Are you sure you want to delete</h5>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hdFieldCategoryCodeDelete" runat="server" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Confirm" OnClick="btnDelete_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <!-- JavaScript for Confirmation and Select All Functionality -->
    <script type="text/javascript">

        function SelectAllRows(source) {
            var checkboxes = document.querySelectorAll('input[id*="chkSelect"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i] != source)
                    checkboxes[i].checked = source.checked;
            }
        }

        function showEditModal() {
            var editModal = new bootstrap.Modal(document.getElementById('editModal'));
            editModal.show();
        }

        function showDeleteModal() {
            var deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            deleteModal.show();
        }

        $(document).ready(function () {
            // Initialize datepicker
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });

            // Initialize timepicker
            $('.timepicker').timepicker({
                defaultTime: 'current',
                minuteStep: 1,
                showSeconds: false,
                showMeridian: true,
                snapToStep: true
            });
        });
    </script>
    <!-- Include jQuery first, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Include Bootstrap Datepicker JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <!-- Include Timepicker JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/js/bootstrap-timepicker.min.js"></script>

</asp:Content>
