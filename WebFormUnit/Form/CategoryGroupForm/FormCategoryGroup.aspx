<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCategoryGroup.aspx.cs" Inherits="WebFormUnit.Form.CategoryGroupForm.FormCategoryGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-straight/css/uicons-regular-straight.css'>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }

        .pagination {
            display: flex;
            justify-content: end;
            list-style: none;
            padding-left: 0;
        }

        .pagination .page-item {
            margin: 0 2px;
        }

        .pagination .page-link {
            padding: 0.5rem 0.75rem;
            border: 1px solid #dee2e6;
            color: #007bff;
            text-decoration: none;
        }

        .pagination .page-link:hover {
            background-color: #e9ecef;
            color: #0056b3;
        }

        .table-hover tbody tr:hover {
            background-color: #f5f5f5;
        }

        .btn-icon {
            padding: 0.375rem 0.75rem;
        }
    </style>

    <div class="wrapper mt-3">
        <h5 class="text-primary">Category Group</h5>
        <p class="text-muted">Make changes to the category group in the list below.</p>

        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">Home</a></li>
                <li class="breadcrumb-item"><a href="#">Warehouse</a></li>
                <li class="breadcrumb-item active" aria-current="page">Category Group</li>
            </ol>
        </nav>

        <div class="card">
            <div class="card-header">
                <div class="row">
                    <div class="col-6">
                        <div class="input-group w-50">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Search categories Group"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="col-6 d-flex justify-content-end">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" />
                        <asp:DropDownList ID="ddlExport" runat="server" CssClass="form-select w-auto ms-2">
                            <asp:ListItem Text="Export to Excel" Value="Excel"></asp:ListItem>
                            <asp:ListItem Text="Export to PDF" Value="PDF"></asp:ListItem>
                            <asp:ListItem Text="Print Current View" Value="PrintView"></asp:ListItem>
                            <asp:ListItem Text="Print All Data" Value="PrintAll"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-primary ms-2" OnClick="btnExport_Click" />
                    </div>
                </div>
            </div>

            <div class="card-body">
                <asp:GridView ID="gvCategoryGroupList" runat="Server" CssClass="table table-hover table-striped table-bordered" AutoGenerateColumns="False"
                    DataKeyNames="CategoryGroupId"
                    EmptyDataText="Information is empty."
                    OnRowCommand="gvCategoryGroupList_RowCommand"
                    AllowPaging="true"
                    PageSize="8"
                    OnPageIndexChanging="gvCategoryGroupList_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="RowNo" HeaderText="#" />
                        <asp:BoundField DataField="CategoryGroupName" HeaderText="Category Name" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="d-flex justify-content-end hstack gap-2">
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary"  CommandName="UpdateCG" CommandArgument='<%# Eval("CategoryGroupId") %>'> <i class="fi fi-rr-edit"></i></asp:LinkButton>
                                  <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger" CommandName="DeleteCG" CommandArgument='<%# Eval("CategoryGroupId") %>'><i class="fi fi-rr-trash"></i></asp:LinkButton>
                                  <asp:LinkButton ID="btnInsert" runat="server" CssClass="btn btn-info" CommandName="InsertCG" CommandArgument='<%# Eval("CategoryGroupId") %>'><i class="fi fi-br-overview"></i></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                    <PagerStyle BackColor="LightBlue" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />

                    <%-- <PagerTemplate>
                        <nav aria-label="Page navigation example">
                            <ul class="pagination">
                                <!-- Previous button -->
                                <li class="page-item">
                                    <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Page" CommandArgument='<%# gvCategoryGroupList.PageIndex - 1 %>' CssClass="page-link" aria-label="Previous" Enabled='<%# gvCategoryGroupList.PageIndex > 0 %>'>
                                         <span aria-hidden="true">&laquo;</span>
                                    </asp:LinkButton>
                                </li>

                                <!-- Page numbers -->
                                <asp:Repeater ID="rpt" runat="server" OnItemCommand="rptPager_ItemCommand">
                                    <ItemTemplate>
                                        <li class="page-item <%# Convert.ToInt32(Container.DataItem) == gvCategoryGroupList.PageIndex ? "active" : "" %>">
                                            <asp:LinkButton ID="lnkPage" runat="server" CommandName="Page" CommandArgument='<%# Container.DataItem %>' CssClass="page-link">
                                                <%# (Convert.ToInt32(Container.DataItem) + 1).ToString() %>
                                            </asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <!-- Next button -->
                                <li class="page-item">
                                    <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument='<%# gvCategoryGroupList.PageIndex + 1 %>' CssClass="page-link" aria-label="Next" Enabled='<%# gvCategoryGroupList.PageIndex < gvCategoryGroupList.PageCount - 1 %>'>
                                        <span aria-hidden="true">&raquo;</span>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </nav>
                    </PagerTemplate>--%>
                </asp:GridView>
            </div>
        </div>

        <%-- Add Modal --%>
        <div class="modal fade" id="AddModal" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="addModalLabel">Add Category Group</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="txtCategoryGroupName" class="form-label">Name</label>
                            <asp:TextBox ID="txtCategoryGroupName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>

        <%-- Edit Modal --%>
        <div class="modal fade" id="EditModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="editModalLabel">Edit Category Group</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="txtEditCategoryGroupName" class="form-label">Name</label>
                            <asp:TextBox ID="txtEditCategoryGroupName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hfCategoryGroupId" runat="server" />
                        <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>

        <%-- Delete Alert Modal --%>
        <div class="modal fade" id="AlertDelete" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteModalLabel">Confirm Deletion</h5>
                    </div>
                    <div class="modal-body">
                        <p class="card-title">Are you sure you want to delete this record?</p>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hidenFieldDelete" runat="server" />
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Confirm" OnClick="btnDelete_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function showAdd() {
            var showAddCG = new bootstrap.Modal(document.getElementById('AddModal'));
            showAddCG.show();
        }

        function showEdit() {
            var showEditCG = new bootstrap.Modal(document.getElementById('EditModal'));
            showEditCG.show();
        }

        function showAlertDelete() {
            var deleteAlert = new bootstrap.Modal(document.getElementById('AlertDelete'));
            deleteAlert.show();
        }

        function printCurrentView() {
            var width = 800;
            var height = 600;

            // Calculate the center position
            var left = (window.innerWidth / 2) - (width / 2);
            var top = (window.innerHeight / 2) - (height / 2);

            var printWindow = window.open('', '', 'width=' + width + ',height=' + height + ',left=' + left + ',top=' + top);
            printWindow.document.write('<html><head><title>Print View</title></head><body>');
            printWindow.document.write(document.getElementById('<%= gvCategoryGroupList.ClientID %>').outerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>

    <script src="../../Scripts/bootstrap.bundle.js"></script>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
