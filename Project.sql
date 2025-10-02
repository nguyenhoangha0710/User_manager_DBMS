


-- View Danh sach nhan vien + chuc vu + dang lam viec
create or alter view v_UserWithRole 
as 
select u.user_id, u.full_name, u.dob,u.Gender, u.phone, u.email, u.hire_date, r.role_name
from Users u 
join Role r on u.role_id = r.role_id
where u.status = 1 


-- View: Bang cham cong kem theo thong tin nhan vien
create or alter view v_AttendanceDetail
as 
select
	a.attendance_id, 
	u.user_id, 
	u.full_name, 
	r.role_name, 
	a.check_in, 
	a.check_out, 
	a.hours_worked, 
	a.note 
from Attendance a
join Users u on a.user_id = u.user_id
join Role r on r.role_id = u.role_id

-- Lay thong tin luong nhan vien theo thang
CREATE or alter view v_SalaryMonthDetail
as 
select 
	s.salary_id, 
	u.user_id,
	u.full_name,
	r.role_name,
	s.month,
	s.year,
	s.total_hours,
	s.total_Bonus,
	s.total_Penalty,
	s.final_salary
from Salary s
join Users u on s.user_id = u.user_id
join role r on r.role_id = u.role_id 



-- Tinh tong gio lam cua 1 nhan vien trong 1 thang
create or alter function fn_TotalHoursWorked
(
	@user_id int, -- nhan vien nao 
	@month int, -- thang nao 
	@year int -- nam nao
)
returns Decimal(10,2)
as begin 
	declare @total Decimal(10,2);

	select @total = isnull(sum(hours_worked),0)
	from Attendance a
	where a.user_id = @user_id
	and month(work_date) = @month
	and year(work_date) = @year;

	return ISNULL(@total,0)
end 


-- tinh tong thong 
create or alter function fn_TotalBonus
(
	@user_id int, 
	@month int, 
	@year int
)
returns Decimal(10,2)
as begin
	Declare @bonus Decimal(10,2); 
	select @bonus = ISNULL(sum(amount),0)
	from AllowanceAndPenalty a
	WHERE a.user_id = @user_id
	and a.month = @month
	and a.year = @year
	and a.type = 1
	return ISNULL(@bonus,0)
end


-- tinh tong phat
create or alter function fn_TotalPenalty 
(
	@user_id int, 
	@month int, 
	@year int
)
returns Decimal(10,2)
as begin
	Declare @Penalty Decimal(10,2); 
	select @Penalty = ISNULL(sum(amount),0)
	from AllowanceAndPenalty a
	WHERE a.user_id = @user_id
	and a.month = @month
	and a.year = @year
	and a.type = 0
	return ISNULL(@Penalty,0)
end

-- Users (có thêm Gender và Address)
UPDATE Users
SET full_name = N'Nguyễn Văn A',
    dob = '1990-05-12',
    phone = '0901111222',
    email = 'a.nguyen@example.com',
    role_id = 4,
    hire_date = '2022-01-01',
    Gender = 1,  -- 1 = Nam, 0 = Nữ
    address = N'Quận 1, TP Hồ Chí Minh',
    status = 1
WHERE user_id = 8;

UPDATE Users
SET full_name = N'Trần Thị B',
    dob = '1995-08-21',
    phone = '0903333444',
    email = 'b.tran@example.com',
    role_id = 5,
    hire_date = '2023-03-15',
    Gender = 0,
    address = N'Quận 1, TP Hồ Chí Minh',
    status = 1
WHERE user_id = 9;

UPDATE Users
SET full_name = N'Lê Văn C',
    dob = '1998-12-05',
    phone = '0905555666',
    email = 'c.le@example.com',
    role_id = 5,
    hire_date = '2024-07-10',
    Gender = 1,
    address = N'Quận 1, TP Hồ Chí Minh',
    status = 1
WHERE user_id = 10;

UPDATE Users
SET full_name = N'Phạm Thị D',
    dob = '1992-11-30',
    phone = '0907777888',
    email = 'd.pham@example.com',
    role_id = 5,
    hire_date = '2021-09-20',
    Gender = 0,
    address = N'Quận 1, TP Hồ Chí Minh',
    status = 1
WHERE user_id = 11;



select * from Users

select * from role 


INSERT INTO Attendance(user_id, work_date, check_in, check_out, note)
VALUES
-- A làm ngày 15/09
(1, '2025-09-15', '2025-09-15 08:00:00', '2025-09-15 17:00:00', N'Đi làm đúng giờ'),
-- A làm ngày 16/09
(1, '2025-09-16', '2025-09-16 08:15:00', '2025-09-16 17:10:00', N'Tăng ca buổi chiều'),
-- B làm ngày 15/09 nhưng chưa checkout
(8, '2025-09-15', '2025-09-15 09:00:00', '2025-09-15 17:00:00', N''),
-- C làm ca đêm ngày 16/09
(8, '2025-09-16', '2025-09-16 22:00:00', '2025-09-17 06:00:00', N'Ca đêm'),
-- A làm ngày 15/09
(1, '2025-09-16', '2025-09-16 08:00:00', '2025-09-16 17:00:00', N'Đi làm đúng giờ'),
-- A làm ngày 16/09
(9, '2025-09-17', '2025-09-17 08:15:00', '2025-09-17 17:10:00', N'Tăng ca buổi chiều'),
-- B làm ngày 15/09 nhưng chưa checkout
(10, '2025-09-16', '2025-09-16 09:00:00', '2025-09-16 17:00:00', N'Quên checkout'),
-- C làm ca đêm ngày 16/09
(10, '2025-09-16', '2025-09-16 22:00:00', '2025-09-17 06:00:00', N'Ca đêm');




INSERT INTO AllowanceAndPenalty(user_id, month, year, amount, reason, type)
VALUES
(1, 9, 2025, 500000, N'Thưởng chuyên cần', 1),   -- A được thưởng
(10, 9, 2025, 20000, N'Đi làm muộn', 0),          -- B bị phạt
(10, 9, 2025, 300000, N'Thưởng làm ca đêm', 1),    -- C được thưởng
(9, 9, 2025, 10000, N'Đi trễ 15 phút', 0);       -- C bị phạt


select * from AllowanceAndPenalty

-- Test View
SELECT * FROM v_UserWithRole;
SELECT * FROM v_AttendanceDetail;
SELECT * FROM v_SalaryMonthDetail;
select * from AllowanceAndPenalty

-- Test Function
SELECT dbo.fn_TotalHoursWorked(10, 9, 2025) AS TongGioLam_NV_A;
SELECT dbo.fn_TotalBonus(10, 9, 2025) AS Thuong_NV_C;
SELECT dbo.fn_TotalPenalty(10, 9, 2025) AS Phat_NV_C;


-- Cập nhật lương cho một nhân viên 1 tháng
create or alter procedure sp_UpdateSalaryForUser
	@user_id int, 
	@month int,
	@year int 
as begin 
	set NOCOUNT on; 

	Declare @hourly_rate Decimal(10,2) -- tien/gio
	-- lay luong/gio tu role
	select @hourly_rate=r.salary from Users u 
	join Role r on u.role_id = r.role_id
	where u.user_id = @user_id

	-- Neu nhan vien khong co rule --> return
	if @hourly_rate is null
	begin 
		RAISERROR(N'Không tìm thấy mức lương cho nhân viên %d', 16, 1, @user_id);
        RETURN;
	end

	-- Tinh luong cuoi thang cho Nhan Vien

	-- kiem tra , neu ton tai du lieu trong Salary thi update, khong co thi insert
	if Exists(select 1 from Salary WHERE Salary.user_id=@user_id and Salary.month = @month and Salary.year = @year )
	BEGIN
		UPDATE Salary set  final_salary = (Salary.total_hours * @hourly_rate ) + ISNULL(total_bonus, 0) 
                           - ISNULL(total_penalty, 0)
		WHERE Salary.user_id=@user_id and Salary.month = @month and Salary.year = @year; 
	end
	else 
	begin 
		INSERT INTO Salary(user_id, month, year, total_hours, total_bonus, total_penalty, final_salary)
        VALUES (@user_id, @month, @year, 0, 0, 0, 0);
	end 
end

select * from salary
EXEC sp_UpdateSalaryForUser @user_id = 8 ,@month=9, @year = 2025


--================================================
-- Procedure Cap nhat luong cho tat ca nhan vien 1 lan , 
-- Với mỗi nhân viên là 1 transaction riêng biệt
-- Nêus lỗi khi xử lý 1 nhân viên thì rollback nhân viên đó
-- ghi log lỗi vào bảng ProcessErrorLog
--================================================
create or ALTER procedure sp_UpdateSalaryForAllUsers
	@month int, 
	@year int
as begin
	set NOCOUNT on;
	-- Kiem tra du lieu dau vao
	if @month not between 1 and 12 or @year > Year(GETDATE())
		begin
			RAISERROR ('Tháng hoặc năm không hợp lệ.', 16, 1);
        RETURN;
		end
	
	-- kiem tra su ton tai cua procedure con : sp_UpdateSalaryForUser
	if Object_id('sp_UpdateSalaryForUser') is null
	begin
		RAISERROR ('Stored procedure sp_UpdateSalaryForUser chưa tồn tại.', 16, 1);
        RETURN;
	end 

	declare @user_id int ; -- luu bien cua tung nhan vien
	declare @error_count int=0 ; --dem so loi xay ra

	-- Tao cursor de duyet tung nhan vien dang lam viec
	declare cur Cursor for 
		select u.user_id from Users u WHERE u.status =1

	OPEN cur;
    FETCH NEXT FROM cur INTO @user_id;
		
	-- Vong lap duyet tung nhan vien
	While @@fetch_status = 0 
		begin
			begin TRY
				begin TRANSACTION

				-- goi thu tuc tnh luong cho tung nhan vien
				EXEC sp_UpdateSalaryForUser @user_id,@month,@year; 
				commit TRANSACTION
			end TRY
			begin CATCH
				if @@trancount > 0 ROLLBACK TRANSACTION; 
				-- ghi loi vao bang log
				INSERT INTO ProcessErrorLog(process_name, user_id, err_msg, created_at)
				VALUES('UpdateSalaryForUser', @user_id, ERROR_MESSAGE(), GETDATE());
				 -- Tăng số lỗi lên
				SET @error_count = @error_count + 1;


				-- Nếu quá 10 lỗi thì dừng toàn bộ
				IF @error_count > 10
				BEGIN
					RAISERROR ('Quá nhiều lỗi xảy ra, tiến trình dừng lại.', 16, 1);
					CLOSE cur;
					DEALLOCATE cur;
					RETURN;
				END

			end CATCH

			Fetch next from cur into @user_id;

		end

		-- dong cursor
		close cur; 
		deallocate cur;
		
		IF @error_count > 0
			RAISERROR ('Quá trình hoàn tất với %d lỗi.', 10, 1, @error_count);
		ELSE
			PRINT 'Quá trình hoàn tất thành công cho tất cả nhân viên.';

end 


SELECT * FROM Attendance

EXEC sp_UpdateSalaryForAllUsers @month = 9, @year = 2025;

select * from Salary
delete from Salary

-- Bảng ghi log lỗi trong quá trình xử lý
CREATE TABLE ProcessErrorLog (
    log_id INT IDENTITY(1,1) PRIMARY KEY,   -- Khóa chính tự tăng
    process_name NVARCHAR(100) NOT NULL,    -- Tên tiến trình (vd: UpdateSalaryForUser)
    user_id INT NULL,                       -- Nhân viên liên quan (có thể NULL nếu lỗi chung)
    err_msg NVARCHAR(4000) NOT NULL,        -- Nội dung lỗi
    created_at DATETIME DEFAULT GETDATE()   -- Thời điểm ghi log
);



-- tao view xem chi tiet luong
CREATE or alter VIEW v_SalaryDetailForMonth AS
select
	u.user_id,
	u.full_name,
	u.dob,
	u.phone,
	r.role_name,
	s.month,
	s.year, 
	s.total_hours, 
	s.total_Bonus, 
	s.total_Penalty, 
	s.final_salary
from Users u 
join Role r on u.role_id = r.role_id
join Salary s on u.user_id = s.user_id
WHERE u.status =1 

select * from v_SalaryMonthDetail

-- ham xem vhi tiết lương theo thangs
CREATE OR ALTER FUNCTION fn_GetSalaryDetailForMonth
(
    @month INT,
    @year INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM v_SalaryMonthDetail
    WHERE month = @month
      AND year = @year
);




select * from v_SalaryDetailForMonth
select * from v_SalaryMonthDetail
select * from dbo.fn_GetSalaryDetailForMonth(9,2025)

-- viet trigger cap nhat gio lam khi check out
create or alter trigger trg_UpdateHoursOnCheckout
on Attendance
after update
as begin
	set NOCOUNT on;
	IF UPDATE(check_out)
		begin
			-- lay nhung ban ghi Attendance vua update checkout tu NUll --> gia tri
			; with cte as (
				SELECT
					i.attendance_id, 
					i.user_id,
	                MONTH(i.work_date) AS month,
					YEAR(i.work_date) AS year,
					i.hours_worked
				from INSERTED i 
				join DELETED d on i.attendance_id = d.attendance_id
				where d.check_out is NULL -- truoc check out
				and i.check_out is not null -- sau check out
			)
			-- Cap nhat vao bang salary
			MERGE Salary as target 
			using cte as src
			on target.user_id = src.user_id
				and target.month = src.month 
				and target.year = src.year
			when MATCHED then 
				update set total_hours = target.total_hours + src.hours_worked
			when not MATCHED then 
				INSERT (user_id, month, year, total_hours, total_Bonus, total_Penalty, final_salary)
            VALUES (src.user_id, src.month, src.year, src.hours_worked, 0, 0, 0);
		end 
end

select * from Salary


SELECT * from Attendance
-- Giả sử nhân viên user_id = 1 đi làm
INSERT INTO Attendance (user_id, work_date, check_in, check_out, note)
VALUES (8, '2025-09-27', '2025-09-27 08:00:00', NULL, N'Đi làm ca sáng');

-- Kiểm tra lại
SELECT * FROM Attendance 
SELECT * from Salary
EXEC sp_UpdateSalaryForUser @user_id =8,@month = 9, @year = 2025 
-- Cập nhật giờ checkout
UPDATE Attendance
SET check_out = '2025-09-27 17:00:00'
WHERE user_id = 8 AND work_date = '2025-09-27';

select * from v_SalaryDetailForMonth
EXEC sp_UpdateSalaryForAllUsers @month = 9, @year =2025


-- trigger tu dong cap nhat tong phat va tong thuong ben 
create or alter trigger trg_UpdateBonusPenaltyOnInsert
on  AllowanceAndPenalty
after insert 
as begin 
	set NOCOUNT on;

	-- lay thong tin nhung thang moi nhap
	; with cte as (
		SELECT
			i.user_id,
			i.month,
			i.year, 
			sum(case when i.type=1 then i.amount ELSE 0 end) as Bonus, 
			sum(case when i.type =0 then i.amount ELSE 0 end) as Penalty
		from INSERTED i 
		GROUP by i.user_id , i.month, i.year
	)

	MERGE Salary as target
	using cte as src
	on target.user_id = src.user_id
	and target.month = src.month
	and target.year = src.year
	when MATCHED then 
		update SET 
			target.total_Bonus=target.total_Bonus + src.Bonus, 
			target.total_Penalty=target.total_Penalty + src.Penalty
	when not MATCHED then 
		INSERT (user_id, month, year, total_hours, total_Bonus, total_Penalty, final_salary)
        VALUES (src.user_id, src.month, src.year, 0, src.bonus, src.penalty, 0);
end

SELECT * from AllowanceAndPenalty
select * from Salary


-- Thêm thưởng cho user_id = 1 tháng 9/2025
INSERT INTO AllowanceAndPenalty(user_id, month, year, amount, type)
VALUES (1, 9, 2025, 500000, 1);

-- Thêm phạt
INSERT INTO AllowanceAndPenalty(user_id, month, year, amount, type)
VALUES (1, 9, 2025, 200000, 0);

-- Kiểm tra lại
SELECT * FROM Salary WHERE user_id = 1 AND month = 9 AND year = 2025;
EXEC sp_UpdateSalaryForAllUsers @month =9 , @year = 2025



-- viet trigger khi co su insert hoac update Salary(total_hours,total_Bonus,total_Penalty) thi se cap nhat lai final_salary 
/*
create or alter trigger trg_RecalculateFinalSalary
on Salary
after INSERT, UPDATE
as begin 
	set nocount on;

	-- chi xu ly neu lien quan den total_hours, Bonus, Penlty
	IF (UPDATE(total_hours) OR UPDATE(total_Bonus) OR UPDATE(total_Penalty))
       OR (EXISTS (SELECT 1 FROM INSERTED)  AND NOT EXISTS (SELECT 1 FROM DELETED)) -- khi insert
	begin
		update s set s.final_salary = (i.total_hours * r.salary) + i.total_Bonus - i.total_Penalty 
		from Salary s join Users u on u.user_id = s.user_id
		join Role r on r.role_id = u.role_id
		join INSERTED i on s.user_id = i.user_id and s.month = i.month and s.year = i.year
		where i.month BETWEEN 1 AND 12
              AND i.year BETWEEN 1900 AND YEAR(GETDATE());
	end;
end
*/

DROP TRIGGER dbo.trg_RecalculateFinalSalary;


-- Check-in trước, chưa có checkout
INSERT INTO Attendance (user_id, work_date, check_in)
VALUES (5, '2025-09-18', '2025-09-18 08:00:00');

select * from Attendance

-- Sau đó update checkout => trigger cập nhật Salary
UPDATE Attendance
SET check_out = '2025-09-18 17:00:00'
WHERE attendance_id = 13;

-- Nhân viên A được thưởng
INSERT INTO AllowanceAndPenalty (user_id, month, year, bonus, penalty, note)
VALUES (1, 9, 2025, 200000, 0, N'Thưởng đạt KPI');

INSERT into AllowanceAndPenalty (AllowanceAndPenalty.user_id, AllowanceAndPenalty.month, AllowanceAndPenalty.year, AllowanceAndPenalty.amount, AllowanceAndPenalty.type)
VALUES(11,9,2025,55000,1)

-- Nhân viên A bị phạt
INSERT INTO AllowanceAndPenalty (user_id, month, year, bonus, penalty, note)
VALUES (1, 9, 2025, 0, 50000, N'Đi muộn');




select * from salary

select * from v_UserWithRole
SELECT * from role
select * from Users
select * from role

DELETE from Salary Where user_id = 11 



-- viet procedure cho search vơi typeSearch(user_id,phone,full_name) va value di kem 

CREATE OR ALTER PROCEDURE SearchUser
    @TypeSearch NVARCHAR(50),   -- cột cần tìm kiếm
    @ValueSearch NVARCHAR(100)  -- giá trị tìm kiếm
AS
BEGIN
    SET NOCOUNT ON; 
    BEGIN TRY
        -- kiểm tra cột tìm kiếm có hợp lệ
        IF @TypeSearch NOT IN ('user_id', 'full_name', 'phone')
        BEGIN
            RAISERROR('Cột tìm kiếm không hợp lệ!', 16, 1);
            RETURN;
        END;

        DECLARE @sql NVARCHAR(MAX);

        IF @TypeSearch = 'user_id'
        BEGIN
            -- Kiểm tra xem @ValueSearch có phải số nguyên
           IF TRY_CAST(@ValueSearch AS INT) IS NULL
            BEGIN
                RAISERROR('Giá trị tìm kiếm cho user_id phải là số nguyên hợp lệ!', 16, 1);
                RETURN;
            END;

            DECLARE @paramNum INT = CAST(@ValueSearch AS INT);

            SET @sql = N'SELECT * FROM v_UserWithRole WHERE user_id = @paramNum';
            EXEC sys.sp_executesql 
                @sql, 
                N'@paramNum INT',
                @paramNum = @paramNum;
        END
        ELSE
        BEGIN
            DECLARE @paramStr NVARCHAR(200) = N'%' + @ValueSearch + N'%';

            SET @sql = N'SELECT * FROM v_UserWithRole WHERE ' + QUOTENAME(@TypeSearch) + N' LIKE @paramStr';
            EXEC sys.sp_executesql 
                @sql,
                N'@paramStr NVARCHAR(200)', 
                @paramStr = @paramStr;
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

select * from Users
select * from v_UserWithRole
EXEC SearchUser 'user_id', 'a';
EXEC SearchUser 'full_name', 'a';





CREATE or alter PROCEDURE sp_AddUserManagement
    @FullName NVARCHAR(100),
    @dob DATE,
    @phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @RoleId INT,
    @HireDate DATE,
    @Gender bit,
    @Address NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra không được bỏ trống
        IF (@FullName IS NULL OR LTRIM(RTRIM(@FullName)) = '')
            THROW 50010, 'Full name không được bỏ trống', 1;

        IF (@dob IS NULL)
            THROW 50011, 'Ngày sinh không được bỏ trống', 1;

        IF (@phone IS NULL OR LTRIM(RTRIM(@phone)) = '')
            THROW 50012, 'Số điện thoại không được bỏ trống', 1;

        IF (@Email IS NULL OR LTRIM(RTRIM(@Email)) = '')
            THROW 50013, 'Email không được bỏ trống', 1;

        IF (@RoleId IS NULL)
            THROW 50014, 'Role ID không được bỏ trống', 1;

        IF (@HireDate IS NULL)
            THROW 50015, 'HireDate không được bỏ trống', 1;

        IF (@Gender IS NULL)
            THROW 50017, 'Gender không được bỏ trống', 1;

        IF (@Address IS NULL OR LTRIM(RTRIM(@Address)) = '')
            THROW 50018, 'Địa chỉ không được bỏ trống', 1;


		IF NOT EXISTS (SELECT 1 FROM Role WHERE role_id = @RoleId)
			THROW 50019, 'Role ID không tồn tại', 1;
        -- Kiểm tra trùng email
        IF EXISTS (SELECT 1 FROM Users WHERE email = @Email)
            THROW 50001, 'Email đã tồn tại', 1;

        -- Kiểm tra trùng số điện thoại
        IF EXISTS (SELECT 1 FROM Users WHERE phone = @phone)
            THROW 50002, 'Số điện thoại đã tồn tại', 1;

        -- Insert
        INSERT INTO Users(full_name, dob, phone, email, role_id, hire_date, status, Gender, address)
        VALUES(@FullName, @dob, @phone, @Email, @RoleId, @HireDate, 1, @Gender, @Address);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrSeverity INT = ERROR_SEVERITY();
        RAISERROR(@ErrMsg, @ErrSeverity, 1);
    END CATCH
END
GO



CREATE OR ALTER PROCEDURE sp_UpdateUserManagement
    @UserId INT,              -- ID của user cần update
    @FullName NVARCHAR(100),
    @dob DATE,
    @phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @RoleId INT,
    @HireDate DATE,
    @Gender BIT,
    @Address NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra UserId hợp lệ
        IF NOT EXISTS (SELECT 1 FROM Users WHERE user_id = @UserId)
            THROW 50020, 'User ID không tồn tại', 1;

        -- Kiểm tra dữ liệu không được bỏ trống
        IF (@FullName IS NULL OR LTRIM(RTRIM(@FullName)) = '')
            THROW 50010, 'Full name không được bỏ trống', 1;

        IF (@dob IS NULL)
            THROW 50011, 'Ngày sinh không được bỏ trống', 1;

        IF (@phone IS NULL OR LTRIM(RTRIM(@phone)) = '')
            THROW 50012, 'Số điện thoại không được bỏ trống', 1;

        IF (@Email IS NULL OR LTRIM(RTRIM(@Email)) = '')
            THROW 50013, 'Email không được bỏ trống', 1;

        IF (@RoleId IS NULL)
            THROW 50014, 'Role ID không được bỏ trống', 1;

        IF (@HireDate IS NULL)
            THROW 50015, 'HireDate không được bỏ trống', 1;

        IF (@Gender IS NULL)
            THROW 50017, 'Gender không được bỏ trống', 1;

        IF (@Address IS NULL OR LTRIM(RTRIM(@Address)) = '')
            THROW 50018, 'Địa chỉ không được bỏ trống', 1;

        -- Kiểm tra RoleId tồn tại
        IF NOT EXISTS (SELECT 1 FROM Role WHERE role_id = @RoleId)
            THROW 50019, 'Role ID không tồn tại', 1;

        -- Kiểm tra trùng email với user khác
        IF EXISTS (SELECT 1 FROM Users WHERE email = @Email AND user_id != @UserId)
            THROW 50001, 'Email đã tồn tại', 1;

        -- Kiểm tra trùng số điện thoại với user khác
        IF EXISTS (SELECT 1 FROM Users WHERE phone = @phone AND user_id  != @UserId)
            THROW 50002, 'Số điện thoại đã tồn tại', 1;

        -- Update dữ liệu
        UPDATE Users
        SET full_name = @FullName,
            dob = @dob,
            phone = @phone,
            email = @Email,
            role_id = @RoleId,
            hire_date = @HireDate,
            Gender = @Gender,
            address = @Address
        WHERE user_id = @UserId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrSeverity INT = ERROR_SEVERITY();
        RAISERROR(@ErrMsg, @ErrSeverity, 1);
    END CATCH
END
GO






select * from users

EXEC sp_UpdateUserManagement
	@UserId = 30,
    @FullName = N'Nguyen Van em',
    @dob = '1990-05-15',
    @phone = N'0914667375678',
    @Email = N'nguyenvanem@example.com',
    @RoleId = 5,
    @HireDate = '2025-09-24',
    @Gender = 0,
    @Address = N'Vo Van Ngan';


	DROP PROCEDURE IF EXISTS sp_AddUser;
GO

-- Sau đó CREATE lại thủ tục đúng 8 tham số
EXEC sp_help 'sp_AddUser';



-- procedure xoa user khi truyen vao id
CREATE PROCEDURE DisableUserById
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra user có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM Users WHERE user_id = @UserId)
        BEGIN
            RAISERROR('Người dùng không tồn tại!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;

        -- Cập nhật status về 0 trong Users
        UPDATE Users
        SET status = 0
        WHERE user_id = @UserId;

        -- Nếu muốn khóa luôn tài khoản đăng nhập thì update luôn Account
        UPDATE Account
        SET status = 0
        WHERE user_id = @UserId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT;
        SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY();
        RAISERROR(@ErrMsg, @ErrSeverity, 1);
    END CATCH
END;
GO


SELECT * from Account


select * from Attendance
-- Ham lay lich lam viec cua nhan vien
CREATE FUNCTION fn_GetAttendanceByUser
(
    @UserId INT,
    @Month INT,
    @Year INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT  * 
    FROM Attendance
    WHERE user_id = @UserId
      AND MONTH(work_date) = @Month
      AND YEAR(work_date) = @Year
)
GO

SELECT * from dbo.fn_GetAttendanceByUser(1,9,2025)


-- Ham lay thuong va phat cua nhan vien
SELECT a.id,a.user_id,a.month,a.year,a.amount,a.reason,a.type from AllowanceAndPenalty a

CREATE FUNCTION fn_GetAllowanceByUserForMonth
(
    @UserId INT,
    @Month INT,
    @Year INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        id,
        user_id,
        month,
        year,
        amount,
        reason,
        type
    FROM AllowanceAndPenalty
    WHERE user_id = @UserId
      AND month = @Month
      AND year = @Year
      AND type = 1  -- Chỉ lấy thưởng
)
GO

SELECT * from dbo.fn_GetAllowanceByUserForMonth(11,9,2025)


CREATE or alter FUNCTION fn_GetPenaltyByUserForMonth
(
    @UserId INT,
    @Month INT,
    @Year INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        id,
        user_id,
        month,
        year,
        amount,
        reason,
        type
    FROM AllowanceAndPenalty
    WHERE user_id = @UserId
      AND month = @Month
      AND year = @Year
      AND type = 0  -- Chỉ lấy phạt
)
GO

select * from dbo.fn_GetPenaltyByUserForMonth(1,9,2025)


-- tao procedure check in cho moi nhan vien
create   procedure sp_UserCheckIn
as 
begin
	set NOCOUNT on;

	Declare @current_user_id int;

	-- lay user_id cua nguoi dang dung thuc thi lenh dua vao dang nhap
    SELECT @current_user_id = user_id FROM dbo.Account WHERE username = USER_NAME();

	if @current_user_id is null
		begin
			RAISERROR('Không tìm thấy thông tin người dùng hợp lệ.', 16, 1);
			RETURN;
		end

	-- kiểm tra người dùng có bản ghi check in trong ngày mà chưa check out không
	if exists (
		 SELECT 1
        FROM dbo.Attendance
        WHERE user_id = @current_user_id
          AND CAST(check_in AS DATE) = CAST(GETDATE() AS DATE) -- So sánh chỉ ngày, không tính giờ
          AND check_out IS NULL
	)
		begin 
				  -- Nếu tồn tại, ném ra lỗi và dừng lại
			RAISERROR('Bạn đã check-in hôm nay nhưng chưa check-out. Vui lòng check-out trước khi thực hiện check-in mới.', 16, 1);
			RETURN;
		end

	-- Neu khong vi pham thuc hien insert ban ghii moi
	INSERT INTO dbo.Attendance (user_id, work_date, check_in, check_out)
    VALUES (@current_user_id, CAST(GETDATE() AS DATE), GETDATE(), NULL);

	SELECT 'Check-in thành công' AS Message, GETDATE() AS Time;

end
GO

CREATE   PROCEDURE dbo.sp_UserCheckOut
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @current_user_id INT;
    DECLARE @attendance_id_to_update INT;

    --  Lấy user_id của người dùng đang thực thi lệnh
    SELECT @current_user_id = user_id FROM dbo.Account WHERE username = USER_NAME();

    -- Kiểm tra nếu không tìm thấy user thì báo lỗi
    IF @current_user_id IS NULL
    BEGIN
        RAISERROR('Không tìm thấy thông tin người dùng hợp lệ.', 16, 1);
        RETURN;
    END

    -- Tìm bản ghi check-in gần nhất trong ngày của user này mà chưa có check-out
    SELECT TOP 1 @attendance_id_to_update = attendance_id
    FROM dbo.Attendance
    WHERE user_id = @current_user_id
      AND CAST(check_in AS DATE) = CAST(GETDATE() AS DATE)
      AND check_out IS NULL
    ORDER BY check_in DESC; -- Lấy cái mới nhất phòng trường hợp có lỗi dữ liệu

    -- Kiểm tra xem có tìm thấy bản ghi nào để check-out không
    IF @attendance_id_to_update IS NULL
    BEGIN
        -- Nếu không tìm thấy, nghĩa là họ chưa check-in hoặc đã check-out rồi. Ném ra lỗi.
        RAISERROR('Không tìm thấy bản ghi check-in hợp lệ để check-out. Bạn cần check-in trước.', 16, 1);
        RETURN;
    END

    -- Nếu tìm thấy, cập nhật thời gian check-out cho bản ghi đó
    UPDATE dbo.Attendance
    SET check_out = GETDATE()
    WHERE attendance_id = @attendance_id_to_update;
	SELECT 'Check-out thành công' AS Message, GETDATE() AS Time;
END


-- ham lay final luong moi nhat
CREATE FUNCTION dbo.fn_GetSalaryForPeriod
(
    @target_user_id INT,
    @month INT,
    @year INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @salary_for_period DECIMAL(18,2);

    -- Lấy chính xác bản ghi lương khớp với user, tháng, và năm
    SELECT @salary_for_period = final_salary
    FROM dbo.Salary
    WHERE user_id = @target_user_id
      AND month = @month
      AND year = @year;

    -- Trả về giá trị tìm được, nếu không có thì trả về 0
    RETURN ISNULL(@salary_for_period, 0);
END
GO


-- tra ve tong tien da tra cho nhan vien
CREATE FUNCTION dbo.fn_GetTotalPaidAmount
(
    @target_user_id INT,
    @month INT,
    @year INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @total_paid DECIMAL(18,2);

    -- Tính tổng số tiền từ bảng SalaryPayment
    SELECT @total_paid = ISNULL(SUM(amount), 0)
    FROM dbo.SalaryPayment
    WHERE user_id = @target_user_id
      AND payment_month = @month
      AND payment_year = @year;

    RETURN @total_paid;
END



-- so tien con lai can phai tra cho nhan vien
CREATE FUNCTION dbo.fn_GetRemainingSalary
(
    @target_user_id INT,
    @month INT,
    @year INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @final_salary DECIMAL(18,2);
    DECLARE @total_paid DECIMAL(18,2);
    DECLARE @remaining_amount DECIMAL(18,2);

    -- Bước 1: Lấy lương phải trả từ bảng Salary
    -- (Giả định rằng sp_UpdateSalaryForUser đã được chạy trước đó)
    SELECT @final_salary = ISNULL(final_salary, 0)
    FROM dbo.Salary
    WHERE user_id = @target_user_id
      AND month = @month
      AND year = @year;

    -- Bước 2: Lấy tổng số tiền đã trả 
    SET @total_paid = dbo.fn_GetTotalPaidAmount(@target_user_id, @month, @year);

    -- Bước 3: Tính số tiền còn lại
    SET @remaining_amount = @final_salary - @total_paid;

    -- Nếu số còn lại âm (do trả dư), thì coi như không còn nợ (trả về 0)
    IF @remaining_amount < 0
    BEGIN
        SET @remaining_amount = 0;
    END

    RETURN @remaining_amount;
END

-- thah toan luong cho nhan vien
CREATE   PROCEDURE dbo.sp_ProcessPayment
    @target_user_id INT,            -- ID của nhân viên nhận tiền
    @amount DECIMAL(18,2),          -- Số tiền thanh toán
    @payment_month INT,             -- Giao dịch này cho lương tháng mấy
    @payment_year INT,              -- Giao dịch này cho lương năm nào
    @note NVARCHAR(255) = NULL      -- Ghi chú (không bắt buộc)
AS
BEGIN
    SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE user_id = @target_user_id)
	BEGIN
		RAISERROR('Không tìm thấy nhân viên với ID đã nhập.', 16, 1);
		RETURN;
	END
	IF @amount <= 0
	BEGIN
		RAISERROR('Số tiền thanh toán phải lớn hơn 0.', 16, 1);
		RETURN;
	END


    -- Nếu hợp lệ, chèn dữ liệu vào bảng SalaryPayment
    INSERT INTO dbo.SalaryPayment (
        user_id,
        amount,
        payment_month,
        payment_year,
        note
    )
    VALUES (
        @target_user_id,
        @amount,
        @payment_month,
        @payment_year,
        @note
    );
    
    SELECT 'Thanh toán thành công!' AS Message;
END
GO