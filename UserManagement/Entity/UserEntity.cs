using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class UserEntity
    {
        public int user_id { get; set; }
        public String full_name { get; set; }

        public bool status { get; set; }

        public DateTime? dob { get; set; }
        public Boolean Gender { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public DateTime? hire_date { get; set; }

        public String address { get; set; }

        public int Role_id { get; set; }
    }
}
