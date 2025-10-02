create database UserManager
go 


use UserManager

-- 1. Role : Phan quyen cho nhan vien
create table Role (
	role_id int identity(1,1) primary key, 
	role_name nvarchar(100) not null unique 
)
--2. User table -- Thong tin nhan vien
CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    full_name NVARCHAR(100) NOT NULL,
    dob DATE,
    phone NVARCHAR(20) not null unique,
    email NVARCHAR(100) not null unique,
    role_id INT NOT NULL,
    hire_date DATE DEFAULT GETDATE(), -- Ngày bắt đầu làm
    status BIT DEFAULT 1, -- 1: đang làm, 0: nghỉ việc
    FOREIGN KEY (role_id) REFERENCES Role(role_id) -- chuc vu =
);

ALTER TABLE Users
ADD address NVARCHAR(255) NULL;


-- 3. Shift - ca lam viec (sang chieu toi)
CREATE TABLE Shift (
    shift_id INT IDENTITY(1,1) PRIMARY KEY,
    shift_name NVARCHAR(50), -- VD: Ca sáng, Ca chiều, Ca tối
    start_time TIME NOT NULL,
    end_time TIME NOT NULL
);

-- bo sung		
ALTER TABLE Attendance
ADD shift_id INT NULL 
    FOREIGN KEY REFERENCES Shift(shift_id);


--4. thiet ke bang check in va  check out cho nhan vien 
CREATE TABLE Attendance (
    attendance_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    work_date DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    check_in DATETIME NOT NULL,
    check_out DATETIME NULL, -- NULL nếu chưa checkout
    hours_worked AS 
        (CASE 
            WHEN check_out IS NOT NULL 
            THEN DATEDIFF(MINUTE, check_in, check_out) / 60.0 
            ELSE 0 
         END) PERSISTED, -- tự động tính giờ làm
	note nvarchar (100),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);



DROP TABLE IF EXISTS Salary;
--5. Salary : Luong nhan vien tinh tu thoi tong thoi gian lam trong thang 
CREATE  TABLE Salary (
    salary_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,        -- Mã nhân viên
    month INT NOT NULL,          -- Tháng tính lương
    year INT NOT NULL,           -- Năm tính lương
    total_hours DECIMAL(10,2) DEFAULT 0, -- Tổng số giờ làm trong tháng
	total_Bonus DECIMAL(10,2) DEFAULT 0,
	total_Penalty DECIMAL(10,2) DEFAULT 0,
    final_salary DECIMAL(18,2) NULL, -- Lương thực nhận sau thưởng/phạt, goi ham hoac thu tuc 
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    CONSTRAINT UQ_Salary UNIQUE(user_id, month, year),
	CHECK (month BETWEEN 1 AND 12 AND year > 0)
);


ALTER TABLE Salary
ADD note NVARCHAR(255) NULL;




-- 6. Account : Tai khoan dang nhap ung dung ban ve 
CREATE TABLE Account (
    account_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,          -- Liên kết với nhân viên
    username NVARCHAR(50) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    status BIT DEFAULT 1, -- 1: active, 0: locked
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);


--7. khen thuong va ki luat 
CREATE TABLE AllowanceAndPenalty (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    month INT NOT NULL,
    year INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    reason NVARCHAR(255),
    type bit , -- 1: khen , 0 - phat 
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);


-- tao trigger xoa Nhan Vien, Tai khoan se bi vo hieu hoa
CREATE trigger tg_delUsers on users 
instead of delete 
as begin 
	set NOCOUNT ON
	begin try 
		begin transaction 
			update Users set status = 0 
			WHERE Users.user_id in (select d.user_id from DELETED d)
			update Account set status =0 
			where Account.user_id in (select d.user_id from DELETED d)
		commit TRANSACTION;
	end try 
	begin CATCH
		ROLLBACK TRANSACTION;
	end catch
end;

CREATE TABLE SalaryAudit (
    audit_id INT IDENTITY(1,1) PRIMARY KEY,
    salary_id INT,
    old_final_salary DECIMAL(18,2),
    new_final_salary DECIMAL(18,2),
    changed_at DATETIME DEFAULT GETDATE(),
    modified_by NVARCHAR(100)  -- tên tài khoản hoặc ID người sửa
);