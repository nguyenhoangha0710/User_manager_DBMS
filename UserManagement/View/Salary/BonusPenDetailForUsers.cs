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
    public partial class BonusPenDetailForUsers : Form
    {
        private int CurrentUserId;
        private bool isBonusMode = true; // Cờ để biết đang xem Thưởng hay Phạt

        public BonusPenDetailForUsers()
        {
            InitializeComponent();
        }

        public BonusPenDetailForUsers(int id)
        {
            InitializeComponent();
            CurrentUserId = id;
            txt_StaffId.Text = id.ToString();
            this.dvg_relateUser.CellDoubleClick += new DataGridViewCellEventHandler(this.dvg_relateUser_CellDoubleClick);
            LoadInfoToForm();
            LoadData();
        }


        public void LoadInfoToForm()
        {
            UserEntity user = UserDAO.Instance.GetUserById(this.CurrentUserId);
            if (user == null)
            {
                MessageBox.Show("Không tìm thấy người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                txt_NameStaff.Text = user.full_name;
            }
        }



        private void btn_AddBonus_Click(object sender, EventArgs e)
        {
            AddBonusAndPen cudUserForm = new AddBonusAndPen(this.CurrentUserId);
            cudUserForm.ShowDialog();

        }

        private void btn_ReadBonus_Click_1(object sender, EventArgs e)
        {
            isBonusMode = true; // Chuyển sang chế độ xem thưởng
            LoadData();
        }

        private void btn_ReadPenalty_Click_1(object sender, EventArgs e)
        {
            isBonusMode = false; // Chuyển sang chế độ xem phạt
            LoadData();
        }

        private void dvg_relateUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// Sự kiện mới: Xử lý khi người dùng double-click vào một ô trong DataGridView
        /// </summary>
        private void dvg_relateUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo người dùng không click vào header và dòng đó có dữ liệu
            if (e.RowIndex >= 0 && e.RowIndex < dvg_relateUser.Rows.Count)
            {
                // Lấy ID của bản ghi từ ô đầu tiên của dòng được click
                // Giả sử cột 'Id' là cột đầu tiên (index = 0)
                if (dvg_relateUser.Rows[e.RowIndex].Cells["Id"].Value != null)
                {
                    int recordId = Convert.ToInt32(dvg_relateUser.Rows[e.RowIndex].Cells["Id"].Value);

                    // Mở form AddBonusAndPen ở chế độ "Cập nhật/Xóa"
                    AddBonusAndPen updateForm = new AddBonusAndPen(this.CurrentUserId, recordId);

                    // Nếu người dùng có thực hiện Cập nhật hoặc Xóa, load lại lưới
                    if (updateForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }
        /// <summary>
        /// Hàm chung để load lại dữ liệu trên DataGridView
        /// </summary>
        private void LoadData()
        {
            this.dvg_relateUser.DataSource = null;
            int month = DTP_month_filter.Value.Month;
            int year = DTP_month_filter.Value.Year;

            try
            {
                if (isBonusMode)
                {
                    // Lấy danh sách thưởng
                    this.dvg_relateUser.DataSource = SalaryDAO.Instance.GetAllowanceByUserIdForMonth(this.CurrentUserId, month, year);
                }
                else
                {
                    // Lấy danh sách phạt
                    this.dvg_relateUser.DataSource = SalaryDAO.Instance.GetPenaltyByUserIdForMonth(this.CurrentUserId, month, year);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
