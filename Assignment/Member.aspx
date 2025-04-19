<%@ Page Title="Member Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="Assignment.Member" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Optional: Custom styling or additional scripts -->
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <h1 class="mb-4">Upcoming Events</h1>

        <asp:Repeater ID="EventRepeater" runat="server" OnItemCommand="EventRepeater_ItemCommand">
            <ItemTemplate>
                <div class="card mb-4 shadow-sm">
                    <div class="card-body">
                        <h4 class="card-title"><%# Eval("Name") %></h4>
                        <p class="card-text"><strong>Date:</strong> <%# Eval("Date") %></p>
                        <p class="card-text"><strong>Time:</strong> <%# Eval("Time") %></p>
                        <p class="card-text"><strong>Location:</strong> <%# Eval("Location") %></p>
                        <p class="card-text"><strong>Hours:</strong> <%# Eval("Hours") %></p>
                        <asp:Button ID="MoreInfoButton" runat="server" Text="More Info" CommandName="MoreInfo" CommandArgument='<%# Eval("Name") %>' CssClass="btn btn-outline-info btn-sm me-2" />
                        <asp:Button 
    ID="RSVPButton" 
    runat="server" 
    Text="RSVP" 
    CommandName="RSVP" 
    CommandArgument='<%# Eval("Name") %>' 
    Enabled='<%# !(bool)Eval("IsRSVPed") %>' 
    CssClass='<%# (bool)Eval("IsRSVPed") ? "btn btn-secondary btn-sm me-2" : "btn btn-outline-success btn-sm me-2" %>' />

                        <asp:Button ID="CheckInButton" runat="server" Text="Check In" CommandName="CheckIn" CommandArgument='<%# Eval("Name") %>' CssClass="btn btn-outline-primary btn-sm" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="FeedbackModal" tabindex="-1" aria-labelledby="FeedbackModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content border-0 shadow rounded">
                <div class="modal-header">
                    <h5 class="modal-title" id="FeedbackModalLabel">Event Update</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" id="ModalMessage">
                    <!-- Message will be injected here -->
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap modal logic -->
    <script>
        function showModal(message) {
            const modalMessage = document.getElementById("ModalMessage");
            modalMessage.innerText = message;
            const feedbackModal = new bootstrap.Modal(document.getElementById('FeedbackModal'));
            feedbackModal.show();
        }
    </script>
</asp:Content>
