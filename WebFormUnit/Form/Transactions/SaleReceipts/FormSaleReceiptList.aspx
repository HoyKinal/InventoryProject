<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormSaleReceiptList.aspx.cs" Inherits="WebFormUnit.Form.Transactions.SaleReceipts.FormSaleReceiptList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-straight/css/uicons-solid-straight.css'>
    <style>
        /* General datepicker styling */
        .datepicker {
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Subtle shadow for depth */
            border-radius: 0; /* Remove border radius */
        }

            /* Header styling */
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
    </style>

    <div class="wrapper">
        <div class="mt-3 shadow-sm pt-0 py-3">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />
        </div>
        <div class="row">
            <div class="col-3">
                <div class="card mt-3">
                    <div class="card-header">
                        <h5 class="text-primary">Find Sale Receipt</h5>
                        <p class="text-muted">Choose criteria below to filter your sale receipts</p>
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
                    <div class="card-footer text-end">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-9 mt-3">
                <asp:GridView ID="gvReceiptList" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                    EmptyDataText="Empty Information Receipt Header"
                    DataKeyNames="InvoiceNo"
                    OnRowCommand="gvReceiptList_RowCommand"
                    >
                    <Columns>
                        <asp:BoundField DataField="RowNo" HeaderText="#" />
                        <asp:BoundField DataField="InvoiceDate" HeaderText="ReceiptDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="InvoiceNo" HeaderText="ReceiptNo" />
                        <asp:BoundField DataField="CustomerCode" HeaderText="Customer" />
                        <asp:BoundField DataField="Memo" HeaderText="Memo" />
                        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="TotalDiscountItem" HeaderText="Discount Item" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="GrandTotal" HeaderText="Grand Total" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="TotalDiscountHeader" HeaderText="Discount Header" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="TotalVatAmount" HeaderText="Total VatAmount" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="GrandTotalWithVat" HeaderText="Grand Total(VAT)" DataFormatString="{0:F2}" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnOpenEdit" runat="server" CssClass="text-decoration-none text-success" CommandName="OpenEdit" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-ss-home"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- Include jQuery and Bootstrap JavaScript -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Include Bootstrap Datepicker JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <!-- Include Bootstrap Datepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet">
    <!-- Include Timepicker JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/js/bootstrap-timepicker.min.js"></script>
    <!-- Include Timepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/css/bootstrap-timepicker.min.css" rel="stylesheet">
    <script>
        function DeleteModal() {
            var isDelete = new bootstrap.Modal(document.getElementById('deleteModal'));
            isDelete.show();
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
