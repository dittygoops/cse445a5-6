<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Directory.aspx.cs" Inherits="Assignment.Directory" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="col">
            <div class="card h-100 shadow-sm border-0">
                <div class="card-body">
                    <h2 id="directoryTableTitle" class="card-title fw-bold">Services and Components Directory Table</h2>
                    <div class="bg-light mb-3 p-2">
                        <strong>Member Contribution: Abhave Abhilash 50%, Aditya Gupta 50%</strong>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Member Name</th>
                                    <th>Provider Name</th>
                                    <th>Component Type</th>
                                    <th>Operation Name</th>
                                    <th>Inputs</th>
                                    <th>Outputs</th>
                                    <th>Description</th>
                                    <th>Implementation Details</th>
                                    <th>TryIt Page Link</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Abhave Abhilash</td>
                                    <td>Hash Service</td>
                                    <td>DLL Function</td>
                                    <td>Hash</td>
                                    <td>Input to be hashed (string)</td>
                                    <td>Hashed string (string)</td>
                                    <td>Hashing the input string</td>
                                    <td>DLL function, System.Security.Cryptography.SHA256 library</td>
                                    <td><a href="HashTryIt.aspx">TryIt Page</a></td>
                                </tr>
                                <!-- Add more rows as needed -->
                                 <tr>
                                    <td rowspan="2">Abhave Abhilash</td>
                                    <td rowspan="2">Captcha Service</td>
                                    <td rowspan="2">ASMX Web Service</td>
                                    <td>GenerateCaptcha</td>
                                    <td>None</td>
                                    <td>Base64 encoded image (string)</td>
                                    <td>Generates a new captcha image</td>
                                    <td rowspan="2">System.Drawing library, sessions, base64 encoding</td>
                                    <td rowspan="2"><a href="CaptchaTryIt.aspx">TryIt Page</a></td>
                                </tr>
                                <tr>
                                    <td>VerifyCaptcha</td>
                                    <td>User input (string)</td>
                                    <td>Boolean</td>
                                    <td>Verifies if the user's input matches the captcha</td>
                                </tr>

                                <tr>
                                    <td rowspan="2">Aditya Gupta</td>
                                    <td rowspan="2">GeoLocation Service</td>
                                    <td rowspan="2">SOAP Web Service</td>
                                    <td>GetLatLong</td>
                                    <td>Address (string)</td>
                                    <td>LocationResult (object)</td>
                                    <td>Retrieves latitude and longitude for a given address</td>
                                    <td rowspan="2">3rd party Geoapify API</td>
                                    <td rowspan="2"><a href="GeoLocationTryIt.aspx">TryIt Page</a></td>
                                </tr>

                                <tr>
                                    <td>GetMapImageUrl</td>
                                    <td>Latitude (double), Longitude (double)</td>
                                    <td>Map URL (string)</td>
                                    <td>Retrieves a map image URL for a given latitude and longitude</td>
                                </tr>

                                <tr>
                                    <td>Aditya Gupta</td>
                                    <td>Member Storage</td>
                                    <td>XML Data</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>Store member role and credentials</td>
                                    <td>Member.xml</td>
                                </tr>

                                <tr>
                                    <td>Aditya Gupta</td>
                                    <td>Events Storage</td>
                                    <td>XML Data</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>Store event information, RSVPees and checkins</td>
                                    <td>Events.xml</td>
                                </tr>

                                <tr>
                                    <td>Abhave Abhilash</td>
                                    <td>Session starting application</td>
                                    <td>Global.asax</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>Logs session user, role, and start and end times</td>
                                    <td>C# code in Global.asax</td>
                                </tr>

                                <tr>
                                    <td>Abhave Abhilash</td>
                                    <td>Cookies</td>
                                    <td>HTTP Cookie</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>Convey user role and username</td>
                                    <td>C# code</td>
                                </tr>

                                <tr>
                                    <td>Aditya Gupta</td>
                                    <td>Landing Page</td>
                                    <td>.aspx page</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>Display landing page with BAP introduction, login, register options, and directory link</td>
                                    <td>GUI Design and C# code behind GUI</td>
                                </tr>

                                <tr>
                                    <td>Abhave Abhilash</td>
                                    <td>User Authentication Pages</td>
                                    <td>.aspx page</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>Pages to handle user authentication with captcha</td>
                                    <td>GUI Design and C# code behind GUI</td>
                                </tr>

                                <tr>
                                    <td>Aditya Gupta</td>
                                    <td>Dashboards</td>
                                    <td>.aspx page</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>Display dashboards for members and providers</td>
                                    <td>GUI Design and C# code behind GUI</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>