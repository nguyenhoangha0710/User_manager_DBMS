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
using UserManagement.View.User;

namespace UserManagement
{
    public partial class UserUS : UserControl
    {
        public UserUS()
        {
            InitializeComponent();
            this.dgv_Users.CellDoubleClick += dgv_Users_CellDoubleClick;
            this.LoadTypeSearch();
            this.CB_RoleName.Hide();
            this.GetListRole();
            this.dgv_Users.DataSource = UserDAO.Instance.GetListUser();
        }

        private void okok_Load(object sender, EventArgs e)
        {

        }

        // viet hàm khi bấm 2 lần vào 1 dòng trong datagridview
        private void dgv_Users_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có click đúng vào 1 dòng hợp lệ
            if (e.RowIndex >= 0)
            {
                // Lấy dòng hiện tại
                DataGridViewRow row = dgv_Users.Rows[e.RowIndex];

                // Lấy giá trị UserId (giả sử cột tên là "UserId")
                int id = Convert.ToInt32(row.Cells["user_id"].Value);

                // Mở form CUDUser với id
                CUDUser cudUserForm = new CUDUser(id);
                cudUserForm.ShowDialog();
                this.dgv_Users.DataSource = UserDAO.Instance.GetListUser();
            }
        }

        public void GetListRole()
        {
            var roles = RoleDAO.Instance.GetListRole();
            CB_RoleName.DataSource = roles;
            CB_RoleName.DisplayMember = "role_name";
            CB_RoleName.ValueMember = "role_id";
        }


        public void LoadTypeSearch()
        {
            // Tạo dictionary chứa DisplayMember (hiển thị) và ValueMember (giá trị)
            var typeSearch = new Dictionary<string, string>()
            {
                {"--Chọn kiểu tìm kiếm--","0"},
                { "Mã nhân viên", "user_id" },
                { "Tên nhân viên", "full_name" },
                { "SDT", "phone" },
                { "Chức vụ", "role_name" }
            };

            CB_TypeSearch.DataSource = new BindingSource(typeSearch, null);
            CB_TypeSearch.DisplayMember = "Key";   // Hiển thị cho user
            CB_TypeSearch.ValueMember = "Value";   // Lưu giá trị thực
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu người dùng nhập
                string searchValue = txt_infoSearch.Text.Trim();

                // Lấy kiểu tìm kiếm được chọn
                string selectedType = CB_TypeSearch.SelectedValue.ToString();

                // Gọi DAO
                var users = UserDAO.Instance.GetListUserByKey(selectedType, searchValue);

                // Bind vào DataGridView
                this.dgv_Users.DataSource = users;
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


        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            CUDUser cudUserForm = new CUDUser();
            cudUserForm.ShowDialog();
            this.dgv_Users.DataSource = UserDAO.Instance.GetListUser();
        }

        private void CB_TypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_TypeSearch.SelectedValue != null)
            {
                string selectedValue = CB_TypeSearch.SelectedValue.ToString();

                if (selectedValue == "role_name")
                {
                    txt_infoSearch.Hide();
                    CB_RoleName.Show();
                }
                else
                {
                    txt_infoSearch.Show();
                    CB_RoleName.Hide();
                }
            }
            this.dgv_Users.DataSource = UserDAO.Instance.GetListUser();
        }

        private void CB_RoleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgv_Users.DataSource = UserDAO.Instance.GetListUserByRoleName(CB_RoleName.Text);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.dgv_Users.DataSource = UserDAO.Instance.GetListUser();
            txt_infoSearch.Show();
            CB_RoleName.Hide();
        }

        private void btn_Overview_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tổng quan số lượng nhân viên theo chức vụ
                var overview = UserDAO.Instance.GetUserCountByRoleName();
                
                // Hiển thị lên DataGridView
                this.dgv_Users.DataSource = overview;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
