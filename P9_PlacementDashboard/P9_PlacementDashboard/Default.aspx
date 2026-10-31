<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="P9_PlacementDashboard.Default" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Placement Dashboard - Practical 9</title>
    <link href="Content/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-shell">
            <!-- Header -->
            <header class="topbar">
                <div>
                    <div class="eyebrow">STUDENT PLACEMENT CELL · PRACTICAL 9</div>
                    <h1>Placement Dashboard</h1>
                    <p class="subtitle">Comprehensive student placement tracking using ASP.NET Data Controls</p>
                </div>
                <div class="header-actions">
                    <a class="nav-button" href="Companies.aspx">Company Reports &amp; Summary <span aria-hidden="true">→</span></a>
                </div>
            </header>

            <main>
                <!-- Alert Message -->
                <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false" EnableViewState="false" />

                <!-- Section: Statistics -->
                <section class="section-heading">
                    <div>
                        <div class="eyebrow">METRICS</div>
                        <h2>Placement Key Performance Indicators</h2>
                    </div>
                    <span class="section-note">Live data from PlacementDB</span>
                </section>

                <section class="stats-grid stats-grid-6">
                    <article class="stat-card">
                        <span class="stat-label">Total Students</span>
                        <asp:Label ID="lblTotal" runat="server" CssClass="stat-value" Text="0" />
                        <span class="stat-foot">Enrolled in cohort</span>
                    </article>
                    <article class="stat-card stat-card-success">
                        <span class="stat-label">Placed Students</span>
                        <asp:Label ID="lblPlaced" runat="server" CssClass="stat-value text-green" Text="0" />
                        <span class="stat-foot">Secured job offers</span>
                    </article>
                    <article class="stat-card">
                        <span class="stat-label">Unplaced Students</span>
                        <asp:Label ID="lblUnplaced" runat="server" CssClass="stat-value text-orange" Text="0" />
                        <span class="stat-foot">Seeking placement</span>
                    </article>
                    <article class="stat-card">
                        <span class="stat-label">Placement Rate</span>
                        <asp:Label ID="lblPercentage" runat="server" CssClass="stat-value" Text="0.00%" />
                        <span class="stat-foot">Placed / Total</span>
                    </article>
                    <article class="stat-card stat-card-highlight">
                        <span class="stat-label">Highest Package</span>
                        <asp:Label ID="lblHighestPackage" runat="server" CssClass="stat-value text-blue" Text="0.00" />
                        <span class="stat-foot">Top offer in cohort</span>
                    </article>
                    <article class="stat-card">
                        <span class="stat-label">Average Package</span>
                        <asp:Label ID="lblAveragePackage" runat="server" CssClass="stat-value" Text="0.00" />
                        <span class="stat-foot">Placed students average</span>
                    </article>
                </section>

                <!-- Filter & Search Toolbar -->
                <section class="panel filter-panel">
                    <div class="filter-bar">
                        <div class="filter-group">
                            <label for="ddlStatus">Placement Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged">
                                <asp:ListItem Text="All Statuses" Value="-1" Selected="True" />
                                <asp:ListItem Text="Placed Only" Value="1" />
                                <asp:ListItem Text="Not Placed Only" Value="0" />
                            </asp:DropDownList>
                        </div>
                        <div class="filter-group">
                            <label for="ddlCompany">Company</label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged">
                                <asp:ListItem Text="All Companies" Value="ALL" Selected="True" />
                            </asp:DropDownList>
                        </div>
                        <div class="filter-group search-group">
                            <label for="txtSearch">Search Student</label>
                            <div class="search-box">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Name or Enrollment No..." />
                                <asp:Button ID="btnFilter" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnFilter_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </section>

                <!-- DetailsView Modal/Card for Selected Student -->
                <asp:Panel ID="pnlDetails" runat="server" Visible="false" CssClass="panel details-panel">
                    <div class="panel-heading">
                        <div>
                            <div class="eyebrow">STUDENT DETAILS</div>
                            <h2>Selected Student Placement Profile</h2>
                        </div>
                        <div class="panel-tools">
                            <span class="pill pill-purple">DetailsView Control</span>
                            <asp:Button ID="btnCloseDetails" runat="server" Text="✕ Close" CssClass="btn btn-sm btn-outline" OnClick="btnCloseDetails_Click" />
                        </div>
                    </div>
                    <div class="details-wrap">
                        <asp:DetailsView ID="dvStudentDetails" runat="server" AutoGenerateRows="False"
                            CssClass="details-view-table" GridLines="None">
                            <Fields>
                                <asp:BoundField DataField="StudentId" HeaderText="Student ID" />
                                <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                <asp:BoundField DataField="Department" HeaderText="Department" />
                                <asp:BoundField DataField="PassingYear" HeaderText="Passing Year" />
                                <asp:TemplateField HeaderText="Placement Status">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToBoolean(Eval("IsPlaced")) ? "status status-placed" : "status status-pending" %>'>
                                            <%# Convert.ToBoolean(Eval("IsPlaced")) ? "Placed" : "Not Placed" %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                <asp:BoundField DataField="JobRole" HeaderText="Designation / Role" />
                                <asp:BoundField DataField="PackageDisplay" HeaderText="Annual Package" />
                            </Fields>
                        </asp:DetailsView>
                    </div>
                </asp:Panel>

                <!-- Main Data Control: GridView -->
                <section class="panel">
                    <div class="panel-heading">
                        <div>
                            <div class="eyebrow">DATA CONTROL 1</div>
                            <h2>Student Placement Records (GridView)</h2>
                        </div>
                        <div class="panel-tools">
                            <span class="pill">GridView Control</span>
                        </div>
                    </div>
                    <div class="table-wrap">
                        <asp:GridView ID="gvPlacements" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="StudentId" OnSelectedIndexChanged="gvPlacements_SelectedIndexChanged"
                            CssClass="data-grid" GridLines="None"
                            EmptyDataText="No student placement records match your search."
                            EmptyDataRowStyle-CssClass="empty-row">
                            <Columns>
                                <asp:BoundField DataField="StudentId" HeaderText="ID" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                <asp:BoundField DataField="Department" HeaderText="Dept" />
                                <asp:BoundField DataField="PassingYear" HeaderText="Batch" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToBoolean(Eval("IsPlaced")) ? "status status-placed" : "status status-pending" %>'>
                                            <%# Convert.ToBoolean(Eval("IsPlaced")) ? "Placed" : "Not Placed" %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                <asp:BoundField DataField="JobRole" HeaderText="Job Role" />
                                <asp:TemplateField HeaderText="Package (LPA)">
                                    <ItemTemplate>
                                        <%# Eval("PackageLPA") != DBNull.Value && Eval("PackageLPA") != null ? string.Format("{0:N2} LPA", Eval("PackageLPA")) : "<span class='text-muted'>—</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" SelectText="View Profile" HeaderText="Action"
                                    ButtonType="Link" ControlStyle-CssClass="action-link" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </section>

                <footer class="footer-note">
                    Practical 9 · Placement Dashboard using ASP.NET Data Controls (GridView, DetailsView, Repeater &amp; DataList)
                </footer>
            </main>
        </div>
    </form>
</body>
</html>
