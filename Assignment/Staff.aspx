<%@ Page Title="Staff Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="Assignment.Staff" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h1 class="mb-4">All Events</h1>

        <asp:Repeater ID="StaffRepeater" runat="server" OnItemCommand="StaffRepeater_ItemCommand">
            <ItemTemplate>
                <div class="card mb-4 shadow-sm">
                    <div class="card-body">
                        <h4 class="card-title"><%# Eval("Name") %></h4>

                        <asp:HiddenField ID="EventNameHidden" runat="server" Value='<%# Eval("Name") %>' />

                        <label><strong>Date:</strong></label>
                        <asp:TextBox ID="DateTextBox" runat="server" CssClass="form-control mb-2" Text='<%# Eval("Date") %>' />

                        <label><strong>Time:</strong></label>
                        <asp:TextBox ID="TimeTextBox" runat="server" CssClass="form-control mb-2" Text='<%# Eval("Time") %>' />

                        <label><strong>Location:</strong></label>
                        <asp:TextBox ID="LocationTextBox" runat="server" CssClass="form-control mb-2" Text='<%# Eval("Location") %>' />

                        <label><strong>Hours:</strong></label>
                        <asp:TextBox ID="HoursTextBox" runat="server" CssClass="form-control mb-2" Text='<%# Eval("Hours") %>' />

                        <label><strong>Info:</strong></label>
                        <asp:TextBox ID="InfoTextBox" runat="server" CssClass="form-control mb-3" Text='<%# Eval("Info") %>' TextMode="MultiLine" Rows="3" />

                        <p><strong>RSVPed Users:</strong> <%# Eval("RSVPList") %></p>

                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="btn btn-primary" CommandName="Save" CommandArgument='<%# Eval("Name") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
