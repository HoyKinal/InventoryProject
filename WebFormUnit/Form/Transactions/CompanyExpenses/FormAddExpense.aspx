<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormAddExpense.aspx.cs" Inherits="WebFormUnit.Form.Transactions.CompanyExpenses.FormAddExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-straight/css/uicons-regular-straight.css'>

    <%-- Hidden Fields --%>
    <asp:HiddenField ID="hdfNumberNo" runat="server" />
    <asp:HiddenField ID="hdfSupplierCode" runat="server" />
    <asp:HiddenField ID="hdfDate" runat="server" />
    <asp:HiddenField ID="hdfReference" runat="server" />
    <asp:HiddenField ID="hdfMemo" runat="server" />
    <asp:HiddenField ID="hdfVatPercent" runat="server" />
    <asp:HiddenField ID="hdfVatAmount" runat="server" />
    <asp:HiddenField ID="hdfDiscountPercent" runat="server" />
    <asp:HiddenField ID="hdfDiscountAmount" runat="server" />
    <asp:HiddenField ID="hdfTotalDiscountPercent" runat="server" />
    <asp:HiddenField ID="hdfTotalDiscount" runat="server" />
    <asp:HiddenField ID="hdfTotal" runat="server" />
    <asp:HiddenField ID="hdfGrandTotal" runat="server" />

    <asp:HiddenField ID="hdfDisplayTotalAmount" runat="server"/>

    <div class="container-fluid">
        <div class="bg-light pt-2 rounded shadow-sm mb-4">
            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary mb-3" Text="Back" OnClick="btnBack_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Main Content Row -->
            <div class="row g-3">
                <div class="col-lg-3">
                    <div class="card shadow-sm">
                        <div class="card-header bg-primary text-white">
                            <h5 class="card-title mb-0">Expense Form</h5>
                            <small>Fill in information below to add a expense item to your expense.</small>
                        </div>
                        <div class="card-body">
                            <div class="mb-3">
                                <label class="form-label fw-bold">Category</label>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <label class="form-label fw-bold">Item Name</label>
                                <asp:DropDownList ID="ddlItemCode" runat="server" CssClass="form-select" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="row">
                                <div class="col-6 mb-3">
                                    <label class="form-label fw-bold">Cost</label>
                                    <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                </div>
                                <div class="col-6 mb-3">
                                    <label class="form-label fw-bold">Quantity</label>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="rqfDate"
                                        runat="server"
                                        ErrorMessage="*requier"
                                        CssClass="text-danger"
                                        ControlToValidate="txtQuantity"
                                        Display="Dynamic"
                                        ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-6 mb-3">
                                    <label class="form-label fw-bold">Unit Stock</label>
                                    <asp:TextBox ID="txtUnitStock" runat="server" CssClass="form-control "></asp:TextBox>
                                </div>
                                <div class="col-6 mb-3">
                                    <label class="form-label fw-bold">Total</label>
                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control " onkeyup="removeNonNumeric(this)" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-6 mb-3">
                                    <label class="form-label fw-bold">Discount %</label>
                                    <asp:TextBox ID="txtDiscountPercent" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                </div>
                                <div class="col-6 mb-3">
                                    <label class="form-label fw-bold">Discount Amount</label>
                                    <asp:TextBox ID="txtDiscountAmount" runat="server" CssClass="form-control " onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                </div>
                                <div class="col-6 mb-3">
                                    <label class="form-label fw-bold">Total Discount</label>
                                    <asp:TextBox ID="txtTotalDiscount" runat="server" CssClass="form-control " ReadOnly="true" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-6 mb-3">
                                    <label class="form-label fw-bold">Sub Total</label>
                                    <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control " ReadOnly="true" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-12 text-end mb-3">
                                    <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="Add" ValidationGroup="Save" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-9">
                    <div class="card shadow-sm">
                        <div class="card-header bg-success text-white">
                            <div class="row">
                                <div class="col-6">
                                    <h5 class="card-title mb-0">Expense Items</h5>
                                    <small>List all your expense items below.</small>
                                </div>
                                <div class="col-6 text-end">
                                    <label class="h5 card-title text-light">Expense No: </label>
                                    <asp:Label ID="lbExpenseNoDisplay" CssClass="text-light" runat="server" Text="HQ-E24090003"></asp:Label>
                                    <h6 class="card-title">Total AmountItem</h6>
                                    <asp:Label ID="lbTotalAmountDisplay" CssClass="text-warning fw-bold h5" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="gvAddExpense" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                EmptyDataText="Empty Information Item." 
                                DataKeyNames="BillItemCode"
                                OnRowDataBound="gvAddExpense_RowDataBound"
                                OnRowCommand="gvAddExpense_RowCommand"
                                >
                                <Columns>
                                    <asp:BoundField DataField="RowNo" HeaderText="#" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="PurDescription" HeaderText="Description" />
                                    <asp:BoundField DataField="OrderQty" HeaderText="Quantity" DataFormatString="{0:N0}" />
                                    <asp:BoundField DataField="UnitBill" HeaderText="Unit" />
                                    <asp:BoundField DataField="Cost" HeaderText="Cost" DataFormatString="{0:##0,#.00}" />
                                    <asp:BoundField DataField="DiscountPercent" HeaderText="Discount %" DataFormatString="{0:##0,#.00}" />
                                    <asp:BoundField DataField="Discount" HeaderText="Discount Amount" DataFormatString="{0:##0,#.00}"/>
                                    <asp:BoundField DataField="TotalDiscount" HeaderText="Total Discount" DataFormatString="{0:##0,#.00}" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:##0,#.00}" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-decoration-none" CommandName="EditItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'><i class="fi fi-rr-edit text-primary"></i> </asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-decoration-none" CommandName="DeleteItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'><i class="fi fi-rr-trash text-danger"></i> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var cost = $('#' + '<%=txtCost.ClientID%>');
            var qty = $('#' + '<%=txtQuantity.ClientID%>');
            var total = $('#' + '<%=txtTotal.ClientID%>');
            var discountPercent = $('#' + '<%= txtDiscountPercent.ClientID%>');
            var discountAmount = $('#' + '<%= txtDiscountAmount.ClientID%>');
            var totalDiscount = $('#' + '<%= txtTotalDiscount.ClientID%>');
            var subTotal = $('#' + '<%= txtSubTotal.ClientID%>');

            function updateTotal() {
                var qtyVal = parseFloat(qty.val()) || 0;
                var costVal = parseFloat(cost.val()) || 0;

                var totalVal = qtyVal * costVal;

                total.val(totalVal.toFixed(2));

                updateTotalDiscount();
            }

            function updateQuantity() {
                var totalVal = parseFloat(total.val()) || 0;
                var costVal = parseFloat(cost.val()) || 0;

                var qtyVal = costVal > 0 ? totalVal / costVal : 0;

                qty.val(qtyVal.toFixed(2));
            }

            function updateTotalDiscount() {
                var totalVal = parseFloat(total.val()) || 0;
                var discountPerVal = parseFloat(discountPercent.val()) || 0;
                var discountAmVal = parseFloat(discountAmount.val()) || 0;

                var totalDiscountAmount = (totalVal * discountPerVal) / 100 + discountAmVal;

                totalDiscount.val(totalDiscountAmount.toFixed(2));

                var subTotalVal = totalVal - totalDiscountAmount;
                subTotal.val(subTotalVal.toFixed(2));
            }

            cost.add(qty).on('keyup', function () {
                updateTotal();
            });

            total.on('keyup', function () {
                updateQuantity();
            });

            discountPercent.add(discountAmount).on('keyup', function () {
                updateTotalDiscount();
            });

        });
        function removeNonNumeric(input) {
            //input.value = input.value.replace(/\D/g,''); // \D : if non-num will remove
            input.value = input.value.replace(/[^0-9.]/g, ''); // Allow only numbers and dot
            if (input.value.split('.').length > 2) {
                input.value = input.value.replace(/\.+$/, ""); // Remove any extra dots
            }
        }
    </script>
</asp:Content>
