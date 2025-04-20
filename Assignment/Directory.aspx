<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Directory.aspx.cs" Inherits="Assignment.Directory" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="col">
            <div class="card h-100 shadow-sm border-0">
                <div class="card-body">
                    <h2 id="directoryTableTitle" class="card-title fw-bold">Service Directory Table</h2>
                    <div class="table-responsive">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Provider Name</th>
                                    <th>Component Type</th>
                                    <th>Operation Name</th>
                                    <th>Parameters and Their Types</th>
                                    <th>Return Type</th>
                                    <th>Function Description</th>
                                    <th>TryIt Page Link</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Hash Service</td>
                                    <td>DLL Function</td>
                                    <td>Hash</td>
                                    <td>Input to be hashed (string)</td>
                                    <td>Hashed string (string)</td>
                                    <td>Hashing the input string</td>
                                    <td><a href="HashTryIt.aspx">TryIt Page</a></td>
                                </tr>
                                <!-- Add more rows as needed -->
                                 <tr>
                                    <td rowspan="2">Captcha Service</td>
                                    <td rowspan="2">ASMX Web Service</td>
                                    <td>GenerateCaptcha</td>
                                    <td>None</td>
                                    <td>Base64 encoded image (string)</td>
                                    <td>Generates a new captcha image</td>
                                    <td rowspan="2"><a href="CaptchaTryIt.aspx">TryIt Page</a></td>
                                </tr>
                                <tr>
                                    <td>VerifyCaptcha</td>
                                    <td>User input (string)</td>
                                    <td>Boolean</td>
                                    <td>Verifies if the user's input matches the captcha</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>