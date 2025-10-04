using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Entity;

namespace UserManagement.DAO
{
    public class AllowanceAndPenaltyDAO
    {
        private static AllowanceAndPenaltyDAO instance;

        public static AllowanceAndPenaltyDAO Instance
        {
            get { if (instance == null) instance = new AllowanceAndPenaltyDAO(); return instance; }
            private set { instance = value; }
        }
        private ConnectDataContext GetContext()
        {
            // Luôn tạo mới DataContext với chuỗi kết nối của người dùng đang đăng nhập
            return new ConnectDataContext(UserSession.MainConnectionString);
        }


        private AllowanceAndPenaltyDAO() { }

        public void InsertAllowanceAndPenalty(AllowanceAndPenaltyEntity data)
        {
            using (var conn = GetContext())
            {
                // Gọi stored procedure đã tạo
                conn.sp_InsertAllowanceAndPenalty(
                    data.UserId,
                    data.Month,
                    data.Year,
                    data.Amount,
                    data.Type,
                    data.Reason
                );
            }
        }


        public AllowanceAndPenaltyEntity GetById(int id)
        {
            using (var conn = GetContext())
            {
                // Sử dụng Linq to SQL để truy vấn
                var dbRecord = conn.AllowanceAndPenalties.FirstOrDefault(r => r.id == id);
                if (dbRecord == null) return null;

                // Map dữ liệu sang Entity
                return new AllowanceAndPenaltyEntity
                {
                    Id = dbRecord.id,
                    UserId = dbRecord.user_id,
                    Month = dbRecord.month,
                    Year = dbRecord.year,
                    Amount = dbRecord.amount,
                    Reason = dbRecord.reason,
                    Type = dbRecord.type
                };
            }
        }

        // Phương thức Sửa mới
        public void UpdateAllowanceAndPenalty(AllowanceAndPenaltyEntity data)
        {
            using (var conn = GetContext())
            {
                conn.sp_UpdateAllowanceAndPenalty(
                    data.Id, data.Month, data.Year,
                    data.Amount, data.Type, data.Reason
                );
            }
        }

        // Phương thức Xóa mới
        public void DeleteAllowanceAndPenalty(int id)
        {
            using (var conn = GetContext())
            {
                conn.sp_DeleteAllowanceAndPenalty(id);
            }
        }

    }
}
