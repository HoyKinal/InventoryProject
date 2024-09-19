<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormItems.aspx.cs" Inherits="WebFormUnit.Form.ItemsForm.FormItems" EnableEventValidation="false" %>

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

        .wrapper {
            padding-top: 10px;
        }

        .breadcrumb-item + .breadcrumb-item::after {
            content: "*";
            color: red;
            padding: 0;
            margin: 0;
        }

        .center-content {
            text-align: center;
            vertical-align: middle;
        }

        .custom-image-border {
            border: 1px dashed #000;
            border-radius: 5px;
            padding: 1px;
            box-shadow: 2px 5px 5px rgba(0, 0, 0, 0.3);
            object-fit: cover;
        }

        .gridview-header a {
            text-decoration: none !important;
            /*color: inherit;*/
        }
    </style>
    <div class="wrapper mt-3">
        <h5 class="text-primary">Item List</h5>
        <small class="text-muted">Make changes your items information in the list below.</small>
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">Home</a></li>
                <li class="breadcrumb-item"><a href="#">Warehouse</a></li>
                <li class="breadcrumb-item"><a href="#">Item List</a></li>
                <li class="breadcrumb-item active" aria-current="page">
                    <asp:Label ID="labelLink" runat="server"></asp:Label>
                </li>
            </ol>
        </nav>
        <div class="row">
            <div class="col-3">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title text-primary">Category</h5>
                        <p class="card-text text-muted">Select a category to show your items</p>
                    </div>
                    <div class="card-body text-end">
                        <asp:GridView ID="gvCategoryT" CssClass=" table table-striped table-hover text-center " runat="server" AutoGenerateColumns="false"
                            DataKeyNames="CategoryCode"
                            EmptyDataText="Empty data information."
                            OnRowDataBound="gvCategoryT_RowDataBound"
                            OnSelectedIndexChanged="gvCategoryT_SelectedIndexChanged"
                            AutoPostBack="false"
                            OnRowCommand="gvCategoryT_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="DisplayText" HeaderText="Category - Name" />
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("CategoryCode") %>' Text="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="card">
                    <div class="card-header p-3">
                        <div class="row">
                            <div class="col-4">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" PlaceHolder="Search Items List"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" CssClass=" btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                            <div class="col-8 d-flex justify-content-end">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" />
                                <asp:DropDownList ID="ddlExport" runat="server" CssClass="form-select w-auto ms-2 text-center" AutoPostBack="true" OnSelectedIndexChanged="ddlExport_SelectedIndexChanged">
                                    <asp:ListItem Text="Export Files" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Export to Excel" Value="Excel"></asp:ListItem>
                                    <asp:ListItem Text="Export to PDF" Value="PDF"></asp:ListItem>
                                    <asp:ListItem Text="Print Current View" Value="PrintView"></asp:ListItem>
                                    <asp:ListItem Text="Print All Data" Value="PrintAll"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:UpdatePanel ID="upGridView" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvItemList" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                                    DataKeyNames="ItemCode"
                                    EmptyDataText="Information is empty"
                                    OnRowCommand="gvItemList_RowCommand"
                                    AllowPaging="True"
                                    PageSize="5"
                                    OnPageIndexChanging="gvItemList_PageIndexChanging"
                                    OnRowDataBound="gvItemList_RowDataBound"
                                    AllowSorting="true"
                                    OnSorting="gvItemList_Sorting"
                                    OnRowUpdating="gvItemList_RowUpdating"
                                    >
                                    <Columns>
                                        <asp:BoundField DataField="RowNo" HeaderText="#" SortExpression="RowNo"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />

                                        <asp:TemplateField HeaderText="Image"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header">
                                            <ItemTemplate>
                                                <asp:Image ID="imgItem" runat="server" CssClass="custom-image-border"
                                                    ImageUrl='<%# string.IsNullOrEmpty(Eval("ImageName") as string) ? ResolveUrl("~/Images/Placeholder.png") : ResolveUrl("~/Images/" + Eval("ImageName")) %>'
                                                    AlternateText="Item Image" Width="50" Height="50" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />
                                        <asp:BoundField DataField="SaleDescription" HeaderText="Sale Description" SortExpression="SaleDescription"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />
                                        <asp:BoundField DataField="PurDescription" HeaderText="Purchase Description" SortExpression="PurDescription"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />
                                        <asp:BoundField DataField="SalePrice" HeaderText="Price" SortExpression="SalePrice" DataFormatString="{0:N2}"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />
                                        <asp:BoundField DataField="Ana5" HeaderText="Stock" SortExpression="Ana5" DataFormatString="{0:F0}" HtmlEncode="false"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />
                                        <asp:BoundField DataField="ItemType" HeaderText="Type" SortExpression="ItemType"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />
                                        <asp:BoundField DataField="ItemStatus" HeaderText="Status" SortExpression="ItemStatus"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />
                                        <asp:BoundField DataField="DateModified" HeaderText="Date Modified" SortExpression="DateModified" DataFormatString="{0:dd-MM-yyyy}"
                                            ItemStyle-CssClass="center-content" HeaderStyle-CssClass="center-content gridview-header" />

                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="center-content gridview-header" ItemStyle-CssClass="center-content">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnView" runat="server" CssClass="text-info text-decoration-none" CommandName="ViewItem" CommandArgument='<%# Eval("ItemCode") %>'><i class="fi fi-br-overview"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary text-decoration-none" CommandName="EditItem" CommandArgument='<%# Eval("ItemCode") %>'><i class="fi fi-rr-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-danger text-decoration-none" CommandName="DeleteItem" CommandArgument='<%# Eval("ItemCode") %>'><i class="fi fi-rr-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                                    <PagerStyle BackColor="LightCyan" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </div>
            </div>
        </div>
        <%-- Modal Alert Delete --%>
        <div class="modal fade" id="modalAlertDelete" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteModalLabel">Delete Alert</h5>
                    </div>
                    <div class="modal-body">
                        <h6 class="text-muted">Are you sure want to delete this Items.</h6>
                    </div>
                    <div class="modal-footer">      
                        <asp:HiddenField ID="hdfItemCode" runat="server" />
                        <asp:Button ID="btnDelete" CssClass="btn btn-danger" Text="Confirm" runat="Server" OnClick="btnDelete_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function ItemListDeleteAlert() {
            var DeleteAlert = new bootstrap.Modal(document.getElementById('modalAlertDelete'));
            DeleteAlert.show();
        }
    </script>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
</asp:Content>
