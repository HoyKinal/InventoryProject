<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCompanyPayBillList.aspx.cs" Inherits="WebFormUnit.Form.Transactions.EnterBill.FormCompanyPayBillList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-straight/css/uicons-solid-straight.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-rounded/css/uicons-solid-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <style>
        .datepicker {
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 0;
        }

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

        .btn-icon {
            background-color: transparent;
            border: none;
            font-size: 20px;
            color: #0094ff;
        }

        .dropdown-menu {
            min-width: 160px;
            padding: 0.5rem 0;
            border: 1px solid #ced4da;
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
        }

        .dropdown-item {
            padding: 0.5rem 1rem;
            font-size: 0.875rem;
            color: #495057;
            transition: background-color 0.15s, color 0.15s;
        }

            .dropdown-item:hover {
                background-color: #f8f9fa;
                color: #0094ff;
            }

        .custom-dropdown-wrapper {
            position: relative;
        }
    </style>
    <div class="wrapper bg-light p-3 rounded shadow-sm mb-2">
        <div class="row">
            <div class="col-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-outline-success me-2 mb-2 mb-md-0" OnClick="btnBack_Click"> Back </asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-6 d-flex justify-content-end align-items-center custom-dropdown-wrapper">
                <asp:DropDownList ID="ddlExports" runat="server" CssClass="form-select w-auto d-none">
                    <asp:ListItem Value="" Text="Export Fille" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Excel" Text="Excel"></asp:ListItem>
                    <asp:ListItem Value="PDF" Text="PDF"></asp:ListItem>
                    <asp:ListItem Value="PrintCurrentPriview" Text="Print current preview"></asp:ListItem>
                    <asp:ListItem Value="PrintAllData" Text="Print all data"></asp:ListItem>
                </asp:DropDownList>
                <button type="button" class="btn-icon" data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="fi fi-br-menu-dots-vertical icon"></i>
                </button>
                <ul class="dropdown-menu">
                    <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('Excel')">Export Excel</a></li>
                    <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('PDF')">Export PDF</a></li>
                    <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('PrintCurrentPriview')">Current Preview</a></li>
                    <li><a class="dropdown-item" href="#" onclick="selectDropdownItem('PrintAllData')">Preview All Data</a></li>
                </ul>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <div class="card shadow-sm mt-3">
                    <div class="card-header">
                        <h5 class="card-title text-primary fw-bold">Find Paid Bill</h5>
                        <p class="card-text text-muted">Choose criteria below to filter your paid bill</p>
                    </div>
                    <div class="card-body">
                        <div class="mt-1">
                            <label class="form-label">From Date</label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">To Date</label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                        <div class="mt-3">
                            <label class="form-label">Search</label>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeyup=""></asp:TextBox>
                        </div>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary mt-3" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="card shadow-sm mt-3">
                    <div class="card-header">
                        <h5 class="card-title text-primary fw-bold">Paid Bill List</h5>
                        <p class="card-text text-muted">Showing your paid bill based on the selected criteria</p>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvPayBill" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                            DataKeyNames="BillNo, PurchaseReturnNo"
                            EmptyDataText="Enpty Information Expense Header."
                            OnRowCommand="gvPayBill_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="RowNo" HeaderText="#" />
                                <asp:BoundField DataField="DatePaid" HeaderText="PaidDate" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                <asp:BoundField DataField="MemoReturnPaid" HeaderText="Memo" />
                                <asp:BoundField DataField="PaidAmount" HeaderText="Paid" DataFormatString="{0:F2}" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnOpenItem" runat="server" CssClass="text-decoration-none text-secondary" CommandName="OpenItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-ss-box-open-full"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEditItem" runat="server" CssClass="text-decoration-none text-primary" CommandName="EditItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-ss-file-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDeleteItem" runat="server" CssClass="text-decoration-none text-danger" CommandName="DeleteItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="fi fi-br-trash"></i></asp:LinkButton>
                                        <asp:HiddenField ID="hdfPaidDate"  runat="server" Value='<%# Eval("DatePaid") %>' />
                                        <asp:HiddenField ID="hdfPaidAmount"  runat="server" Value='<%# Eval("PaidAmount") %>' />
                                        <asp:HiddenField ID="hdfMemoReturnPaid"  runat="server" Value='<%# Eval("MemoReturnPaid") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
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
                            <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                        </div>
                        <div class="col-6">
                            <label class="form-label">Bill Date</label>
                            <asp:TextBox ID="txtDateBill" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-12 mt-3">
                            <label class="form-label">Unpaid Amount</label>
                            <asp:TextBox ID="txtUnpaidAmount" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                        </div>
                        <div class="col-12 mt-3">
                            <label class="form-label">Paid Date</label>
                            <asp:TextBox ID="txtDatePaid" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                        <div class="col-12 mt-3">
                            <label class="form-label">Pay Amount</label>
                            <asp:TextBox ID="txtPayAmount" runat="server" CssClass="form-control"  onkeyup="removeNonNumeric(this)"></asp:TextBox>
                        </div>
                        <div class="col-12 mt-3">
                            <label class="form-label">Memo</label>
                            <asp:TextBox ID="txtMemo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"  />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
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
        });
    </script>
</asp:Content>
