namespace UserManagement
{
    partial class SalaryManagement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dvg_Salary = new System.Windows.Forms.DataGridView();
            this.DTP_MonthFilter = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btn_ReadAllSalary = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_UpdateSalary = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_payment = new Guna.UI2.WinForms.Guna2GradientButton();
            ((System.ComponentModel.ISupportInitialize)(this.dvg_Salary)).BeginInit();
            this.SuspendLayout();
            // 
            // dvg_Salary
            // 
            this.dvg_Salary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvg_Salary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvg_Salary.Location = new System.Drawing.Point(0, 196);
            this.dvg_Salary.Name = "dvg_Salary";
            this.dvg_Salary.RowHeadersWidth = 51;
            this.dvg_Salary.RowTemplate.Height = 24;
            this.dvg_Salary.Size = new System.Drawing.Size(1201, 457);
            this.dvg_Salary.TabIndex = 0;
            // 
            // DTP_MonthFilter
            // 
            this.DTP_MonthFilter.BorderRadius = 20;
            this.DTP_MonthFilter.Checked = true;
            this.DTP_MonthFilter.CustomFormat = "MM/yy";
            this.DTP_MonthFilter.FillColor = System.Drawing.Color.White;
            this.DTP_MonthFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DTP_MonthFilter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_MonthFilter.Location = new System.Drawing.Point(22, 21);
            this.DTP_MonthFilter.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.DTP_MonthFilter.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DTP_MonthFilter.Name = "DTP_MonthFilter";
            this.DTP_MonthFilter.Size = new System.Drawing.Size(200, 50);
            this.DTP_MonthFilter.TabIndex = 1;
            this.DTP_MonthFilter.Value = new System.DateTime(2025, 9, 19, 17, 18, 26, 137);
            this.DTP_MonthFilter.ValueChanged += new System.EventHandler(this.DTP_MonthFilter_ValueChanged);
            // 
            // btn_ReadAllSalary
            // 
            this.btn_ReadAllSalary.BorderRadius = 20;
            this.btn_ReadAllSalary.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ReadAllSalary.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ReadAllSalary.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ReadAllSalary.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ReadAllSalary.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ReadAllSalary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_ReadAllSalary.ForeColor = System.Drawing.Color.White;
            this.btn_ReadAllSalary.Location = new System.Drawing.Point(257, 21);
            this.btn_ReadAllSalary.Name = "btn_ReadAllSalary";
            this.btn_ReadAllSalary.Size = new System.Drawing.Size(180, 50);
            this.btn_ReadAllSalary.TabIndex = 2;
            this.btn_ReadAllSalary.Text = "Xem lương";
            this.btn_ReadAllSalary.Click += new System.EventHandler(this.btn_ReadAllSalary_Click);
            // 
            // btn_UpdateSalary
            // 
            this.btn_UpdateSalary.BorderRadius = 20;
            this.btn_UpdateSalary.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_UpdateSalary.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_UpdateSalary.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_UpdateSalary.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_UpdateSalary.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_UpdateSalary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_UpdateSalary.ForeColor = System.Drawing.Color.White;
            this.btn_UpdateSalary.Location = new System.Drawing.Point(467, 21);
            this.btn_UpdateSalary.Name = "btn_UpdateSalary";
            this.btn_UpdateSalary.Size = new System.Drawing.Size(180, 50);
            this.btn_UpdateSalary.TabIndex = 3;
            this.btn_UpdateSalary.Text = "Cập nhật lương";
            this.btn_UpdateSalary.Click += new System.EventHandler(this.btn_UpdateSalary_Click);
            // 
            // btn_payment
            // 
            this.btn_payment.BorderRadius = 20;
            this.btn_payment.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_payment.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_payment.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_payment.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_payment.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_payment.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_payment.ForeColor = System.Drawing.Color.White;
            this.btn_payment.Location = new System.Drawing.Point(682, 21);
            this.btn_payment.Name = "btn_payment";
            this.btn_payment.Size = new System.Drawing.Size(180, 50);
            this.btn_payment.TabIndex = 4;
            this.btn_payment.Text = "Thanh toán lương";
            this.btn_payment.Click += new System.EventHandler(this.btn_payment_Click);
            // 
            // SalaryManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_payment);
            this.Controls.Add(this.btn_UpdateSalary);
            this.Controls.Add(this.btn_ReadAllSalary);
            this.Controls.Add(this.DTP_MonthFilter);
            this.Controls.Add(this.dvg_Salary);
            this.Name = "SalaryManagement";
            this.Size = new System.Drawing.Size(1201, 653);
            this.Load += new System.EventHandler(this.SalaryManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dvg_Salary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dvg_Salary;
        private Guna.UI2.WinForms.Guna2DateTimePicker DTP_MonthFilter;
        private Guna.UI2.WinForms.Guna2GradientButton btn_ReadAllSalary;
        private Guna.UI2.WinForms.Guna2GradientButton btn_UpdateSalary;
        private Guna.UI2.WinForms.Guna2GradientButton btn_payment;
    }
}
