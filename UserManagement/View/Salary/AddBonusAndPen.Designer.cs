namespace UserManagement.View.Salary
{
    partial class AddBonusAndPen
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_StaffId = new System.Windows.Forms.TextBox();
            this.txt_NameStaff = new System.Windows.Forms.TextBox();
            this.txt_amount = new System.Windows.Forms.TextBox();
            this.txt_reason = new System.Windows.Forms.TextBox();
            this.DT_timeBonus = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cb_type = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btn_Add = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_Cancel = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(34, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã nhân viên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(34, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Loại:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(34, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Lý do:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(34, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Số tiền:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(34, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Thời gian:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(34, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên nhân viên:";
            // 
            // txt_StaffId
            // 
            this.txt_StaffId.Location = new System.Drawing.Point(175, 24);
            this.txt_StaffId.Name = "txt_StaffId";
            this.txt_StaffId.Size = new System.Drawing.Size(489, 22);
            this.txt_StaffId.TabIndex = 7;
            // 
            // txt_NameStaff
            // 
            this.txt_NameStaff.Location = new System.Drawing.Point(175, 62);
            this.txt_NameStaff.Name = "txt_NameStaff";
            this.txt_NameStaff.Size = new System.Drawing.Size(489, 22);
            this.txt_NameStaff.TabIndex = 8;
            // 
            // txt_amount
            // 
            this.txt_amount.Location = new System.Drawing.Point(175, 153);
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.Size = new System.Drawing.Size(489, 22);
            this.txt_amount.TabIndex = 9;
            // 
            // txt_reason
            // 
            this.txt_reason.Location = new System.Drawing.Point(175, 194);
            this.txt_reason.Name = "txt_reason";
            this.txt_reason.Size = new System.Drawing.Size(489, 22);
            this.txt_reason.TabIndex = 10;
            // 
            // DT_timeBonus
            // 
            this.DT_timeBonus.Checked = true;
            this.DT_timeBonus.CustomFormat = "dd/MM/yyyy";
            this.DT_timeBonus.FillColor = System.Drawing.Color.White;
            this.DT_timeBonus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DT_timeBonus.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_timeBonus.Location = new System.Drawing.Point(175, 98);
            this.DT_timeBonus.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.DT_timeBonus.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DT_timeBonus.Name = "DT_timeBonus";
            this.DT_timeBonus.Size = new System.Drawing.Size(265, 36);
            this.DT_timeBonus.TabIndex = 11;
            this.DT_timeBonus.Value = new System.DateTime(2025, 10, 3, 10, 29, 26, 839);
            // 
            // cb_type
            // 
            this.cb_type.BackColor = System.Drawing.Color.Transparent;
            this.cb_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_type.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cb_type.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cb_type.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cb_type.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cb_type.ItemHeight = 30;
            this.cb_type.Location = new System.Drawing.Point(175, 238);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(265, 36);
            this.cb_type.TabIndex = 12;
            // 
            // btn_Add
            // 
            this.btn_Add.BorderRadius = 15;
            this.btn_Add.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Add.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Add.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Add.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Add.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Add.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_Add.ForeColor = System.Drawing.Color.White;
            this.btn_Add.Location = new System.Drawing.Point(546, 294);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(149, 45);
            this.btn_Add.TabIndex = 13;
            this.btn_Add.Text = "Cập nhật";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BorderRadius = 15;
            this.btn_Cancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Cancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Cancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Cancel.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Cancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Cancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Location = new System.Drawing.Point(391, 294);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(149, 45);
            this.btn_Cancel.TabIndex = 14;
            this.btn_Cancel.Text = "Hủy";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // AddBonusAndPen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 372);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.DT_timeBonus);
            this.Controls.Add(this.txt_reason);
            this.Controls.Add(this.txt_amount);
            this.Controls.Add(this.txt_NameStaff);
            this.Controls.Add(this.txt_StaffId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "AddBonusAndPen";
            this.Text = "AddBonusAndPen";
            this.Load += new System.EventHandler(this.AddBonusAndPen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_StaffId;
        private System.Windows.Forms.TextBox txt_NameStaff;
        private System.Windows.Forms.TextBox txt_amount;
        private System.Windows.Forms.TextBox txt_reason;
        private Guna.UI2.WinForms.Guna2DateTimePicker DT_timeBonus;
        private Guna.UI2.WinForms.Guna2ComboBox cb_type;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Add;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Cancel;
    }
}