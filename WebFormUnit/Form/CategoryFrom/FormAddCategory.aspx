<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormAddCategory.aspx.cs" Inherits="WebFormUnit.Form.CategoryFrom.FormAddCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <!-- Include Bootstrap Datepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet">
    <!-- Include Timepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/css/bootstrap-timepicker.min.css" rel="stylesheet">

    <div class="container-fluid mt-4">
        <!-- Button Actions -->
        <div class="shadow-sm p-3 mb-4">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="Back" OnClick="btnBack_Click" />
            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary ms-1" Text="Clear" OnClick="btnClear_Click" />
            <asp:Button ID="btnSaveNew" runat="server" CssClass="btn btn-info ms-1" Text="SaveNew" OnClick="btnSaveNew_Click" ValidationGroup="vgCategory" />
            <asp:Button ID="btnSaveClose" runat="server" CssClass="btn btn-success ms-1" Text="SaveClose" OnClick="btnSaveClose_Click" ValidationGroup="vgCategory" />
            <asp:Button ID="btnAddGroup" runat="server" CssClass="btn btn-primary ms-1" Text="GroupID" OnClick="btnAddGroup_Click" />
        </div>

        <!-- Form Card -->
        <div class="card container">
            <div class="card-header">
                <h5 class="text-primary">Item Category</h5>
            </div>
            <div class="card-body">
                <small class="text-muted d-block mb-3">Category is used to control all items in the company.</small>
                <div class="row">
                    <!-- Category Code -->
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Category Code</label>
                        <asp:TextBox ID="txtCategoryCode" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="rfvCategoryCode"
                            ControlToValidate="txtCategoryCode"
                            ErrorMessage="Category Code is required"
                            CssClass="text-danger"
                            Display="Dynamic"
                            ValidationGroup="vgCategory"></asp:RequiredFieldValidator>
                    </div>

                    <!-- Category Name -->
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Category Name</label>
                        <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="rfvCategoryName"
                            ControlToValidate="txtCategoryName"
                            ErrorMessage="Category Name is required"
                            CssClass="text-danger"
                            Display="Dynamic"
                            ValidationGroup="vgCategory"></asp:RequiredFieldValidator>
                    </div>

                    <!-- Status -->
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Active" Value="A" />
                            <asp:ListItem Text="Inactive" Value="I" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="rfvStatus"
                            ControlToValidate="ddlStatus"
                            InitialValue=""
                            ErrorMessage="Status is required"
                            CssClass="text-danger"
                            Display="Dynamic"
                            ValidationGroup="vgCategory"></asp:RequiredFieldValidator>
                    </div>

                    <!-- Date Modified -->
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Date Modified</label>
                        <asp:TextBox ID="txtDateModified" runat="server" CssClass="form-control" ReadOnly="True"/>
                        <%--<asp:CompareValidator
                            runat="server"
                            ID="cvDateModified"
                            ControlToValidate="txtDateModified"
                            Operator="DataTypeCheck"
                            Type="Date"
                            ErrorMessage="Invalid date format"
                            CssClass="text-danger"
                            Display="Dynamic"
                            ValidationGroup="vgCategory"></asp:CompareValidator>
                          </div>--%>
                    </div>
                 
                    <!-- Use InPost Checkbox -->
                    <div class="form-check p-0">
                        <asp:CheckBox ID="chkUseInPost" runat="server" Checked="true" />
                        <label for="chkUseInPost" class="form-check-label">Use InPost</label>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Add Group-->
    <div class="modal fade" id="modalAddGroup" tabindex="-1" aria-labelledby="modalAddGroupLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalAddGroupLabel">Add Group</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="labelCategoryGroup" runat="server" CssClass="form-label" Text="CategoryGroupCode"></asp:Label>
                            <asp:TextBox ID="txtCategoryGroup" runat="server" CssClass="form-control mt-2" PlaceHolder="CG-001"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                runat="server"
                                ID="rfvCategoryGroup"
                                ControlToValidate="txtCategoryGroup"
                                ErrorMessage="Category Group Code is required"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationGroup="vgCategoryGroup"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="labelCategoryGroupName" runat="server" CssClass="form-label" Text="CategoryGroupName"></asp:Label>
                            <asp:TextBox ID="txtCategoryGroupName" runat="server" CssClass="form-control mt-2" PlaceHolder="Drink"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                runat="server"
                                ID="rfvCategoryGroupName"
                                ControlToValidate="txtCategoryGroupName"
                                ErrorMessage="Category Group Name is required"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationGroup="vgCategoryGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hdFieldCategoryCodeDelete" runat="server" />
                    <asp:Button ID="btnAddCG" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnAddCG_Click" ValidationGroup="vgCategoryGroup" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>
</div>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/bootstrap.bundle.js"></script>
    <!-- Bootstrap Bundle with Popper (JavaScript) -->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Include Bootstrap Datepicker JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <!-- Include Timepicker JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/js/bootstrap-timepicker.min.js"></script>

    <script>
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

        function showGroupCategory() {
            var showCG = new bootstrap.Modal(document.getElementById('modalAddGroup'));
            showCG.show();
        }
    </script>
</asp:Content>
