<%@ Page Title="Captcha TryIt" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CaptchaTryIt.aspx.cs" Inherits="Assignment.CaptchaTryIt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container py-5">
        <h2>TryIt: Captcha Service</h2>

        <asp:Button ID="RefreshCaptchaButton" runat="server" Text="Refresh Captcha" CssClass="btn btn-primary mb-3" OnClick="RefreshCaptchaButton_Click" />

        <asp:Label ID="CaptchaEncodedText" runat="server" CssClass="form-text" />

        <div style="width: 240px; height: 100px; background-color: #f8f9fa; display: flex; align-items: center; justify-content: center;" class="border rounded mb-3">
            <asp:Image ID="CaptchaImage" runat="server" Width="200px" Height="70px" />
        </div>

        <!-- Captcha Input -->
        <asp:TextBox ID="CaptchaInput" runat="server" CssClass="form-control mb-2" placeholder="Enter the text shown above" />

        <asp:Button ID="VerifyCaptchaButton" runat="server" Text="Verify Captcha" CssClass="btn btn-primary mb-3" OnClick="VerifyCaptchaButton_Click" />

        <asp:Label ID="CaptchaOutput" runat="server" CssClass="form-text" />
    </main>
</asp:Content>

