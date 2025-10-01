using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class AttendenceEntity
    {
        public AttendenceEntity() { }
        public int AttendenceId { get; set; }
        public int UserId { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal? WorkHours { get; set; }
        public string Note { get; set; }

    }
}
