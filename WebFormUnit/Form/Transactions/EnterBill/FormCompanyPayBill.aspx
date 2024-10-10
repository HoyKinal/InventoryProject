<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCompanyPayBill.aspx.cs" Inherits="WebFormUnit.Form.Transactions.EnterBill.FormCompanyPayBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />

    <style>
        /* Header styling */
        .datepicker .datepicker-days .table-condensed thead {
            background-color: #007bff; /* Vibrant blue background */
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

        /* Header hover effect */
        .datepicker .datepicker-days .table-condensed thead:hover,
        .datepicker .datepicker-months .table-condensed thead:hover,
        .datepicker .datepicker-years .table-condensed thead:hover,
        .datepicker .datepicker-decades .table-condensed thead:hover {
            background-color: #0056b3; 
        }

        /* Navigation arrows styling */
        .datepicker .datepicker-days .prev,
        .datepicker .datepicker-days .next {
            background-color: #0056b3; 
            color: white;
            border-radius: 0;
        }
        /* Navigation arrows styling */
        .datepicker .datepicker-days .prev:hover,
        .datepicker .datepicker-days .next:hover {
            background-color: darkblue;
            color: white;
            border-radius: 0; 
        }
        /* Highlighting today's date */
        .datepicker .datepicker-days .table-condensed td.today {
            background-color: darkblue;
            color: white;
            border-radius: 0; 
        }

        /* Highlighting current date */
        .datepicker .datepicker-days .table-condensed td.active {
            background-color: #007bff;
            color: white; 
            border-radius: 0; 
        }

        /* Date cell styling */
        .datepicker .datepicker-days .table-condensed td {
            text-align: center; 
            border-radius: 0; 
        }

        /* Adjusting the overall padding and spacing */
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

        /* Adjusting overall padding and spacing */
        .datepicker .datepicker-days .table-condensed td,
        .datepicker .datepicker-days .table-condensed th {
            padding: 10px; 
        }

        /* Specific styles for the active/current date cell */
        .datepicker .datepicker-days .table-condensed td.active {
            background-color: blue; 
            color: white;
        }
    </style>

    <div class="wrapper bg-light p-3 rounded shadow-sm mb-2">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-outline-success me-2 mb-2 mb-md-0" OnClick="btnBack_Click">Back</asp:LinkButton>
                <asp:LinkButton ID="btnOpen" runat="server" CssClass="btn btn-outline-primary me-2 mb-2 mb-md-0" OnClick="btnOpen_Click">Open</asp:LinkButton>
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
                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary mt-3" Text="Search" OnClick="btnSearch_Click" />
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

                        <asp:GridView ID="gvPayBillHeader" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                            DataKeyNames="BillNumber" EmptyDataText="Empty Information Expense Header."
                            OnRowCommand="gvPayBillHeader_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="RowNo" HeaderText="#" />
                                <asp:BoundField DataField="DateBill" HeaderText="Date Bill" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                <asp:BoundField DataField="BillNumber" HeaderText="Expense No" />
                                <asp:BoundField DataField="VenderCode" HeaderText="Supplier" />
                                <asp:BoundField DataField="RefereceNo" HeaderText="Reference" />
                                <asp:BoundField DataField="GrandTotalWithVat" HeaderText="Grand Total (VAT)" DataFormatString="{0:F2}" />
                                <asp:BoundField DataField="Paid" HeaderText="Paid" DataFormatString="{0:F2}" />
                                <asp:BoundField DataField="Unpaid" HeaderText="Unpaid" DataFormatString="{0:F2}" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPay" runat="server" CssClass="text-decoration-none text-success" CommandName="PayItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-chart-mixed-up-circle-dollar"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnOpen" runat="server" CssClass="text-decoration-none text-primary" CommandName="OpenItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-rr-edit"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>

        <!-- Modal for Pay Bill -->
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
                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-6">
                                <label class="form-label">Bill Date</label>
                                <asp:TextBox ID="txtDateBill" runat="server" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-12 mt-3">
                                <label class="form-label">Unpaid Amount</label>
                                <asp:TextBox ID="txtUnpaidAmount" runat="server" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-12 mt-3">
                                <label class="form-label">Pay Date</label>
                                <asp:TextBox ID="txtDatePaid" runat="server" CssClass="form-control datepicker" Enabled="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-12 mt-3">
                                <label class="form-label">Pay Amount</label>
                                <asp:TextBox ID="txtPayAmount" runat="server" CssClass="form-control" Enabled="true" ClientIDMode="Static" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqfPayAmount" runat="server"
                                     ErrorMessage="*requier"
                                     CssClass="text-danger"
                                     Display="Dynamic"
                                     ControlToValidate="txtPayAmount"
                                     ValidationGroup="Save"
                                ></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 mt-3">
                                <label class="form-label">Memo</label>
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="form-control" Enabled="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hdfBillNumber" runat="server" />
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
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
        function showAddModal() {
            var showModal = new bootstrap.Modal(document.getElementById('addModalPayment'));
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
