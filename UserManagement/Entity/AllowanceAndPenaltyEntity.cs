using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class AllowanceAndPenaltyEntity
    {
        public int Id { get; set; }           // id của bản ghi
        public int UserId { get; set; }       // id của nhân viên
        public int Month { get; set; }        // tháng
        public int Year { get; set; }         // năm
        public decimal Amount { get; set; }   // số tiền thưởng hoặc phạt
        public String Reason { get; set; }    // lý do
        public bool? Type { get; set; }      // "Allowance" hoặc "Penalty"
    }
}
