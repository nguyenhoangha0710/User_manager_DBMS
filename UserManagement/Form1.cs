using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UserManagement.View.Login;

namespace UserManagement
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.userUS1.Show();
            this.salaryManagement1.Hide();
            this.accountManagement1.Hide();
            this.checkShift1.Hide();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            this.salaryManagement1.Show();
            this.userUS1.Hide();
            this.accountManagement1.Hide();
            this.checkShift1.Hide();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.userUS1.Show();
            this.salaryManagement1.Hide();
            this.accountManagement1.Hide();
            this.checkShift1.Hide();
        }

        private void btn_AccountManagement_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void btn_CheckShift_Click(object sender, EventArgs e)
        {
            this.checkShift1.Show();
            this.userUS1.Hide();
            this.salaryManagement1.Hide();
            this.accountManagement1.Hide();
        }

        private void btn_BonusOrPen_Click(object sender, EventArgs e)
        {
            this.checkShift1.Hide();
            this.userUS1.Hide();
            this.salaryManagement1.Hide();
            this.accountManagement1.Show();
        }
    }
}
