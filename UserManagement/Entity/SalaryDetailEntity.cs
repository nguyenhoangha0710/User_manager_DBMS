using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class SalaryDetailEntity
    {
        public int salary_id { get; set; }
        public int user_id { get; set; }
        public string full_name { get; set; }

        public String role_name { get; set; }


        public int month { get; set; }

        public int year { get; set; }

        public decimal? total_hours {  get; set; } 
        public decimal? total_bonus { get; set; }
        public decimal? total_penalty { get; set; }
        public decimal? final_salary { get; set; }
    }
}
