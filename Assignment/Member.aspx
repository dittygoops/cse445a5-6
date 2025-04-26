<%@ Page Title="Member Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="Assignment.Member" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Optional: Custom styling or additional scripts -->
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card h-100 shadow-sm border-0">
        <div class="card-body">
            <h2 id="directoryTitle" class="card-title fw-bold">Member Page</h2>
            <p class="card-text">
                This page allows members to:
                <ul>
                    <li>List view upcoming events and check in to them.</li>
                    <li>View details about each event, including location, date, time, and description.</li>
                    <li>RSVP for events and manage their RSVP status.</li>
                    <li>Check in to events and receive confirmation.</li>
                </ul>
            </p>
        </div>
    </div>
    <div class="container py-4">
        <h1 class="mb-4">Upcoming Events</h1>

        <asp:Repeater ID="EventRepeater" runat="server" OnItemCommand="EventRepeater_ItemCommand">
                <ItemTemplate>
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <h4 class="card-title"><%# Eval("Name") %></h4>

                            <!-- Collapsible section -->
                            <div id='info_<%# Eval("Name").ToString().Replace(" ", "") %>' class="collapse">
                                <p><strong>Date:</strong> <%# Eval("Date") %></p>
                                <p><strong>Time:</strong> <%# Eval("Time") %></p>
                                <p><strong>Location:</strong> <%# Eval("Location") %></p>
                                <p><strong>Hours:</strong> <%# Eval("Hours") %></p>
                                <p><strong>Info:</strong> <%# Eval("Info") %></p>
                            </div>

                            <!-- Buttons -->
                            <asp:Button 
                                ID="MoreInfoButton" 
                                runat="server" 
                                Text="More Info" 
                                CssClass="btn btn-outline-info btn-sm me-2" 
                                OnClientClick='<%# "toggleInfo(\"info_" + Eval("Name").ToString().Replace(" ", "") + "\"); return false;" %>' />

                            <asp:Button 
                                ID="RSVPButton" 
                                runat="server" 
                                Text='<%# (bool)Eval("IsRSVPed") ? "Un-RSVP" : "RSVP" %>' 
                                CommandName="RSVP" 
                                CommandArgument='<%# Eval("Name") %>' 
                                CssClass='<%# (bool)Eval("IsRSVPed") ? "btn btn-secondary btn-sm me-2" : "btn btn-outline-success btn-sm me-2" %>' />


                            <asp:Button 
                                ID="CheckInButton" 
                                runat="server" 
                                Text='<%# (bool)Eval("IsCheckedIn") ? "Checked In" : "Check In" %>'
                                CommandName="CheckIn"
                                CommandArgument='<%# Eval("Name") %>'
                                CssClass='<%# (bool)Eval("IsRSVPed") && (bool)Eval("IsCheckedIn") ? "btn btn-secondary btn-sm me-2" : "btn btn-outline-primary btn-sm me-2" %>' />


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
            // Wait for document ready to ensure DOM elements exist
            document.addEventListener('DOMContentLoaded', function() {
                const modalMessage = document.getElementById("ModalMessage");
                if (modalMessage) {
                    modalMessage.innerText = message;
                    
                    // Check if bootstrap is loaded
                    if (typeof bootstrap !== 'undefined') {
                        const feedbackModal = new bootstrap.Modal(document.getElementById('FeedbackModal'));
                        feedbackModal.show();
                    } else {
                        // Fallback if bootstrap isn't loaded yet
                        console.error("Bootstrap is not loaded. Message:", message);
                        alert(message); // Fallback to regular alert
                    }
                }
            });
        }
    </script>
    <script>
        function toggleInfo(id) {
            const element = document.getElementById(id);
            if (element.classList.contains("show")) {
                bootstrap.Collapse.getInstance(element)?.hide();
            } else {
                new bootstrap.Collapse(element).show();
            }
        }
    </script>
    <script>
        function checkInWithLocation(eventName) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    const lat = position.coords.latitude;
                    const lon = position.coords.longitude;
                    const argument = eventName + "|" + lat + "|" + lon;

                    __doPostBack('CheckInWithLocation', argument);
                }, function (error) {
                    alert("❌ Failed to get your location. Please enable location services.");
                });
            } else {
                alert("❌ Geolocation is not supported by this browser.");
            }
        }
    </script>

</asp:Content>
