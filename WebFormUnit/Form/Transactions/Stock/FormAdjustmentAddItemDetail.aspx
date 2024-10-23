<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormAdjustmentAddItemDetail.aspx.cs" Inherits="WebFormUnit.Form.Transactions.Stock.FormAdjustmentItemDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrapper mt-3">
        <div class="mt-0 shadow-sm pb-3">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="Back" />
        </div>
        <div class="row mt-3">
            <div class="col-3">
                <div class="card">
                    <div class="card-header">
                        <h5 class="text-primary">Item Form</h5>
                        <p class="text-muted">Select items to adjust quantity.</p>
                    </div>
                    <div class="card-body">
                        <div class="mt-0">
                            <label class="form-label">Product Category</label>
                            <asp:DropDownList ID="ddlCategoryCode" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryCode_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Item Name</label>
                            <asp:DropDownList ID="ddlItemCode" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <div class="row">
                            <div class="col-6 mt-3">
                                <label class="form-label">Old Quantity</label>
                                <asp:TextBox ID="txtOldQty" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">New Quantity</label>
                                <asp:TextBox ID="txtNewQty" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Adjust Stock</label>
                                <asp:TextBox ID="txtAdjustStock" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Unit</label>
                                <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">AVG Cost</label>
                                <asp:TextBox ID="txtAvgCost" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Sub Total</label>
                                <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class=" mt-3">
                            <label class="form-label">Memo</label>
                            <asp:TextBox ID="txtMemo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="card shadow-sm">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-6">
                                <h5 class="text-success">Item</h5>
                                <p class="text-muted"> List all your adjusted items.</p>
                            </div>
                            <div class="col-6 text-end">
                                <h5 class="text-success">AdjustmentNo: <asp:Label ID="lbDisplayAdjustmentNo" runat="server" CssClass="text-success h5" Text="unknown"></asp:Label></h5>
                                <label class="text-danger h5">Amount</label> <br />
                                <asp:Label ID="lbDisplayTotalAmount" runat="server" CssClass="text-danger h5" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
