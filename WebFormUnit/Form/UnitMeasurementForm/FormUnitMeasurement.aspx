<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormUnitMeasurement.aspx.cs" Inherits="WebFormUnit.Form.UnitMeasurementForm.FormUnitMeasurement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Protest+Guerrilla&family=Roboto+Slab:wght@100..900&display=swap');

        * {
            font-family: "Roboto Slab", serif;
        }
    </style>
    <%-- Card --%>
    <div class="wrapper">
        <div class="shadow mb-3 p-3">
            <h5 class="text-primary">Unit Measurement List</h5>
            <p class="text-muted">Make change to your unit measurement conversion for controlling item sale unit and stock unit in the list below.</p>
        </div>
        <div class="card">
            <div class="card-header">
                <div class="row">
                    <div class="col-5">
                        <div class="input-group">
                            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="Server" PlaceHolder="Find here"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="Server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="col-7 d-flex justify-content-end pe-5">
                        <asp:Button ID="btnAdd" runat="Server" CssClass="btn btn-primary" Text="Add" OnClientClick="showAddModal();return false;" />
                    </div>
                </div>
                <div class="row">
                </div>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvUnitMeasurement" runat="server" CssClass="table table-bordered table-striped table-responsive" AutoGenerateColumns="False" DataKeyNames="UnitFrom, UnitTo" OnRowCommand="gvUnitMeasurement_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="RowNo" HeaderText="#" ItemStyle-CssClass="CheckList" />
                        <asp:BoundField DataField="UnitFromName" HeaderText="Base Unit" />
                        <asp:BoundField DataField="UnitToName" HeaderText="Convert Unit" />
                        <asp:BoundField DataField="UnitFromDesc" HeaderText="Description" />
                        <asp:BoundField DataField="UnitOperator" HeaderText="Operator" />
                        <asp:BoundField DataField="UnitFactor" DataFormatString="{0:#,#.00}" HeaderText="Factor" />
                        <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="text-end pe-5">
                            <ItemTemplate>
                                <div class="text-end">
                                    <asp:Button ID="btnEdit" runat="Server" CssClass="btn btn-primary" Text="Edit" CommandName="EditMeasurement" CommandArgument='<%# string.Format("{0}|{1}", Eval("UnitFrom"), Eval("UnitTo")) %>' />
                                    <asp:Button ID="btnDelete" runat="Server" CssClass="btn btn-danger" Text="Delete" CommandName="DeleteMeasurement" CommandArgument='<%# string.Format("{0}|{1}", Eval("UnitFrom"), Eval("UnitTo")) %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
    <%-- Modal Add --%>
    <div class="modal fade" id="addModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-primary">Add New UnitMeasurement</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="label1" runat="server" CssClass="text-black" Text="Base Unit"></asp:Label>
                            <asp:DropDownList ID="ddlUnitFrom" runat="Server" CssClass="form-select mb-3">
                                <asp:ListItem Selected="True" Text="Select UnitFrom"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <asp:Label ID="label2" runat="server" CssClass="text-black" Text="Convert Unit"></asp:Label>
                            <asp:DropDownList ID="ddlUnitTo" runat="Server" CssClass="form-select mb-3">
                                <asp:ListItem Selected="True" Text="Select UnitTo"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="label3" runat="server" CssClass="text-black" Text="Description"></asp:Label>
                        <asp:TextBox ID="txtUnitFromDescription" runat="server" CssClass="form-control mb-3"></asp:TextBox>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="label4" runat="server" CssClass="text-black" Text="Operator"></asp:Label>
                            <asp:DropDownList ID="ddlOperator" runat="Server" CssClass="form-select mb-3">
                                <asp:ListItem Value="*" Text="Multiply"></asp:ListItem>
                                <asp:ListItem Value="/" Text="Divide"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <asp:Label ID="label5" runat="server" CssClass="text-black" Text="Conversion Factor"></asp:Label>
                            <asp:TextBox ID="txtUnitFactor" runat="server" CssClass="form-control mb-3"></asp:TextBox>
                            <!-- Required Field Validator -->
                            <asp:RequiredFieldValidator
                                ID="rqfvUnitFactor"
                                runat="server"
                                ControlToValidate="txtUnitFactor"
                                Display="Dynamic"
                                CssClass="text-danger"
                                ErrorMessage="*Factor require"
                                ></asp:RequiredFieldValidator>
                            <!-- Regular Expression Validator for float numbers -->
                            <asp:RegularExpressionValidator
                                 ID="revUnitFactor"
                                 runat="server"
                                 ControlToValidate="txtUnitFactor"
                                 Display="Dynamic"
                                 CssClass="text-danger"
                                 ErrorMessage="(e.g. 123.45)."
                                 ValidationExpression="^\d+(\.\d+)?$"
                                ></asp:RegularExpressionValidator>
                            <!-- Custom Validator to enforce specific length (e.g. max 5 characters) -->
                            <asp:CustomValidator
                                 ID="cvUnitFactor"
                                 runat="server"
                                 ControlToValidate="txtUnitFactor"
                                 Display="Dynamic"
                                 CssClass="text-danger"
                                 ErrorMessage="can support 9 digit"
                                 OnServerValidate="cvUnitFactor_ServerValidate"
                                 ClientValidationFunction="validateLength"
                                ></asp:CustomValidator>

                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Modal Edit --%>
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-primary">Add New UnitMeasurement</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="label16" runat="server" CssClass="text-black" Text="Base Unit"></asp:Label>
                            <asp:DropDownList ID="ddlEditUnitFromName" runat="Server" CssClass="form-select mb-3">
                                <asp:ListItem Selected="True" Text="Select UnitFrom"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <asp:Label ID="label17" runat="server" CssClass="text-black" Text="Convert Unit"></asp:Label>
                            <asp:DropDownList ID="ddlEditUnitToName" runat="Server" CssClass="form-select mb-3">
                                <asp:ListItem Selected="True" Text="Select UnitTo"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="label18" runat="server" CssClass="text-black" Text="Description"></asp:Label>
                        <asp:TextBox ID="txtEditUnitFromDesc" runat="server" CssClass="form-control mb-3"></asp:TextBox>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="label19" runat="server" CssClass="text-black" Text="Operator"></asp:Label>
                            <asp:DropDownList ID="ddlEditOperator" runat="Server" CssClass="form-select mb-3">
                                <asp:ListItem Value="*" Text="Multiply"></asp:ListItem>
                                <asp:ListItem Value="/" Text="Divide"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <asp:Label ID="label20" runat="server" CssClass="text-black" Text="Conversion Factor"></asp:Label>
                            <asp:TextBox ID="txtEditUnitFactor" runat="server" CssClass="form-control mb-3"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:HiddenField ID="hiddenFieldMeasurementId" runat="Server"/>
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnUpdate_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>


    <%-- Modal Edit Alert --%>
    <div class="modal fade" id="alertEditModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Success</h5>
                </div>
                <div class="modal-body">
                    <p class="card-title">Record is update successfully.</p>
                </div>
                <div class="modal-footer">
                    <p>Thanks</p>
                </div>
            </div>
        </div>
    </div>

    <%-- Modal Delete --%>

    <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-primary" id="deleteModalLabel">Delete Unit Measurement</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="label11" runat="server" CssClass="text-black" Text="Base Unit"></asp:Label>
                            <asp:TextBox ID="txtDeleteUnitFrom" runat="server" CssClass="form-control" ReadOnly="True" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col">
                            <asp:Label ID="label12" runat="server" CssClass="text-black" Text="Convert Unit"></asp:Label>
                            <asp:TextBox ID="txtDeleteUnitTo" runat="server" CssClass="form-control" ReadOnly="True" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="label13" runat="server" CssClass="text-black" Text="Description"></asp:Label>
                        <asp:TextBox ID="txtDeleteDesc" runat="server" CssClass="form-control mb-3" ReadOnly="True" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="label14" runat="server" CssClass="text-black" Text="Operator"></asp:Label>
                            <asp:DropDownList ID="ddlDeleteOperator" runat="server" CssClass="form-select mb-3" Enabled="False">
                                <asp:ListItem Value="*" Text="Multiply"></asp:ListItem>
                                <asp:ListItem Value="/" Text="Divide"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col">
                            <asp:Label ID="label15" runat="server" CssClass="text-black" Text="Conversion Factor"></asp:Label>
                            <asp:TextBox ID="txtDeleteFactor" runat="server" CssClass="form-control mb-3" ReadOnly="True" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hiddenFieldUnitFrom" runat="Server" />
                    <asp:HiddenField ID="hiddenFieldUnitTo" runat="Server" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="btnDelete_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

     <%-- Modal Delete Alert --%>
     <div class="modal fade" id="alertDeleteModal1" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
         <div class="modal-dialog modal-dialog-centered">
             <div class="modal-content">
                 <div class="modal-header">
                     <h5 class="modal-title">Success</h5>
                 </div>
                 <div class="modal-body">
                     <p class="card-title"">Record is Delete successfully.</p>
                 </div>
                 <div class="modal-footer">
                     <p>Thanks</p>
                 </div>
             </div>
         </div>
     </div>


    <script>
        function showAddModal() {
            var AddModal = new bootstrap.Modal(document.getElementById('addModal'));
            AddModal.show();
        }
        function showEditModal() {
            var EditModal = new bootstrap.Modal(document.getElementById('editModal'));
            EditModal.show();
        }
        function showAlertEditModal() {
            var alertEditModal = new bootstrap.Modal(document.getElementById('alertEditModal'));
            alertEditModal.show();

            setTimeout(function () {
                alertEditModal.hide();
            }, 1000);
        }
        function showDeleteModal() {
            var DeleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            DeleteModal.show();
        }
        function showDeleteAlert() {
            var DeleteAlert = new bootstrap.Modal(document.getElementById('alertDeleteModal1'));
            DeleteAlert.show();

            setTimeout(() => { DeleteAlert.hide(); }, 1000);
        }
        function validateLength(sender, args) {
            var value = args.value;
            args.isValid = value.length == 9;
        }
    </script>

    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
</asp:Content>
