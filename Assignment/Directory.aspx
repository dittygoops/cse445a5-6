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
                                    <td><!-- Provider Name --></td>
                                    <td><!-- Component Type --></td>
                                    <td><!-- Operation Name --></td>
                                    <td><!-- Parameters and Their Types --></td>
                                    <td><!-- Return Type --></td>
                                    <td><!-- Function Description --></td>
                                    <td><a href="HashTryIt.aspx">TryIt Page</a></td>
                                </tr>
                                <!-- Add more rows as needed -->
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>