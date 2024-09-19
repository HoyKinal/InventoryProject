<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCommissionType.aspx.cs" Inherits="WebFormUnit.Form.ItemsForm.FormCommissionType" %>

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
        .underline-header a{
            text-decoration:none;
            color:inherit;  
        }
    </style>
    <div class="wrapper pt-3">
        <div class="row shadow-sm pb-2">
            <div class="col-6">
                <h5 class="text-primary">Commission Type List</h5>
                <p class="text-muted">Make change to your commission type information in the list below.</p>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <button id="btnAddPopup" type="button" class="btn btn-primary">Add</button>
            </div>
        </div>
        <div class="input-group w-25 mt-3 mb-3">
            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" PlaceHolder="Search..."></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="gvItemCommissiontype" CssClass="table table-hover table-striped" runat="server" AutoGenerateColumns="false"
            EmptyDataText="Empty Information CommissionType"
            OnRowCommand="gvItemCommissiontype_RowCommand"
            OnRowDataBound="gvItemCommissiontype_RowDataBound"
            AllowPaging="true"
            PageSize="5"
            OnPageIndexChanging="gvItemCommissiontype_PageIndexChanging" 
            AllowSorting="true"
            OnSorting="gvItemCommissiontype_Sorting"
            HeaderStyle-CssClass="underline-header">
            <Columns>
                <asp:BoundField DataField="RowNo" HeaderText="#" SortExpression="RowNo" />
                <asp:BoundField DataField="CommissionTypeName" HeaderText="Name" SortExpression="CommissionTypeName" />
                <asp:BoundField DataField="PayableAccount" HeaderText="Payable Account" SortExpression="PayableAccount" />
                <asp:BoundField DataField="ExpenseAccount" HeaderText="Expense Account" SortExpression="ExpenseAccount" />
                <asp:BoundField DataField="CommissionTypeStatus" HeaderText="Status" SortExpression="CommissionTypeStatus" />
                <asp:BoundField DataField="ModifiedDate" HeaderText="Date Modified" DataFormatString="{0:dd-MM-yyyy HH:mm:ss tt}" SortExpression="ModifiedDate" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnView" runat="server" CssClass="text-info text-decoration-none" CommandName="ViewItem" CommandArgument='<%# Eval("CommissionTypeCode") %>'><i class="fi fi-br-overview"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary text-decoration-none" CommandName="EditItem" CommandArgument='<%# Eval("CommissionTypeCode") %>'><i class="fi fi-rr-edit"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-danger text-decoration-none" CommandName="DeleteItem" CommandArgument='<%# Eval("CommissionTypeCode") %>'><i class="fi fi-rr-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="5"  />
            <PagerStyle BackColor="WhiteSmoke" HorizontalAlign="Right" VerticalAlign="Bottom"/>
        </asp:GridView>
        <%-- Modal Add Edit --%>
        <div id="addEditModal" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add/Edit ItemCommissionType</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-6">
                                <label class="form-label">Name</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                  ID="rqfName"
                                  runat="server"
                                  ErrorMessage="*name require"
                                  CssClass="text-danger"
                                  Display="Dynamic"
                                  ControlToValidate="txtName"
                                  ValidationGroup="Insert"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-6">
                                <label class="form-label">Status</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="true" Text="Active" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="false" Text="Disable"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Payable Account</label>
                                <asp:DropDownList ID="ddlPayabelAccount" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="5000">5000-Account Payable</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Expense Account</label>
                                <asp:DropDownList ID="ddlExpenseAccount" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="6001" Selected="True">6001-Stock Lose</asp:ListItem>
                                    <asp:ListItem Value="6100">6100-Retal Expense</asp:ListItem>
                                    <asp:ListItem Value="6110">6110-Avertising Expense</asp:ListItem>
                                    <asp:ListItem Value="6130">6130-Office Supply Expense</asp:ListItem>
                                    <asp:ListItem Value="6140">6140-Gasoline Expense</asp:ListItem>
                                    <asp:ListItem Value="6150">6150-Repair & Maintenance</asp:ListItem>
                                    <asp:ListItem Value="6200">6200-Transportation</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAddEdit" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnAddEdit_Click" ValidationGroup="Insert" />
                        <asp:Button ID="btnCancelClear" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancelClear_Click" /> 
                    </div>
                </div>
            </div>
        </div>
        <%-- Modal Delete --%>
        <div id="deleteModal" class="modal fade" tabindex="-1" role="dialog" >
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
    <script>
        $(document).ready(function () {
            $('#btnAddPopup').click(function () {
                $('#addEditModal').modal('show');
            });

            <%--function showDeleteModal() {
                $('#deleteModal').modal('show');
            }

            $('<%=btnDelete.ClientID%>').click(function () {
                $('deleteModal').modal('hide');
            });
            showDeleteModal();--%>
        });
        function showDeleteModal() {
            var deleteAlert = new bootstrap.Modal(document.getElementById('deleteModal'));
            deleteAlert.show();
        }
        function showEditModal() {
            var editModal = new bootstrap.Modal(document.getElementById('addEditModal'));
            editModal.show();
        }
    </script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
</asp:Content>
