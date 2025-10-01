using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.DAO
{
    public class RoleDAO
    {
        ConnectDataContext conn = new ConnectDataContext();


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
            return conn.Roles.Select(r => new DTO.RoleDTO
            {
                role_id = r.role_id,
                role_name = r.role_name,
                salary = r.salary
            }).ToList();
        }

        public String GetRoleNameById(int role_id)
        {
            var role = conn.Roles.FirstOrDefault(r => r.role_id == role_id);
            return role != null ? role.role_name : null;
        }


    }
}
