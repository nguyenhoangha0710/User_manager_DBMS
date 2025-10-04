using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserManagement.DAO;
using UserManagement.Entity;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UserManagement.DTO
{
    public class UserDAO
    {

        // Tạo một phương thức private để lấy DataContext mới một cách nhanh chóng
        private ConnectDataContext GetContext()
        {
            // Luôn tạo mới DataContext với chuỗi kết nối của người dùng đang đăng nhập
            return new ConnectDataContext(UserSession.MainConnectionString);
        }

        private static UserDAO instance;
        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        private UserDAO() { }

        public List<UserWithRole> GetListUser()
        {
            try
            {
                using (var db = GetContext())
                {
                    return db.v_UserWithRoles.Select(v => new UserWithRole
                    {
                        user_id = v.user_id,
                        full_name = v.full_name,
                        dob = v.dob,
                        Gender = v.Gender,
                        email = v.email,
                        phone = v.phone,
                        hire_date = v.hire_date,
                        role_name = v.role_name
                    }).ToList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserWithRole>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserWithRole>();
            }

        }

        // viet ham lay danh sach user theo role_id
        public List<UserWithRole> GetListUserByRoleName(String name)
        {
            try
            {
                using (var conn = GetContext())
                {
                    return conn.v_UserWithRoles
                        .Where(v => v.role_name == name)
                        .Select(v => new UserWithRole
                        {
                            user_id = v.user_id,
                            full_name = v.full_name,
                            dob = v.dob,
                            Gender = v.Gender,
                            email = v.email,
                            phone = v.phone,
                            hire_date = v.hire_date,
                            role_name = v.role_name
                        }).ToList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserWithRole>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserWithRole>();
            }
        }

        // Tim kiem user theo key va value
        public List<UserWithRole> GetListUserByKey(String key, String value)
        {
            try
            {
                using (var conn = GetContext())
                {
                    var result = conn.fn_SearchUser(key, value);
                    return result
                        .Select(v => new UserWithRole
                        {
                            user_id = v.user_id.GetValueOrDefault(),
                            full_name = v.full_name,
                            dob = v.dob,
                            Gender = v.Gender.GetValueOrDefault(),
                            email = v.email,
                            phone = v.phone,
                            hire_date = v.hire_date,
                            role_name = v.role_name
                        }).ToList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserWithRole>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserWithRole>();
            }
        }


        // viet ham them user 
        public void InsertUser(UserEntity user, string username, string password, string securityRoleName)
        {
            try
            {
                using (var conn = GetContext())
                {
                    if (user.user_id == null || user.user_id == -1)
                    {
                        // sp_CreateNew_EmployeeWithAccount parameter order:
                        // @FullName, @dob, @phone, @Email, @HireDate, @Gender, @Address,
                        // @JobRoleID, @SecurityRoleName, @Username, @Password
                        conn.sp_CreateNew_EmployeeWithAccount(
                            user.full_name,
                            user.dob,
                            user.phone,
                            user.email,
                            user.hire_date,
                            user.Gender,
                            user.address,
                            user.Role_id,
                            securityRoleName,
                            username,
                            password
                        );
                    }
                    else
                    {
                        conn.sp_UpdateUserManagement(
                            user.user_id,
                            user.full_name,
                            user.dob,
                            user.phone,
                            user.email,
                            user.Role_id,
                            user.hire_date,
                            user.Gender,
                            user.address);
                    }
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

        // viet ham lay user theo id
        public UserEntity GetUserById(int id)
        {
            try
            {
                using (var conn = GetContext())
                {
                    var dbUser = conn.Users.FirstOrDefault(u => u.user_id == id);
                    if (dbUser == null)
                        return null;

                    // Gán từng property thủ công để đảm bảo mapping đúng
                    UserEntity user = new UserEntity()
                    {
                        user_id = dbUser.user_id,
                        full_name = dbUser.full_name,
                        dob = dbUser.dob,
                        Gender = dbUser.Gender,
                        email = dbUser.email,
                        phone = dbUser.phone,
                        hire_date = dbUser.hire_date,
                        address = dbUser.address,
                        Role_id = dbUser.role_id
                    };

                    return user;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        // viet ham xoa user theo id
        public void DeleteUser(int id)
        {
            try
            {
                using (var conn = GetContext())
                {
                    // BƯỚC 1: Dùng LINQ để tìm bản ghi User cần xóa
                    var userToDelete = conn.Users.FirstOrDefault(user => user.user_id == id);

                    // BƯỚC 2: Kiểm tra xem người dùng có tồn tại không
                    if (userToDelete != null)
                    {
                        // BƯỚC 3: Đánh dấu đối tượng để xóa
                        conn.Users.DeleteOnSubmit(userToDelete);

                        // BƯỚC 4: Gửi lệnh xóa đến CSDL. 
                        // Lệnh này sẽ được dịch thành "DELETE FROM Users..." và kích hoạt trigger của bạn.
                        conn.SubmitChanges();
                    }
                    else
                    {
                        // Tùy chọn: Xử lý trường hợp không tìm thấy user
                        MessageBox.Show("Không tìm thấy người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
        /// Lấy tổng quan số lượng nhân viên theo chức vụ
        /// </summary>
        public List<UserCountByRole> GetUserCountByRoleName()
        {
            try
            {
                using (var conn = GetContext())
                {
                    var result = conn.fn_GetUserCountByRoleName();
                    return result.Select(x => new UserCountByRole
                    {
                        role_name = x.role_name,
                        NumberOfUsers = x.NumberOfUsers ?? 0
                    }).ToList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserCountByRole>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserCountByRole>();
            }
        }

    }
}

// Class để chứa kết quả tổng quan
public class UserCountByRole
{
    public string role_name { get; set; }
    public int NumberOfUsers { get; set; }
}
