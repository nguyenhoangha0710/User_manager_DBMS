using Guna.UI2.WinForms;
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
    public partial class AddBonusAndPen : Form
    {
        private int CurrentUserId;
        private int CurrentRecordId;
        private bool isUpdateMode = false;

        public AddBonusAndPen()
        {
            InitializeComponent();
            CurrentUserId = -1;
            LoadTypeComboBox();

        }

        public AddBonusAndPen(int id)
        {
            InitializeComponent();
            CurrentUserId = id;
            this.CurrentRecordId = -1;
            this.isUpdateMode = false;
            txt_StaffId.Text = id.ToString();
            LoadInfoToForm();
            LoadTypeComboBox();

            this.Text = "Thêm Thưởng/Phạt";
            btn_Add.Text = "Thêm";
            btn_Cancel.Text = "Hủy";

        }

        public AddBonusAndPen(int Userid,int recordId)
        {
            InitializeComponent();
            CurrentUserId = Userid;
            this.CurrentRecordId = recordId;
            this.isUpdateMode = true;
            txt_StaffId.Text = Userid.ToString();
            LoadInfoToForm();
            LoadTypeComboBox();

            LoadDataForUpdate(); // Load dữ liệu của bản ghi cần sửa

            this.Text = "Cập nhật Thưởng/Phạt";
            btn_Add.Text = "Cập nhật";
            btn_Cancel.Text = "Xóa"; // Đổi nút Hủy thành Xóa
        }
        private void LoadDataForUpdate()
        {
            var record = AllowanceAndPenaltyDAO.Instance.GetById(this.CurrentRecordId);
            if (record != null)
            {
                DT_timeBonus.Value = new DateTime(record.Year, record.Month, 1);
                txt_amount.Text = record.Amount.ToString();
                cb_type.SelectedValue = record.Type;
                txt_reason.Text = record.Reason;
            }
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

        /// <summary>
        /// Hàm mới để load dữ liệu vào ComboBox
        /// </summary>
        private void LoadTypeComboBox()
        {
            // Tạo một danh sách các đối tượng để làm nguồn dữ liệu
            var dataSource = new List<object>
            {
                new { Text = "Thưởng", Value = 1 },
                new { Text = "Phạt", Value = 0 }
            };

            //Giả sử ComboBox của bạn tên là cb_Type
             cb_type.DataSource = dataSource;
            cb_type.DisplayMember = "Text";
            cb_type.ValueMember = "Value";

            //Mặc định chọn "Thưởng"
             cb_type.SelectedIndex = 0;
        }
        private void AddBonusAndPen_Load(object sender, EventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal amount)) return;

            var data = new AllowanceAndPenaltyEntity
            {
                Id = this.CurrentRecordId, // Quan trọng cho việc cập nhật
                UserId = this.CurrentUserId,
                Month = DT_timeBonus.Value.Month,
                Year = DT_timeBonus.Value.Year,
                Amount = amount,
                Type = Convert.ToBoolean(cb_type.SelectedValue),
                Reason = txt_reason.Text.Trim()
            };

            try
            {
                if (isUpdateMode)
                {
                    AllowanceAndPenaltyDAO.Instance.UpdateAllowanceAndPenalty(data);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AllowanceAndPenaltyDAO.Instance.InsertAllowanceAndPenalty(data);
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi từ cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (isUpdateMode) // Nếu đang ở chế độ cập nhật, nút này là "Xóa"
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        AllowanceAndPenaltyDAO.Instance.DeleteAllowanceAndPenalty(this.CurrentRecordId);
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else // Nếu đang ở chế độ thêm mới, nút này là "Hủy"
            {
                this.Close();
            }
        }
        // Hàm kiểm tra dữ liệu đầu vào
        private bool ValidateInput(out decimal amount)
        {
            amount = 0;
            if (string.IsNullOrWhiteSpace(txt_amount.Text))
            {
                MessageBox.Show("Vui lòng nhập số tiền.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_amount.Focus();
                return false;
            }
            if (!decimal.TryParse(txt_amount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ. Vui lòng nhập một số lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_amount.Focus();
                return false;
            }
            return true;
        }
    }
}
