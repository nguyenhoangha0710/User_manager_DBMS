using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManagement.DAO;
using UserManagement.View.Account;

namespace UserManagement.View.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txt_UserName.Text;
            string password = txt_PassWord.Text;

            // Lấy chuỗi kết nối xác thực từ App.config
            string authConnectionString = ConfigurationManager.ConnectionStrings["AuthConnection"].ConnectionString;

            // Dùng chuỗi kết nối quyền thấp này để gọi sp_Login
            using (var authContext = new ConnectDataContext(authConnectionString))
            {
                try
                {
                    var userResult = authContext.sp_Login(username, password).FirstOrDefault();
                    //String Role = authContext.fn_Login_Secure(username, password);
                    MessageBox.Show(userResult != null ? "Đăng nhập thành công!" : "Đăng nhập thất bại!");


                    if (userResult != null && userResult.AccountStatus == true)
                    {
                        // ĐĂNG NHẬP THÀNH CÔNG!

                        // Xây dựng chuỗi kết nối chính bằng chính tài khoản người dùng
                        string mainConnTemplate = ConfigurationManager.ConnectionStrings["MainConnectionTemplate"].ConnectionString;
                        string mainConnectionString = string.Format(mainConnTemplate, username, password);

                        // Lưu tất cả thông tin vào phiên làm việc
                        UserSession.StartSession(
                            userResult.user_id,
                            userResult.full_name,
                            userResult.SecurityRole,
                            mainConnectionString // Truyền chuỗi kết nối chính vào
                        );

                        Home mainForm = new Home();
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại hoặc tài khoản đã bị khóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi kết nối hoặc xác thực: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
