<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="FanHub.User.Profile" %>

<%@ Import Namespace="FanHub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% string imageURL = Session["imageURL"].ToString(); %>
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="labelMessage" runat="server" Visible="false"></asp:Label>
                </div>
                <h2>User Profile</h2>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-title mb-4">
                                <div class="d-flex justify-content-start">
                                    <div class="image-container">
                                        <img src="<%= util.GetImageUrl(imageURL) %>" id="imgProfile" style="width: 150px; height: 150px;" class="img-thumbnail" />
                                        <div class="middle pt-2">
                                            <a href="Registration.aspx?id=<% Response.Write(Session["userID"]);%>" class="btn btn-danger"><i class="fa fa-pencil"></i>Edit Details</a>
                                        </div>
                                    </div>
                                    <div class="userData ml-3">
                                        <h2 class="d-block" style="font-size: 1.5rem; font-weight: bold">
                                            <a href="javascript:void(0);"><% Response.Write(Session["name"]); %></a>
                                        </h2>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0);">
                                                <asp:Label ID="lblusername" runat="server" ToolTip="Unique Username"><% Response.Write(Session["username"]);%></asp:Label>
                                            </a>
                                        </h6>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0);">
                                                <asp:Label ID="lblemail" runat="server" ToolTip="User Email"><% Response.Write(Session["email"]); %></asp:Label>
                                            </a>
                                        </h6>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0);">
                                                <asp:Label ID="lblcreatedDate" runat="server" ToolTip="Created Date"><% Response.Write(Session["createdDate"]); %></asp:Label>
                                            </a>
                                        </h6>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active text-info" id="basic-info" data-toggle="tab" href="#basicInfo" role="tab" aria-controls="basicInfo" aria-selected="true">
                                            <i class="fa fa-id-badge mr-2"></i>Basic Info
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link text-info" id="connected-Services" data-toggle="tab" href="#connectedServices" role="tab" aria-controls="connectedServices" aria-selected="false">
                                            <i class="fa fa-clock-o mr-2"></i>Purchase History
                                        </a>
                                    </li>
                                </ul>

                                <div class="tab-content ml-1" id="myTabContent">
                                    <div class="tab-fane fade show active" id="basicInfo" role="tabpanel" aria-labelledby="basic-info">
                                        <asp:Repeater ID="userProfile" runat="server">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Full Name</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("Name") %>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Username</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("Username") %>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Mobile</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("Mobile") %>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Email</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("Email") %>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Post Code</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("PostCode") %>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Address</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("Address") %>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="tab-fane fade" id="connectedServices" role="tabpanel" aria-labelledby="connected-Services">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
