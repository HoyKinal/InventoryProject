<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormCompanyEnterBill.aspx.cs" Inherits="WebFormUnit.Form.Transactions.EnterBill.FormCompanyEnterBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-regular-straight/css/uicons-regular-straight.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-straight/css/uicons-solid-straight.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-solid-rounded/css/uicons-solid-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-bold-straight/css/uicons-bold-straight.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-rounded/css/uicons-regular-rounded.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.5.1/uicons-regular-straight/css/uicons-regular-straight.css'>
    <link rel='stylesheet' href='https://cdn-uicons.flaticon.com/2.6.0/uicons-bold-rounded/css/uicons-bold-rounded.css'>
    <style>
        /* General datepicker styling */
        /* General datepicker styling */
        .datepicker {
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Subtle shadow for depth */
            border-radius: 0; /* Remove border radius */
        }

            /* Header styling */
            .datepicker .datepicker-days .table-condensed thead {
                background-color: #007bff; /* Vibrant blue background */
                color: white; /* White text for contrast */
                border-radius: 0; /* Ensure no border radius */
            }

            .datepicker .datepicker-months .table-condensed thead,
            .datepicker .datepicker-years .table-condensed thead,
            .datepicker .datepicker-decades .table-condensed thead {
                background-color: #007bff; /* Matching the background color */
                color: white;
                border-radius: 0; /* Remove border radius */
            }

                /* Header hover effect */
                .datepicker .datepicker-days .table-condensed thead:hover,
                .datepicker .datepicker-months .table-condensed thead:hover,
                .datepicker .datepicker-years .table-condensed thead:hover,
                .datepicker .datepicker-decades .table-condensed thead:hover {
                    background-color: #0056b3; /* Darker blue on hover */
                }

            /* Navigation arrows styling */
            .datepicker .datepicker-days .prev,
            .datepicker .datepicker-days .next {
                background-color: #0056b3; /* Darker blue for arrows */
                color: white;
                border-radius: 0; /* Ensure no border radius for arrows */
            }
                /* Navigation arrows styling */
                .datepicker .datepicker-days .prev:hover,
                .datepicker .datepicker-days .next:hover {
                    background-color: darkblue; /* Darker blue for arrows */
                    color: white;
                    border-radius: 0; /* Ensure no border radius for arrows */
                }
            /* Highlighting today's date */
            .datepicker .datepicker-days .table-condensed td.today {
                background-color: darkblue; /* Orange highlight for today's date */
                color: white;
                border-radius: 0; /* No border radius on today's date */
            }

            /* Highlighting current date */
            .datepicker .datepicker-days .table-condensed td.active {
                background-color: #007bff; /* Blue background for the current date */
                color: white; /* White text for contrast */
                border-radius: 0; /* Remove border radius */
            }

            /* Date cell styling */
            .datepicker .datepicker-days .table-condensed td {
                text-align: center; /* Centered text */
                border-radius: 0; /* Remove border radius for individual date cells */
            }

            /* Adjusting the overall padding and spacing */
            .datepicker .datepicker-days .table-condensed td,
            .datepicker .datepicker-days .table-condensed th {
                padding: 10px; /* Increased padding for better readability */
                border-radius: 0; /* Remove border radius for table cells */
            }

                .datepicker .datepicker-days .table-condensed td.active {
                    background-color: blue; /* Blue background for current date */
                    color: white;
                }
    </style>
    <asp:HiddenField ID="hdfBillNumber" runat="server" />

    <div class="container-fluid">
        <div class="d-flex flex-wrap justify-content-start align-items-center bg-light p-3 rounded shadow-sm mb-4">
            <asp:LinkButton ID="btnOpenItem" runat="server" CssClass="btn btn-outline-primary me-2 mb-2 mb-md-0" OnClick="btnOpenItem_Click">
                 <i class="fi fi-ss-folder-open"></i> Open
            </asp:LinkButton>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="btnNewItem" runat="server" CssClass="btn btn-outline-success me-2 mb-2 mb-md-0" OnClick="btnNewItem_Click" OnClientClick="showSpinner();return false">
                    <i class="fi fi-ss-add-document"></i> New
                    </asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:LinkButton ID="btnDeleteItem" runat="server" CssClass="btn btn-outline-danger me-2 mb-2 mb-md-0" OnClick="btnDeleteItem_Click">
                 <i class="fi fi-ss-trash"></i> Delete
            </asp:LinkButton>
            <asp:LinkButton ID="btnSaveItem" runat="server" CssClass="btn btn-outline-primary me-2 mb-2 mb-md-0" OnClick="btnSaveItem_Click">
                 <i class="fi fi-ss-disk"></i> Save
            </asp:LinkButton>
             <asp:LinkButton ID="btnPayment" runat="server" CssClass="btn btn-outline-primary me-2 mb-2 mb-md-0" OnClick="btnPayment_Click">
                 <i class="fi fi-br-payroll-calendar"></i> Payment
            </asp:LinkButton>
        </div>
        <!-- MainRow -->
        <div class="row g-3">
            <div class="col-lg-2">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h5 class="card-title mb-0">Enter Bill</h5>
                        <small>Enter bill for accounts and items that you purchase</small>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <label class="form-label fw-bold">Expense No</label>
                                    <asp:TextBox ID="txtBillNumberNo" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="mb-3">
                            <label class="form-label fw-bold">Supplier</label>
                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label class="form-label fw-bold">Start Date</label>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rqfDate"
                                runat="server"
                                ErrorMessage="*requier"
                                CssClass="text-danger"
                                ControlToValidate="txtStartDate"
                                Display="Dynamic"
                                ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <label class="form-label fw-bold">Expire Date</label>
                            <asp:TextBox ID="txtExpireDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1"
                                runat="server"
                                ErrorMessage="*requier"
                                CssClass="text-danger"
                                ControlToValidate="txtExpireDate"
                                Display="Dynamic"
                                ValidationGroup="AddNew"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <label class="form-label fw-bold">Reference</label>
                            <asp:TextBox ID="txtReference" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label fw-bold">Memo</label>
                            <asp:TextBox ID="txtMemo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8">
                <div class="card shadow-sm">
                    <div class="card-header bg-success text-white py-3">
                        <div class="row">
                            <div class="col-6">
                                <h5 class="card-title mb-0">Items</h5>
                                <small>Enables you to track how much money you spend on Accounts and Items .</small>
                            </div>
                            <div class="col-6 text-end">
                                <h5 class="card-title mb-0 text-light">Grand Total</h5>
                                <asp:Label ID="lbDisplayGrandTotal" runat="server" CssClass="text-warning h5" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <nav>
                            <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                <button class="nav-link fw-bold" id="nav-item-tab" data-bs-toggle="tab" data-bs-target="#nav-item" type="button" role="tab" aria-controls="nav-item" aria-selected="false">
                                    Item
                            <span class="badge bg-success ms-2">
                                <asp:Label ID="lbIncreaseItem" runat="server" Text="0"></asp:Label></span>
                                </button>
                            </div>
                            <div class="tab-content p-3" id="nav-tabContent">
                                <div class="tab-pane fade show active" id="nav-item" role="tabpanel" aria-labelledby="nav-item-tab">
                                    <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-primary mt-1" Text="Add New Expense" ValidationGroup="AddNew" OnClick="btnAddItem_Click" />
                                    <p class="text-muted mt-2">You can enter expense for your inventory, non-inventory parts and services.</p>
                                </div>
                                <div class="border border-bottom"></div>
                            </div>
                        </nav>
                        <div class="mt-3">
                            <asp:GridView ID="gvEnterBill" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                EmptyDataText="Empty Information Item."
                                DataKeyNames="BillItemCode"
                                OnRowCommand="gvEnterBill_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="RowNo" HeaderText="#" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="PurDescription" HeaderText="Description" />
                                    <asp:BoundField DataField="OrderQty" HeaderText="Quantity" DataFormatString="{0:N0}" />
                                    <asp:BoundField DataField="UnitBill" HeaderText="Unit" />
                                    <asp:BoundField DataField="Cost" HeaderText="Cost" DataFormatString="{0:##0,#.00}" />
                                    <asp:BoundField DataField="DiscountPercent" HeaderText="Discount %" DataFormatString="{0:##0,#.00}" />
                                    <asp:BoundField DataField="Discount" HeaderText="Discount Amount" DataFormatString="{0:##0,#.00}" />
                                    <asp:BoundField DataField="TotalDiscount" HeaderText="Total Discount" DataFormatString="{0:##0,#.00}" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:##0,#.00}" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-decoration-none" CommandName="EditItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'><i class="fi fi-rr-edit text-primary"></i> </asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="text-decoration-none" CommandName="DeleteItem" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'><i class="fi fi-rr-trash text-danger"></i> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                                <PagerStyle BackColor="LightCyan" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="card shadow-sm mt-4">
                    <div class="card-body hstack gap-3 py-4">
                        <div class="form-group">
                            <label class="form-label">VAT%</label>
                            <asp:TextBox ID="txtVATPercent" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">VAT Amount</label>
                            <asp:TextBox ID="txtVatAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Discount %</label>
                            <asp:TextBox ID="txtDiscountPercent" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Discount Amount</label>
                            <asp:TextBox ID="txtDiscountAmount" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Total Discount %</label>
                            <asp:TextBox ID="txtTotalDiscountPercent" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Total Discount</label>
                            <asp:TextBox ID="txtTotalDiscount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-2">
                <div class="card shadow-sm">
                    <div class="card-header bg-info text-white">
                        <h5 class="card-title mb-0">Summary Item Info.</h5>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-between mb-2">
                            <span class="fw-bold">Discount on Item:</span>
                            <asp:Label ID="lbDiscount" runat="server" CssClass="fw-bold" Text="0"></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between mb-2">
                            <span class="fw-bold">Amount:</span>
                            <asp:Label ID="lbAmount" runat="server" CssClass="fw-bold" Text="0"></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between mb-2">
                            <span class="fw-bold">Total Discount:</span>
                            <asp:Label ID="lbTotalDicount" runat="server" CssClass="fw-bold" Text="0"></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between mb-2">
                            <span class="fw-bold">Grand Total:</span>
                            <asp:Label ID="lbGrandTotalHeader" runat="server" CssClass="fw-bold" Text="0"></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between mb-2">
                            <span class="fw-bold">Total VAT:</span>
                            <asp:Label ID="lbTotalVat" runat="server" CssClass="fw-bold" Text="0"></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span class="fw-bold">Grand Total (VAT):</span>
                            <asp:Label ID="lbGrandTotalVat" runat="server" CssClass="fw-bold" Text="0"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
 
    <%--<div class="d-flex justify-content-end">
        <div id="loadingSpinner" class="spinner-border text-success" style="width: 3rem; height: 3rem; display: none;" role="status">
            <span class="sr-only"></span>
        </div>
    </div>--%>


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
        //Spinner
        function showSpinner() {
            document.getElementById("loadingSpinner").style.display = "block";

            setTimeout(function () {
                document.getElementById("loadingSpinner").style.display = "none";
            }, 2000);
        }
    </script>
</asp:Content>
