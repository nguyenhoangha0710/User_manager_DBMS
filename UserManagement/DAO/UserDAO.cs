using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserManagement.Entity;

namespace UserManagement.DTO
{
    public class UserDAO
    {
        ConnectDataContext hihi = new ConnectDataContext();


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
            using (var db = new ConnectDataContext()) {
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
                    //GenderDisplay = v.Gender ? "Nam" : "Nữ"
                }).ToList();
            }

        }

        // viet ham lay danh sach user theo role_id
        public List<UserWithRole> GetListUserByRoleName(String name)
        {
            using (var conn = new ConnectDataContext())
            {
                return conn.v_UserWithRoles.
                    Where(v => v.role_name == name).
                    Select(v => new UserWithRole
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

        // Tim kiem user theo key va value
        public List<UserWithRole> GetListUserByKey(String key, String value)
        {
            using (var conn = new ConnectDataContext())
            {
                var result = conn.fn_SearchUser(key, value);
                return result.
                    Select(v => new UserWithRole
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


        // viet ham them user 
        public void InsertUser(UserEntity user, string username, string password, string securityRoleName)
        {
            using (var conn = new ConnectDataContext())
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

        // viet ham lay user theo id
        public UserEntity GetUserById(int id)
        {
            using (var conn = new ConnectDataContext())
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
                    Role_id = dbUser.role_id   // chú ý nếu tên khác, phải map đúng
                };

                return user;
            }
        }


        // viet ham xoa user theo id
        public void DeleteUser(int id)
        {
            using (var conn = new ConnectDataContext())
            {
                conn.DisableUserById(id);
            }
        }

        /// <summary>
        /// Lấy tổng quan số lượng nhân viên theo chức vụ
        /// </summary>
        public List<UserCountByRole> GetUserCountByRoleName()
        {
            using (var conn = new ConnectDataContext())
            {
                var result = conn.fn_GetUserCountByRoleName();
                return result.Select(x => new UserCountByRole
                {
                    role_name = x.role_name,
                    NumberOfUsers = x.NumberOfUsers ?? 0
                }).ToList();
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
