<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormSaleReceiptAddItem.aspx.cs" Inherits="WebFormUnit.Form.Transactions.SaleReceipts.FormSaleReceiptAddItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-regular-rounded/css/uicons-regular-rounded.css'>
   <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-straight/css/uicons-solid-straight.css'>
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <asp:HiddenField ID="hdfInvoiceCode" runat="server" />
    <div class="wrapper">
        <div class="shadow-sm ps-2 pt-0 m-3 py-3">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="Back" OnClick="btnBack_Click" />
            <asp:Button ID="btnNew" runat="server" CssClass="btn btn-primary" Text="New" OnClick="btnNew_Click" />
        </div>
        <div class="row">
            <div class="col-3">
                <div class="card shadow-sm">
                    <div class="card-header">
                        <h5 class="text-success">Sale Form</h5>
                        <p class="text-muted">Fill in information below to add a sale item to your receipt.</p>
                    </div>
                    <div class="card-body">
                        <div class="mt-0">
                            <label class="form-label">Category</label>
                            <asp:DropDownList ID="ddlCategoryCode" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryCode_SelectedIndexChanged" ></asp:DropDownList>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Item Name</label>
                            <asp:DropDownList ID="ddlItemCode" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged" ></asp:DropDownList>
                        </div>
                        <div class="row">
                            <div class="col-6 mt-3">
                                <label class="form-label">Sale Price</label>
                                <asp:TextBox ID="txtSalePrice" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Quantity</label>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                     ID="rfvQuantity"
                                     runat="server"
                                     ErrorMessage="*require"
                                     ControlToValidate="txtQuantity"
                                     CssClass="text-danger"
                                     Display="Dynamic"
                                     ValidationGroup="Save"
                                    ></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Unit Sale</label>
                                <asp:DropDownList ID="ddlUnitSale" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Total</label>
                                <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Discount %</label>
                                <asp:TextBox ID="txtDiscountPercent" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Discount Amount</label>
                                <asp:TextBox ID="txtDiscountAmount" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Total Discount</label>
                                <asp:TextBox ID="txtTotalDiscount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-6 mt-3">
                                <label class="form-label">Sub Total</label>
                                <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                            </div>                         
                        </div>
                    </div>
                    <div class="card-footer text-end">
                         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="Save" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="card shadow-sm">
                     <div class="card-header">
                         <div class="row">
                             <div class="col-6">
                                 <h5 class="text-primary">Sale Items</h5>
                                 <p class="text-muted">List all your sale receipt items below.</p>
                             </div>
                             <div class="col-6 text-end">
                                 <h5 class="text-success">Invoice No: <asp:Label ID="lbDisplayInvoiceNo" runat="server" CssClass="text-success h5"></asp:Label> </h5>
                                 <asp:Label ID="lbDisplayAmountDue" runat="server" CssClass="text-danger h5" Text="0"></asp:Label>
                             </div>
                         </div>
                     </div>
                    <div class="card-body">
                        <asp:GridView ID="gvSaleReceiptDetail" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                             EmptyDataText="Empty SaleReceipt Info here."
                             DataKeyNames="InvoiceCode"
                             OnRowCommand="gvSaleReceiptDetail_RowCommand"
                             OnRowDataBound="gvSaleReceiptDetail_RowDataBound"
                            >
                            <Columns>
                                <asp:BoundField DataField="RowNo" HeaderText="#" />
                                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" />
                                <asp:BoundField DataField="SaleDescription" HeaderText="Description" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:F0}"/>
                                <asp:BoundField DataField="SaleUnit" HeaderText="Unit" />
                                <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" DataFormatString="{0:F2}"/>
                                <asp:BoundField DataField="DiscountAmount" HeaderText="Discount Amount" DataFormatString="{0:F2}" />
                                <asp:BoundField DataField="DiscountPercent" HeaderText="Discount %" DataFormatString="{0:F2}" />
                                <asp:BoundField DataField="TotalDiscount" HeaderText="Total Discount" DataFormatString="{0:F2}" />
                                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:F2}" />
                               <asp:TemplateField HeaderText="Action">
                                   <ItemTemplate>
                                       <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-decoration-none text-primary" CommandName="EditItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-edit"></i></asp:LinkButton>
                                       <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-decoration-none text-danger" CommandName="DeleteItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-ss-trash"></i></asp:LinkButton>
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Delete Modal --%>
    <div class="modal fade" id="deleteModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-muted">Confirm Delete Item</h5>                   
                </div>
                <div class="modal-body">
                    <p class="text-muted">Are you sure want to delete this item.</p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click"/>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script src="../../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../../Scripts/bootstrap.min.js"></script>
    <script src="../../../Scripts/bootstrap.bundle.min.js"></script>
    <script>
        function DeleteModal() {
            var isDelete = new bootstrap.Modal(document.getElementById('deleteModal'));
            isDelete.show();
        }
        function removeNonNumeric(input) {        
            input.value = input.value.replace(/[^0-9.]/g, ''); 
            if (input.value.split('.').length > 2) {
                input.value = input.value.replace(/\.+$/, ""); 
            }
        }
        $(document).ready(function () {
            var salePrice = $('#' + '<%=txtSalePrice.ClientID%>');
            var qty = $('#' + '<%=txtQuantity.ClientID%>');
            var total = $('#' + '<%=txtTotal.ClientID%>');
            var discountPercent = $('#' + '<%=txtDiscountPercent.ClientID%>');
            var discountAmount = $('#' + '<%=txtDiscountAmount.ClientID%>');
            var totalDiscount = $('#'+'<%=txtTotalDiscount.ClientID%>');
            var subTotal = $('#'+'<%=txtSubTotal.ClientID%>');

            function GetTotal() {
                var SalePriceVal = parseFloat(salePrice.val()) || 0;
                var qtyVal = parseFloat(qty.val()) || 0;
                var totalVal = SalePriceVal * qtyVal;
                total.val(totalVal);
            }

            function GetTotalDiscount() {
                var totalVal = parseFloat(total.val()) || 0;
                var discountPercentVal = parseFloat(discountPercent.val()) || 0;
                var discountAmountVal = parseFloat(discountAmount.val()) || 0;
                var totalDiscountVal = (totalVal * (discountPercentVal / 100)) + discountAmountVal
                totalDiscount.val(totalDiscountVal.toFixed("F2"));nt
            }
            function GetSubTotal() {
                var totalVal = parseFloat(total.val()) || 0;
                var totalDiscountVal = parseFloat(totalDiscount.val()) || 0;
                var subTotalVal = totalVal - totalDiscountVal;
                subTotal.val(subTotalVal.toFixed("F2"));
            }

            qty.add(salePrice).on("keyup", function () {
                GetTotal();
                GetSubTotal();
            });
            discountPercent.add(discountAmount).on('keyup', function () {
                GetTotalDiscount();
                GetSubTotal();
            });
        });
    </script>
    
</asp:Content>
