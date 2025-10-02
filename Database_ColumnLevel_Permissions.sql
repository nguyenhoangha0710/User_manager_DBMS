-- =============================================
-- Script: Column-Level Permissions & Row-Level Security
-- Database: UserManager
-- Description: Phân quyền chi tiết đến CỘT và DÒNG cho Quản lý và Nhân viên
-- Created: 2025-10-01
-- =============================================

USE UserManager;
GO

-- =============================================
-- PHẦN I: GIẢI THÍCH VỀ TRIGGER VÀ QUYỀN
-- =============================================

/*
CÂU HỎI: Nếu không gán quyền UPDATE cho NhanVien trên bảng Salary,
         liệu trigger có cập nhật được total_hours không?

TRẢ LỜI: CÓ - Trigger sẽ hoạt động BÌNH THƯỜNG!

GIẢI THÍCH:
- Trigger thực thi với EXECUTION CONTEXT của người tạo trigger (thường là dbo)
- Trigger KHÔNG bị giới hạn bởi quyền của user đang thực thi câu lệnh
- Khi user check-out (UPDATE Attendance), trigger sẽ tự động UPDATE Salary
  ngay cả khi user KHÔNG có quyền UPDATE trực tiếp trên bảng Salary

VÍ DỤ:
  User: NhanVien (không có quyền UPDATE Salary)
  Action: UPDATE Attendance SET check_out = GETDATE() WHERE user_id = 5
  Trigger: Tự động UPDATE Salary SET total_hours = ... WHERE user_id = 5
  Result: ✅ THÀNH CÔNG - Trigger chạy với quyền của dbo

KẾT LUẬN:
  Bạn KHÔNG CẦN gán quyền UPDATE Salary cho NhanVien.
  Trigger sẽ tự động cập nhật total_hours mà không bị chặn.
*/

-- =============================================
-- PHẦN II: THU HỒI QUYỀN ĐÃ CẤP TRƯỚC ĐÓ
-- =============================================
-- Thu hồi quyền cũ để áp dụng quyền mới chi tiết hơn

PRINT 'Revoking old permissions...';
GO

-- Thu hồi quyền trên Attendance
REVOKE SELECT, INSERT, UPDATE, DELETE ON dbo.Attendance FROM QuanLy;
REVOKE SELECT, INSERT, UPDATE ON dbo.Attendance FROM NhanVien;

-- Thu hồi quyền trên Salary
REVOKE SELECT, INSERT, UPDATE, DELETE ON dbo.Salary FROM QuanLy;
REVOKE SELECT ON dbo.Salary FROM NhanVien;

PRINT 'Old permissions revoked successfully';
GO

-- =============================================
-- PHẦN III: PHÂN QUYỀN CHO BẢNG ATTENDANCE
-- =============================================

PRINT 'Setting up Attendance permissions...';
GO

-- ------------------------------------
-- A. QUYỀN SELECT VÀ INSERT (Cả 2 role)
-- ------------------------------------

-- Cả Quản lý và Nhân viên đều có quyền SELECT và INSERT (check-in)
GRANT SELECT, INSERT ON dbo.Attendance TO QuanLy;
GRANT SELECT, INSERT ON dbo.Attendance TO NhanVien;

-- ------------------------------------
-- B. QUYỀN UPDATE CHỈ TRÊN CỘT check_out
-- ------------------------------------

-- Quản lý: CHỈ được UPDATE cột check_out của chính mình
GRANT UPDATE (check_out) ON dbo.Attendance TO QuanLy;

-- Nhân viên: CHỈ được UPDATE cột check_out của chính mình
GRANT UPDATE (check_out) ON dbo.Attendance TO NhanVien;

-- LƯU Ý: 
-- - User chỉ có thể UPDATE cột check_out, không thể UPDATE các cột khác
-- - Row-Level Security (file Database_RowLevelSecurity.sql) sẽ đảm bảo
--   user chỉ UPDATE được dòng của chính mình (dựa vào user_id)

PRINT 'Granted Attendance permissions (SELECT, INSERT, UPDATE check_out only)';
GO

-- =============================================
-- PHẦN IV: PHÂN QUYỀN CHO BẢNG SALARY
-- =============================================

PRINT 'Setting up Salary permissions...';
GO

-- ------------------------------------
-- A. QUYỀN CHO QUẢN LÝ
-- ------------------------------------

-- Quản lý: SELECT tất cả cột
GRANT SELECT ON dbo.Salary TO QuanLy;

-- Quản lý: CHỈ được UPDATE cột final_salary (để tính lương)
GRANT UPDATE (final_salary) ON dbo.Salary TO QuanLy;

-- Quản lý: Có quyền INSERT (tạo bản ghi lương mới)
GRANT INSERT ON dbo.Salary TO QuanLy;

-- LƯU Ý:
-- - Quản lý KHÔNG có quyền UPDATE các cột: total_hours, total_Bonus, total_Penalty
-- - Các cột này chỉ được cập nhật qua trigger hoặc stored procedure của hệ thống

-- ------------------------------------
-- B. QUYỀN CHO NHÂN VIÊN
-- ------------------------------------

-- Nhân viên: CHỈ được SELECT (xem lương của mình)
GRANT SELECT ON dbo.Salary TO NhanVien;

-- Nhân viên: KHÔNG có quyền UPDATE, INSERT, DELETE
-- Lương của nhân viên chỉ được cập nhật qua:
--   1. Trigger (khi check-out) -> Cập nhật total_hours
--   2. Stored Procedure (do quản lý chạy) -> Tính final_salary

PRINT 'Granted Salary permissions';
PRINT '  - QuanLy: SELECT all, UPDATE (final_salary only), INSERT';
PRINT '  - NhanVien: SELECT only (no UPDATE permission)';
GO

-- =============================================
-- PHẦN V: PHÂN QUYỀN CHO BẢNG ALLOWANCEANDPENALTY
-- =============================================

PRINT 'Setting up AllowanceAndPenalty permissions...';
GO

-- Quản lý: Full access để thêm/sửa thưởng phạt
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.AllowanceAndPenalty TO QuanLy;

-- Nhân viên: Chỉ xem thưởng phạt của mình
GRANT SELECT ON dbo.AllowanceAndPenalty TO NhanVien;

-- LƯU Ý: Quản lý có thể sửa thưởng phạt của tất cả nhân viên
-- Nếu muốn giới hạn, cần áp dụng Row-Level Security (xem file Database_RowLevelSecurity.sql)

PRINT 'Granted AllowanceAndPenalty permissions';
GO

-- =============================================
-- PHẦN VI: TẠO VIEW ĐỂ AN TOÀN HƠN
-- =============================================

-- ------------------------------------
-- View: Chỉ hiển thị thông tin check-out
-- ------------------------------------

IF OBJECT_ID('dbo.v_AttendanceCheckOut', 'V') IS NOT NULL
    DROP VIEW dbo.v_AttendanceCheckOut;
GO

CREATE VIEW dbo.v_AttendanceCheckOut
AS
SELECT 
    attendance_id,
    user_id,
    work_date,
    check_in,
    check_out,
    hours_worked,
    shift_id
    -- Không hiển thị cột 'note' nếu muốn bảo mật
FROM dbo.Attendance;
GO

GRANT SELECT ON dbo.v_AttendanceCheckOut TO QuanLy, NhanVien;
GO

PRINT 'Created view v_AttendanceCheckOut';
GO

-- ------------------------------------
-- View: Hiển thị lương (chỉ đọc)
-- ------------------------------------

IF OBJECT_ID('dbo.v_SalaryReadOnly', 'V') IS NOT NULL
    DROP VIEW dbo.v_SalaryReadOnly;
GO

CREATE VIEW dbo.v_SalaryReadOnly
AS
SELECT 
    salary_id,
    user_id,
    month,
    year,
    total_hours,
    total_Bonus,
    total_Penalty,
    final_salary
FROM dbo.Salary;
GO

GRANT SELECT ON dbo.v_SalaryReadOnly TO QuanLy, NhanVien;
GO

PRINT 'Created view v_SalaryReadOnly';
GO

-- =============================================
-- PHẦN VII: TẠO STORED PROCEDURE AN TOÀN
-- =============================================

-- ------------------------------------
-- Procedure: Check-out an toàn
-- ------------------------------------

IF OBJECT_ID('dbo.sp_CheckOut', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CheckOut;
GO

CREATE PROCEDURE dbo.sp_CheckOut
    @attendance_id INT,
    @check_out_time DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Mặc định là thời gian hiện tại
    IF @check_out_time IS NULL
        SET @check_out_time = GETDATE();
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra attendance có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM dbo.Attendance WHERE attendance_id = @attendance_id)
        BEGIN
            RAISERROR('Attendance ID không tồn tại', 16, 1);
            ROLLBACK;
            RETURN;
        END
        
        -- Kiểm tra đã check-out chưa
        IF EXISTS (SELECT 1 FROM dbo.Attendance WHERE attendance_id = @attendance_id AND check_out IS NOT NULL)
        BEGIN
            RAISERROR('Đã check-out rồi, không thể check-out lại', 16, 1);
            ROLLBACK;
            RETURN;
        END
        
        -- Cập nhật check_out
        UPDATE dbo.Attendance
        SET check_out = @check_out_time
        WHERE attendance_id = @attendance_id;
        
        COMMIT;
        PRINT 'Check-out thành công tại: ' + CONVERT(VARCHAR, @check_out_time, 120);
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- Cấp quyền execute cho cả 2 role
GRANT EXECUTE ON dbo.sp_CheckOut TO QuanLy, NhanVien;
GO

PRINT 'Created stored procedure sp_CheckOut';
GO

-- ------------------------------------
-- Procedure: Quản lý tính lương
-- ------------------------------------

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
        
        -- Lấy thông tin từ bảng Salary
        SELECT 
            @total_hours = ISNULL(total_hours, 0),
            @total_bonus = ISNULL(total_Bonus, 0),
            @total_penalty = ISNULL(total_Penalty, 0)
        FROM dbo.Salary
        WHERE user_id = @user_id AND month = @month AND year = @year;
        
        IF @total_hours IS NULL
        BEGIN
            RAISERROR('Không tìm thấy dữ liệu lương cho user này', 16, 1);
            RETURN;
        END
        
        -- Lấy lương cơ bản từ role của user
        SELECT @hourly_rate = ISNULL(r.salary, 0) / 160 -- Giả sử 160 giờ/tháng
        FROM dbo.Users u
        INNER JOIN dbo.Role r ON u.role_id = r.role_id
        WHERE u.user_id = @user_id;
        
        -- Tính lương cuối cùng
        SET @calculated_salary = (@total_hours * @hourly_rate) + @total_bonus - @total_penalty;
        
        -- Cập nhật final_salary
        UPDATE dbo.Salary
        SET final_salary = @calculated_salary
        WHERE user_id = @user_id AND month = @month AND year = @year;
        
        PRINT 'Đã tính lương thành công cho user_id ' + CAST(@user_id AS VARCHAR);
        PRINT 'Lương cuối cùng: ' + CAST(@calculated_salary AS VARCHAR);
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- CHỈ Quản lý mới có quyền tính lương
GRANT EXECUTE ON dbo.sp_CalculateFinalSalary TO QuanLy;
GO

PRINT 'Created stored procedure sp_CalculateFinalSalary (QuanLy only)';
GO

-- =============================================
-- PHẦN VIII: XEM QUYỀN ĐÃ CẤP
-- =============================================

PRINT '';
PRINT '========================================';
PRINT 'COLUMN-LEVEL PERMISSIONS SUMMARY';
PRINT '========================================';

-- Xem quyền trên Attendance
SELECT 
    'Attendance' AS [Table],
    dp.grantee_principal_id,
    USER_NAME(dp.grantee_principal_id) AS [Role/User],
    dp.permission_name AS [Permission],
    c.name AS [Column],
    dp.state_desc AS [State]
FROM sys.database_permissions dp
LEFT JOIN sys.columns c ON dp.major_id = c.object_id AND dp.minor_id = c.column_id
WHERE dp.major_id = OBJECT_ID('dbo.Attendance')
    AND USER_NAME(dp.grantee_principal_id) IN ('QuanLy', 'NhanVien')
ORDER BY USER_NAME(dp.grantee_principal_id), dp.permission_name;

PRINT '';

-- Xem quyền trên Salary
SELECT 
    'Salary' AS [Table],
    dp.grantee_principal_id,
    USER_NAME(dp.grantee_principal_id) AS [Role/User],
    dp.permission_name AS [Permission],
    c.name AS [Column],
    dp.state_desc AS [State]
FROM sys.database_permissions dp
LEFT JOIN sys.columns c ON dp.major_id = c.object_id AND dp.minor_id = c.column_id
WHERE dp.major_id = OBJECT_ID('dbo.Salary')
    AND USER_NAME(dp.grantee_principal_id) IN ('QuanLy', 'NhanVien')
ORDER BY USER_NAME(dp.grantee_principal_id), dp.permission_name;

GO

-- =============================================
-- PHẦN IX: VÍ DỤ TRIGGER CẬP NHẬT TOTAL_HOURS
-- =============================================

-- Đây là ví dụ trigger tự động cập nhật total_hours khi check-out
-- Trigger này chạy với quyền của dbo, không bị giới hạn bởi quyền user

IF OBJECT_ID('dbo.trg_UpdateTotalHours', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_UpdateTotalHours;
GO

CREATE TRIGGER dbo.trg_UpdateTotalHours
ON dbo.Attendance
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Chỉ xử lý khi check_out được cập nhật
    IF UPDATE(check_out)
    BEGIN
        -- Cập nhật total_hours trong bảng Salary
        UPDATE s
        SET s.total_hours = ISNULL(s.total_hours, 0) + DATEDIFF(HOUR, i.check_in, i.check_out)
        FROM dbo.Salary s
        INNER JOIN inserted i ON s.user_id = i.user_id 
            AND s.month = MONTH(i.work_date) 
            AND s.year = YEAR(i.work_date)
        WHERE i.check_out IS NOT NULL;
        
        -- Nếu chưa có bản ghi Salary cho tháng này, tạo mới
        INSERT INTO dbo.Salary (user_id, month, year, total_hours, total_Bonus, total_Penalty)
        SELECT 
            i.user_id,
            MONTH(i.work_date),
            YEAR(i.work_date),
            DATEDIFF(HOUR, i.check_in, i.check_out),
            0,
            0
        FROM inserted i
        WHERE i.check_out IS NOT NULL
            AND NOT EXISTS (
                SELECT 1 FROM dbo.Salary s
                WHERE s.user_id = i.user_id
                    AND s.month = MONTH(i.work_date)
                    AND s.year = YEAR(i.work_date)
            );
    END
END
GO

PRINT 'Created trigger trg_UpdateTotalHours';
PRINT 'Trigger sẽ tự động cập nhật total_hours khi user check-out';
PRINT 'User KHÔNG CẦN quyền UPDATE trên Salary, trigger chạy với quyền dbo';
GO

-- =============================================
-- PHẦN X: HƯỚNG DẪN SỬ DỤNG
-- =============================================

/*
===========================================
HƯỚNG DẪN SỬ DỤNG:
===========================================

1. PHÂN QUYỀN ĐÃ THIẾT LẬP:

   A. BẢNG ATTENDANCE:
      ┌─────────────┬─────────┬──────────┬─────────────────────┐
      │ Role        │ SELECT  │ INSERT   │ UPDATE              │
      ├─────────────┼─────────┼──────────┼─────────────────────┤
      │ QuanLy      │ ✅      │ ✅       │ check_out only ✅   │
      │ NhanVien    │ ✅      │ ✅       │ check_out only ✅   │
      └─────────────┴─────────┴──────────┴─────────────────────┘
      
      + Row-Level Security: User chỉ UPDATE được dòng của mình
      + Column-Level: Chỉ UPDATE được cột check_out
   
   B. BẢNG SALARY:
      ┌─────────────┬─────────┬──────────┬──────────────────────┐
      │ Role        │ SELECT  │ INSERT   │ UPDATE               │
      ├─────────────┼─────────┼──────────┼──────────────────────┤
      │ QuanLy      │ ✅      │ ✅       │ final_salary only ✅ │
      │ NhanVien    │ ✅      │ ❌       │ ❌                   │
      └─────────────┴─────────┴──────────┴──────────────────────┘
      
      + Quản lý chỉ UPDATE được cột final_salary
      + total_hours, total_Bonus, total_Penalty được cập nhật qua trigger/SP

2. WORKFLOW CHECK-IN / CHECK-OUT:

   A. CHECK-IN (Tất cả users):
      
      -- Cách 1: Insert trực tiếp
      INSERT INTO dbo.Attendance (user_id, work_date, check_in, shift_id)
      VALUES (5, '2025-10-01', GETDATE(), 1);
      
      -- Cách 2: Dùng stored procedure (khuyến nghị)
      -- (Bạn có thể tạo sp_CheckIn tương tự)
   
   B. CHECK-OUT (Tất cả users):
      
      -- Cách 1: Update trực tiếp (chỉ được update check_out)
      UPDATE dbo.Attendance
      SET check_out = GETDATE()
      WHERE attendance_id = 123;
      ✅ OK - User có quyền UPDATE check_out
      
      UPDATE dbo.Attendance
      SET note = 'Changed'  -- ❌ FAIL - Không có quyền UPDATE note
      WHERE attendance_id = 123;
      
      -- Cách 2: Dùng stored procedure (khuyến nghị)
      EXEC dbo.sp_CheckOut @attendance_id = 123;
      
   C. KHI CHECK-OUT:
      ✅ Trigger tự động chạy
      ✅ Trigger UPDATE total_hours trong bảng Salary
      ✅ User KHÔNG CẦN quyền UPDATE Salary
      ✅ Trigger chạy với quyền của dbo (owner)

3. WORKFLOW TÍNH LƯƠNG (Chỉ Quản lý):

   -- Quản lý chạy procedure để tính lương
   EXEC dbo.sp_CalculateFinalSalary 
       @user_id = 5,
       @month = 10,
       @year = 2025;
   
   -- Hoặc update trực tiếp (không khuyến nghị)
   UPDATE dbo.Salary
   SET final_salary = 15000000
   WHERE user_id = 5 AND month = 10 AND year = 2025;
   ✅ OK - QuanLy có quyền UPDATE final_salary
   
   -- Thử update cột khác
   UPDATE dbo.Salary
   SET total_hours = 200  -- ❌ FAIL - Không có quyền UPDATE total_hours
   WHERE user_id = 5;

4. KIỂM TRA QUYỀN:

   -- Test với user cụ thể
   EXECUTE AS USER = 'NhanVien_NguyenVanA';
   
   -- Test update check_out (OK)
   UPDATE dbo.Attendance 
   SET check_out = GETDATE() 
   WHERE attendance_id = 1;
   
   -- Test update note (FAIL)
   UPDATE dbo.Attendance 
   SET note = 'Test' 
   WHERE attendance_id = 1;
   -- Error: The UPDATE permission was denied on the column 'note'
   
   REVERT;

5. KẾT HỢP VỚI ROW-LEVEL SECURITY:

   Để đảm bảo user chỉ UPDATE được dòng của chính mình:
   
   1. Chạy file này (Database_ColumnLevel_Permissions.sql) trước
   2. Sau đó chạy file Database_RowLevelSecurity.sql
   3. Thiết lập UserLoginMapping cho mỗi user
   
   Khi đó:
   ✅ Column-Level: Chỉ UPDATE được cột check_out
   ✅ Row-Level: Chỉ UPDATE được dòng của mình

6. VỀ TRIGGER VÀ QUYỀN:

   Q: Nhân viên không có quyền UPDATE Salary, trigger có hoạt động không?
   A: CÓ - Trigger hoạt động bình thường!
   
   Lý do:
   - Trigger chạy trong EXECUTION CONTEXT của owner (dbo)
   - Trigger KHÔNG bị giới hạn bởi quyền của user
   - Khi user UPDATE Attendance.check_out, trigger tự động UPDATE Salary
   
   Ví dụ:
   User: NhanVien (không có quyền UPDATE Salary)
   Action: UPDATE Attendance SET check_out = GETDATE() WHERE attendance_id = 1
   Trigger: Tự động UPDATE Salary SET total_hours = ...
   Result: ✅ THÀNH CÔNG

===========================================
LƯU Ý QUAN TRỌNG:
===========================================

1. COLUMN-LEVEL PERMISSIONS:
   - Ưu tiên cao hơn table-level permissions
   - Nếu có GRANT UPDATE (column), không thể UPDATE cột khác
   - Phải list rõ từng cột trong GRANT statement

2. TRIGGER EXECUTION:
   - Trigger chạy với quyền của owner
   - Nên tạo trigger bằng account có đủ quyền (sa hoặc dbo)
   - Trigger bypass Row-Level Security và Column-Level Security

3. STORED PROCEDURES:
   - SP chạy với quyền của owner (nếu dùng EXECUTE AS OWNER)
   - Có thể bypass permissions để thực hiện logic phức tạp
   - Nên dùng SP thay vì cho user quyền trực tiếp

4. BẢO MẬT:
   - Không cho user quyền trực tiếp trên sensitive columns
   - Dùng SP để kiểm soát logic
   - Log tất cả các thao tác quan trọng
   - Sử dụng transactions để đảm bảo data integrity

5. TESTING:
   - Luôn test với EXECUTE AS USER trước khi deploy
   - Kiểm tra từng permission riêng lẻ
   - Test cả positive và negative cases

===========================================
*/

PRINT '';
PRINT '=============================================';
PRINT 'COLUMN-LEVEL PERMISSIONS SETUP COMPLETED!';
PRINT '';
PRINT 'Attendance: Chỉ UPDATE check_out (cả QuanLy và NhanVien)';
PRINT 'Salary: QuanLy chỉ UPDATE final_salary';
PRINT 'Trigger: Tự động UPDATE total_hours (không cần quyền user)';
PRINT '';
PRINT 'Next: Chạy Database_RowLevelSecurity.sql để giới hạn theo user_id';
PRINT '=============================================';
GO
