-- ============================================================================
-- PRACTICAL 9: PLACEMENT DASHBOARD USING DATA CONTROLS
-- Database Setup Script for SQL Server / LocalDB
-- ============================================================================

-- 1. Create PlacementDB database if not exists
IF DB_ID(N'PlacementDB') IS NULL
BEGIN
    CREATE DATABASE [PlacementDB];
END;
GO

USE [PlacementDB];
GO

-- 2. Clean up existing tables if re-running
IF OBJECT_ID(N'dbo.Placements', N'U') IS NOT NULL 
    DROP TABLE dbo.Placements;
GO

IF OBJECT_ID(N'dbo.Students', N'U') IS NOT NULL 
    DROP TABLE dbo.Students;
GO

-- 3. Create Students Table
CREATE TABLE Students
(
    StudentId INT PRIMARY KEY,
    EnrollmentNo VARCHAR(20) NOT NULL,
    StudentName VARCHAR(100) NOT NULL,
    Department VARCHAR(50) NOT NULL,
    PassingYear INT NOT NULL,
    IsPlaced BIT NOT NULL DEFAULT 0
);
GO

-- 4. Create Placements Table (Linked to Students via Foreign Key)
CREATE TABLE Placements
(
    PlacementId INT PRIMARY KEY,
    StudentId INT NOT NULL UNIQUE,
    CompanyName VARCHAR(100) NOT NULL,
    JobRole VARCHAR(100) NOT NULL,
    PackageLPA DECIMAL(6,2) NOT NULL,
    FOREIGN KEY (StudentId)
        REFERENCES Students(StudentId)
);
GO

-- 5. Insert Student Records
INSERT INTO Students VALUES
(1, 'CE001', 'Aarav Patel', 'CE', 2026, 1),
(2, 'CE002', 'Diya Shah', 'CE', 2026, 1),
(3, 'CE003', 'Krish Mehta', 'CE', 2026, 1),
(4, 'CE004', 'Riya Joshi', 'CE', 2026, 0),
(5, 'CE005', 'Arjun Desai', 'CE', 2026, 0),
(6, 'CE006', 'Neha Trivedi', 'CE', 2026, 1),
(7, 'CE007', 'Dev Patel', 'CE', 2026, 1),
(8, 'CE008', 'Mira Shah', 'CE', 2026, 0);
GO

-- 6. Insert Placement Records
INSERT INTO Placements VALUES
(1, 1, 'TCS', 'System Engineer', 3.60),
(2, 2, 'Infosys', 'Systems Engineer', 4.00),
(3, 3, 'Wipro', 'Project Engineer', 3.50),
(4, 6, 'TCS', 'Assistant System Engineer', 3.60),
(5, 7, 'Pinnacle IIT', 'Software Developer', 6.00);
GO

-- 7. Verification Queries
PRINT '========================================';
PRINT 'STUDENTS TABLE:';
SELECT * FROM Students;

PRINT '========================================';
PRINT 'PLACEMENTS TABLE:';
SELECT * FROM Placements;

PRINT '========================================';
PRINT 'JOINED DASHBOARD VIEW:';
SELECT 
    s.StudentId,
    s.EnrollmentNo,
    s.StudentName,
    s.Department,
    s.PassingYear,
    s.IsPlaced,
    p.PlacementId,
    p.CompanyName,
    p.JobRole,
    p.PackageLPA
FROM Students s
LEFT JOIN Placements p ON s.StudentId = p.StudentId
ORDER BY s.StudentId;
GO
