using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManagement.DAO;
using UserManagement.DTO;
using UserManagement.Entity;

namespace UserManagement.View.User
{
    public partial class CUDUser : Form
    {
        private int User_id;
        public CUDUser()
        {
            InitializeComponent();
            this.GetListRole();
            this.User_id = -1;
        }
        public CUDUser(int id)
        {
            InitializeComponent();
            this.GetListRole();
            this.User_id = id;
            LoadInfoUser();
        }


        void LoadInfoUser()
        {
            UserEntity user = UserDAO.Instance.GetUserById(this.User_id);
            if (user == null)
            {
                MessageBox.Show("Không tìm thấy người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Điền dữ liệu vào các control trên form
            txt_UserName.Text = user.full_name;
            DTP_DateOfBirth.Value = user.dob ?? DateTime.Now; // Nếu dob là null, đặt giá trị mặc định
            txt_PhoneNumber.Text = user.phone;
            txt_email.Text = user.email;
            if (cb_Role.DataSource != null)
            {
                cb_Role.SelectedValue = user.Role_id;
            }
            txt_address.Text = user.address;
            DTP_DateBegin.Value = user.hire_date ?? DateTime.Now;
            if (user.Gender)   // true = Nam
            {
                Radio_Male.Checked = true;
                radio_Female.Checked = false;
            }
            else               // false = Nữ
            {
                Radio_Male.Checked = false;
                radio_Female.Checked = true;
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            // Lấy trực tiếp dữ liệu từ control
            string fullName = txt_UserName.Text.Trim();
            DateTime dob = DTP_DateOfBirth.Value;
            string phone = txt_PhoneNumber.Text.Trim();
            string email = txt_email.Text.Trim();
            int roleId = Convert.ToInt32(cb_Role.SelectedValue);
            DateTime hireDate = DTP_DateBegin.Value;
            bool gender = Radio_Male.Checked; // true = Nam, false = Nữ
            string address = txt_address.Text.Trim();

            // set du lieu vao entity

            // set dữ liệu vào entity (UserDTO)
            UserEntity user = new UserEntity()
            {
                user_id = this.User_id, // Nếu là thêm mới, có thể để mặc định hoặc 0
                full_name = fullName,
                dob = dob,
                phone = phone,
                email = email,
                Role_id = roleId,
                hire_date = hireDate,
                Gender = gender,
                address = address
            };

            // Gọi DAO
            try
            {
                if(user.user_id == null || user.user_id ==-1)
                {
                    UserDAO.Instance.InsertUser(user);
                    MessageBox.Show("Thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    UserDAO.Instance.InsertUser(user);
                    MessageBox.Show("Cập nhật nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void GetListRole()
        {
            var roles = RoleDAO.Instance.GetListRole();
            cb_Role.DataSource = roles;
            cb_Role.DisplayMember = "role_name";
            cb_Role.ValueMember = "role_id";
        }


        private void btn_PhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if(this.User_id == null || this.User_id == -1)
            {
                MessageBox.Show("Không tìm thấy ID nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    DialogResult result = MessageBox.Show(
                                                "Bạn có chắc chắn muốn xóa nhân viên này không?",
                                                "Xác nhận xóa",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question
                                            );

                    // Gọi DAO để update status = 0d);

                    if (result == DialogResult.Yes)
                    {
                        UserDAO.Instance.DeleteUser(this.User_id);
                        MessageBox.Show("Xóa nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }
    }
}
