-- Tạo Role Quản lý
CREATE ROLE QuanLy;
GO

-- Tạo Role Nhân viên
CREATE ROLE NhanVien;
GO

-- ===================================================================
-- I. GÁN QUYỀN CHO ROLE `QuanLy` (Quản Lý)
-- Mô tả: Có quyền quản lý nhân viên, chấm công, tính lương và xem báo cáo.
-- ===================================================================

-- 1. Quyền trên các Bảng (Table)
-- Toàn quyền quản lý thông tin nhân viên, tài khoản, vai trò và ca làm việc
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Account TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Role TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Shift TO QuanLy;

-- Quyền quản lý chấm công, thưởng phạt
GRANT SELECT ON dbo.Attendance TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.AllowanceAndPenalty TO QuanLy;

-- Quyền quản lý lương và các giao dịch thanh toán
GRANT SELECT, INSERT ON dbo.Salary TO QuanLy;
GRANT SELECT ON dbo.SalaryPayment TO QuanLy;

-- 2. Quyền trên các View
-- Cho phép xem tất cả các view để phục vụ báo cáo và quản lý
GRANT SELECT ON dbo.v_UserWithRole TO QuanLy;
GRANT SELECT ON dbo.v_AttendanceDetail TO QuanLy;
GRANT SELECT ON dbo.v_SalaryMonthDetail TO QuanLy;

-- 3. Quyền trên các Stored Procedure
-- Cho phép thực thi tất cả các procedure nghiệp vụ
GRANT EXECUTE ON dbo.sp_UserCheckIn TO QuanLy;
GRANT EXECUTE ON dbo.sp_UserCheckOut TO QuanLy;
GRANT EXECUTE ON dbo.sp_UpdateSalaryForUser TO QuanLy;
GRANT EXECUTE ON dbo.sp_UpdateSalaryForAllUsers TO QuanLy;
GRANT EXECUTE ON dbo.sp_ProcessPayment TO QuanLy;
GRANT EXECUTE ON dbo.sp_PayAllEmployees TO QuanLy;
GRANT EXECUTE ON dbo.sp_GetLatestRemainingSalary TO QuanLy;
GRANT EXECUTE ON dbo.sp_GenerateMonthlySalaryRecords TO QuanLy;
GRANT EXECUTE ON dbo.SearchUser TO QuanLy;

-- 4. Quyền trên các Function
-- Cho phép sử dụng tất cả các function để lấy dữ liệu tính toán và báo cáo
GRANT SELECT,EXECUTE  ON dbo.fn_SearchUser TO QuanLy;
GRANT SELECT,EXECUTE  ON dbo.fn_GetTotalPaidAmount TO QuanLy;
GRANT SELECT,EXECUTE  ON dbo.fn_GetRemainingSalary TO QuanLy;
GRANT SELECT,EXECUTE  ON dbo.fn_GetUserCountByRoleName TO QuanLy;
GRANT SELECT,EXECUTE  ON dbo.fn_GetUserCountByActiveRole TO QuanLy;
GRANT SELECT,EXECUTE  ON dbo.fn_GetLatestFinalSalary TO QuanLy;
GRANT SELECT,EXECUTE  ON dbo.fn_GetSalaryForPeriod TO QuanLy;
GRANT SELECT,EXECUTE  ON dbo.fn_GetMonthlySalaryReport TO QuanLy;
GRANT SELECT,EXECUTE  ON dbo.fn_TotalHoursWorked TO QuanLy;


-- ===================================================================
-- II. GÁN QUYỀN CHO ROLE `NhanVien` (Nhân Viên)
-- Mô tả: Chỉ có quyền thực hiện các tác vụ cá nhân (chấm công, xem thông tin của mình)
-- và bị hạn chế tối đa quyền xem/sửa dữ liệu của người khác.
-- ===================================================================
-- 1. Quyền trên các View
-- Cho phép xem các view chứa thông tin chung. 
GRANT SELECT ON dbo.v_UserWithRole TO NhanVien;
GRANT SELECT ON dbo.v_AttendanceDetail TO NhanVien;
GRANT SELECT ON dbo.v_SalaryMonthDetail TO NhanVien;

-- 2. Quyền trên các Stored Procedure
-- Chỉ cho phép thực hiện chấm công cho chính mình
GRANT EXECUTE ON dbo.sp_UserCheckIn TO NhanVien;
GRANT EXECUTE ON dbo.sp_UserCheckOut TO NhanVien;

-- 3. Quyền trên các Function
-- Cho phép sử dụng các hàm lấy thông tin lương của bản thân.
GRANT SELECT ON dbo.fn_GetTotalPaidAmount TO NhanVien;
GRANT SELECT ON dbo.fn_GetRemainingSalary TO NhanVien;
GRANT SELECT ON dbo.fn_GetLatestFinalSalary TO NhanVien;
GRANT SELECT ON dbo.fn_GetSalaryForPeriod TO NhanVien;
-- 4. CHẶN (DENY) các quyền truy cập trực tiếp vào bảng
-- Đây là bước quan trọng nhất để đảm bảo nhân viên không thể xem trộm hoặc sửa đổi dữ liệu của người khác.
DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO NhanVien;
DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Account TO NhanVien;
DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Attendance TO NhanVien;
DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Salary TO NhanVien;
DENY SELECT, INSERT, UPDATE, DELETE ON dbo.SalaryPayment TO NhanVien;
DENY SELECT, INSERT, UPDATE, DELETE ON dbo.AllowanceAndPenalty TO NhanVien;
DENY EXECUTE ON dbo.sp_PayAllEmployees TO NhanVien; -- Chặn quyền chạy thanh toán hàng loạt
DENY EXECUTE ON dbo.sp_ProcessPayment TO NhanVien; -- Chặn quyền xử lý thanh toán





CREATE OR ALTER PROCEDURE dbo.sp_CreateUserAccount
    @user_id INT,                   -- ID của nhân viên cần tạo tài khoản
    @username NVARCHAR(100),        -- Tên đăng nhập mong muốn
    @password NVARCHAR(100),        -- Mật khẩu (dạng văn bản gốc)
    @roleName NVARCHAR(100)         -- Vai trò cần gán ('QuanLy' hoặc 'NhanVien')
AS
BEGIN
    SET NOCOUNT ON;
    -- Tăng cường bảo mật: Xử lý các câu lệnh DDL (CREATE/DROP) không nằm trong transaction tự động
    SET XACT_ABORT ON; -- Nếu có lỗi runtime, transaction sẽ tự động ROLLBACK

    -- Cờ để theo dõi các đối tượng đã được tạo, phục vụ cho việc dọn dẹp khi lỗi
    DECLARE @loginCreated BIT = 0;
    DECLARE @userCreated BIT = 0;

    BEGIN TRY
        -- -- == BƯỚC 1: KIỂM TRA ĐẦU VÀO (VALIDATION) == -- --
        
        -- [Cải tiến] Whitelist cho Role: Chỉ cho phép các role hợp lệ
        IF @roleName NOT IN (N'QuanLy', N'NhanVien')
        BEGIN
            RAISERROR (N'Vai trò "%s" không hợp lệ. Chỉ chấp nhận ''QuanLy'' hoặc ''NhanVien''.', 16, 1, @roleName);
            RETURN;
        END

        -- Các kiểm tra khác
        IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @username)
        BEGIN
            RAISERROR ('Tên đăng nhập đã tồn tại trên server. Vui lòng chọn tên khác.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE user_id = @user_id AND status = 1)
        BEGIN
            RAISERROR('Nhân viên không tồn tại hoặc đã nghỉ việc.', 16, 1);
            RETURN;
        END

        -- -- == BƯỚC 2: TẠO LOGIN VÀ USER TRÊN SERVER/DATABASE == -- --
        
        -- [Cải tiến] Bật chính sách mật khẩu của Windows
        DECLARE @sqlCreateLogin NVARCHAR(MAX);
        SET @sqlCreateLogin = N'CREATE LOGIN ' + QUOTENAME(@username) 
                            + N' WITH PASSWORD = N''' + REPLACE(@password, N'''', N'''''') + N''', '
                            + N' CHECK_POLICY = ON, CHECK_EXPIRATION = ON;';
        EXEC sp_executesql @sqlCreateLogin;
        SET @loginCreated = 1; -- Đánh dấu đã tạo login thành công

        -- Tạo User trong Database và liên kết với Login
        DECLARE @sqlCreateUser NVARCHAR(MAX);
        SET @sqlCreateUser = N'CREATE USER ' + QUOTENAME(@username) + N' FOR LOGIN ' + QUOTENAME(@username) + N';';
        EXEC sp_executesql @sqlCreateUser;
        SET @userCreated = 1; -- Đánh dấu đã tạo user thành công

        -- Gán User vào Role
        DECLARE @sqlAddUserToRole NVARCHAR(MAX);
        SET @sqlAddUserToRole = N'ALTER ROLE ' + QUOTENAME(@roleName) + N' ADD MEMBER ' + QUOTENAME(@username) + N';';
        EXEC sp_executesql @sqlAddUserToRole;

        -- -- == BƯỚC 3: TẠO BẢN GHI TRONG BẢNG ACCOUNT CỦA ỨNG DỤNG == -- --

        DECLARE @password_hash VARBINARY(32) = HASHBYTES('SHA2_256', @password);
        INSERT INTO dbo.Account (user_id, username, password_hash)
        VALUES (@user_id, @username, @password_hash);

        PRINT 'Tạo tài khoản hoàn chỉnh thành công cho nhân viên ID: ' + CAST(@user_id AS VARCHAR);

    END TRY
    BEGIN CATCH
        -- Nếu có transaction đang mở, rollback nó
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        -- -- == BƯỚC 4: [Cải tiến] DỌN DẸP CÁC ĐỐI TƯỢNG "MỒ CÔI" KHI LỖI == -- --
        -- Vì CREATE/DROP LOGIN không được transaction rollback, ta phải làm thủ công
        IF @userCreated = 1
        BEGIN
            DECLARE @sqlDropUser NVARCHAR(MAX) = N'DROP USER ' + QUOTENAME(@username);
            EXEC sp_executesql @sqlDropUser;
            PRINT 'Đã dọn dẹp User mồ côi: ' + @username;
        END

        IF @loginCreated = 1
        BEGIN
            DECLARE @sqlDropLogin NVARCHAR(MAX) = N'DROP LOGIN ' + QUOTENAME(@username);
            EXEC sp_executesql @sqlDropLogin;
            PRINT 'Đã dọn dẹp Login mồ côi: ' + @username;
        END
        
                DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_CreateNew_EmployeeWithAccount
    -- Tham số thông tin cá nhân của nhân viên
    @FullName NVARCHAR(100),
    @dob DATE,
    @phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @HireDate DATE,
    @Gender BIT,
    @Address NVARCHAR(200),
    
    -- Tham số cho 2 loại Role khác nhau
    @JobRoleID INT,                 -- ID của Chức Vụ trong bảng dbo.Role (VD: 1='Trưởng phòng')
    @SecurityRoleName NVARCHAR(100),-- Tên của Vai Trò Phân Quyền ('QuanLy' hoặc 'NhanVien')

    -- Tham số cho tài khoản đăng nhập
    @Username NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    -- Lệnh này đảm bảo nếu có lỗi runtime, toàn bộ transaction sẽ được hủy bỏ
    SET XACT_ABORT ON;

    -- Cờ để theo dõi các đối tượng đã tạo, phục vụ cho việc dọn dẹp khi có lỗi
    DECLARE @loginCreated BIT = 0;
    DECLARE @userCreated BIT = 0;

    BEGIN TRY
        -- -- =================================================================
        -- -- BƯỚC 1: KIỂM TRA TÍNH HỢP LỆ CỦA DỮ LIỆU ĐẦU VÀO (VALIDATION)
        -- -- =================================================================
        
        -- Kiểm tra các trường bắt buộc
        IF (@FullName IS NULL OR LTRIM(RTRIM(@FullName)) = '') THROW 50010, 'Họ tên không được bỏ trống', 1;
        IF (@Username IS NULL OR LTRIM(RTRIM(@Username)) = '') THROW 50020, 'Tên đăng nhập không được bỏ trống', 1;
        IF (@Password IS NULL OR LTRIM(RTRIM(@Password)) = '') THROW 50021, 'Mật khẩu không được bỏ trống', 1;

        -- Kiểm tra Chức vụ (Job Role) có tồn tại trong bảng dbo.Role không
        IF NOT EXISTS (SELECT 1 FROM dbo.Role WHERE role_id = @JobRoleID)
        BEGIN
            THROW 50019, 'Chức vụ được chọn không tồn tại.', 1;
        END

        -- Whitelist: Chỉ cho phép 2 vai trò phân quyền hợp lệ để tăng cường bảo mật
        IF @SecurityRoleName NOT IN (N'QuanLy', N'NhanVien')
        BEGIN
            THROW 50022, 'Vai trò phân quyền không hợp lệ. Chỉ chấp nhận ''QuanLy'' hoặc ''NhanVien''.', 1;
        END
        
        -- Kiểm tra sự trùng lặp thông tin
        IF EXISTS (SELECT 1 FROM dbo.Users WHERE email = @Email) THROW 50001, 'Email này đã được sử dụng.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Users WHERE phone = @phone) THROW 50002, 'Số điện thoại này đã được sử dụng.', 1;
        IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @Username) THROW 50004, 'Tên đăng nhập này đã tồn tại ở cấp độ Server. Vui lòng chọn tên khác.', 1;

        -- Bắt đầu một transaction để đảm bảo tất cả các bước đều thành công
        BEGIN TRANSACTION;

        -- -- =================================================================
        -- -- BƯỚC 2: TẠO NHÂN VIÊN MỚI TRONG BẢNG USERS
        -- -- =================================================================
        INSERT INTO dbo.Users(full_name, dob, phone, email, role_id, hire_date, status, Gender, address)
        VALUES(@FullName, @dob, @phone, @Email, @JobRoleID, @HireDate, 1, @Gender, @Address);
        
        -- Lấy ID của nhân viên vừa được tạo ra
        DECLARE @NewUserID INT = SCOPE_IDENTITY();

		EXEC dbo.sp_CreateUserAccount
		@user_id = @NewUserID,
		@username = @Username,
		@password = @Password,
		@roleName =  @SecurityRoleName;

		COMMIT TRANSACTION;

        PRINT 'Tạo nhân viên và tài khoản hoàn chỉnh thành công cho ' + @Username;
        -- Trả về ID của nhân viên mới để ứng dụng có thể sử dụng nếu cần
        SELECT @NewUserID AS NewUserID;

    END TRY
    BEGIN CATCH
                DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO