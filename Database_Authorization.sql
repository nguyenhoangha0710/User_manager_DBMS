-- =============================================
-- Script: Database Authorization and Role Management
-- Database: UserManager
-- Description: Phân quyền cho 2 Role: Quản lý (Manager) và Nhân viên (Employee)
-- Created: 2025-10-01
-- =============================================

USE UserManager;
GO

-- =============================================
-- SECTION 1: TẠO DATABASE ROLES
-- =============================================

-- Kiểm tra và xóa role nếu đã tồn tại (để có thể chạy lại script)
IF DATABASE_PRINCIPAL_ID('QuanLy') IS NOT NULL
    DROP ROLE QuanLy;
GO

IF DATABASE_PRINCIPAL_ID('NhanVien') IS NOT NULL
    DROP ROLE NhanVien;
GO

-- Tạo Role Quản lý
CREATE ROLE QuanLy;
GO

-- Tạo Role Nhân viên
CREATE ROLE NhanVien;
GO

PRINT 'Created Roles: QuanLy and NhanVien';
GO

-- =============================================
-- SECTION 2: PHÂN QUYỀN CHO BẢNG (TABLES)
-- =============================================
-- LƯU Ý: Đây là phân quyền CẤP ĐỘ BẢNG (Table-Level Permissions)
-- Nếu muốn giới hạn user chỉ truy cập dữ liệu của chính mình (Row-Level Security),
-- vui lòng xem file: Database_RowLevelSecurity.sql
-- =============================================

-- ------------------------------------
-- QUYỀN CHO QUẢN LÝ (Full Access)
-- ------------------------------------

-- Role: Account Management - Quản lý có toàn quyền
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Account TO QuanLy;

-- Users: Quản lý có toàn quyền quản lý nhân viên
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO QuanLy;

-- Role: Xem và cập nhật thông tin vai trò
GRANT SELECT, INSERT, UPDATE ON dbo.Role TO QuanLy;

-- Attendance: Quản lý có toàn quyền xem và chỉnh sửa chấm công
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Attendance TO QuanLy;

-- Shift: Quản lý có quyền quản lý ca làm việc
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Shift TO QuanLy;

-- Salary: Quản lý có toàn quyền xem và tính lương
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Salary TO QuanLy;

-- AllowanceAndPenalty: Quản lý có quyền thêm/sửa thưởng phạt
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.AllowanceAndPenalty TO QuanLy;

-- ProcessErrorLog: Quản lý có quyền xem log lỗi
GRANT SELECT, INSERT ON dbo.ProcessErrorLog TO QuanLy;

PRINT 'Granted table permissions for QuanLy (Manager)';
GO

-- ------------------------------------
-- QUYỀN CHO NHÂN VIÊN (Limited Access)
-- ------------------------------------

-- Account: Nhân viên chỉ xem thông tin tài khoản của mình (qua VIEW)
-- Không cấp quyền trực tiếp trên bảng Account

-- Users: Nhân viên chỉ được xem thông tin cơ bản
GRANT SELECT ON dbo.Users TO NhanVien;

-- Role: Nhân viên chỉ được xem danh sách vai trò
GRANT SELECT ON dbo.Role TO NhanVien;

-- Attendance: Nhân viên có quyền xem chấm công của mình và thêm/sửa chấm công
GRANT SELECT, INSERT, UPDATE ON dbo.Attendance TO NhanVien;

-- Shift: Nhân viên chỉ được xem ca làm việc
GRANT SELECT ON dbo.Shift TO NhanVien;

-- Salary: Nhân viên chỉ được xem lương của mình (qua VIEW hoặc FUNCTION)
GRANT SELECT ON dbo.Salary TO NhanVien;

-- AllowanceAndPenalty: Nhân viên chỉ được xem thưởng phạt của mình
GRANT SELECT ON dbo.AllowanceAndPenalty TO NhanVien;

-- ProcessErrorLog: Nhân viên không có quyền xem log
-- (Không cấp quyền)

PRINT 'Granted table permissions for NhanVien (Employee)';
GO

-- =============================================
-- SECTION 3: PHÂN QUYỀN CHO VIEW
-- =============================================

-- ------------------------------------
-- QUYỀN CHO QUẢN LÝ
-- ------------------------------------

-- Quản lý có quyền xem tất cả các VIEW
GRANT SELECT ON dbo.v_AttendanceDetail TO QuanLy;
GRANT SELECT ON dbo.v_SalaryDetailForMonth TO QuanLy;
GRANT SELECT ON dbo.v_SalaryMonthDetail TO QuanLy;
GRANT SELECT ON dbo.v_UserWithRole TO QuanLy;

PRINT 'Granted view permissions for QuanLy';
GO

-- ------------------------------------
-- QUYỀN CHO NHÂN VIÊN
-- ------------------------------------

-- Nhân viên có quyền xem các VIEW (với row-level security nếu cần)
GRANT SELECT ON dbo.v_AttendanceDetail TO NhanVien;
GRANT SELECT ON dbo.v_SalaryDetailForMonth TO NhanVien;
GRANT SELECT ON dbo.v_SalaryMonthDetail TO NhanVien;
GRANT SELECT ON dbo.v_UserWithRole TO NhanVien;

PRINT 'Granted view permissions for NhanVien';
GO

-- =============================================
-- SECTION 4: PHÂN QUYỀN CHO STORED PROCEDURES
-- =============================================

-- ------------------------------------
-- QUYỀN CHO QUẢN LÝ
-- ------------------------------------

-- Quản lý có quyền thực thi tất cả các stored procedure
GRANT EXECUTE ON dbo.sp_AddUserManagement TO QuanLy;
GRANT EXECUTE ON dbo.sp_UpdateUserManagement TO QuanLy;
GRANT EXECUTE ON dbo.sp_MergeUserManagement TO QuanLy;
GRANT EXECUTE ON dbo.DisableUserById TO QuanLy;
GRANT EXECUTE ON dbo.sp_UpdateSalaryForUser TO QuanLy;
GRANT EXECUTE ON dbo.sp_UpdateSalaryForAllUsers TO QuanLy;
GRANT EXECUTE ON dbo.SearchUser TO QuanLy;

PRINT 'Granted stored procedure permissions for QuanLy';
GO

-- ------------------------------------
-- QUYỀN CHO NHÂN VIÊN
-- ------------------------------------

-- Nhân viên chỉ có quyền tìm kiếm thông tin người dùng
GRANT EXECUTE ON dbo.SearchUser TO NhanVien;

-- Nhân viên có thể cập nhật thông tin cá nhân (giới hạn qua application logic)
GRANT EXECUTE ON dbo.sp_UpdateUserManagement TO NhanVien;

-- Nhân viên KHÔNG có quyền:
-- - Thêm người dùng mới (sp_AddUserManagement)
-- - Xóa/Vô hiệu hóa người dùng (DisableUserById)
-- - Cập nhật lương (sp_UpdateSalaryForUser, sp_UpdateSalaryForAllUsers)
-- - Merge người dùng (sp_MergeUserManagement)

PRINT 'Granted stored procedure permissions for NhanVien';
GO

-- =============================================
-- SECTION 5: PHÂN QUYỀN CHO FUNCTIONS
-- =============================================

-- ------------------------------------
-- QUYỀN CHO QUẢN LÝ
-- ------------------------------------

-- Quản lý có quyền thực thi tất cả các function
GRANT SELECT ON dbo.fn_GetSalaryDetailForMonth TO QuanLy;
GRANT SELECT ON dbo.fn_GetAttendanceByUser TO QuanLy;
GRANT SELECT ON dbo.fn_GetAllowanceByUserForMonth TO QuanLy;
GRANT SELECT ON dbo.fn_GetPenaltyByUserForMonth TO QuanLy;

PRINT 'Granted function permissions for QuanLy';
GO

-- ------------------------------------
-- QUYỀN CHO NHÂN VIÊN
-- ------------------------------------

-- Nhân viên có quyền xem thông tin của mình qua function
GRANT SELECT ON dbo.fn_GetSalaryDetailForMonth TO NhanVien;
GRANT SELECT ON dbo.fn_GetAttendanceByUser TO NhanVien;
GRANT SELECT ON dbo.fn_GetAllowanceByUserForMonth TO NhanVien;
GRANT SELECT ON dbo.fn_GetPenaltyByUserForMonth TO NhanVien;

PRINT 'Granted function permissions for NhanVien';
GO

-- =============================================
-- SECTION 6: TẠO LOGIN VÀ USER MẪU (OPTIONAL)
-- =============================================

/*
-- Tạo login cho Quản lý (ví dụ)
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'ManagerLogin')
BEGIN
    CREATE LOGIN ManagerLogin WITH PASSWORD = 'Manager@123';
END
GO

-- Tạo user trong database và gán vào role Quản lý
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'ManagerUser')
BEGIN
    CREATE USER ManagerUser FOR LOGIN ManagerLogin;
END
GO

ALTER ROLE QuanLy ADD MEMBER ManagerUser;
GO

-- Tạo login cho Nhân viên (ví dụ)
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'EmployeeLogin')
BEGIN
    CREATE LOGIN EmployeeLogin WITH PASSWORD = 'Employee@123';
END
GO

-- Tạo user trong database và gán vào role Nhân viên
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'EmployeeUser')
BEGIN
    CREATE USER EmployeeUser FOR LOGIN EmployeeLogin;
END
GO

ALTER ROLE NhanVien ADD MEMBER EmployeeUser;
GO

PRINT 'Created sample users and assigned to roles';
*/

-- =============================================
-- SECTION 7: KIỂM TRA QUYỀN ĐÃ CẤP
-- =============================================

-- Xem quyền của Role QuanLy
PRINT '';
PRINT '========================================';
PRINT 'PERMISSIONS FOR ROLE: QuanLy (Manager)';
PRINT '========================================';
SELECT 
    dp.class_desc,
    OBJECT_NAME(dp.major_id) AS object_name,
    dp.permission_name,
    dp.state_desc
FROM sys.database_permissions dp
INNER JOIN sys.database_principals dpr ON dp.grantee_principal_id = dpr.principal_id
WHERE dpr.name = 'QuanLy'
ORDER BY dp.class_desc, object_name, dp.permission_name;
GO

-- Xem quyền của Role NhanVien
PRINT '';
PRINT '=========================================';
PRINT 'PERMISSIONS FOR ROLE: NhanVien (Employee)';
PRINT '=========================================';
SELECT 
    dp.class_desc,
    OBJECT_NAME(dp.major_id) AS object_name,
    dp.permission_name,
    dp.state_desc
FROM sys.database_permissions dp
INNER JOIN sys.database_principals dpr ON dp.grantee_principal_id = dpr.principal_id
WHERE dpr.name = 'NhanVien'
ORDER BY dp.class_desc, object_name, dp.permission_name;
GO

-- =============================================
-- SECTION 8: HƯỚNG DẪN SỬ DỤNG
-- =============================================

/*
===========================================
HƯỚNG DẪN SỬ DỤNG:
===========================================

1. TẠO USER VÀ GÁN ROLE:
   - Tạo login trên SQL Server instance
   - Tạo user trong database từ login
   - Gán user vào role tương ứng

   Ví dụ:
   -- Tạo login
   CREATE LOGIN NguyenVanA WITH PASSWORD = 'Password123!';
   
   -- Tạo user và gán role
   USE UserManager;
   CREATE USER NguyenVanA FOR LOGIN NguyenVanA;
   ALTER ROLE QuanLy ADD MEMBER NguyenVanA;

2. PHÂN QUYỀN CHI TIẾT:

   QUẢN LÝ (QuanLy):
   - Full access trên tất cả tables
   - Xem tất cả views
   - Execute tất cả stored procedures
   - Execute tất cả functions
   - Quản lý nhân viên, chấm công, lương, thưởng phạt

   NHÂN VIÊN (NhanVien):
   - Xem thông tin cơ bản (Users, Role, Shift)
   - Xem và cập nhật chấm công của mình
   - Xem lương, thưởng phạt của mình
   - Không thể thêm/xóa người dùng
   - Không thể cập nhật lương
   - Không thể xem log hệ thống

3. BẢO MẬT ROW-LEVEL (Tùy chọn):
   - Có thể implement Row-Level Security để nhân viên chỉ xem được dữ liệu của mình
   - Sử dụng SECURITY POLICY và PREDICATE FUNCTION

4. AUDIT:
   - Có thể bật SQL Server Audit để theo dõi hoạt động của users
   - Sử dụng bảng ProcessErrorLog để ghi log lỗi

===========================================
LƯU Ý BẢO MẬT:
===========================================

- Thay đổi mật khẩu mặc định sau khi tạo login
- Định kỳ review và audit quyền
- Sử dụng password policy mạnh
- Enable encryption cho kết nối database
- Backup database thường xuyên
- Monitor hoạt động bất thường

===========================================
*/

PRINT '';
PRINT '=============================================';
PRINT 'AUTHORIZATION SETUP COMPLETED SUCCESSFULLY!';
PRINT 'Database: UserManager';
PRINT 'Roles Created: QuanLy, NhanVien';
PRINT '=============================================';
GO
