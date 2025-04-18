<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assignment._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <!-- Hero Section with Full-Screen Image -->
        <section 
            class="text-center vw-100 vh-100 position-relative" 
            style="background-image: url('Images/BAP_Homepage_BG.png'); 
                   background-size: cover; 
                   background-position: center; 
                   background-attachment: fixed; 
                   display: flex; 
                   align-items: center; 
                   justify-content: center; 
                   margin: 0; 
                   padding: 0;" 
            aria-labelledby="aspnetTitle">
            <div class="position-absolute w-100 h-100" style="background: rgba(0,0,0,0.4); z-index: 1;"></div>
            <div class="container position-relative" style="z-index: 2;">
                <div class="row py-lg-5">
                    <div class="col-lg-8 col-md-10 mx-auto">
                        <h1 id="aspnetTitle" class="fw-bold display-3 text-white" style="text-shadow: 2px 2px 6px rgba(0,0,0,0.7);">
                            BETA ALPHA PSI
                        </h1>
                        <p class="lead text-white mt-4" style="background: rgba(0,0,0,0.7); padding: 25px; border-radius: 15px; font-size: 1.2rem;">
                            Beta Alpha Psi is the premier international honor and service organization for accounting, finance, and information systems students and professionals. Our mission is to encourage and recognize scholastic and professional excellence in the business information field. We promote the study and practice of accounting, finance, economics, and information systems, while fostering opportunities for self-development, service, and networking among members and professionals. We are committed to upholding ethical, social, and public responsibility.
                        </p>
                    </div>
                </div>
            </div>
        </section>

        <!-- Cards Section -->
        <div class="container px-4 py-5">
            <div class="row g-4 py-5 row-cols-1 row-cols-lg-2">
                <div class="col">
                    <div class="card h-100 shadow-sm border-0">
                        <div class="card-body">
                            <h2 id="memberSignInTitle" class="card-title fw-bold">Member Sign In</h2>
                            <p class="card-text">
                                Access your account to manage your preferences and settings. If you don't have an account, you can create one.
                            </p>
                            <div class="d-grid gap-2 d-md-flex justify-content-md-start mt-4">
                                <a class="btn btn-primary" href="#">Log In</a>
                                <a class="btn btn-outline-primary" href="#">Create Account</a>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="col">
                    <div class="card h-100 shadow-sm border-0">
                        <div class="card-body">
                            <h2 id="staffLoginTitle" class="card-title fw-bold">Staff Login</h2>
                            <p class="card-text">
                                Staff members can log in to access internal resources and tools.
                            </p>
                            <div class="d-grid gap-2 d-md-flex justify-content-md-start mt-4">
                                <a class="btn btn-primary" href="#">Log In</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>