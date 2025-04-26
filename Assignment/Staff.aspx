<%@ Page Title="Staff Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="Assignment.Staff" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card h-100 shadow-sm border-0">
        <div class="card-body">
            <h2 id="directoryTitle" class="card-title fw-bold">Staff Page</h2>
            <p class="card-text">
                This page allows staff to:
                <ul>
                    <li>Create new events.</li>
                    <li>View all events including the RSVPed and checked-in users.</li>
                    <li>Edit event details.</li>
                </ul>
            </p>
        </div>
    </div>
    <div class="container py-4">
        <h1 class="mb-4">All Events</h1>

        <!-- + New Event Button -->
        <button type="button" class="btn btn-success mb-3" data-bs-toggle="modal" data-bs-target="#NewEventModal">
            + New Event
        </button>


        <!-- New Event Modal -->
        <asp:PlaceHolder ID="NewEventModalHolder" runat="server">
            <div class="modal fade" id="NewEventModal" tabindex="-1" aria-labelledby="NewEventModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="NewEventModalLabel">Add New Event</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:TextBox ID="NewEventName" runat="server" CssClass="form-control mb-2" placeholder="Event Name" />
                            <asp:TextBox ID="NewEventDate" runat="server" CssClass="form-control mb-2" placeholder="Date (YYYY-MM-DD)" />
                            <asp:TextBox ID="NewEventTime" runat="server" CssClass="form-control mb-2" placeholder="Time (e.g., 6:00 PM)" />
                            <asp:TextBox ID="NewEventLocation" runat="server" CssClass="form-control mb-2" placeholder="Location" />
                            <asp:TextBox ID="NewEventHours" runat="server" CssClass="form-control mb-2" placeholder="Hours" />
                            <asp:TextBox ID="NewEventInfo" runat="server" CssClass="form-control mb-2" TextMode="MultiLine" Rows="3" placeholder="Description / Info" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="CreateEventButton" runat="server" Text="Save Event" CssClass="btn btn-primary" OnClick="CreateEventButton_Click" UseSubmitBehavior="false" />
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <!-- Event Cards Repeater -->
        <asp:Repeater ID="StaffRepeater" runat="server" OnItemCommand="StaffRepeater_ItemCommand">
            <ItemTemplate>
                <div class="card mb-4 shadow-sm">
                    <div class="card-body">
                        <h4 class="card-title"><%# Eval("Name") %></h4>

                        <p><strong>RSVPed Users:</strong> <%# Eval("RSVPList") %></p>

                        <p><strong>Checked-in Users:</strong></p>
                        <table class="table table-bordered table-sm w-75">
                            <thead class="table-light">
                                <tr>
                                    <th>Name</th>
                                    <th>Date</th>
                                    <th>Time</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%# Eval("AttendingList") %>
                            </tbody>
                        </table>

                        <!-- READ-ONLY INFO -->
                        <div class="mb-3">
                            <p><strong>Date:</strong> <%# Eval("Date") %></p>
                            <p><strong>Time:</strong> <%# Eval("Time") %></p>
                            <p><strong>Location:</strong> <%# Eval("Location") %></p>
                            <img src="<%# Eval("Map") %>" alt="Map" />
                            <p><strong>Hours:</strong> <%# Eval("Hours") %></p>
                            <p><strong>Info:</strong> <%# Eval("Info") %></p>
                        </div>

                        <!-- Toggle Button -->
                        <button 
                            class="btn btn-outline-secondary btn-sm mb-3 toggle-btn" 
                            type="button" 
                            data-bs-toggle="collapse" 
                            data-bs-target='#edit_<%# Eval("Name").ToString().Replace(" ", "") %>' 
                            aria-expanded="false" 
                            aria-controls='edit_<%# Eval("Name").ToString().Replace(" ", "") %>' 
                            onclick='toggleButtonText(this)'>
                            Edit Event Info
                        </button>

                        <!-- Editable Section -->
                        <div id='edit_<%# Eval("Name").ToString().Replace(" ", "") %>' class="collapse">
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

                            <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="btn btn-primary" CommandName="Save" CommandArgument='<%# Eval("Name") %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Toggle Button Text Script -->
    <script>
        function toggleButtonText(btn) {
            const targetId = btn.getAttribute("data-bs-target").replace("#", "");
            const target = document.getElementById(targetId);

            setTimeout(() => {
                const isExpanded = target.classList.contains("show");
                btn.innerText = isExpanded ? "Hide Event Info" : "Edit Event Info";
            }, 200);
        }
    </script>
</asp:Content>
