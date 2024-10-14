<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FanHub.User.Index" %>

<%@ Import Namespace="FanHub" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- about section -->
    <section class="about_section layout_padding">
        <div class="container  ">
            <div class="row">
                <div class="col-md-6 ">
                    <div class="img-box">
                        <img src="../UserEnd/images/logo.png" alt="">
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="detail-box">
                        <div class="heading_container">
                            <h2>We Are FanHub </h2>
                        </div>
                        <p>
                        </p>
                        <a href="About.aspx">Read More </a>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- end about section -->
</asp:Content>
