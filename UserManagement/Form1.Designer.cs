namespace UserManagement
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.btn_CheckShift = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_SalaryManagement = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_AccountManagement = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_UserManagement = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkShift1 = new UserManagement.View.CheckShift();
            this.accountManagement1 = new UserManagement.View.Account.AccountManagement();
            this.salaryManagement1 = new UserManagement.SalaryManagement();
            this.userUS1 = new UserManagement.UserUS();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.btn_CheckShift);
            this.guna2CustomGradientPanel1.Controls.Add(this.btn_SalaryManagement);
            this.guna2CustomGradientPanel1.Controls.Add(this.btn_AccountManagement);
            this.guna2CustomGradientPanel1.Controls.Add(this.btn_UserManagement);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2CustomGradientPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(240, 753);
            this.guna2CustomGradientPanel1.TabIndex = 0;
            // 
            // btn_CheckShift
            // 
            this.btn_CheckShift.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_CheckShift.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_CheckShift.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_CheckShift.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_CheckShift.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_CheckShift.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_CheckShift.ForeColor = System.Drawing.Color.White;
            this.btn_CheckShift.Location = new System.Drawing.Point(5, 467);
            this.btn_CheckShift.Margin = new System.Windows.Forms.Padding(5);
            this.btn_CheckShift.Name = "btn_CheckShift";
            this.btn_CheckShift.Size = new System.Drawing.Size(227, 73);
            this.btn_CheckShift.TabIndex = 3;
            this.btn_CheckShift.Text = "Điểm danh ca làm";
            this.btn_CheckShift.Click += new System.EventHandler(this.btn_CheckShift_Click);
            // 
            // btn_SalaryManagement
            // 
            this.btn_SalaryManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_SalaryManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_SalaryManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_SalaryManagement.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_SalaryManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_SalaryManagement.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_SalaryManagement.ForeColor = System.Drawing.Color.White;
            this.btn_SalaryManagement.Location = new System.Drawing.Point(5, 269);
            this.btn_SalaryManagement.Margin = new System.Windows.Forms.Padding(5);
            this.btn_SalaryManagement.Name = "btn_SalaryManagement";
            this.btn_SalaryManagement.Size = new System.Drawing.Size(227, 73);
            this.btn_SalaryManagement.TabIndex = 2;
            this.btn_SalaryManagement.Text = "Quản lý Lương";
            this.btn_SalaryManagement.Click += new System.EventHandler(this.guna2GradientButton3_Click);
            // 
            // btn_AccountManagement
            // 
            this.btn_AccountManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_AccountManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_AccountManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_AccountManagement.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_AccountManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_AccountManagement.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_AccountManagement.ForeColor = System.Drawing.Color.White;
            this.btn_AccountManagement.Location = new System.Drawing.Point(5, 368);
            this.btn_AccountManagement.Margin = new System.Windows.Forms.Padding(5);
            this.btn_AccountManagement.Name = "btn_AccountManagement";
            this.btn_AccountManagement.Size = new System.Drawing.Size(227, 73);
            this.btn_AccountManagement.TabIndex = 1;
            this.btn_AccountManagement.Text = "Quản lý tái khoản";
            this.btn_AccountManagement.Click += new System.EventHandler(this.btn_AccountManagement_Click);
            // 
            // btn_UserManagement
            // 
            this.btn_UserManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_UserManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_UserManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_UserManagement.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_UserManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_UserManagement.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_UserManagement.ForeColor = System.Drawing.Color.White;
            this.btn_UserManagement.Location = new System.Drawing.Point(5, 170);
            this.btn_UserManagement.Margin = new System.Windows.Forms.Padding(5);
            this.btn_UserManagement.Name = "btn_UserManagement";
            this.btn_UserManagement.Size = new System.Drawing.Size(227, 73);
            this.btn_UserManagement.TabIndex = 0;
            this.btn_UserManagement.Text = "Quản lý nhân viên";
            this.btn_UserManagement.Click += new System.EventHandler(this.guna2GradientButton1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(240, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1242, 60);
            this.panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(205, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 34);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "hoangha ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkShift1);
            this.panel2.Controls.Add(this.accountManagement1);
            this.panel2.Controls.Add(this.salaryManagement1);
            this.panel2.Controls.Add(this.userUS1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(240, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1242, 693);
            this.panel2.TabIndex = 2;
            // 
            // checkShift1
            // 
            this.checkShift1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkShift1.Location = new System.Drawing.Point(0, 0);
            this.checkShift1.Margin = new System.Windows.Forms.Padding(5);
            this.checkShift1.Name = "checkShift1";
            this.checkShift1.Size = new System.Drawing.Size(1242, 693);
            this.checkShift1.TabIndex = 3;
            // 
            // accountManagement1
            // 
            this.accountManagement1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accountManagement1.Location = new System.Drawing.Point(0, 0);
            this.accountManagement1.Margin = new System.Windows.Forms.Padding(5);
            this.accountManagement1.Name = "accountManagement1";
            this.accountManagement1.Size = new System.Drawing.Size(1242, 693);
            this.accountManagement1.TabIndex = 2;
            // 
            // salaryManagement1
            // 
            this.salaryManagement1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.salaryManagement1.Location = new System.Drawing.Point(0, 0);
            this.salaryManagement1.Margin = new System.Windows.Forms.Padding(5);
            this.salaryManagement1.Name = "salaryManagement1";
            this.salaryManagement1.Size = new System.Drawing.Size(1242, 693);
            this.salaryManagement1.TabIndex = 1;
            // 
            // userUS1
            // 
            this.userUS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userUS1.Location = new System.Drawing.Point(0, 0);
            this.userUS1.Margin = new System.Windows.Forms.Padding(5);
            this.userUS1.Name = "userUS1";
            this.userUS1.Size = new System.Drawing.Size(1242, 693);
            this.userUS1.TabIndex = 0;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 753);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Home";
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Home_Load);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2GradientButton btn_UserManagement;
        private Guna.UI2.WinForms.Guna2GradientButton btn_CheckShift;
        private Guna.UI2.WinForms.Guna2GradientButton btn_SalaryManagement;
        private Guna.UI2.WinForms.Guna2GradientButton btn_AccountManagement;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private UserUS userUS1;
        private System.Windows.Forms.TextBox textBox1;
        private SalaryManagement salaryManagement1;
        private View.Account.AccountManagement accountManagement1;
        private View.CheckShift checkShift1;
    }
}

