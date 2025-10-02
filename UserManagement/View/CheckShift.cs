using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManagement.DAO;

namespace UserManagement.View
{
    public partial class CheckShift : UserControl
    {
        public CheckShift()
        {
            InitializeComponent();
        }

        private void btn_Checkin_Click(object sender, EventArgs e)
        {
            try
            {
                string result = AttendanceDAO.Instance.UserCheckIn();
                MessageBox.Show(result, "Thông báo", MessageBoxButtons.OK, 
                    result.Contains("thành công") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Checkout_Click(object sender, EventArgs e)
        {
            try
            {
                string result = AttendanceDAO.Instance.UserCheckOut();
                MessageBox.Show(result, "Thông báo", MessageBoxButtons.OK,
                    result.Contains("thành công") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
