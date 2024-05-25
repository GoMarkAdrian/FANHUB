<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="FanHub.Admin.Category" %>

<%@ Import Namespace="FanHub" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        window.onload = function () {
            var seconds = 10;
            setTimeout(function () {
                document.getElementById("<%=labelMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgCategory.ClientID %>').prop('src', e.target.result).width(200).height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pcoded-inner-content pt-0">
        <div>
            <asp:Label ID="labelMessage" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header">
                                </div>
                                <div class="card-block">
                                    <div class="row">
                                        <div class="col-sm-6 col-md-4 col-lg-4">
                                            <h4 class="sub-title">Category</h4>

                                            <div class="form-group">
                                                <label>Category Name</label>
                                                <div>
                                                    <!-- TEXT NAME -->
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Category Name" required></asp:TextBox>
                                                    <asp:HiddenField ID="hiddenID" runat="server" Value="0" />
                                                </div>
                                                <div class="form-group">
                                                    <label>Category Image</label>
                                                    <div>
                                                        <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                                                    </div>
                                                </div>
                                                <div class="form-check pl-4">
                                                    <div>
                                                        <!-- IS ACTIVE -->
                                                        <asp:CheckBox ID="cbIsActive" runat="server" Text="Is Active" CssClass="form-check-input" />
                                                    </div>
                                                </div>

                                                <div class="pb-5">
                                                    <div>
                                                        <!-- UPDATE BUTTON -->
                                                        <asp:Button ID="btnAddorUpdate" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddorUpdate_Click" />
                                                        &nbsp;
                                                        <!-- CLEAR BUTTON -->
                                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" CausesValidation="false" />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Image ID="imgCategory" runat="server" CssClass="img-thumbnail" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                            <h4 class="sub-title">Category List</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="dataTable_Category" runat="server" OnItemCommand="dataTable_Category_ItemCommand" OnItemDataBound="dataTable_Category_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">Name</th>
                                                                        <th>Image</th>
                                                                        <th>IsActive</th>
                                                                        <th>CreatedDate</th>
                                                                        <th class="datatable-nosort">Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("Name") %></td>
                                                                <td>
                                                                    <img width="40" src="<%# util.GetImageUrl(Eval("ImageURL")) %>" /></td>
                                                                <td>
                                                                    <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label></td>
                                                                <td><%# Eval("CreatedDate") %></td>
                                                                <td>
                                                                    <!-- EDIT LINK COMMAND -->
                                                                    <asp:LinkButton ID="CategoryEdit" Text="Edit" runat="server" CssClass="badge badge-primary" CommandArgument='<%# Eval("CategoryID") %>' CommandName="Edit">
                                                                        <i class="ti-pencil">Edit</i>
                                                                    </asp:LinkButton>
                                                                    <!-- DELETE COMMAND -->
                                                                    <asp:LinkButton ID="CategoryDelete" Text="Delete" runat="server" CssClass="badge badge-danger" CommandArgument='<%# Eval("CategoryID") %>' CommandName="Delete">
                                                                        <i class="ti-trash">Delete</i>
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
