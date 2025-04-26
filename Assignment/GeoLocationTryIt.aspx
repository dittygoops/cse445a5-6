<%@ Page Title="GeoLocation TryIt" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GeoLocationTryIt.aspx.cs" Inherits="Assignment.GeoLocationTryIt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container py-5">
        <h2>TryIt: GeoLocation Service</h2>

        <asp:TextBox ID="AddressInput" runat="server" CssClass="form-control mb-2" placeholder="Enter an address" />
        
        <asp:Button ID="GetLocationButton" runat="server" Text="Get Location" CssClass="btn btn-primary mb-2" OnClick="GetLocationButton_Click" />

        <asp:Label ID="LocationResult" runat="server" CssClass="form-text" />

        <asp:TextBox ID="LatitudeInput" runat="server" CssClass="form-control mb-2" placeholder="Enter a latitude" />
        <asp:TextBox ID="LongitudeInput" runat="server" CssClass="form-control mb-2" placeholder="Enter a longitude" />

        <asp:Button ID="GetMapImageUrlButton" runat="server" Text="Get Map Image URL" CssClass="btn btn-primary mb-2" OnClick="GetMapImageUrlButton_Click" />

        <asp:Image ID="MapImage" runat="server" CssClass="img-fluid" />
    </main>
</asp:Content>

