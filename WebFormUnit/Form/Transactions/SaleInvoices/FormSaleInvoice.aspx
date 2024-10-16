<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormSaleInvoice.aspx.cs" Inherits="WebFormUnit.Form.Transactions.SaleInvoices.FormSaleInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-straight/css/uicons-solid-straight.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-straight/css/uicons-solid-straight.css'>

    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
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
        <div class="shadow-sm mb-3 py-3 ps-2 hstack gap-4">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="btnNewInvoice" runat="server" CssClass="text-decoration-none text-success " OnClick="btnNewInvoice_Click"><i class="fi fi-ss-add-document"></i> New</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:LinkButton ID="btnSave" runat="server" CssClass="text-decoration-none text-primary"><i class="fi fi-ss-disk"></i> Save</asp:LinkButton>
            <asp:LinkButton ID="btnOpen" runat="server" CssClass="text-decoration-none text-primary"><i class="fi fi-ss-folder-open"></i> Open</asp:LinkButton>
        </div>
        <div class="row mt-3">
            <div class="col-3">
                <div class="card shadow-sm">
                    <div class="card-header">
                        <h5 class="card-title text-primary">Invoice No</h5>
                        <p class="card-text text-muted">Enter invoice for items that you made from sale</p>
                    </div>
                    <div class="card-body">
                        <div class="mt-0">
                            <label class="form-label">Invoice No</label>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Customer</label>
                            <asp:DropDownList ID="ddlCustomerCode" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Date</label>
                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Memo</label>
                            <asp:TextBox ID="txtMemoInvoice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title text-primary">Item</h5>
                        <p class="text-muted">Enables you to track how much money you made from sale items.</p>
                    </div>
                    <div class="card-body">
                        <div class="card-body">
                            <nav class="nav nav-tabs">
                                <button class="nav-link text-secondary active" id="nav-item-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true">
                                    Add Item <span class="badge bg-success">
                                        <asp:Label ID="lbDisplayTotalInvoiceDetail" runat="server" CssClass="text-light" Text="0"></asp:Label></span></button>
                            </nav>
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-item-tab" tabindex="0">
                                    <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-primary mt-3" Text="Add New Sale Item" OnClick="btnAddItem_Click" />
                                    <p class="text-muted mt-2">You can enter invoices for your inventory and non-inventory parts and services.</p>

                                    <asp:GridView ID="gvSaleInvoice" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                        EmptyDataText="Empty Item Information."
                                        DataKeyNames="InvoiceCode,InvoiceNo"
                                        OnRowCommand="gvSaleInvoice_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="RowNo" HeaderText="#" />
                                            <asp:BoundField DataField="" HeaderText="ItemCode" />
                                            <asp:BoundField DataField="" HeaderText="Description" />
                                            <asp:BoundField DataField="" HeaderText="Quantity" DataFormatString="{0:F0}" />
                                            <asp:BoundField DataField="" HeaderText="Unit" />
                                            <asp:BoundField DataField="" HeaderText="Sale Price" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="" HeaderText="Discount Amount" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="" HeaderText="Discount %" DataFormatString="{0:F2}" />
                                            <asp:BoundField DataField="" HeaderText="Total Discount" DataFormatString="{0:F2}" />
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
                <div class="card shadow-sm mt-5">
                    <div class="card-body">
                        <div class="hstack gap-4">
                            <div class="py-3">
                                <label class="form-label">VAT%</label>
                                <asp:TextBox ID="txtVatePercent" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="py-3">
                                <label class="form-label">Vat Amount</label>
                                <asp:TextBox ID="txtVatAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="py-3">
                                <label class="form-label">Discount %</label>
                                <asp:TextBox ID="txtDiscountPercent" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="py-3">
                                <label class="form-label">Discount Amount</label>
                                <asp:TextBox ID="txtDiscountAmount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="py-3">
                                <label class="form-label">Total Discount %</label>
                                <asp:TextBox ID="txtTotalDiscountPercent" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="py-3">
                                <label class="form-label">Total Discount</label>
                                <asp:TextBox ID="txtTotalDiscount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
