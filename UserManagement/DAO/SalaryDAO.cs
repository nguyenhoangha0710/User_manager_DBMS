using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.DTO;
using UserManagement.Entity;

namespace UserManagement.DAO
{
    public class SalaryDAO
    {
        ConnectDataContext conn = new ConnectDataContext();


        private static SalaryDAO instance;
        public static SalaryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SalaryDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        private SalaryDAO() { }


        public List<SalaryDetailEntity> getListSalary()
        {
            return conn.v_SalaryMonthDetails.Select(x => new SalaryDetailEntity
            {
                salary_id = x.salary_id,
                user_id = x.user_id,
                full_name = x.full_name,
                role_name = x.role_name,
                month = x.month,
                year = x.year,
                total_hours = x.total_hours,
                total_bonus = x.total_Bonus,
                total_penalty = x.total_Penalty,
                final_salary = x.final_salary
            }).ToList();
        }


        public void UpdateSalaryForAllUsers(int month, int year)
        {
            conn.sp_UpdateSalaryForAllUsers(month, year);
        }


        public void UpdateSalaryForUser(int userId, int month, int year)
        {
            conn.sp_UpdateSalaryForUser(userId, month, year);
        }

        public List<SalaryDetailEntity> GetSalaryDetailForMonth(int month, int year)
        {
            return conn.fn_GetSalaryDetailForMonth(month, year).Select(x => new SalaryDetailEntity
            {
                salary_id = x.salary_id,
                user_id = x.user_id,
                full_name = x.full_name,
                role_name = x.role_name,
                month = x.month,
                year = x.year,
                total_hours = x.total_hours,
                total_bonus = x.total_Bonus,
                total_penalty = x.total_Penalty,
                final_salary = x.final_salary
            }).ToList();
        }


        public List<AttendenceEntity> GetAttendenceByUesrIdForMonth(int userId, int month, int year)
        {
            // Gọi function từ DataContext
            var result = conn.fn_GetAttendanceByUser(userId, month, year);

            // Map kết quả sang entity
            List<AttendenceEntity> list = result
                .Select(x => new AttendenceEntity
                {
                    AttendenceId = x.attendance_id,   // chú ý: tên property theo class result của EF
                    UserId = x.user_id,
                    WorkDate = x.work_date,
                    CheckIn = x.check_in,
                    CheckOut = x.check_out,
                    WorkHours = x.hours_worked,
                    Note = x.note
                }).ToList();

            return list;
        }

        public List<AllowanceAndPenaltyEntity> GetAllowanceByUserIdForMonth(int userId, int month, int year)
        {
            var result = conn.fn_GetAllowanceByUserForMonth(userId, month, year);
            List<AllowanceAndPenaltyEntity> list = result
                .Select(x => new AllowanceAndPenaltyEntity
                {
                    Id = x.id,
                    UserId = x.user_id,
                    Month = x.month,
                    Year = x.year,
                    Amount = x.amount,
                    Reason = x.reason,
                    Type = x.type
                }).ToList();
            return list;

        }

        public List<AllowanceAndPenaltyEntity> GetPenaltyByUserIdForMonth(int userId, int month, int year)
        {
            var result = conn.fn_GetPenaltyByUserForMonth(userId, month, year);
            List<AllowanceAndPenaltyEntity> list = result
                .Select(x => new AllowanceAndPenaltyEntity
                {
                    Id = x.id,
                    UserId = x.user_id,
                    Month = x.month,
                    Year = x.year,
                    Amount = x.amount,
                    Reason = x.reason,
                    Type = x.type
                }).ToList();
            return list;
        }

        /// <summary>
        /// Lấy tổng lương (final_salary) của user trong tháng/năm
        /// </summary>
        public decimal GetSalaryForPeriod(int userId, int month, int year)
        {
            return conn.fn_GetSalaryForPeriod(userId, month, year) ?? 0;
        }

        /// <summary>
        /// Lấy tổng số tiền đã trả cho user trong tháng/năm
        /// </summary>
        public decimal GetTotalPaidAmount(int userId, int month, int year)
        {
            return conn.fn_GetTotalPaidAmount(userId, month, year) ?? 0;
        }

        /// <summary>
        /// Lấy số tiền còn lại cần trả cho user trong tháng/năm
        /// </summary>
        public decimal GetRemainingSalary(int userId, int month, int year)
        {
            return conn.fn_GetRemainingSalary(userId, month, year) ?? 0;
        }

        /// <summary>
        /// Xử lý thanh toán lương cho nhân viên
        /// </summary>
        public void ProcessPayment(int userId, decimal amount, int month, int year, string note)
        {
            conn.sp_ProcessPayment(userId, amount, month, year, note);
        }

        /// <summary>
        /// Thanh toán lương hàng loạt cho tất cả nhân viên trong tháng
        /// Tự động cập nhật lương trước khi thanh toán
        /// </summary>
        public void PayAllEmployees(int month, int year, string note)
        {
            conn.sp_PayAllEmployees(month, year, note);
        }

    }
}
