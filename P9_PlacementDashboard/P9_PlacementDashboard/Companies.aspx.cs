using System;
using System.Data;
using System.Data.SqlClient;

namespace P9_PlacementDashboard
{
    public partial class Companies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Database.EnsureInitialized();
                    LoadOverviewMetrics();
                    LoadPlacedStudents();
                    LoadCompanySummary();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Database connection/loading error: " +
                        Server.HtmlEncode(ex.Message) +
                        " Please check SQL Server LocalDB connection.";
                    lblMessage.Visible = true;
                }
            }
        }

        private void LoadOverviewMetrics()
        {
            const string query = @"
SELECT
    COUNT(p.PlacementId) AS TotalPlaced,
    COUNT(DISTINCT p.CompanyName) AS TotalCompanies,
    COALESCE(MAX(p.PackageLPA), 0) AS HighestPackage,
    COALESCE(AVG(p.PackageLPA), 0) AS AveragePackage
FROM dbo.Placements p;";

            using (SqlConnection connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblPlacedCount.Text = reader["TotalPlaced"].ToString();
                        lblCompanyCount.Text = reader["TotalCompanies"].ToString();
                        lblCompanyHighest.Text = Convert.ToDecimal(reader["HighestPackage"]).ToString("N2") + " LPA";
                        lblCompanyAverage.Text = Convert.ToDecimal(reader["AveragePackage"]).ToString("N2") + " LPA";
                    }
                }
            }
        }

        private void LoadPlacedStudents()
        {
            const string query = @"
SELECT
    p.PlacementId,
    s.StudentId,
    s.EnrollmentNo,
    s.StudentName,
    s.Department,
    p.CompanyName,
    p.JobRole,
    p.PackageLPA
FROM dbo.Placements p
INNER JOIN dbo.Students s ON p.StudentId = s.StudentId
ORDER BY p.PackageLPA DESC, p.PlacementId;";

            using (SqlConnection connection = Database.GetConnection())
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                rptPlacements.DataSource = table;
                rptPlacements.DataBind();
                lblNoPlacements.Visible = table.Rows.Count == 0;
            }
        }

        private void LoadCompanySummary()
        {
            const string query = @"
SELECT
    p.CompanyName,
    COUNT(p.StudentId) AS TotalHires,
    MAX(p.PackageLPA) AS HighestPackage,
    AVG(p.PackageLPA) AS AveragePackage,
    STRING_AGG(s.StudentName, ', ') AS HiredStudents
FROM dbo.Placements p
INNER JOIN dbo.Students s ON p.StudentId = s.StudentId
GROUP BY p.CompanyName
ORDER BY TotalHires DESC, HighestPackage DESC;";

            using (SqlConnection connection = Database.GetConnection())
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                dlCompanies.DataSource = table;
                dlCompanies.DataBind();
                lblNoCompanies.Visible = table.Rows.Count == 0;
            }
        }
    }
}
