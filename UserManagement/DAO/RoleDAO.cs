using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UserManagement.DAO
{
    public class RoleDAO
    {

        private ConnectDataContext GetContext()
        {
            // Luôn tạo mới DataContext với chuỗi kết nối của người dùng đang đăng nhập
            return new ConnectDataContext(UserSession.MainConnectionString);
        }

        private static RoleDAO instance;
        public static RoleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoleDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }


        // viet ham lay danh sach role
        public List<DTO.RoleDTO> GetListRole()
        {
            try
            {
                using (ConnectDataContext conn = GetContext())
                {
                    return conn.Roles.Select(r => new DTO.RoleDTO
                    {
                        role_id = r.role_id,
                        role_name = r.role_name,
                        salary = r.salary
                    }).ToList();
                }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<DTO.RoleDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<DTO.RoleDTO>();
            }
        }

        public String GetRoleNameById(int role_id)
        {
            try
            {
                using (ConnectDataContext conn = GetContext())
                {
                    var role = conn.Roles.FirstOrDefault(r => r.role_id == role_id);
                    return role != null ? role.role_name : null;
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


    }
}
