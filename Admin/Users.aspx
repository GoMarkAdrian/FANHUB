﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="FanHub.Admin.Users" %>
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
                                        <div class="col-12 mobile-inputs">
                                            <h4 class="sub-title">Users List</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="dataTable_Users" runat="server" OnItemCommand="dataTable_Users_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">ID No</th>
                                                                        <th>Name</th>
                                                                        <th>Username</th>
                                                                        <th>Email</th>
                                                                        <th>Joined Date</th>
                                                                        <th class="datatable-nosort">Delete</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("ID") %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("Username") %></td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("CreatedDate") %></td>
                                                                <td>
                                                                    <!-- DELETE COMMAND -->
                                                                    <asp:LinkButton ID="CategoryDelete" Text="Delete" runat="server" CssClass="badge badge-danger" CommandArgument='<%# Eval("UserID") %>' CommandName="Delete" OnClientClick="return confirm('Do you want to delete this User?');">
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
