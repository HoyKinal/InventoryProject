<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateFKCategory.aspx.cs" Inherits="WebFormUnit.Form.ItemListForm.FormItemList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }
        .breadcrumb-item + .breadcrumb-item::after{
            content:"*";
            color:red;
            padding:0;
            margin:0;
        }
    </style>
    <div class="wrapper mt-3">
        <h5 class="text-primary">Item List</h5>
        <p class="text-muted">Make changes your items information in the list below.</p>
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">Home</a></li>
                <li class="breadcrumb-item"><a href="#">Warehouse</a></li>
                <li class="breadcrumb-item"><a href="#">Category Group</a></li>
                <li class="breadcrumb-item active" aria-current="page">
                     <asp:Label ID="labelLink" runat="server"></asp:Label>
                </li>
            </ol>
        </nav>
        <div class="row">
            <div class="col-3">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title text-primary">Inventory Item</h5>
                        <p class="card-text text-muted">Please select an item to add a commission</p>
                    </div>
                    <div class="card-body text-end">
                        <asp:DropDownList ID="ddlCategoryItem" runat="server" CssClass="form-select">
                            
                        </asp:DropDownList>
                         <asp:Button ID="InsertCategoryItem" runat="server" CssClass="btn btn-primary mt-3" Text="Insert" OnClick="InsertCategoryItem_Click" />
                    </div>
                </div>
            </div>
            <div class="col-7">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title text-primary">Inventory Item List</h5>
                        <p class="card-text text-muted">List of inventory items with commission set</p>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvUpdateCategoryGroupID" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="" EmptyDataText="Information is empty" OnRowCommand="gvUpdateCategoryGroupID_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="CategoryCode" HeaderText="Product Category" />
                                <asp:BoundField DataField="CategoryName" HeaderText="Item Code" />
                                <asp:BoundField DataField="CategoryStatus" HeaderText="Description"/>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-end pe-4" ItemStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" CommandName="DeleteCategory" CommandArgument='<%# Eval("CategoryCode") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>     
                </div>
            </div>
            <div class="col-2">
                <div class="card">
                    <div class="card-body">
                        <h5 class="text-primary">Commission Type Info</h5>
                        <p class="text-muted">Detailed information of selected commission type</p>
                        <span class="h5 text-muted">Name: </span>
                        <asp:Label ID="labelNameGroupCategory" CssClass="h5 text-muted" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
</asp:Content>
