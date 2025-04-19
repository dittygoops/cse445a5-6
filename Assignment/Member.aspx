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

                            <!-- Collapsible section -->
                            <div id='info_<%# Eval("Name").ToString().Replace(" ", "") %>' class="collapse">
                                <p><strong>Date:</strong> <%# Eval("Date") %></p>
                                <p><strong>Time:</strong> <%# Eval("Time") %></p>
                                <p><strong>Location:</strong> <%# Eval("Location") %></p>
                                <p><strong>Hours:</strong> <%# Eval("Hours") %></p>
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
                                Text="Check In" 
                                OnClientClick='<%# "checkInWithLocation(\"" + Eval("Name") + "\"); return false;" %>' 
                                CssClass="btn btn-outline-primary btn-sm" />


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
