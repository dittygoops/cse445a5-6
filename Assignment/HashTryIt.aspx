<%@ Page Title="Hash TryIt" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HashTryIt.aspx.cs" Inherits="Assignment.HashTryIt" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container py-5">
        <h2>TryIt: Hashing Service</h2>

        <asp:TextBox ID="InputToHash" runat="server" CssClass="form-control mb-3" placeholder="Enter string to hash" />

        <asp:Button ID="HashButton" runat="server" Text="Generate Hash" CssClass="btn btn-primary mb-3" OnClick="HashButton_Click" />

        <asp:Label ID="HashOutput" runat="server" CssClass="form-text" />
    </main>
</asp:Content>
