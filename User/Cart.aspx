<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="FanHub.User.Cart" %>
<%@ Import Namespace="FanHub" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-seld-end">
                   <asp:Label ID="labelMsg" runat="server" Visible="false"></asp:Label>
                </div>
                <h2>Shopping Cart</h2>
            </div>
        </div>
        <div class="'container">
            <asp:Repeater ID="CartItem" runat="server" OnItemCommand="CartItem_ItemCommand" OnItemDataBound="CartItem_ItemDataBound">
                <HeaderTemplate>
                    <table class="table ">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Image</th>
                                <th>Unit Price</th>
                                <th>Quantity</th>
                                <th>Total Price</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                   <tr>
                       <td>
                           <asp:Label ID="LabelName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                       </td>
                       <td>
                           <img width="60" src="<%# util.GetImageUrl(Eval("ImageUrl"))%>" alt="" />
                       </td>
                       <td>
                           $<asp:Label ID="LabelPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                           <asp:HiddenField ID="HiddenProductID" runat="server" value='<%# Eval("ProductID") %>'/>
                           <asp:HiddenField ID="HiddenQuantity" runat="server"  value='<%# Eval("Qty") %>'/>
                           <asp:HiddenField ID="HiddenPrdQuantity" runat="server"  value='<%# Eval("PrdQty") %>'/>
                       </td>
                       <td>
                           <div class="product__details__option">
                               <div class="quantity">
                                   <div class="pro-qty">
                                       <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                                       <asp:RegularExpressionValidator ID="revQuantity" runat="server" ErrorMessage="*" ForeColor="Red" 
                                           Font-Size="Small" ValidationExpression="[1-9]*" ControlToValidate="txtQuantity"
                                           Display="Dynamic" SetFocusOnError="true" EnableClientScript="true"></asp:RegularExpressionValidator>
                                   </div>
                               </div>
                           </div>
                       </td>
                       <td>$<asp:Label ID="LabelTotalPrice" runat="server"></asp:Label></td>
                           <td> 
                               <asp:LinkButton ID="lbDelete" runat="server" Text="Remove" CommandName="remove" 
                                   CommandArgument='<%# Eval("ProductID") %>' 
                                   OnClientClick="return confirm('Do you want to remove item from cart?');"> 
                                   <i class="fa fa-close"></i></asp:LinkButton>
                           </td>

                       
                   </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="3"></td>
                        <td class="pl-lg-5">
                            <b> Grand Total: </b>
                        </td>
                        <td>$<%Response.Write(Session["grandTotalPrice"]); %></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="continue__btn">
                            <a href="Shop.aspx" class="btn btn-info"> <i class="fa fa-arrow-circle-left mr-2"></i>Continue Shoppping</a>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbUpdateCart" runat="server" CommandName="updateCart" CssClass="btn btn-warning">
                                <i class="fa fa-refresh mr-2"></i>Update Cart
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbCheckout" runat="server" CommandName="checkout" CssClass="btn btn-success">
                               Checkout <i class="fa fa-arrow-circle-right ml-2"></i>
                            </asp:LinkButton>
                        </td>
                    </tr>
                    </tbody>
                    </table>
                </FooterTemplate>
                    
            </asp:Repeater>
        </div>
    </section>


</asp:Content>
