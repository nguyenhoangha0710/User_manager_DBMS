namespace UserManagement.View.Account
{
    partial class AccountManagement
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Refresh = new Guna.UI2.WinForms.Guna2GradientButton();
            this.CB_RoleName = new Guna.UI2.WinForms.Guna2ComboBox();
            this.CB_TypeSearch = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btn_Search = new Guna.UI2.WinForms.Guna2GradientButton();
            this.txt_infoSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_Users = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Users)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Controls.Add(this.CB_RoleName);
            this.panel1.Controls.Add(this.CB_TypeSearch);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Controls.Add(this.txt_infoSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1201, 122);
            this.panel1.TabIndex = 1;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BorderRadius = 20;
            this.btn_Refresh.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.btn_Refresh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Refresh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Refresh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Refresh.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Refresh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Refresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_Refresh.ForeColor = System.Drawing.Color.White;
            this.btn_Refresh.Location = new System.Drawing.Point(541, 33);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(144, 49);
            this.btn_Refresh.TabIndex = 16;
            this.btn_Refresh.Text = "Làm mới";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // CB_RoleName
            // 
            this.CB_RoleName.BackColor = System.Drawing.Color.Transparent;
            this.CB_RoleName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CB_RoleName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_RoleName.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CB_RoleName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CB_RoleName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CB_RoleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.CB_RoleName.ItemHeight = 30;
            this.CB_RoleName.Location = new System.Drawing.Point(216, 33);
            this.CB_RoleName.Name = "CB_RoleName";
            this.CB_RoleName.Size = new System.Drawing.Size(169, 36);
            this.CB_RoleName.TabIndex = 15;
            this.CB_RoleName.SelectedIndexChanged += new System.EventHandler(this.CB_RoleName_SelectedIndexChanged);
            // 
            // CB_TypeSearch
            // 
            this.CB_TypeSearch.BackColor = System.Drawing.Color.Transparent;
            this.CB_TypeSearch.BorderRadius = 10;
            this.CB_TypeSearch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CB_TypeSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_TypeSearch.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CB_TypeSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CB_TypeSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CB_TypeSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.CB_TypeSearch.ItemHeight = 30;
            this.CB_TypeSearch.Location = new System.Drawing.Point(17, 33);
            this.CB_TypeSearch.Name = "CB_TypeSearch";
            this.CB_TypeSearch.Size = new System.Drawing.Size(172, 36);
            this.CB_TypeSearch.TabIndex = 14;
            this.CB_TypeSearch.SelectedIndexChanged += new System.EventHandler(this.CB_TypeSearch_SelectedIndexChanged);
            // 
            // btn_Search
            // 
            this.btn_Search.BorderRadius = 20;
            this.btn_Search.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.btn_Search.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Search.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Search.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Search.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Search.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Search.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_Search.ForeColor = System.Drawing.Color.White;
            this.btn_Search.Location = new System.Drawing.Point(391, 33);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(144, 49);
            this.btn_Search.TabIndex = 12;
            this.btn_Search.Text = "Tìm kiếm ";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // txt_infoSearch
            // 
            this.txt_infoSearch.BorderRadius = 20;
            this.txt_infoSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_infoSearch.DefaultText = "Nhập thông tin tìm kiếm";
            this.txt_infoSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_infoSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_infoSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_infoSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_infoSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_infoSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_infoSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_infoSearch.Location = new System.Drawing.Point(216, 33);
            this.txt_infoSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_infoSearch.Name = "txt_infoSearch";
            this.txt_infoSearch.PlaceholderText = "";
            this.txt_infoSearch.SelectedText = "";
            this.txt_infoSearch.Size = new System.Drawing.Size(169, 49);
            this.txt_infoSearch.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_Users);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1201, 531);
            this.panel2.TabIndex = 2;
            // 
            // dgv_Users
            // 
            this.dgv_Users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Users.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Users.Location = new System.Drawing.Point(0, 0);
            this.dgv_Users.Name = "dgv_Users";
            this.dgv_Users.RowHeadersWidth = 51;
            this.dgv_Users.RowTemplate.Height = 24;
            this.dgv_Users.Size = new System.Drawing.Size(1201, 531);
            this.dgv_Users.TabIndex = 0;
            // 
            // AccountManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AccountManagement";
            this.Size = new System.Drawing.Size(1201, 653);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Users)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Refresh;
        private Guna.UI2.WinForms.Guna2ComboBox CB_RoleName;
        private Guna.UI2.WinForms.Guna2ComboBox CB_TypeSearch;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Search;
        private Guna.UI2.WinForms.Guna2TextBox txt_infoSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_Users;
    }
}
