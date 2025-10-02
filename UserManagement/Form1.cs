using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.accountManagement1.Show();
            this.userUS1.Hide();
            this.salaryManagement1.Hide();
            this.checkShift1.Hide();
        }

        private void btn_CheckShift_Click(object sender, EventArgs e)
        {
            this.checkShift1.Show();
            this.userUS1.Hide();
            this.salaryManagement1.Hide();
            this.accountManagement1.Hide();
        }
    }
}
