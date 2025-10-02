-- =============================================
-- Script: Master Setup - Tổng hợp tất cả phân quyền
-- Database: UserManager
-- Description: File này chạy tất cả các scripts phân quyền theo thứ tự đúng
-- Created: 2025-10-01
-- =============================================

/*
===========================================
HƯỚNG DẪN SỬ DỤNG FILE NÀY:
===========================================

CÓ 2 CÁCH SETUP:

CÁCH 1: Chạy file này (tất cả cùng lúc)
   - Mở file này trong SQL Server Management Studio
   - Bấm Execute (F5)
   - Tất cả permissions sẽ được setup tự động

CÁCH 2: Chạy từng file riêng lẻ (khuyến nghị để hiểu rõ)
   - Chạy theo thứ tự:
     1. Database_Authorization.sql         (Table-level permissions)
     2. Database_ColumnLevel_Permissions.sql  (Column-level permissions)
     3. Database_RowLevelSecurity.sql      (Row-level security)

===========================================
TÓM TẮT CÁC LOẠI PHÂN QUYỀN:
===========================================

1. TABLE-LEVEL PERMISSIONS (Database_Authorization.sql):
   - Phân quyền cơ bản trên bảng, view, stored procedure, function
   - QuanLy: Full access
   - NhanVien: Limited access

2. COLUMN-LEVEL PERMISSIONS (Database_ColumnLevel_Permissions.sql):
   - Giới hạn quyền UPDATE chỉ trên một số cột cụ thể
   - Attendance: Chỉ UPDATE check_out
   - Salary: QuanLy chỉ UPDATE final_salary
   - Bao gồm trigger để tự động cập nhật total_hours

3. ROW-LEVEL SECURITY (Database_RowLevelSecurity.sql):
   - Giới hạn user chỉ xem/sửa dữ liệu của chính mình
   - Dựa vào user_id trong bảng UserLoginMapping
   - QuanLy (is_manager=1): Xem tất cả
   - NhanVien (is_manager=0): Chỉ xem dữ liệu của mình

===========================================
*/

USE UserManager;
GO

PRINT '=============================================';
PRINT 'STARTING PERMISSIONS SETUP';
PRINT 'Database: UserManager';
PRINT 'Date: ' + CONVERT(VARCHAR, GETDATE(), 120);
PRINT '=============================================';
PRINT '';
GO

-- =============================================
-- STEP 1: TABLE-LEVEL PERMISSIONS
-- =============================================

PRINT '--- STEP 1: Setting up Table-Level Permissions ---';
GO

-- Tạo roles
IF DATABASE_PRINCIPAL_ID('QuanLy') IS NOT NULL
    DROP ROLE QuanLy;
IF DATABASE_PRINCIPAL_ID('NhanVien') IS NOT NULL
    DROP ROLE NhanVien;
GO

CREATE ROLE QuanLy;
CREATE ROLE NhanVien;
GO

PRINT 'Created Roles: QuanLy and NhanVien';
GO

-- Phân quyền cơ bản trên các bảng khác (không phải Attendance và Salary)
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Account TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO QuanLy;
GRANT SELECT, INSERT, UPDATE ON dbo.Role TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Shift TO QuanLy;
GRANT SELECT, INSERT ON dbo.ProcessErrorLog TO QuanLy;

GRANT SELECT ON dbo.Users TO NhanVien;
GRANT SELECT ON dbo.Role TO NhanVien;
GRANT SELECT ON dbo.Shift TO NhanVien;

-- Views
GRANT SELECT ON dbo.v_AttendanceDetail TO QuanLy, NhanVien;
GRANT SELECT ON dbo.v_SalaryDetailForMonth TO QuanLy, NhanVien;
GRANT SELECT ON dbo.v_SalaryMonthDetail TO QuanLy, NhanVien;
GRANT SELECT ON dbo.v_UserWithRole TO QuanLy, NhanVien;

-- Stored Procedures
GRANT EXECUTE ON dbo.sp_AddUserManagement TO QuanLy;
GRANT EXECUTE ON dbo.sp_UpdateUserManagement TO QuanLy, NhanVien;
GRANT EXECUTE ON dbo.sp_MergeUserManagement TO QuanLy;
GRANT EXECUTE ON dbo.DisableUserById TO QuanLy;
GRANT EXECUTE ON dbo.sp_UpdateSalaryForUser TO QuanLy;
GRANT EXECUTE ON dbo.sp_UpdateSalaryForAllUsers TO QuanLy;
GRANT EXECUTE ON dbo.SearchUser TO QuanLy, NhanVien;

-- Functions
GRANT SELECT ON dbo.fn_GetSalaryDetailForMonth TO QuanLy, NhanVien;
GRANT SELECT ON dbo.fn_GetAttendanceByUser TO QuanLy, NhanVien;
GRANT SELECT ON dbo.fn_GetAllowanceByUserForMonth TO QuanLy, NhanVien;
GRANT SELECT ON dbo.fn_GetPenaltyByUserForMonth TO QuanLy, NhanVien;

PRINT 'Step 1 completed: Basic table-level permissions granted';
PRINT '';
GO

-- =============================================
-- STEP 2: COLUMN-LEVEL PERMISSIONS
-- =============================================

PRINT '--- STEP 2: Setting up Column-Level Permissions ---';
GO

-- ATTENDANCE: Chỉ cho UPDATE cột check_out
GRANT SELECT, INSERT ON dbo.Attendance TO QuanLy, NhanVien;
GRANT UPDATE (check_out) ON dbo.Attendance TO QuanLy, NhanVien;

PRINT 'Attendance: Granted SELECT, INSERT, UPDATE(check_out) to QuanLy and NhanVien';
GO

-- SALARY: QuanLy chỉ UPDATE final_salary
GRANT SELECT, INSERT ON dbo.Salary TO QuanLy;
GRANT UPDATE (final_salary) ON dbo.Salary TO QuanLy;
GRANT SELECT ON dbo.Salary TO NhanVien;

PRINT 'Salary: QuanLy can UPDATE(final_salary), NhanVien can only SELECT';
GO

-- ALLOWANCEANDPENALTY
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.AllowanceAndPenalty TO QuanLy;
GRANT SELECT ON dbo.AllowanceAndPenalty TO NhanVien;

PRINT 'Step 2 completed: Column-level permissions granted';
PRINT '';
GO

-- =============================================
-- STEP 3: TRIGGER FOR AUTO-UPDATE TOTAL_HOURS
-- =============================================

PRINT '--- STEP 3: Creating Trigger for total_hours ---';
GO

IF OBJECT_ID('dbo.trg_UpdateTotalHours', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_UpdateTotalHours;
GO

CREATE TRIGGER dbo.trg_UpdateTotalHours
ON dbo.Attendance
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF UPDATE(check_out)
    BEGIN
        -- Cập nhật total_hours cho salary đã tồn tại
        UPDATE s
        SET s.total_hours = ISNULL(s.total_hours, 0) + 
            CAST(DATEDIFF(MINUTE, i.check_in, i.check_out) AS DECIMAL(10,2)) / 60.0
        FROM dbo.Salary s
        INNER JOIN inserted i ON s.user_id = i.user_id 
            AND s.month = MONTH(i.work_date) 
            AND s.year = YEAR(i.work_date)
        WHERE i.check_out IS NOT NULL
            AND NOT EXISTS (
                SELECT 1 FROM deleted d 
                WHERE d.attendance_id = i.attendance_id 
                AND d.check_out IS NOT NULL
            );
        
        -- Tạo mới salary record nếu chưa tồn tại
        INSERT INTO dbo.Salary (user_id, month, year, total_hours, total_Bonus, total_Penalty)
        SELECT 
            i.user_id,
            MONTH(i.work_date),
            YEAR(i.work_date),
            CAST(DATEDIFF(MINUTE, i.check_in, i.check_out) AS DECIMAL(10,2)) / 60.0,
            0,
            0
        FROM inserted i
        WHERE i.check_out IS NOT NULL
            AND NOT EXISTS (
                SELECT 1 FROM dbo.Salary s
                WHERE s.user_id = i.user_id
                    AND s.month = MONTH(i.work_date)
                    AND s.year = YEAR(i.work_date)
            )
            AND NOT EXISTS (
                SELECT 1 FROM deleted d 
                WHERE d.attendance_id = i.attendance_id 
                AND d.check_out IS NOT NULL
            );
    END
END
GO

PRINT 'Trigger created: trg_UpdateTotalHours';
PRINT '  - Auto updates total_hours when user checks out';
PRINT '  - Works even if user has no UPDATE permission on Salary';
PRINT '';
GO

-- =============================================
-- STEP 4: UTILITY STORED PROCEDURES
-- =============================================

PRINT '--- STEP 4: Creating Utility Stored Procedures ---';
GO

-- Procedure: Check-out
IF OBJECT_ID('dbo.sp_CheckOut', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CheckOut;
GO

CREATE PROCEDURE dbo.sp_CheckOut
    @attendance_id INT,
    @check_out_time DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF @check_out_time IS NULL
        SET @check_out_time = GETDATE();
    
    BEGIN TRY
        UPDATE dbo.Attendance
        SET check_out = @check_out_time
        WHERE attendance_id = @attendance_id
            AND check_out IS NULL;
        
        IF @@ROWCOUNT = 0
            RAISERROR('Attendance not found or already checked out', 16, 1);
        ELSE
            PRINT 'Check-out successful at ' + CONVERT(VARCHAR, @check_out_time, 120);
    END TRY
    BEGIN CATCH
        DECLARE @Err NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Err, 16, 1);
    END CATCH
END
GO

GRANT EXECUTE ON dbo.sp_CheckOut TO QuanLy, NhanVien;
GO

-- Procedure: Tính lương
IF OBJECT_ID('dbo.sp_CalculateFinalSalary', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CalculateFinalSalary;
GO

CREATE PROCEDURE dbo.sp_CalculateFinalSalary
    @user_id INT,
    @month INT,
    @year INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @hourly_rate DECIMAL(18,2);
        DECLARE @total_hours DECIMAL(10,2);
        DECLARE @total_bonus DECIMAL(10,2);
        DECLARE @total_penalty DECIMAL(10,2);
        DECLARE @calculated_salary DECIMAL(18,2);
        
        SELECT 
            @total_hours = ISNULL(total_hours, 0),
            @total_bonus = ISNULL(total_Bonus, 0),
            @total_penalty = ISNULL(total_Penalty, 0)
        FROM dbo.Salary
        WHERE user_id = @user_id AND month = @month AND year = @year;
        
        SELECT @hourly_rate = ISNULL(r.salary, 0) / 160
        FROM dbo.Users u
        INNER JOIN dbo.Role r ON u.role_id = r.role_id
        WHERE u.user_id = @user_id;
        
        SET @calculated_salary = (@total_hours * @hourly_rate) + @total_bonus - @total_penalty;
        
        UPDATE dbo.Salary
        SET final_salary = @calculated_salary
        WHERE user_id = @user_id AND month = @month AND year = @year;
        
        PRINT 'Final salary calculated: ' + CAST(@calculated_salary AS VARCHAR);
    END TRY
    BEGIN CATCH
        DECLARE @Err2 NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Err2, 16, 1);
    END CATCH
END
GO

GRANT EXECUTE ON dbo.sp_CalculateFinalSalary TO QuanLy;
GO

PRINT 'Stored procedures created: sp_CheckOut, sp_CalculateFinalSalary';
PRINT '';
GO

-- =============================================
-- STEP 5: ROW-LEVEL SECURITY SETUP
-- =============================================

PRINT '--- STEP 5: Setting up Row-Level Security ---';
GO

-- Tạo schema cho security
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Security')
    EXEC('CREATE SCHEMA Security');
GO

-- Tạo bảng mapping
IF OBJECT_ID('dbo.UserLoginMapping', 'U') IS NOT NULL
    DROP TABLE dbo.UserLoginMapping;
GO

CREATE TABLE dbo.UserLoginMapping
(
    mapping_id INT IDENTITY(1,1) PRIMARY KEY,
    database_username NVARCHAR(128) NOT NULL UNIQUE,
    user_id INT NOT NULL,
    is_manager BIT NOT NULL DEFAULT 0,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES dbo.Users(user_id)
);
GO

PRINT 'Created UserLoginMapping table';
GO

-- Tạo predicate functions
IF OBJECT_ID('Security.fn_AttendanceAccessPredicate', 'IF') IS NOT NULL
    DROP FUNCTION Security.fn_AttendanceAccessPredicate;
GO

CREATE FUNCTION Security.fn_AttendanceAccessPredicate(@user_id INT)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
    SELECT 1 AS result
    WHERE 
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME() AND is_manager = 1
        )
        OR
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME() AND user_id = @user_id
        );
GO

-- Tương tự cho Salary
IF OBJECT_ID('Security.fn_SalaryAccessPredicate', 'IF') IS NOT NULL
    DROP FUNCTION Security.fn_SalaryAccessPredicate;
GO

CREATE FUNCTION Security.fn_SalaryAccessPredicate(@user_id INT)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
    SELECT 1 AS result
    WHERE 
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME() AND is_manager = 1
        )
        OR
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME() AND user_id = @user_id
        );
GO

PRINT 'Created security predicate functions';
GO

-- Tạo security policies
IF EXISTS (SELECT * FROM sys.security_policies WHERE name = 'AttendanceAccessPolicy')
    DROP SECURITY POLICY Security.AttendanceAccessPolicy;
GO

CREATE SECURITY POLICY Security.AttendanceAccessPolicy
ADD FILTER PREDICATE Security.fn_AttendanceAccessPredicate(user_id) ON dbo.Attendance,
ADD BLOCK PREDICATE Security.fn_AttendanceAccessPredicate(user_id) ON dbo.Attendance AFTER INSERT,
ADD BLOCK PREDICATE Security.fn_AttendanceAccessPredicate(user_id) ON dbo.Attendance AFTER UPDATE
WITH (STATE = ON);
GO

IF EXISTS (SELECT * FROM sys.security_policies WHERE name = 'SalaryAccessPolicy')
    DROP SECURITY POLICY Security.SalaryAccessPolicy;
GO

CREATE SECURITY POLICY Security.SalaryAccessPolicy
ADD FILTER PREDICATE Security.fn_SalaryAccessPredicate(user_id) ON dbo.Salary,
ADD BLOCK PREDICATE Security.fn_SalaryAccessPredicate(user_id) ON dbo.Salary AFTER INSERT,
ADD BLOCK PREDICATE Security.fn_SalaryAccessPredicate(user_id) ON dbo.Salary AFTER UPDATE
WITH (STATE = ON);
GO

PRINT 'Created security policies: AttendanceAccessPolicy, SalaryAccessPolicy';
GO

-- Procedure quản lý mapping
IF OBJECT_ID('dbo.sp_AddUserMapping', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_AddUserMapping;
GO

CREATE PROCEDURE dbo.sp_AddUserMapping
    @database_username NVARCHAR(128),
    @user_id INT,
    @is_manager BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE user_id = @user_id)
        BEGIN
            RAISERROR('User ID does not exist', 16, 1);
            RETURN;
        END
        
        INSERT INTO dbo.UserLoginMapping (database_username, user_id, is_manager)
        VALUES (@database_username, @user_id, @is_manager);
        
        PRINT 'User mapping added: ' + @database_username + ' -> user_id ' + CAST(@user_id AS VARCHAR);
    END TRY
    BEGIN CATCH
        DECLARE @Err3 NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Err3, 16, 1);
    END CATCH
END
GO

PRINT 'Created sp_AddUserMapping procedure';
PRINT 'Step 5 completed: Row-level security configured';
PRINT '';
GO

-- =============================================
-- FINAL: SUMMARY
-- =============================================

PRINT '';
PRINT '=============================================';
PRINT 'PERMISSIONS SETUP COMPLETED SUCCESSFULLY!';
PRINT '=============================================';
PRINT '';
PRINT 'Summary:';
PRINT '  ✓ Roles created: QuanLy, NhanVien';
PRINT '  ✓ Table-level permissions granted';
PRINT '  ✓ Column-level permissions configured';
PRINT '    - Attendance: UPDATE(check_out) only';
PRINT '    - Salary: QuanLy UPDATE(final_salary) only';
PRINT '  ✓ Trigger created: Auto-update total_hours';
PRINT '  ✓ Row-level security enabled';
PRINT '  ✓ Utility procedures created';
PRINT '';
PRINT 'Next Steps:';
PRINT '  1. Create SQL logins for your users';
PRINT '  2. Create database users from logins';
PRINT '  3. Add users to roles (QuanLy or NhanVien)';
PRINT '  4. Add user mappings: EXEC sp_AddUserMapping';
PRINT '';
PRINT 'Example:';
PRINT '  CREATE LOGIN NguyenVanA WITH PASSWORD = ''Pass123!'';';
PRINT '  CREATE USER NguyenVanA FOR LOGIN NguyenVanA;';
PRINT '  ALTER ROLE NhanVien ADD MEMBER NguyenVanA;';
PRINT '  EXEC sp_AddUserMapping ''NguyenVanA'', 5, 0;';
PRINT '';
PRINT '=============================================';
GO

-- Xem tất cả permissions
SELECT 
    USER_NAME(dp.grantee_principal_id) AS [Role],
    OBJECT_NAME(dp.major_id) AS [Object],
    dp.permission_name AS [Permission],
    COL_NAME(dp.major_id, dp.minor_id) AS [Column],
    dp.state_desc AS [State]
FROM sys.database_permissions dp
WHERE USER_NAME(dp.grantee_principal_id) IN ('QuanLy', 'NhanVien')
    AND dp.major_id > 0
ORDER BY [Role], [Object], [Permission];
GO
