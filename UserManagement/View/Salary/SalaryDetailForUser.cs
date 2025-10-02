using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManagement.DAO;
using UserManagement.DTO;
using UserManagement.Entity;

namespace UserManagement.View.Salary
{
    public partial class SalaryDetailForUser : Form
    {
        private int user_id;

        public SalaryDetailForUser()
        {
            InitializeComponent();
            this.user_id = -1;
        }

        public SalaryDetailForUser(int userId)
        {
            InitializeComponent();
            this.user_id = userId;
            LoadInfoToForm();

            // load data len form
            LoadAttendence();
            LoadSalaryInfo(); // Load thông tin lương khi mở form
            dvg_relateUser.AutoGenerateColumns = true;
        }

        public void LoadAttendence()
        {
            int month = DTP_month_filter.Value.Month;
            int year = DTP_month_filter.Value.Year;
            List<AttendenceEntity> attendences = SalaryDAO.Instance.GetAttendenceByUesrIdForMonth(this.user_id, month, year);
            dvg_relateUser.DataSource = null;
            dvg_relateUser.DataSource = attendences;
            dvg_relateUser.Refresh();
        }


        public void LoadInfoToForm()
        {
            UserEntity user = UserDAO.Instance.GetUserById(this.user_id);
            if (user == null)
            {
                MessageBox.Show("Không tìm thấy người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                txt_NameUser.Text = user.full_name;
                txt_PhoneNumber.Text = user.phone;
                txt_DateOfBirth.Text = user.dob.ToString();
                txt_UserId.Text = user.user_id.ToString();
                txt_Role.Text = RoleDAO.Instance.GetRoleNameById(user.Role_id);
            }
        }

        /// <summary>
        /// Load thông tin lương: Tổng lương, Đã trả, Còn lại
        /// </summary>
        public void LoadSalaryInfo()
        {
            try
            {
                int month = DTP_month_filter.Value.Month;
                int year = DTP_month_filter.Value.Year;

                // Lấy thông tin lương từ các functions
                decimal totalSalary = SalaryDAO.Instance.GetSalaryForPeriod(this.user_id, month, year);
                decimal totalPaid = SalaryDAO.Instance.GetTotalPaidAmount(this.user_id, month, year);
                decimal remaining = SalaryDAO.Instance.GetRemainingSalary(this.user_id, month, year);

                // Hiển thị lên textbox
                txt_TotalSalary.Text = totalSalary.ToString("N0") + " VNĐ";
                txt_totalPayment.Text = totalPaid.ToString("N0") + " VNĐ";
                txt_RemainingSalary.Text = remaining.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin lương: " + ex.Message, 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dvg_relateUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_UpdateSalary_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                                            "Bạn có chắc chắn muốn cập nhật lương cho nhân viên này?",
                                            "Xác nhận",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int month = DTP_month_filter.Value.Month;
                int year = DTP_month_filter.Value.Year;
                try
                {
                    SalaryDAO.Instance.UpdateSalaryForUser(this.user_id, month, year);
                    MessageBox.Show("Cập nhật lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSalaryInfo(); // Cập nhật lại thông tin lương sau khi update
                }
                catch (SqlException ex)
                {
                    // Lỗi từ SQL Server (ví dụ RAISERROR trong stored procedure)
                    MessageBox.Show(ex.Message, "Lỗi từ cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Lỗi khác (Null, convert, ...)
                    MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_ReadBonus_Click(object sender, EventArgs e)
        {
            this.dvg_relateUser.DataSource = null;

            int month = DTP_month_filter.Value.Month;
            int year = DTP_month_filter.Value.Year;
            try
            {
                this.dvg_relateUser.DataSource = SalaryDAO.Instance.GetAllowanceByUserIdForMonth(this.user_id,month,year);
                
            }
            catch (SqlException ex)
            {
                // Lỗi từ SQL Server (ví dụ RAISERROR trong stored procedure)
                MessageBox.Show(ex.Message, "Lỗi từ cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Lỗi khác (Null, convert, ...)
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btn_ReadDateWork_Click(object sender, EventArgs e)
        {
            LoadAttendence();
            LoadSalaryInfo(); // Cập nhật thông tin lương khi đổi tháng
        }

        private void btn_ReadPenalty_Click(object sender, EventArgs e)
        {
            this.dvg_relateUser.DataSource = null;

            int month = DTP_month_filter.Value.Month;
            int year = DTP_month_filter.Value.Year;
            try
            {
                this.dvg_relateUser.DataSource = SalaryDAO.Instance.GetPenaltyByUserIdForMonth(this.user_id, month, year);

            }
            catch (SqlException ex)
            {
                // Lỗi từ SQL Server (ví dụ RAISERROR trong stored procedure)
                MessageBox.Show(ex.Message, "Lỗi từ cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Lỗi khác (Null, convert, ...)
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Payment_Click(object sender, EventArgs e)
        {
            try
            {
                int month = DTP_month_filter.Value.Month;
                int year = DTP_month_filter.Value.Year;

                // Lấy số tiền còn lại cần thanh toán
                decimal remainingAmount = SalaryDAO.Instance.GetRemainingSalary(this.user_id, month, year);

                // Kiểm tra có còn tiền cần trả không
                if (remainingAmount <= 0)
                {
                    MessageBox.Show("Không còn số tiền nào cần thanh toán cho tháng này!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Xác nhận thanh toán
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn thanh toán toàn bộ số tiền còn lại?\n\n" +
                    $"Số tiền: {remainingAmount:N0} VNĐ\n" +
                    $"Tháng: {month}/{year}",
                    "Xác nhận thanh toán",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Gọi stored procedure để thanh toán
                    string note = $"Thanh toán lương tháng {month}/{year}";
                    SalaryDAO.Instance.ProcessPayment(this.user_id, remainingAmount, month, year, note);

                    MessageBox.Show("Thanh toán thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại thông tin lương sau khi thanh toán
                    LoadSalaryInfo();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi từ cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
