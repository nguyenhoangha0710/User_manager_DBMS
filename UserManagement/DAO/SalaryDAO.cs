using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.DTO;
using UserManagement.Entity;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UserManagement.DAO
{
    public class SalaryDAO
    {
        // Mỗi lần gọi CSDL sẽ tạo mới DataContext dựa trên chuỗi kết nối phiên đăng nhập
        private ConnectDataContext GetContext()
        {
            return new ConnectDataContext(UserSession.MainConnectionString);
        }


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
            try
            {
                using (var conn = GetContext())
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
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<SalaryDetailEntity>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<SalaryDetailEntity>();
            }
        }


        public bool UpdateSalaryForAllUsers(int month, int year)
        {
            try
            {
                using (var conn = GetContext())
                {
                    conn.sp_UpdateSalaryForAllUsers(month, year);
                }
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
        }


        public void UpdateSalaryForUser(int userId, int month, int year)
        {
            try
            {
                using (var conn = GetContext())
                {
                    conn.sp_UpdateSalaryForUser(userId, month, year);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<SalaryDetailEntity> GetSalaryDetailForMonth(int month, int year)
        {
            try
            {
                using (var conn = GetContext())
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
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<SalaryDetailEntity>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<SalaryDetailEntity>();
            }
        }


        public List<AttendenceEntity> GetAttendenceByUesrIdForMonth(int userId, int month, int year)
        {
            try
            {
                using (var conn = GetContext())
                {
                    var result = conn.fn_GetAttendanceByUser(userId, month, year);
                    List<AttendenceEntity> list = result
                        .Select(x => new AttendenceEntity
                        {
                            AttendenceId = x.attendance_id,
                            UserId = x.user_id,
                            WorkDate = x.work_date,
                            CheckIn = x.check_in,
                            CheckOut = x.check_out,
                            WorkHours = x.hours_worked,
                            Note = x.note
                        }).ToList();
                    return list;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<AttendenceEntity>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<AttendenceEntity>();
            }
        }

        public List<AllowanceAndPenaltyEntity> GetAllowanceByUserIdForMonth(int userId, int month, int year)
        {
            try
            {
                using (var conn = GetContext())
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
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<AllowanceAndPenaltyEntity>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<AllowanceAndPenaltyEntity>();
            }
        }

        public List<AllowanceAndPenaltyEntity> GetPenaltyByUserIdForMonth(int userId, int month, int year)
        {
            try
            {
                using (var conn = GetContext())
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
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<AllowanceAndPenaltyEntity>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<AllowanceAndPenaltyEntity>();
            }
        }

        /// <summary>
        /// Lấy tổng lương (final_salary) của user trong tháng/năm
        /// </summary>
        public decimal GetSalaryForPeriod(int userId, int month, int year)
        {
            try
            {
                using (var conn = GetContext())
                {
                    return conn.fn_GetSalaryForPeriod(userId, month, year) ?? 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số tiền đã trả cho user trong tháng/năm
        /// </summary>
        public decimal GetTotalPaidAmount(int userId, int month, int year)
        {
            try
            {
                using (var conn = GetContext())
                {
                    return conn.fn_GetTotalPaidAmount(userId, month, year) ?? 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        /// <summary>
        /// Lấy số tiền còn lại cần trả cho user trong tháng/năm
        /// </summary>
        public decimal GetRemainingSalary(int userId, int month, int year)
        {
            try
            {
                using (var conn = GetContext())
                {
                    return conn.fn_GetRemainingSalary(userId, month, year) ?? 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        /// <summary>
        /// Xử lý thanh toán lương cho nhân viên
        /// </summary>
        public void ProcessPayment(int userId, decimal amount, int month, int year, string note)
        {
            try
            {
                using (var conn = GetContext())
                {
                    conn.sp_ProcessPayment(userId, amount, month, year, note);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thanh toán lương hàng loạt cho tất cả nhân viên trong tháng
        /// Tự động cập nhật lương trước khi thanh toán
        /// </summary>
        public bool PayAllEmployees(int month, int year, string note)
        {
            try
            {
                using (var conn = GetContext())
                {
                    conn.sp_PayAllEmployees(month, year, note);
                    return true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

    }
}
