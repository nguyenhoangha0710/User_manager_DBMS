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
                var result = conn.SearchUser(key, value);
                return result.
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


        // viet ham them user 
        public void InsertUser(UserEntity user)
        {
            using (var conn = new ConnectDataContext())
            {
                if (user.user_id == null || user.user_id == -1)
                {
                    conn.sp_AddUserManagement(
                    user.full_name,
                    user.dob,
                    user.phone,
                    user.email,
                    user.Role_id,
                    user.hire_date,
                    user.Gender,
                    user.address);
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

    }
}
