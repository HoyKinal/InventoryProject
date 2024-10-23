<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormSaleInvoicePaymentList.aspx.cs" Inherits="WebFormUnit.Form.Transactions.SaleInvoices.FormSaleInvoicePaymentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-straight/css/uicons-solid-straight.css'>
   <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-bold-straight/css/uicons-bold-straight.css'>
    <style>
        .datepicker .datepicker-days .table-condensed thead {
            background-color: #007bff;
            color: white;
            border-radius: 0;
        }

        .datepicker .datepicker-months .table-condensed thead,
        .datepicker .datepicker-years .table-condensed thead,
        .datepicker .datepicker-decades .table-condensed thead {
            background-color: #007bff;
            color: white;
            border-radius: 0;
        }

        .datepicker .datepicker-days .table-condensed thead:hover,
        .datepicker .datepicker-months .table-condensed thead:hover,
        .datepicker .datepicker-years .table-condensed thead:hover,
        .datepicker .datepicker-decades .table-condensed thead:hover
        {
            background-color: #0056b3;
        }

        .datepicker .datepicker-days .prev,
        .datepicker .datepicker-days .next
        {
            background-color: #0056b3;
            color: white;
            border-radius: 0;
        }

        .datepicker .datepicker-days .prev:hover,
        .datepicker .datepicker-days .next:hover,
        .datepicker .datepicker-days .datepicker-switch:hover {
            background-color: darkblue;
            color: white;
            border-radius: 0;
        }

        .datepicker .datepicker-days .table-condensed td.today {
            background-color: darkblue;
            color: white;
            border-radius: 0;
        }

        .datepicker .datepicker-days .table-condensed td.active {
            background-color: #007bff;
            color: white;
            border-radius: 0;
        }

        .datepicker .datepicker-days .table-condensed td {
            text-align: center;
            border-radius: 0;
        }

        .datepicker .datepicker-days .table-condensed td,
        .datepicker .datepicker-days .table-condensed th {
            padding: 10px;
            border-radius: 0;
        }

        .datepicker .datepicker-days .table-condensed td.active {
            background-color: blue;
            color: white;
        }

        ling
        .datepicker .datepicker-days .table-condensed td {
            text-align: center;
        }

        .datepicker .datepicker-days .table-condensed td,
        .datepicker .datepicker-days .table-condensed th {
            padding: 10px;
        }

        .datepicker .datepicker-days .table-condensed td.active {
            background-color: blue;
            color: white;
        }
    </style>
    <div class="wrapper">
        <div class="ps-3 mt-0 shadow-sm py-3 hstack gap-3">
            <asp:LinkButton ID="btnBack" runat="server" CssClass=" text-decoration-none text-secondary" OnClick="btnBack_Click"><i class="fi fi-bs-angle-left"></i> Back</asp:LinkButton>
            <asp:LinkButton ID="btnRecevePayment" runat="server" CssClass=" text-decoration-none text-success" OnClick="btnRecevePayment_Click"><i class="fi fi-ss-expense"></i> Receive Payment</asp:LinkButton>
        </div>
        <div class="row mt-3">
            <div class="col-3">
                <div class="card">
                    <div class="card-header">
                        <h5 class="text-primary">Find Received Payment</h5>
                        <p class="text-muted">Choose criteria below to filter your received payment</p>
                    </div>
                    <div class="card-body">
                        <div class="mt-0">
                            <label class="form-label">From Date</label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">To Date</label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Search</label>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="card-footer ">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="card">
                     <div class="card-header">
                         <h5 class="text-success">Received Payment List</h5>
                         <p class="text-muted">Showing your received payment based on the selected criteria</p>
                     </div>
                    <div class="card-body">
                        <asp:GridView ID="gvReceiptPaymentList" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                            EmptyDataText="Empty receive payment info."
                            DataKeyNames="InvoiceReturnNo,InvoiceNo"
                            OnRowCommand="gvReceiptPaymentList_RowCommand"
                            >
                            <Columns>
                                <asp:BoundField DataField="RowNo" HeaderText="#" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Received Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                                <asp:BoundField DataField="ReceiveMemo" HeaderText="Memo" />
                                <asp:BoundField DataField="ReceiveAmount" HeaderText="Receive Amount" DataFormatString="{0:F2}" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnOpen" runat="server" CssClass="text-decoration-none text-success" CommandName="OpenItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-ss-house-chimney"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-decoration-none" CommandName="EditItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-ss-user-pen"></i></asp:LinkButton>
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
     <!-- Modal for Invoice ADD/Edit -->
 <div id="EditPaymentModal" class="modal">
     <div class="modal-dialog modal-dialog-centered">
         <div class="modal-content">
             <div class="modal-header">
                 <h5 class="modal-title" id="addModalLabel">Receipt Payment</h5>
                 <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
             </div>
             <div class="modal-body">
                 <div class="row">
                     <div class="col-6">
                         <label class="form-label">Invoice No</label>
                         <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                     </div>
                     <div class="col-6">
                         <label class="form-label">Invoice Date</label>
                         <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                     </div>
                     <div class="col-12 mt-3">
                         <label class="form-label">Remain Amount</label>
                         <asp:TextBox ID="txtRemainAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                     </div>
                     <div class="col-12 mt-3">
                         <label class="form-label">Receipt Date</label>
                         <asp:TextBox ID="txtReceiptDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                     </div>
                     <div class="col-12 mt-3">
                         <label class="form-label">Receipt Amount</label>
                         <asp:TextBox ID="txtReceiptAmount" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rqfReceiptAmount" runat="server"
                             ErrorMessage="*requier"
                             CssClass="text-danger"
                             Display="Dynamic"
                             ControlToValidate="txtReceiptAmount"
                             ValidationGroup="Save"></asp:RequiredFieldValidator>
                     </div>
                     <div class="col-12 mt-3">
                         <label class="form-label">Memo</label>
                         <asp:TextBox ID="txtMemo" runat="server" CssClass="form-control" Enabled="true" ClientIDMode="Static"></asp:TextBox>
                     </div>
                 </div>
             </div>
             <div class="modal-footer">
                 <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                 <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
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
                 <p class="text-muted">Description: <asp:Label ID="lbDiscription" runat="server" Text="Unknown"></asp:Label></p>
             </div>
             <div class="modal-footer">
                 <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
                 <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
             </div>
         </div>
     </div>
 </div>
    <script src="../../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../../Scripts/bootstrap.bundle.js"></script>
    <!-- Include Bootstrap Datepicker JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <!-- Include Bootstrap Datepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet">
    <!-- Include Timepicker JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/js/bootstrap-timepicker.min.js"></script>
    <!-- Include Timepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/css/bootstrap-timepicker.min.css" rel="stylesheet">

    <script>
        function removeNonNumeric(input) {
            input.value = input.value.replace(/[^0-9.]/g, '');
            if (input.value.split('.').length > 2) {
                input.value = input.value.replace(/\.+$/, "");
            }
        }
        function showDeleteModal() {
            var showModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            showModal.show();
        }
        function showEditModal() {
            var showModal = new bootstrap.Modal(document.getElementById('EditPaymentModal'));
            showModal.show();
        }
        $(document).ready(function () {
            // Initialize datepicker
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                minViewMode: 0,
                language: "en",
                autoclose: true,
                todayHighlight: true
            }).on('changeDate', function (e) {
                var date = e.date;
                var startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 1);
                var endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 7);

                var options = { year: 'numeric', month: 'numeric', day: 'numeric' };
                var formattedStartDate = startDate.toLocaleDateString('en-GB', options);
                var formattedEndDate = endDate.toLocaleDateString('en-GB', options);

                console.log(formattedStartDate + ' - ' + formattedEndDate);
            });

            // Initialize timepicker
            $('.timepicker').timepicker({
                defaultTime: 'current',
                minuteStep: 1,
                showSeconds: false,
                showMeridian: true,
                snapToStep: true
            }).on('changeTime.timepicker', function (e) {
                var time = e.time.value;
                console.log('Selected time: ' + time);
            });
        });
    </script>
</asp:Content>
