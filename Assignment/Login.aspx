<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container py-5">

        <!-- Title -->
        <h2 id="loginTitle" runat="server" class="mb-4">Login</h2>

        <asp:TextBox ID="UsernameInput" runat="server" CssClass="form-control mb-2" placeholder="Username" />
        <asp:TextBox ID="PasswordInput" runat="server" TextMode="Password" CssClass="form-control mb-2" placeholder="Password" />


        <div style="width: 240px; height: 100px; background-color: #f8f9fa; display: flex; align-items: center; justify-content: center;" class="border rounded mb-3">
            <asp:Image ID="CaptchaImage" runat="server" Width="200px" Height="70px" />
        </div>


        <!-- Captcha Input -->
        <asp:TextBox ID="CaptchaInput" runat="server" CssClass="form-control mb-2" placeholder="Enter the text shown above" />

        <!-- Feedback -->
        <asp:Label ID="CaptchaFeedback" runat="server" CssClass="form-text text-danger" />

        <!-- Refresh & Submit Buttons -->
        <div class="mb-3">
            <asp:Button ID="RefreshCaptcha" runat="server" Text="Refresh Captcha" OnClick="RefreshCaptcha_Click" CssClass="btn btn-secondary btn-sm" />
        </div>

        <asp:Button ID="LoginButton" runat="server" Text="Log In" OnClick="LoginButton_Click" CssClass="btn btn-primary" />

        <!-- Add this line below -->
        <asp:Label ID="LoginStatusLabel" runat="server" CssClass="form-text text-success mt-3" />


    </main>
</asp:Content>
