namespace UserManagement.View.Salary
{
    partial class BonusPenDetailForUsers
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_StaffId = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_NameStaff = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dvg_relateUser = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_ReadPenalty = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_ReadBonus = new Guna.UI2.WinForms.Guna2GradientButton();
            this.DTP_month_filter = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btn_AddBonus = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvg_relateUser)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(37, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã nhân viên:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(37, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên nhân viên:";
            // 
            // txt_StaffId
            // 
            this.txt_StaffId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_StaffId.DefaultText = "";
            this.txt_StaffId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_StaffId.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_StaffId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_StaffId.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_StaffId.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_StaffId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_StaffId.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_StaffId.Location = new System.Drawing.Point(191, 13);
            this.txt_StaffId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_StaffId.Name = "txt_StaffId";
            this.txt_StaffId.PlaceholderText = "";
            this.txt_StaffId.SelectedText = "";
            this.txt_StaffId.Size = new System.Drawing.Size(574, 39);
            this.txt_StaffId.TabIndex = 2;
            // 
            // txt_NameStaff
            // 
            this.txt_NameStaff.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_NameStaff.DefaultText = "";
            this.txt_NameStaff.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_NameStaff.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_NameStaff.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_NameStaff.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_NameStaff.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_NameStaff.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_NameStaff.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_NameStaff.Location = new System.Drawing.Point(191, 77);
            this.txt_NameStaff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_NameStaff.Name = "txt_NameStaff";
            this.txt_NameStaff.PlaceholderText = "";
            this.txt_NameStaff.SelectedText = "";
            this.txt_NameStaff.Size = new System.Drawing.Size(574, 39);
            this.txt_NameStaff.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dvg_relateUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 362);
            this.panel1.TabIndex = 4;
            // 
            // dvg_relateUser
            // 
            this.dvg_relateUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvg_relateUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvg_relateUser.Location = new System.Drawing.Point(0, 0);
            this.dvg_relateUser.Name = "dvg_relateUser";
            this.dvg_relateUser.RowHeadersWidth = 51;
            this.dvg_relateUser.RowTemplate.Height = 24;
            this.dvg_relateUser.Size = new System.Drawing.Size(944, 362);
            this.dvg_relateUser.TabIndex = 0;
            this.dvg_relateUser.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvg_relateUser_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(44, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 20);
            this.label6.TabIndex = 44;
            this.label6.Text = "Thông tin ngày làm";
            // 
            // btn_ReadPenalty
            // 
            this.btn_ReadPenalty.BorderRadius = 10;
            this.btn_ReadPenalty.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ReadPenalty.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ReadPenalty.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ReadPenalty.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ReadPenalty.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ReadPenalty.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_ReadPenalty.ForeColor = System.Drawing.Color.White;
            this.btn_ReadPenalty.Location = new System.Drawing.Point(437, 142);
            this.btn_ReadPenalty.Name = "btn_ReadPenalty";
            this.btn_ReadPenalty.Size = new System.Drawing.Size(149, 42);
            this.btn_ReadPenalty.TabIndex = 43;
            this.btn_ReadPenalty.Text = "Xem phạt";
            this.btn_ReadPenalty.Click += new System.EventHandler(this.btn_ReadPenalty_Click_1);
            // 
            // btn_ReadBonus
            // 
            this.btn_ReadBonus.BorderRadius = 10;
            this.btn_ReadBonus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ReadBonus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ReadBonus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ReadBonus.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ReadBonus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ReadBonus.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_ReadBonus.ForeColor = System.Drawing.Color.White;
            this.btn_ReadBonus.Location = new System.Drawing.Point(266, 142);
            this.btn_ReadBonus.Name = "btn_ReadBonus";
            this.btn_ReadBonus.Size = new System.Drawing.Size(150, 42);
            this.btn_ReadBonus.TabIndex = 42;
            this.btn_ReadBonus.Text = "Xem thưởng";
            this.btn_ReadBonus.Click += new System.EventHandler(this.btn_ReadBonus_Click_1);
            // 
            // DTP_month_filter
            // 
            this.DTP_month_filter.Checked = true;
            this.DTP_month_filter.CustomFormat = "MM/yy";
            this.DTP_month_filter.FillColor = System.Drawing.Color.White;
            this.DTP_month_filter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DTP_month_filter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_month_filter.Location = new System.Drawing.Point(42, 142);
            this.DTP_month_filter.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.DTP_month_filter.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DTP_month_filter.Name = "DTP_month_filter";
            this.DTP_month_filter.Size = new System.Drawing.Size(200, 42);
            this.DTP_month_filter.TabIndex = 41;
            this.DTP_month_filter.Value = new System.DateTime(2025, 9, 19, 17, 46, 22, 707);
            // 
            // btn_AddBonus
            // 
            this.btn_AddBonus.BorderRadius = 10;
            this.btn_AddBonus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_AddBonus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_AddBonus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_AddBonus.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_AddBonus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_AddBonus.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_AddBonus.ForeColor = System.Drawing.Color.White;
            this.btn_AddBonus.Location = new System.Drawing.Point(616, 142);
            this.btn_AddBonus.Name = "btn_AddBonus";
            this.btn_AddBonus.Size = new System.Drawing.Size(149, 42);
            this.btn_AddBonus.TabIndex = 45;
            this.btn_AddBonus.Text = "Thêm Thưởng/Phạt";
            this.btn_AddBonus.Click += new System.EventHandler(this.btn_AddBonus_Click);
            // 
            // BonusPenDetailForUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 572);
            this.Controls.Add(this.btn_AddBonus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_ReadPenalty);
            this.Controls.Add(this.btn_ReadBonus);
            this.Controls.Add(this.DTP_month_filter);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txt_NameStaff);
            this.Controls.Add(this.txt_StaffId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "BonusPenDetailForUsers";
            this.Text = "BonusPenDetailForUsers";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvg_relateUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txt_StaffId;
        private Guna.UI2.WinForms.Guna2TextBox txt_NameStaff;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dvg_relateUser;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2GradientButton btn_ReadPenalty;
        private Guna.UI2.WinForms.Guna2GradientButton btn_ReadBonus;
        private Guna.UI2.WinForms.Guna2DateTimePicker DTP_month_filter;
        private Guna.UI2.WinForms.Guna2GradientButton btn_AddBonus;
    }
}