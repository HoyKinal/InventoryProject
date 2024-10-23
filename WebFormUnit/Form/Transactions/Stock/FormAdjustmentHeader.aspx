<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormAdjustmentHeader.aspx.cs" Inherits="WebFormUnit.Form.Transactions.Stock.FormAdjustmentHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="wrapper mt-3">
        <div class=" shadow-sm pb-3 hstack gap-3">
            <asp:LinkButton ID="btnOpen" runat="server" CssClass="btn btn-primary" Text="Open"></asp:LinkButton>
            <asp:LinkButton ID="btnNew" runat="server" CssClass="btn btn-success" Text="New"></asp:LinkButton>
        </div>
        <div class="row mt-3">
            <div class="col-3">
                <div class="card shadow-sm">
                    <div class="card-header">
                        <h5 class="text-primary">Adjustment</h5>
                        <p class="text-muted">Adjust inventory item quantity</p>
                    </div>
                    <div class="card-body">
                        <div class="mt-0">
                            <label class="form-label">Adjustment No</label>
                            <asp:TextBox ID="txtAdjustmentNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Adjustment Date</label>
                            <asp:TextBox ID="txtAdjustmentDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Memo</label>
                            <asp:TextBox ID="txtAdjustmentMemo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mt-3 hstack gap-5">
                            <label class="form-label fw-bold">Adjustment Total Value:</label>
                            <asp:Label ID="lbTotalValue" runat="server" CssClass="form-label fw-bold" Text="0"></asp:Label>
                        </div>
                        <div class="mt-3 hstack gap-5">
                            <label class="form-label fw-bold">Number of Item Adjustemted:</label>
                            <asp:Label ID="lbItemAdjustmented" runat="server" CssClass="form-label fw-bold" Text="0"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="card shadow-sm">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-6">
                                <h5 class="text-success">Items</h5>
                                <p class="text-muted">Enables you to adjust item quantity.</p>
                            </div>
                            <div class="col-6 text-end">
                                <h4 class="text-success">Amount</h4>
                                <asp:Label ID="lbDisplayTotalAmount" runat="server" CssClass="text-danger h4" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <nav class="nav nav-tabs">
                            <button class="nav-link text-secondary active" id="nav-item-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true">
                                Add Item <span class="badge bg-success">
                                    <asp:Label ID="lbDisplayTotalDetail" runat="server" CssClass="text-light" Text="0"></asp:Label></span></button>
                        </nav>
                        <div class="tab-content">
                            <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-item-tab" tabindex="0">
                                <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-primary mt-3" Text="Add New Sale Item" OnClick="btnAddItem_Click" />
                                <p class="text-muted mt-2">You can enter invoices for your inventory and non-inventory parts and services.</p>
                                <asp:GridView ID="gvSaleInvoice" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                    EmptyDataText="Empty Item Information."
                                    DataKeyNames=""
                                   >
                                    <Columns>
                                        <asp:BoundField DataField="RowNo" HeaderText="#" />
                                        <asp:BoundField DataField="" HeaderText="ItemCode" />
                                        <asp:BoundField DataField="" HeaderText="Purchase Description" />
                                        <asp:BoundField DataField="" HeaderText="Status" />
                                        <asp:BoundField DataField="" HeaderText="Quantity" DataFormatString="{0:F0}" />
                                        <asp:BoundField DataField="" HeaderText="Unit" />
                                        <asp:BoundField DataField="" HeaderText="Cost" DataFormatString="{0:F2}" />                                        
                                        <asp:BoundField DataField="" HeaderText="Total" DataFormatString="{0:F2}" />
                                        <asp:TemplateField>
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
