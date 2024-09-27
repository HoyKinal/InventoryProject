<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCompanyPayBill.aspx.cs" Inherits="WebFormUnit.Form.Transactions.EnterBill.FormCompanyPayBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <div class="wrapper bg-light p-3 rounded shadow-sm mb-2">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-outline-success me-2 mb-2 mb-md-0" OnClick="btnBack_Click"> Back </asp:LinkButton>
                <asp:LinkButton ID="btnOpen" runat="server" CssClass="btn btn-outline-primary me-2 mb-2 mb-md-0" OnClientClick="showAddModal();">Open</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-2">
                <div class="card shadow-sm mt-3">
                    <div class="card-header">
                        <h5 class="card-title text-primary fw-bold">Pay Bill Supplier</h5>
                        <p class="card-text text-muted">Choose criteria below to filter your bill</p>
                    </div>
                    <div class="card-body">
                        <div class="mt-3">
                            <label class="form-label">Search</label>
                            <asp:DropDownList ID="ddlSuplier" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary mt-3" Text="Search" />
                    </div>
                </div>
            </div>
            <div class="col-10">
                <div class="card shadow-sm mt-3">
                    <div class="card-header">
                        <h5 class="card-title text-primary fw-bold">Bill List</h5>
                        <p class="card-text text-muted">Show your bills based on the selected criteria</p>
                    </div>
                    <div class="card-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvPayBillHeader" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                    DataKeyNames="BillNumber"
                                    EmptyDataText="Enpty Information Expense Header.">
                                    <Columns>
                                        <asp:BoundField DataField="RowNo" HeaderText="#" />
                                        <asp:BoundField DataField="DateBill" HeaderText="Date Bill" DataFormatString="{0:dd/MM/yyyy HH:mm:ss tt}" HtmlEncode="false" />
                                        <asp:BoundField DataField="BillNumber" HeaderText="Expense No" />
                                        <asp:BoundField DataField="VenderCode" HeaderText="Supplier" />
                                        <asp:BoundField DataField="RefereceNo" HeaderText="Reference" />
                                        <asp:BoundField DataField="GrandTotalWithVat" HeaderText="Grand Total (VAT)" DataFormatString="{0:F2}" />
                                        <asp:BoundField DataField="" HeaderText="Paid" />
                                        <asp:BoundField DataField="" HeaderText="Upaid" />
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnPay" runat="server" CssClass="text-decoration-none text-primary" CommandName="PayItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnOpen" runat="server" CssClass="text-decoration-none text-primary" CommandName="OpenItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <%-- Pay Modal --%>
                <div id="addModalPayment" class="modal">
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="addModalLabel">Pay Bill</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-6">
                                        <label class="form-label">Bill No</label>
                                        <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Bill Date</label>
                                        <asp:TextBox ID="txtBillDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-12 mt-3">
                                        <label class="form-label">Unpaid Amount</label>
                                        <asp:TextBox ID="txtUnpaidAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-12 mt-3">
                                        <label class="form-label">Pay Date</label>
                                        <asp:TextBox ID="txtPayDate" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                                    </div>
                                    <div class="col-12 mt-3">
                                        <label class="form-label">Pay Amount</label>
                                        <asp:TextBox ID="txtPayAmount" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                                    </div>
                                    <div class="col-6 mt-3">
                                        <label class="form-label">Memo</label>
                                        <asp:TextBox ID="txtMemo" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../../../Scripts/bootstrap.min.js"></script>
    <script>
        function showAddModal() {
            var showModal = new bootstrap.Modal(document.getElementById('addModalPayment'));
            showModal.show();
        }
    </script>
</asp:Content>
