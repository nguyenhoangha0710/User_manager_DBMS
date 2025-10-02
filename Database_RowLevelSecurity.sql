-- =============================================
-- Script: Row-Level Security (RLS) Implementation
-- Database: UserManager
-- Description: Giới hạn quyền truy cập dữ liệu dựa trên user_id
--              User chỉ có thể xem/sửa dữ liệu của chính mình
-- Created: 2025-10-01
-- =============================================

USE UserManager;
GO

-- =============================================
-- SECTION 1: TẠO SCHEMA CHO SECURITY
-- =============================================

-- Tạo schema riêng cho security functions
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Security')
BEGIN
    EXEC('CREATE SCHEMA Security');
    PRINT 'Created Security schema';
END
GO

-- =============================================
-- SECTION 2: TẠO BẢNG ÁNH XẠ USER_ID VỚI DATABASE USER
-- =============================================

-- Bảng này lưu mapping giữa database user và user_id trong bảng Users
IF OBJECT_ID('dbo.UserLoginMapping', 'U') IS NOT NULL
    DROP TABLE dbo.UserLoginMapping;
GO

CREATE TABLE dbo.UserLoginMapping
(
    mapping_id INT IDENTITY(1,1) PRIMARY KEY,
    database_username NVARCHAR(128) NOT NULL UNIQUE, -- Tên user trong database
    user_id INT NOT NULL, -- ID trong bảng Users
    is_manager BIT NOT NULL DEFAULT 0, -- Có phải là quản lý không
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES dbo.Users(user_id)
);
GO

PRINT 'Created UserLoginMapping table';
GO

-- =============================================
-- SECTION 3: TẠO PREDICATE FUNCTIONS
-- =============================================

-- ------------------------------------
-- Function: Kiểm tra quyền truy cập cho Attendance
-- User chỉ được truy cập attendance của chính mình
-- Quản lý được truy cập tất cả
-- ------------------------------------

IF OBJECT_ID('Security.fn_AttendanceAccessPredicate', 'IF') IS NOT NULL
    DROP FUNCTION Security.fn_AttendanceAccessPredicate;
GO

CREATE FUNCTION Security.fn_AttendanceAccessPredicate(@user_id INT)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
    SELECT 1 AS fn_AttendanceAccessPredicate_result
    WHERE 
        -- Cho phép truy cập nếu:
        -- 1. User là quản lý (is_manager = 1)
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME()
            AND is_manager = 1
        )
        OR
        -- 2. User đang truy cập dữ liệu của chính mình
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME()
            AND user_id = @user_id
        );
GO

PRINT 'Created fn_AttendanceAccessPredicate function';
GO

-- ------------------------------------
-- Function: Kiểm tra quyền truy cập cho Salary
-- ------------------------------------

IF OBJECT_ID('Security.fn_SalaryAccessPredicate', 'IF') IS NOT NULL
    DROP FUNCTION Security.fn_SalaryAccessPredicate;
GO

CREATE FUNCTION Security.fn_SalaryAccessPredicate(@user_id INT)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
    SELECT 1 AS fn_SalaryAccessPredicate_result
    WHERE 
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME()
            AND is_manager = 1
        )
        OR
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME()
            AND user_id = @user_id
        );
GO

PRINT 'Created fn_SalaryAccessPredicate function';
GO

-- ------------------------------------
-- Function: Kiểm tra quyền truy cập cho AllowanceAndPenalty
-- ------------------------------------

IF OBJECT_ID('Security.fn_AllowanceAndPenaltyAccessPredicate', 'IF') IS NOT NULL
    DROP FUNCTION Security.fn_AllowanceAndPenaltyAccessPredicate;
GO

CREATE FUNCTION Security.fn_AllowanceAndPenaltyAccessPredicate(@user_id INT)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
    SELECT 1 AS fn_AllowanceAndPenaltyAccessPredicate_result
    WHERE 
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME()
            AND is_manager = 1
        )
        OR
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME()
            AND user_id = @user_id
        );
GO

PRINT 'Created fn_AllowanceAndPenaltyAccessPredicate function';
GO

-- ------------------------------------
-- Function: Kiểm tra quyền truy cập cho Account
-- ------------------------------------

IF OBJECT_ID('Security.fn_AccountAccessPredicate', 'IF') IS NOT NULL
    DROP FUNCTION Security.fn_AccountAccessPredicate;
GO

CREATE FUNCTION Security.fn_AccountAccessPredicate(@user_id INT)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
    SELECT 1 AS fn_AccountAccessPredicate_result
    WHERE 
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME()
            AND is_manager = 1
        )
        OR
        EXISTS (
            SELECT 1 FROM dbo.UserLoginMapping
            WHERE database_username = USER_NAME()
            AND user_id = @user_id
        );
GO

PRINT 'Created fn_AccountAccessPredicate function';
GO

-- =============================================
-- SECTION 4: TẠO SECURITY POLICIES
-- =============================================

-- ------------------------------------
-- Security Policy cho Attendance
-- ------------------------------------

-- Xóa policy cũ nếu tồn tại
IF EXISTS (SELECT * FROM sys.security_policies WHERE name = 'AttendanceAccessPolicy')
    DROP SECURITY POLICY Security.AttendanceAccessPolicy;
GO

CREATE SECURITY POLICY Security.AttendanceAccessPolicy
ADD FILTER PREDICATE Security.fn_AttendanceAccessPredicate(user_id)
    ON dbo.Attendance,
ADD BLOCK PREDICATE Security.fn_AttendanceAccessPredicate(user_id)
    ON dbo.Attendance AFTER INSERT,
ADD BLOCK PREDICATE Security.fn_AttendanceAccessPredicate(user_id)
    ON dbo.Attendance AFTER UPDATE
WITH (STATE = ON);
GO

PRINT 'Created AttendanceAccessPolicy';
GO

-- ------------------------------------
-- Security Policy cho Salary
-- ------------------------------------

IF EXISTS (SELECT * FROM sys.security_policies WHERE name = 'SalaryAccessPolicy')
    DROP SECURITY POLICY Security.SalaryAccessPolicy;
GO

CREATE SECURITY POLICY Security.SalaryAccessPolicy
ADD FILTER PREDICATE Security.fn_SalaryAccessPredicate(user_id)
    ON dbo.Salary,
ADD BLOCK PREDICATE Security.fn_SalaryAccessPredicate(user_id)
    ON dbo.Salary AFTER INSERT,
ADD BLOCK PREDICATE Security.fn_SalaryAccessPredicate(user_id)
    ON dbo.Salary AFTER UPDATE
WITH (STATE = ON);
GO

PRINT 'Created SalaryAccessPolicy';
GO

-- ------------------------------------
-- Security Policy cho AllowanceAndPenalty
-- ------------------------------------

IF EXISTS (SELECT * FROM sys.security_policies WHERE name = 'AllowanceAndPenaltyAccessPolicy')
    DROP SECURITY POLICY Security.AllowanceAndPenaltyAccessPolicy;
GO

CREATE SECURITY POLICY Security.AllowanceAndPenaltyAccessPolicy
ADD FILTER PREDICATE Security.fn_AllowanceAndPenaltyAccessPredicate(user_id)
    ON dbo.AllowanceAndPenalty,
ADD BLOCK PREDICATE Security.fn_AllowanceAndPenaltyAccessPredicate(user_id)
    ON dbo.AllowanceAndPenalty AFTER INSERT,
ADD BLOCK PREDICATE Security.fn_AllowanceAndPenaltyAccessPredicate(user_id)
    ON dbo.AllowanceAndPenalty AFTER UPDATE
WITH (STATE = ON);
GO

PRINT 'Created AllowanceAndPenaltyAccessPolicy';
GO

-- ------------------------------------
-- Security Policy cho Account
-- ------------------------------------

IF EXISTS (SELECT * FROM sys.security_policies WHERE name = 'AccountAccessPolicy')
    DROP SECURITY POLICY Security.AccountAccessPolicy;
GO

CREATE SECURITY POLICY Security.AccountAccessPolicy
ADD FILTER PREDICATE Security.fn_AccountAccessPredicate(user_id)
    ON dbo.Account,
ADD BLOCK PREDICATE Security.fn_AccountAccessPredicate(user_id)
    ON dbo.Account AFTER INSERT,
ADD BLOCK PREDICATE Security.fn_AccountAccessPredicate(user_id)
    ON dbo.Account AFTER UPDATE
WITH (STATE = ON);
GO

PRINT 'Created AccountAccessPolicy';
GO

-- =============================================
-- SECTION 5: VÍ DỤ THIẾT LẬP USER MAPPING
-- =============================================

/*
-- Ví dụ: Tạo user và gán vào mapping

-- 1. Tạo login và user cho nhân viên (user_id = 1)
CREATE LOGIN employee_nguyen WITH PASSWORD = 'Password123!';
CREATE USER employee_nguyen FOR LOGIN employee_nguyen;
ALTER ROLE NhanVien ADD MEMBER employee_nguyen;

-- 2. Thêm mapping cho nhân viên này
INSERT INTO dbo.UserLoginMapping (database_username, user_id, is_manager)
VALUES ('employee_nguyen', 1, 0); -- user_id = 1, không phải quản lý

-- 3. Tạo login và user cho quản lý (user_id = 2)
CREATE LOGIN manager_tran WITH PASSWORD = 'Password123!';
CREATE USER manager_tran FOR LOGIN manager_tran;
ALTER ROLE QuanLy ADD MEMBER manager_tran;

-- 4. Thêm mapping cho quản lý này
INSERT INTO dbo.UserLoginMapping (database_username, user_id, is_manager)
VALUES ('manager_tran', 2, 1); -- user_id = 2, là quản lý
*/

-- =============================================
-- SECTION 6: KIỂM TRA VÀ TEST
-- =============================================

-- Xem tất cả security policies đã tạo
SELECT 
    sp.name AS policy_name,
    sp.is_enabled,
    o.name AS table_name,
    spc.predicate_type_desc,
    pr.name AS predicate_function
FROM sys.security_policies sp
INNER JOIN sys.security_predicates spc ON sp.object_id = spc.object_id
INNER JOIN sys.objects o ON spc.target_object_id = o.object_id
INNER JOIN sys.objects pr ON spc.predicate_object_id = pr.object_id
WHERE sp.name LIKE '%AccessPolicy'
ORDER BY sp.name, o.name;
GO

-- =============================================
-- SECTION 7: PROCEDURE QUẢN LÝ USER MAPPING
-- =============================================

-- Procedure: Thêm user mapping
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
        -- Kiểm tra user_id có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE user_id = @user_id)
        BEGIN
            RAISERROR('User ID không tồn tại trong bảng Users', 16, 1);
            RETURN;
        END
        
        -- Thêm mapping
        INSERT INTO dbo.UserLoginMapping (database_username, user_id, is_manager)
        VALUES (@database_username, @user_id, @is_manager);
        
        PRINT 'Đã thêm mapping thành công cho user: ' + @database_username;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- Procedure: Cập nhật user mapping
IF OBJECT_ID('dbo.sp_UpdateUserMapping', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateUserMapping;
GO

CREATE PROCEDURE dbo.sp_UpdateUserMapping
    @database_username NVARCHAR(128),
    @user_id INT = NULL,
    @is_manager BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.UserLoginMapping WHERE database_username = @database_username)
        BEGIN
            RAISERROR('User mapping không tồn tại', 16, 1);
            RETURN;
        END
        
        -- Cập nhật user_id nếu được cung cấp
        IF @user_id IS NOT NULL
        BEGIN
            UPDATE dbo.UserLoginMapping
            SET user_id = @user_id
            WHERE database_username = @database_username;
        END
        
        -- Cập nhật is_manager nếu được cung cấp
        IF @is_manager IS NOT NULL
        BEGIN
            UPDATE dbo.UserLoginMapping
            SET is_manager = @is_manager
            WHERE database_username = @database_username;
        END
        
        PRINT 'Đã cập nhật mapping thành công cho user: ' + @database_username;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- Procedure: Xóa user mapping
IF OBJECT_ID('dbo.sp_DeleteUserMapping', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DeleteUserMapping;
GO

CREATE PROCEDURE dbo.sp_DeleteUserMapping
    @database_username NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DELETE FROM dbo.UserLoginMapping
        WHERE database_username = @database_username;
        
        IF @@ROWCOUNT > 0
            PRINT 'Đã xóa mapping thành công cho user: ' + @database_username;
        ELSE
            PRINT 'Không tìm thấy mapping cho user: ' + @database_username;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

PRINT 'Created user mapping management procedures';
GO

-- =============================================
-- SECTION 8: TẮT/BẬT SECURITY POLICIES
-- =============================================

-- Procedure: Bật/tắt tất cả security policies
IF OBJECT_ID('dbo.sp_ToggleSecurityPolicies', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ToggleSecurityPolicies;
GO

CREATE PROCEDURE dbo.sp_ToggleSecurityPolicies
    @enable BIT = 1 -- 1: Bật, 0: Tắt
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @state NVARCHAR(10) = CASE WHEN @enable = 1 THEN 'ON' ELSE 'OFF' END;
    
    -- Attendance
    EXEC('ALTER SECURITY POLICY Security.AttendanceAccessPolicy WITH (STATE = ' + @state + ')');
    
    -- Salary
    EXEC('ALTER SECURITY POLICY Security.SalaryAccessPolicy WITH (STATE = ' + @state + ')');
    
    -- AllowanceAndPenalty
    EXEC('ALTER SECURITY POLICY Security.AllowanceAndPenaltyAccessPolicy WITH (STATE = ' + @state + ')');
    
    -- Account
    EXEC('ALTER SECURITY POLICY Security.AccountAccessPolicy WITH (STATE = ' + @state + ')');
    
    PRINT 'Đã ' + CASE WHEN @enable = 1 THEN 'BẬT' ELSE 'TẮT' END + ' tất cả security policies';
END
GO

-- =============================================
-- SECTION 9: HƯỚNG DẪN SỬ DỤNG VÀ TEST
-- =============================================

/*
===========================================
HƯỚNG DẪN SỬ DỤNG ROW-LEVEL SECURITY:
===========================================

1. THIẾT LẬP BAN ĐẦU:

   Bước 1: Tạo login và user trong SQL Server
   ----------------------------------------
   -- Tạo login cho nhân viên Nguyễn Văn A (giả sử user_id = 5 trong bảng Users)
   CREATE LOGIN NguyenVanA WITH PASSWORD = 'StrongPass123!';
   
   -- Tạo user trong database và gán role
   USE UserManager;
   CREATE USER NguyenVanA FOR LOGIN NguyenVanA;
   ALTER ROLE NhanVien ADD MEMBER NguyenVanA;
   
   Bước 2: Thêm mapping cho user này
   ----------------------------------------
   EXEC dbo.sp_AddUserMapping 
       @database_username = 'NguyenVanA',
       @user_id = 5,
       @is_manager = 0;

2. CÁCH HOẠT ĐỘNG:

   Sau khi setup, khi user 'NguyenVanA' đăng nhập và query:
   
   -- Query này chỉ trả về attendance của user_id = 5
   SELECT * FROM dbo.Attendance;
   
   -- User này KHÔNG THỂ update attendance của người khác
   UPDATE dbo.Attendance 
   SET note = 'Test'
   WHERE user_id = 10; -- Sẽ BỊ CHẶN vì user_id khác với user hiện tại
   
   -- User này CỨ THỂ update attendance của chính mình
   UPDATE dbo.Attendance 
   SET note = 'My note'
   WHERE user_id = 5; -- OK vì đúng user_id của mình

3. THIẾT LẬP CHO QUẢN LÝ:

   -- Tạo login cho quản lý
   CREATE LOGIN QuanLyTran WITH PASSWORD = 'StrongPass123!';
   
   -- Tạo user và gán role Quản lý
   CREATE USER QuanLyTran FOR LOGIN QuanLyTran;
   ALTER ROLE QuanLy ADD MEMBER QuanLyTran;
   
   -- Thêm mapping với is_manager = 1
   EXEC dbo.sp_AddUserMapping 
       @database_username = 'QuanLyTran',
       @user_id = 3,
       @is_manager = 1;
   
   Quản lý sẽ có quyền truy cập TẤT CẢ dữ liệu (không bị giới hạn)

4. QUẢN LÝ MAPPING:

   -- Xem tất cả mapping
   SELECT * FROM dbo.UserLoginMapping;
   
   -- Cập nhật mapping
   EXEC dbo.sp_UpdateUserMapping 
       @database_username = 'NguyenVanA',
       @is_manager = 1; -- Nâng lên làm quản lý
   
   -- Xóa mapping
   EXEC dbo.sp_DeleteUserMapping @database_username = 'NguyenVanA';

5. QUẢN LÝ SECURITY POLICIES:

   -- Tắt tất cả policies (khi cần maintenance)
   EXEC dbo.sp_ToggleSecurityPolicies @enable = 0;
   
   -- Bật lại policies
   EXEC dbo.sp_ToggleSecurityPolicies @enable = 1;
   
   -- Tắt từng policy riêng lẻ
   ALTER SECURITY POLICY Security.AttendanceAccessPolicy WITH (STATE = OFF);

6. TEST HOẠT ĐỘNG:

   -- Login như user NguyenVanA và chạy:
   EXECUTE AS USER = 'NguyenVanA';
   
   SELECT * FROM dbo.Attendance; -- Chỉ thấy attendance của user_id = 5
   SELECT * FROM dbo.Salary;     -- Chỉ thấy salary của user_id = 5
   
   -- Thử update dữ liệu của người khác (sẽ fail)
   UPDATE dbo.Attendance SET note = 'Test' WHERE user_id = 10;
   -- Error: The attempted operation failed because the target object 
   -- 'UserManager.dbo.Attendance' has a block predicate that conflicts with this operation
   
   REVERT; -- Quay lại user ban đầu

===========================================
CÁC LOẠI PREDICATE:
===========================================

1. FILTER PREDICATE:
   - Tự động lọc kết quả SELECT
   - User chỉ thấy rows mà họ có quyền

2. BLOCK PREDICATE:
   - Ngăn chặn INSERT/UPDATE/DELETE
   - AFTER INSERT: Chặn insert nếu user không có quyền trên row đó
   - AFTER UPDATE: Chặn update nếu user không có quyền trên row đó
   - BEFORE UPDATE: Kiểm tra trước khi update
   - BEFORE DELETE: Kiểm tra trước khi delete

===========================================
LƯU Ý QUAN TRỌNG:
===========================================

1. Security policies KHÔNG ẢNH HƯỞNG đến:
   - User có quyền CONTROL trên table
   - User có quyền ALTER ANY SECURITY POLICY
   - User là db_owner

2. Performance:
   - RLS có thể ảnh hưởng performance
   - Nên tạo index trên cột user_id
   - Monitor query performance sau khi enable RLS

3. Debugging:
   - Nếu policy không hoạt động, kiểm tra:
     + UserLoginMapping có đúng không
     + Policy có enabled không
     + Function predicate có lỗi không
   
4. Application Integration:
   - Application cần sử dụng connection string với user tương ứng
   - Không dùng shared connection cho tất cả users
   - Mỗi user phải login bằng account riêng của họ

===========================================
*/

PRINT '';
PRINT '=============================================';
PRINT 'ROW-LEVEL SECURITY SETUP COMPLETED!';
PRINT 'Database: UserManager';
PRINT 'Policies Created: 4 (Attendance, Salary, AllowanceAndPenalty, Account)';
PRINT 'Next Steps:';
PRINT '  1. Tạo login và user cho nhân viên';
PRINT '  2. Gán user vào role (QuanLy hoặc NhanVien)';
PRINT '  3. Thêm mapping vào bảng UserLoginMapping';
PRINT '  4. Test với EXECUTE AS USER';
PRINT '=============================================';
GO
