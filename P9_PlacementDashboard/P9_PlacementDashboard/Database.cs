using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace P9_PlacementDashboard
{
    /// <summary>
    /// Handles database initialization and connection management for PlacementDB.
    /// Automatically creates the database, tables, and sample data on first run.
    /// </summary>
    public static class Database
    {
        private static readonly object InitLock = new object();
        private static bool _initialized;

        public static void EnsureInitialized()
        {
            if (_initialized)
                return;

            lock (InitLock)
            {
                if (_initialized)
                    return;

                string masterConnectionString =
                    ConfigurationManager.ConnectionStrings["PlacementDBMaster"] != null
                        ? ConfigurationManager.ConnectionStrings["PlacementDBMaster"].ConnectionString
                        : @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30";

                // Step 1: Ensure PlacementDB exists
                try
                {
                    using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
                    {
                        masterConnection.Open();
                        const string createDatabaseSql = @"
IF DB_ID(N'PlacementDB') IS NULL
BEGIN
    CREATE DATABASE [PlacementDB];
END;";
                        using (SqlCommand command = new SqlCommand(createDatabaseSql, masterConnection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {
                    // If creating database directly via master fails (e.g. restricted permissions),
                    // proceed to attempt connection directly to PlacementDB if it already exists.
                }

                // Step 2: Ensure tables exist with the requested two-table schema
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    const string setupSql = @"
-- Check if legacy single-table schema exists (where Students lacked EnrollmentNo)
IF OBJECT_ID(N'dbo.Students', N'U') IS NOT NULL AND COL_LENGTH(N'dbo.Students', N'EnrollmentNo') IS NULL
BEGIN
    IF OBJECT_ID(N'dbo.Placements', N'U') IS NOT NULL DROP TABLE dbo.Placements;
    DROP TABLE dbo.Students;
END;

-- Create Students table if not exists
IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        StudentId INT PRIMARY KEY,
        EnrollmentNo VARCHAR(20) NOT NULL,
        StudentName VARCHAR(100) NOT NULL,
        Department VARCHAR(50) NOT NULL,
        PassingYear INT NOT NULL,
        IsPlaced BIT NOT NULL DEFAULT 0
    );
END;

-- Create Placements table if not exists
IF OBJECT_ID(N'dbo.Placements', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Placements
    (
        PlacementId INT PRIMARY KEY,
        StudentId INT NOT NULL UNIQUE,
        CompanyName VARCHAR(100) NOT NULL,
        JobRole VARCHAR(100) NOT NULL,
        PackageLPA DECIMAL(6,2) NOT NULL,
        FOREIGN KEY (StudentId)
            REFERENCES dbo.Students(StudentId)
    );
END;

-- Insert Student records if table is empty
IF NOT EXISTS (SELECT 1 FROM dbo.Students)
BEGIN
    INSERT INTO dbo.Students (StudentId, EnrollmentNo, StudentName, Department, PassingYear, IsPlaced) VALUES
    (1, 'CE001', 'Aarav Patel', 'CE', 2026, 1),
    (2, 'CE002', 'Diya Shah', 'CE', 2026, 1),
    (3, 'CE003', 'Krish Mehta', 'CE', 2026, 1),
    (4, 'CE004', 'Riya Joshi', 'CE', 2026, 0),
    (5, 'CE005', 'Arjun Desai', 'CE', 2026, 0),
    (6, 'CE006', 'Neha Trivedi', 'CE', 2026, 1),
    (7, 'CE007', 'Dev Patel', 'CE', 2026, 1),
    (8, 'CE008', 'Mira Shah', 'CE', 2026, 0);
END;

-- Insert Placement records if table is empty
IF NOT EXISTS (SELECT 1 FROM dbo.Placements)
BEGIN
    INSERT INTO dbo.Placements (PlacementId, StudentId, CompanyName, JobRole, PackageLPA) VALUES
    (1, 1, 'TCS', 'System Engineer', 3.60),
    (2, 2, 'Infosys', 'Systems Engineer', 4.00),
    (3, 3, 'Wipro', 'Project Engineer', 3.50),
    (4, 6, 'TCS', 'Assistant System Engineer', 3.60),
    (5, 7, 'Pinnacle IIT', 'Software Developer', 6.00);
END;";

                    using (SqlCommand command = new SqlCommand(setupSql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                _initialized = true;
            }
        }

        public static SqlConnection GetConnection()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["PlacementDBConnection"] != null
                    ? ConfigurationManager.ConnectionStrings["PlacementDBConnection"].ConnectionString
                    : @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=PlacementDB;Integrated Security=True;Connect Timeout=30";

            return new SqlConnection(connectionString);
        }

        public static DataTable ExecuteDataTable(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
