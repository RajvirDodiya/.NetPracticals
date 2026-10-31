================================================================================
PRACTICAL 9 - PLACEMENT DASHBOARD USING DATA CONTROLS
ASP.NET Web Forms | C# | .NET Framework 4.8 | SQL Server LocalDB
================================================================================

PROJECT OVERVIEW
----------------
This project implements a Student Placement Cell Dashboard built using ASP.NET
Web Forms and C# in Visual Studio. It demonstrates the use of core ASP.NET Data
Controls:
  1. GridView    - Main student placement registry with sorting, status badges,
                   search, and row selection.
  2. DetailsView - Detailed single-student placement profile card on row selection.
  3. Repeater    - Customized placed student directory on Companies.aspx.
  4. DataList    - Multi-column card layout displaying company-wise hiring analytics.
  5. DropDownList- Dynamic filtering by Placement Status and Company.

DATABASE SPECIFICATION (PlacementDB)
------------------------------------
Database: PlacementDB

Tables & Schema:
  1. Students
     - StudentId     (INT, Primary Key)
     - EnrollmentNo  (VARCHAR(20), Not Null)
     - StudentName   (VARCHAR(100), Not Null)
     - Department    (VARCHAR(50), Not Null)
     - PassingYear   (INT, Not Null)
     - IsPlaced      (BIT, Not Null, Default 0)

  2. Placements
     - PlacementId   (INT, Primary Key)
     - StudentId     (INT, Not Null, Unique, Foreign Key -> Students.StudentId)
     - CompanyName   (VARCHAR(100), Not Null)
     - JobRole       (VARCHAR(100), Not Null)
     - PackageLPA    (DECIMAL(6,2), Not Null)

SAMPLE DATA & VERIFICATION
--------------------------
Total Students : 8
Placed         : 5 (Aarav Patel, Diya Shah, Krish Mehta, Neha Trivedi, Dev Patel)
Unplaced       : 3 (Riya Joshi, Arjun Desai, Mira Shah)
Placement Rate : 62.50%
Highest Package: 6.00 LPA (Dev Patel -> Pinnacle IIT)
Average Package: 4.14 LPA

Companies & Offers:
  - Pinnacle IIT : 1 hire  | Highest: 6.00 LPA | Average: 6.00 LPA (Dev Patel)
  - Infosys      : 1 hire  | Highest: 4.00 LPA | Average: 4.00 LPA (Diya Shah)
  - TCS          : 2 hires | Highest: 3.60 LPA | Average: 3.60 LPA (Aarav Patel, Neha Trivedi)
  - Wipro        : 1 hire  | Highest: 3.50 LPA | Average: 3.50 LPA (Krish Mehta)

HOW TO RUN IN VISUAL STUDIO
---------------------------
1. Open the folder:
     c:\Users\HARSH\Downloads\P9_PlacementDashboard_VisualStudio\P9_PlacementDashboard
2. Double-click "P9_PlacementDashboard.sln" to open in Visual Studio.
3. In Solution Explorer, right-click "Default.aspx" and choose "Set As Start Page".
4. Press F5 (or click "IIS Express" in the top toolbar).
5. The application will start in your default browser.
   - Default.aspx    : Placement Dashboard (GridView, DetailsView, KPI Cards, Filters)
   - Companies.aspx  : Company Placement Summary (DataList, Repeater, Metrics)

DATABASE SETUP
--------------
- AUTOMATIC:
  The application automatically checks for PlacementDB and the required tables
  and seeds the sample data on the first page request via Database.cs.
  No manual SQL execution is required!

- OPTIONAL MANUAL SETUP:
  If you prefer to execute the SQL script manually in SQL Server Management Studio
  (SSMS) or Visual Studio Server Explorer:
  1. Open "Database_Setup.sql" in SSMS or Visual Studio.
  2. Connect to (LocalDB)\MSSQLLocalDB or your SQL Server instance.
  3. Execute the script.

FILE STRUCTURE
--------------
- Default.aspx            : Main placement dashboard page.
- Default.aspx.cs         : C# code-behind for statistics, search, GridView & DetailsView.
- Default.aspx.designer.cs: Web control declarations.
- Companies.aspx          : Company-wise summary and placed students directory.
- Companies.aspx.cs       : C# code-behind for Repeater and DataList controls.
- Companies.aspx.designer.cs: Web control declarations.
- Database.cs             : ADO.NET database connection & auto-initialization.
- Database_Setup.sql      : Complete SQL script for database creation and seeding.
- Web.config              : Connection strings and .NET Framework 4.8 configuration.
- Content/site.css        : Modern, responsive stylesheet.
- P9_PlacementDashboard.sln   : Visual Studio Solution file.
- P9_PlacementDashboard.csproj: Visual Studio C# Project file.
