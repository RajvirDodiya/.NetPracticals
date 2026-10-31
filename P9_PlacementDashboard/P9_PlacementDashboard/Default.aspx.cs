using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace P9_PlacementDashboard
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Database.EnsureInitialized();
                    LoadSummary();
                    PopulateCompanyDropdown();
                    LoadPlacements();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Database connection or initialization failed: " +
                        Server.HtmlEncode(ex.Message) +
                        " Please ensure SQL Server LocalDB is available and running.";
                    lblMessage.Visible = true;
                }
            }
        }

        private void LoadSummary()
        {
            const string query = @"
SELECT
    COUNT(*) AS TotalStudents,
    COALESCE(SUM(CASE WHEN s.IsPlaced = 1 THEN 1 ELSE 0 END), 0) AS PlacedStudents,
    COALESCE(SUM(CASE WHEN s.IsPlaced = 0 THEN 1 ELSE 0 END), 0) AS UnplacedStudents,
    COALESCE(MAX(p.PackageLPA), 0) AS HighestPackage,
    COALESCE(AVG(p.PackageLPA), 0) AS AveragePackage
FROM dbo.Students s
LEFT JOIN dbo.Placements p ON s.StudentId = p.StudentId;";

            using (SqlConnection connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int total = Convert.ToInt32(reader["TotalStudents"]);
                        int placed = Convert.ToInt32(reader["PlacedStudents"]);
                        int unplaced = Convert.ToInt32(reader["UnplacedStudents"]);
                        decimal highest = Convert.ToDecimal(reader["HighestPackage"]);
                        decimal average = Convert.ToDecimal(reader["AveragePackage"]);
                        decimal percentage = total == 0 ? 0 : (decimal)placed * 100m / total;

                        lblTotal.Text = total.ToString();
                        lblPlaced.Text = placed.ToString();
                        lblUnplaced.Text = unplaced.ToString();
                        lblPercentage.Text = percentage.ToString("N2") + "%";
                        lblHighestPackage.Text = highest.ToString("N2") + " LPA";
                        lblAveragePackage.Text = average.ToString("N2") + " LPA";
                    }
                }
            }
        }

        private void PopulateCompanyDropdown()
        {
            const string query = @"
SELECT DISTINCT CompanyName
FROM dbo.Placements
ORDER BY CompanyName;";

            using (SqlConnection connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string company = reader["CompanyName"].ToString();
                        ddlCompany.Items.Add(company);
                    }
                }
            }
        }

        private void LoadPlacements()
        {
            string query = @"
SELECT
    s.StudentId,
    s.EnrollmentNo,
    s.StudentName,
    s.Department,
    s.PassingYear,
    s.IsPlaced,
    COALESCE(p.PlacementId, 0) AS PlacementId,
    COALESCE(p.CompanyName, '—') AS CompanyName,
    COALESCE(p.JobRole, '—') AS JobRole,
    p.PackageLPA
FROM dbo.Students s
LEFT JOIN dbo.Placements p ON s.StudentId = p.StudentId
WHERE 1 = 1";

            int statusFilter = Convert.ToInt32(ddlStatus.SelectedValue);
            if (statusFilter != -1)
            {
                query += " AND s.IsPlaced = @Status";
            }

            string companyFilter = ddlCompany.SelectedValue;
            if (!string.IsNullOrEmpty(companyFilter) && companyFilter != "ALL")
            {
                query += " AND p.CompanyName = @Company";
            }

            string searchTerm = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " AND (s.StudentName LIKE '%' + @Search + '%' OR s.EnrollmentNo LIKE '%' + @Search + '%')";
            }

            query += " ORDER BY s.StudentId;";

            using (SqlConnection connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (statusFilter != -1)
                {
                    command.Parameters.AddWithValue("@Status", statusFilter);
                }
                if (!string.IsNullOrEmpty(companyFilter) && companyFilter != "ALL")
                {
                    command.Parameters.AddWithValue("@Company", companyFilter);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    command.Parameters.AddWithValue("@Search", searchTerm);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    gvPlacements.DataSource = table;
                    gvPlacements.DataBind();
                }
            }
        }

        protected void FilterChanged(object sender, EventArgs e)
        {
            LoadPlacements();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadPlacements();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "-1";
            ddlCompany.SelectedValue = "ALL";
            txtSearch.Text = string.Empty;
            pnlDetails.Visible = false;
            gvPlacements.SelectedIndex = -1;
            LoadPlacements();
        }

        protected void gvPlacements_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvPlacements.SelectedDataKey != null)
            {
                int studentId = Convert.ToInt32(gvPlacements.SelectedDataKey.Value);
                LoadStudentDetails(studentId);
            }
        }

        private void LoadStudentDetails(int studentId)
        {
            const string query = @"
SELECT
    s.StudentId,
    s.EnrollmentNo,
    s.StudentName,
    s.Department,
    s.PassingYear,
    s.IsPlaced,
    COALESCE(p.PlacementId, 0) AS PlacementId,
    COALESCE(p.CompanyName, 'Not Assigned (Unplaced)') AS CompanyName,
    COALESCE(p.JobRole, 'Not Assigned') AS JobRole,
    CASE 
        WHEN p.PackageLPA IS NOT NULL THEN CAST(p.PackageLPA AS VARCHAR(20)) + ' LPA'
        ELSE '—'
    END AS PackageDisplay
FROM dbo.Students s
LEFT JOIN dbo.Placements p ON s.StudentId = p.StudentId
WHERE s.StudentId = @StudentId;";

            using (SqlConnection connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        dvStudentDetails.DataSource = table;
                        dvStudentDetails.DataBind();
                        pnlDetails.Visible = true;
                    }
                }
            }
        }

        protected void btnCloseDetails_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            gvPlacements.SelectedIndex = -1;
        }
    }
}
