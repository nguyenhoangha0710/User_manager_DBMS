using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.DTO
{
    public class RoleDTO
    {
        public RoleDTO() { }
        public int role_id { get; set; }
        public String role_name { get; set; }
        public decimal? salary { get; set; }
    }
}
