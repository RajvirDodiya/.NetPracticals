<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="P9_PlacementDashboard.Companies" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Company Placement Summary - Practical 9</title>
    <link href="Content/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-shell">
            <!-- Header -->
            <header class="topbar">
                <div>
                    <div class="eyebrow">COMPANY ANALYTICS &amp; REPORTS</div>
                    <h1>Company Placement Summary</h1>
                    <p class="subtitle">Detailed company recruitment reports using Repeater &amp; DataList Data Controls</p>
                </div>
                <div class="header-actions">
                    <a class="nav-button" href="Default.aspx">← Back to Dashboard</a>
                </div>
            </header>

            <main>
                <!-- Alert Message -->
                <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false" EnableViewState="false" />

                <!-- Overview Stat Highlights -->
                <section class="stats-grid stats-grid-4">
                    <article class="stat-card stat-card-success">
                        <span class="stat-label">Placed Students</span>
                        <asp:Label ID="lblPlacedCount" runat="server" CssClass="stat-value text-green" Text="0" />
                        <span class="stat-foot">Successfully hired</span>
                    </article>
                    <article class="stat-card">
                        <span class="stat-label">Participating Companies</span>
                        <asp:Label ID="lblCompanyCount" runat="server" CssClass="stat-value" Text="0" />
                        <span class="stat-foot">Active recruiters</span>
                    </article>
                    <article class="stat-card stat-card-highlight">
                        <span class="stat-label">Highest Package</span>
                        <asp:Label ID="lblCompanyHighest" runat="server" CssClass="stat-value text-blue" Text="0.00" />
                        <span class="stat-foot">Max offer (Pinnacle IIT)</span>
                    </article>
                    <article class="stat-card">
                        <span class="stat-label">Average Package</span>
                        <asp:Label ID="lblCompanyAverage" runat="server" CssClass="stat-value" Text="0.00" />
                        <span class="stat-foot">Across all offers</span>
                    </article>
                </section>

                <!-- Data Control 2: DataList for Company Cards -->
                <section class="panel">
                    <div class="panel-heading">
                        <div>
                            <div class="eyebrow">DATA CONTROL 2</div>
                            <h2>Company Hiring Analytics (DataList)</h2>
                        </div>
                        <span class="pill pill-green">DataList Control</span>
                    </div>
                    <div class="company-cards">
                        <asp:DataList ID="dlCompanies" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" CssClass="company-list">
                            <ItemTemplate>
                                <article class="company-card">
                                    <div class="company-header">
                                        <h3><%#: Eval("CompanyName") %></h3>
                                        <span class="badge-count"><%# Eval("TotalHires") %> Hired</span>
                                    </div>
                                    <div class="company-metric">
                                        <span>Highest Package</span>
                                        <strong><%# Eval("HighestPackage", "{0:N2}") %> LPA</strong>
                                    </div>
                                    <div class="company-metric">
                                        <span>Average Package</span>
                                        <strong><%# Eval("AveragePackage", "{0:N2}") %> LPA</strong>
                                    </div>
                                    <div class="company-metric">
                                        <span>Recruited Students</span>
                                        <span class="company-students"><%#: Eval("HiredStudents") %></span>
                                    </div>
                                </article>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lblNoCompanies" runat="server" CssClass="empty-message" Visible="false" Text="No company recruitment data found." />
                    </div>
                </section>

                <!-- Data Control 3: Repeater for Placed Students Table -->
                <section class="panel">
                    <div class="panel-heading">
                        <div>
                            <div class="eyebrow">DATA CONTROL 3</div>
                            <h2>Placed Students Directory (Repeater)</h2>
                        </div>
                        <span class="pill pill-orange">Repeater Control</span>
                    </div>
                    <div class="table-wrap">
                        <asp:Repeater ID="rptPlacements" runat="server">
                            <HeaderTemplate>
                                <table class="data-grid repeater-grid">
                                    <thead>
                                        <tr>
                                            <th>Placement ID</th>
                                            <th>Student ID</th>
                                            <th>Enrollment No</th>
                                            <th>Student Name</th>
                                            <th>Department</th>
                                            <th>Company</th>
                                            <th>Job Role</th>
                                            <th>Package (LPA)</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><span class="id-tag">#<%# Eval("PlacementId") %></span></td>
                                    <td><%# Eval("StudentId") %></td>
                                    <td><strong><%#: Eval("EnrollmentNo") %></strong></td>
                                    <td><%#: Eval("StudentName") %></td>
                                    <td><%#: Eval("Department") %></td>
                                    <td><span class="company-tag"><%#: Eval("CompanyName") %></span></td>
                                    <td><%#: Eval("JobRole") %></td>
                                    <td><strong class="package-highlight"><%# Eval("PackageLPA", "{0:N2}") %> LPA</strong></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:Label ID="lblNoPlacements" runat="server" CssClass="empty-message" Visible="false" Text="No placed students found." />
                    </div>
                </section>

                <footer class="footer-note">
                    Practical 9 · Data Controls in ASP.NET (GridView, DetailsView, Repeater, DataList)
                </footer>
            </main>
        </div>
    </form>
</body>
</html>
