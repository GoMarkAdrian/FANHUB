<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="FanHub.Admin.Products" %>

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
                    $('#<%=imgProduct.ClientID %>').prop('src', e.target.result).width(200).height(200);
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
                                            <div class="form-group">
                                                <h4 class="sub-title">Product</h4>
                                                <div class="form-group">
                                                    <label>Product Name</label>
                                                    <div>
                                                        <!-- Product Name -->
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Product Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFiedValidator1" runat="server" ErrorMessage="Input a Valid Item name" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                                        <asp:HiddenField ID="hiddenID" runat="server" Value="0" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Product Description</label>
                                                    <div>
                                                        <!-- Product Description -->
                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter Product Description" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Input a Valid Description" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Product Price($)</label>
                                                    <div>
                                                        <!-- Product Price -->
                                                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Product Price"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Input a Valid Price" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPrice"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Price Must be in Decimal" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPrice" ValidationExpression="^\d{0,8}(\.\d{1,4})?$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Product Quantity</label>
                                                    <div>
                                                        <!-- Product Quantity -->
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Enter Product Quantity"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Input a Quantity" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="There should be no negative quantity" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtQuantity" ValidationExpression="^([1-9]\d*|0)$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <!-- Product Image -->
                                                <div class="form-group">
                                                    <label>Product Image</label>
                                                    <div>
                                                        <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Product Category</label>
                                                    <div>
                                                        <!-- Product Category -->
                                                        <asp:DropDownList ID="ddlCategories" CssClass="form-control" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="CategoryID" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Select Category</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Category is Empty" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddlCategories"></asp:RequiredFieldValidator>
                                                        <!-- Connect to SQL for Categories Listed -->
                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CategoryID], [Name] FROM [Categories]"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="form-check pl-4">
                                                    <div>
                                                        <!-- IS ACTIVE -->
                                                        <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; Is Active" CssClass="form-check-input" />
                                                    </div>
                                                </div>

                                                <div class="pb-5">
                                                    <div>
                                                        <!-- UPDATE BUTTON -->
                                                        <asp:Button ID="btnAddorUpdate" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddorUpdate_Click" />
                                                        &nbsp;
                                                        <!-- CLEAR BUTTON -->
                                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnClear_Click" />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Image ID="imgProduct" runat="server" CssClass="img-thumbnail" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                            <h4 class="sub-title">Products List</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="dataTable_Products" runat="server" OnItemCommand="dataTable_Products_ItemCommand" OnItemDataBound="dataTable_Products_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">Name</th>
                                                                        <th>Description</th>
                                                                        <th>Price($)</th>
                                                                        <th>Quantity</th>
                                                                        <th>Category</th>
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
                                                                <td><%#Eval("Description")%></td>
                                                                <td><%#Eval("Price")%></td>
                                                                <td>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                                </td>
                                                                <td><%#Eval("CategoryName")%></td>
                                                                <td>
                                                                    <img width="40" src="<%# util.GetImageUrl(Eval("ImageURL")) %>" /></td>
                                                                <td>
                                                                    <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label></td>
                                                                <td><%# Eval("CreatedDate") %></td>
                                                                <td>
                                                                    <!-- EDIT LINK COMMAND -->
                                                                    <asp:LinkButton ID="ProductsEdit" Text="Edit" runat="server" CssClass="badge badge-primary" CommandArgument='<%# Eval("ProductID") %>' CommandName="Edit" CausesValidation="false">
                                                                        <i class="ti-pencil">Edit</i>
                                                                    </asp:LinkButton>
                                                                    <!-- DELETE COMMAND -->
                                                                    <asp:LinkButton ID="ProductsDelete" Text="Delete" runat="server" CssClass="badge badge-danger" CommandArgument='<%# Eval("ProductID") %>' CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('Do you want to delete this Product?');">
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
