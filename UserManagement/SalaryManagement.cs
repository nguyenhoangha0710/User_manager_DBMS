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
using UserManagement.View.Salary;

namespace UserManagement
{
    public partial class SalaryManagement : UserControl
    {
        public SalaryManagement()
        {
            InitializeComponent();
            this.dvg_Salary.CellDoubleClick += dvg_Salary_CellDoubleClick;
        }

        private void SalaryManagement_Load(object sender, EventArgs e)
        {
            this.dvg_Salary.DataSource = SalaryDAO.Instance.getListSalary();
        }

        // viet ham khi bam double click vao 1 dong tren datagridview thi se hien thi thong tin len cac textbox
        private void dvg_Salary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu của dòng hiện tại
                DataGridViewRow row = dvg_Salary.Rows[e.RowIndex];

                // Lấy user_id từ cột tương ứng, ví dụ cột "user_id"
                int userId = Convert.ToInt32(row.Cells["user_id"].Value);
                SalaryDetailForUser form = new SalaryDetailForUser(userId);
                form.ShowDialog();
                this.dvg_Salary.DataSource = SalaryDAO.Instance.getListSalary();

            }
        }

        private void DTP_MonthFilter_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_UpdateSalary_Click(object sender, EventArgs e)
        {
            int month = DTP_MonthFilter.Value.Month;
            int year = DTP_MonthFilter.Value.Year;

            try
            {
                DialogResult result = MessageBox.Show(
                                                "Bạn có chắc chắn muốn cập nhật lương cho tất cả?",
                                                "Xác nhận",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question
                                            );
                if (result == DialogResult.Yes)
                {

                    SalaryDAO.Instance.UpdateSalaryForAllUsers(month, year);
                    MessageBox.Show("Cập nhật lương thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void btn_ReadAllSalary_Click(object sender, EventArgs e)
        {
            int month = DTP_MonthFilter.Value.Month;
            int year = DTP_MonthFilter.Value.Year;
            try
            {
                this.dvg_Salary.DataSource = SalaryDAO.Instance.GetSalaryDetailForMonth(month, year);

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

        private void btn_payment_Click(object sender, EventArgs e)
        {
            int month = DTP_MonthFilter.Value.Month;
            int year = DTP_MonthFilter.Value.Year;

            try
            {
                // Xác nhận thanh toán
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn thanh toán lương hàng loạt cho tất cả nhân viên?\n\n" +
                    $"Tháng: {month}/{year}\n\n" +
                    $"Lưu ý: Hệ thống sẽ tự động:\n" +
                    $"1. Cập nhật lương cho tất cả nhân viên\n" +
                    $"2. Thanh toán số tiền còn lại cho từng nhân viên",
                    "Xác nhận thanh toán hàng loạt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    string note = $"Thanh toán lương cuối kỳ tháng {month}/{year}";
                    SalaryDAO.Instance.PayAllEmployees(month, year, note);

                    MessageBox.Show(
                        "Thanh toán lương hàng loạt thành công!\n\nĐã thanh toán cho tất cả nhân viên có số dư còn lại.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Cập nhật lại danh sách lương
                    this.dvg_Salary.DataSource = SalaryDAO.Instance.GetSalaryDetailForMonth(month, year);
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
