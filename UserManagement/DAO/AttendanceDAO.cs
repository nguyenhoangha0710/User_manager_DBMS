using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UserManagement.DAO
{
    public class AttendanceDAO
    {
        private static AttendanceDAO instance;

        public static AttendanceDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AttendanceDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }


        private ConnectDataContext GetContext()
        {
            // Luôn tạo mới DataContext với chuỗi kết nối của người dùng đang đăng nhập
            return new ConnectDataContext(UserSession.MainConnectionString);
        }

        private AttendanceDAO() { }

        /// <summary>
        /// Gọi stored procedure sp_UserCheckIn để check-in cho user hiện tại
        /// </summary>
        /// <returns>Thông báo kết quả</returns>
        public string UserCheckIn()
        {
            try
            {
                using (ConnectDataContext conn = GetContext())
                {
                    var result = conn.sp_UserCheckIn();
                    return "Check-in thành công!";
                }
            }
            catch (SqlException ex)
            {
                // Bắt lỗi từ RAISERROR trong stored procedure
                return ex.Message;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        /// <summary>
        /// Gọi stored procedure sp_UserCheckOut để check-out cho user hiện tại
        /// </summary>
        /// <returns>Thông báo kết quả</returns>
        public string UserCheckOut()
        {
            try
            {
                using (ConnectDataContext conn = GetContext())
                {
                    var result = conn.sp_UserCheckOut();
                    return "Check-out thành công!";
                }
            }
            catch (SqlException ex)
            {
                // Bắt lỗi từ RAISERROR trong stored procedure
                return ex.Message;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
