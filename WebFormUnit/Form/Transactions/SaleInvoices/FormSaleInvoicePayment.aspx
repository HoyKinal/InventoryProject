<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormSaleInvoicePayment.aspx.cs" Inherits="WebFormUnit.Form.Transactions.SaleInvoices.FormSaleInvoicePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-straight/css/uicons-solid-straight.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-bold-rounded/css/uicons-bold-rounded.css'>
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
            .datepicker .datepicker-days .next:hover,
            .datepicker .datepicker-days .datepicker-switch:hover {
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
    <div class="wrapper">
        <div class="mt-3 hstack gap-3 shadow-sm pt-0 py-3">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />
            <asp:Button ID="btnOpen" runat="server" CssClass="btn btn-success" Text="Open" OnClick="btnOpen_Click" />
        </div>
        <div class="row mt-3">
            <div class="col-3">
                <div class="card">
                    <div class="card-header">
                        <h5 class="text-primary">Receive Payment</h5>
                        <p class="text-muted">Showing your received payment based on the selected criteria</p>
                    </div>
                    <div class="card-body">
                        <label class="form-label">Customer</label>
                        <asp:DropDownList ID="ddlCustomerCode" runat="server" CssClass="form-select"></asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary mt-3" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-9">
                <asp:GridView ID="gvSaleInvoicePayment" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                    EmptyDataText="Empty record payment info."
                    DataKeyNames="InvoiceNo"
                    OnRowCommand="gvSaleInvoicePayment_RowCommand"
                    OnRowDataBound="gvSaleInvoicePayment_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="RowNo" HeaderText="#" />
                        <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                        <asp:BoundField DataField="Reference" HeaderText="Reference" />
                        <asp:BoundField DataField="TotalInvoice" HeaderText="Invoiced Total" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="TotalReceiptPayment" HeaderText="Received" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="TotalRemainAmount" HeaderText="Remain" DataFormatString="{0:F2}" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnOpenItem" runat="server" CssClass="text-decoration-none text-primary" CommandName="OpenItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-ss-house-chimney"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnPaymentItem" runat="server" CssClass="text-decoration-none text-success" CommandName="PaymentItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-br-chart-mixed-up-circle-dollar"></i></asp:LinkButton>
                                <asp:HiddenField ID="hfTotalInvoice" runat="server" Value='<%# Eval("TotalInvoice") %>' />
                                <asp:HiddenField ID="hfTotalReceiptPayment" runat="server" Value='<%# Eval("TotalReceiptPayment")%>' />
                                <asp:HiddenField ID="hdfRemainAmount" runat="server" Value='<%# Eval("TotalRemainAmount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- Modal for Pay Bill -->
    <div id="AddPaymentModal" class="modal">
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
                            <label class="form-label">Receive Date</label>
                            <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                        <div class="col-12 mt-3">
                            <label class="form-label">Receive Amount</label>
                            <asp:TextBox ID="txtReceiveAmount" runat="server" CssClass="form-control" onkeyup="removeNonNumeric(this)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfReceiveAmount" runat="server"
                                ErrorMessage="*requier"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ControlToValidate="txtReceiveAmount"
                                ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 mt-3">
                            <label class="form-label">Memo</label>
                            <asp:TextBox ID="txtMemo" runat="server" CssClass="form-control" Enabled="true" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hdfReceivedAmount" runat="server" />
                    <asp:HiddenField ID="hdfRemainAmount" runat="server" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
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
            var showModal = new bootstrap.Modal(document.getElementById('AddPaymentModal'));
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
