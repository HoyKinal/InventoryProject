<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnitMainDeleteMore.aspx.cs" Inherits="WebFormUnit.Form.DeleteMoreInOneTime.UnitMainDeleteMore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }
    </style>
    <div class="wrapper">
        <div class="shadow py-1 mb-2 ps-4 pt-4">
            <h5 class="text-primary">Base Unit List</h5>
            <p class="text-muted">Make change to base units in the list below.</p>
        </div>
        <div class="card">
            <div class="card-header">
                <div class="row">
                    <div class="col-4">
                        <div class="input-group">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="col-8 d-flex justify-content-end">
                        <asp:Button CssClass="btn btn-primary me-2" ID="btnAdd" runat="Server" Text="Add" OnClientClick="showAddModal();return false;" />
                        <asp:Button CssClass="btn btn-danger" ID="btnDeleteSelected" runat="Server" Text="Delete Selected" OnClick="btnDeleteSelected_Click" />
                    </div>
                </div>
            </div>
            <div class="card-body">
                <asp:GridView CssClass="table table-striped table-hover" ID="gvUnitMain" runat="Server" AutoGenerateColumns="False" DataKeyNames="UnitCode" OnRowCommand="gvUnitMain_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%-- <HeaderTemplate>
                          <asp:CheckBox ID="chkSelectAll" runat="Server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                          OnClick="SelectAllCheckboxes(this)"
                          </HeaderTemplate>
                                --%>
                                <asp:CheckBox ID="chkSelectAll" runat="server" OnClick="SelectAllCheckboxes(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RowNo" HeaderText="#" />
                        <asp:BoundField DataField="UnitName" HeaderText="Name" />
                        <asp:BoundField DataField="UnitSatus" HeaderText="Status" />
                        <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="text-end pe-5">
                            <ItemTemplate>
                                <div class="text-end">
                                    <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandName="EditUnit" CommandArgument='<%# Eval("UnitCode") %>'></asp:Button>
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" CommandName="DeleteUnit" CommandArgument='<%# Eval("UnitCode") %>'></asp:Button>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <!-- Modal Popup -->
        <div class="modal fade" id="addModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Add/Edit Unit</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <asp:HiddenField ID="txtUnitCode" runat="server" />
                            <label for="txtNameTextBox" class="form-label">Name</label>
                            <asp:TextBox ID="txtNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rqfName"
                                runat="server"
                                CssClass="text-danger"
                                ErrorMessage="*Name require"
                                Display="Dynamic"
                                ControlToValidate="txtNameTextBox"
                                ValidationGroup="add"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <label for="ddlStatusDropdown" class="form-label">Status</label>
                            <asp:DropDownList ID="ddlStatusDropdown" runat="server" CssClass="form-select">
                                <asp:ListItem Selected="True" Text="Active" Value="True"></asp:ListItem>
                                <asp:ListItem Text="Disable" Value="False"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" ValidationGroup="add"/>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Delete -->
        <div class="modal fade" id="modalDelete" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteModalLabel">Are you sure want to delete?</h5>
                    </div>
                    <div class="modal-body">
                        <p class="card-title">Please confirm if you want to delete this record.</p>
                        <div class="hstack gap-3 d-flex justify-content-center">
                            <asp:Label ID="labelUnitID" runat="server" CssClass="form-label"></asp:Label>
                            <asp:Label ID="labelName" runat="server" CssClass="form-label"></asp:Label>
                            <asp:Label ID="labelStatus" runat="server" CssClass="form-label"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hidenCodeFeild" runat="Server" />
                        <asp:Button ID="btnDelete" CssClass="btn btn-danger" Text="Delete" runat="Server" OnClick="btnDelete_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Alert -->
        <div class="modal fade" id="alertModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Success</h5>
                    </div>
                    <div class="modal-body">
                        <p class="card-title">Record is deleted successfully.</p>
                    </div>
                    <div class="modal-footer">
                        <p>Thanks</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script>
        function showAddModal() {
            var addModal = new bootstrap.Modal(document.getElementById('addModal'));
            addModal.show();
        }
        function showEditModal() {
            var EditModal = new bootstrap.Modal(document.getElementById('addModal'));
            EditModal.show();
        }
        function showDeleteModal() {
            var DeleteModal = new bootstrap.Modal(document.getElementById('modalDelete'));
            DeleteModal.show();
        }
        function alertDeleteModal() {
            var successModal = new bootstrap.Modal(document.getElementById('alertModal'));
            successModal.show();
            setTimeout(function () { successModal.hide(); }, 1000);
        }
        function SelectAllCheckboxes(checkbox) {
            var gridView = document.getElementById('<%= gvUnitMain.ClientID %>');
            var checkBoxes = gridView.getElementsByTagName("input");

            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type === "checkbox" && checkBoxes[i] !== checkbox) {
                    checkBoxes[i].checked = checkbox.checked;
                }
            }
        }
    </script>
</asp:Content>
