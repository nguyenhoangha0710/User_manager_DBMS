using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.DAO
{
    public static class UserSession
    {
        public static int UserId { get; private set; }
        public static string FullName { get; private set; }
        public static string Role { get; private set; } // Sẽ lưu "QuanLy" hoặc "NhanVien"

        // Thuộc tính quan trọng nhất: lưu chuỗi kết nối của người dùng
        public static string MainConnectionString { get; private set; }

        public static void StartSession(int userId, string fullName, string role, string connectionString)
        {
            UserId = userId;
            FullName = fullName;
            Role = role;
            MainConnectionString = connectionString;
        }

        public static void EndSession()
        {
            UserId = 0;
            FullName = null;
            Role = null;
            MainConnectionString = null;
        }
    }
}
