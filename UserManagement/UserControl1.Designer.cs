namespace UserManagement
{
    partial class UserUS
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
            this.dgv_Users = new System.Windows.Forms.DataGridView();
            this.txt_infoSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ContextMenuStrip1 = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.btn_Search = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_AddUser = new Guna.UI2.WinForms.Guna2GradientButton();
            this.CB_TypeSearch = new Guna.UI2.WinForms.Guna2ComboBox();
            this.CB_RoleName = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btn_Refresh = new Guna.UI2.WinForms.Guna2GradientButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Users)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Users
            // 
            this.dgv_Users.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_Users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Users.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_Users.Location = new System.Drawing.Point(0, 206);
            this.dgv_Users.Name = "dgv_Users";
            this.dgv_Users.RowHeadersWidth = 51;
            this.dgv_Users.RowTemplate.Height = 24;
            this.dgv_Users.Size = new System.Drawing.Size(1201, 447);
            this.dgv_Users.TabIndex = 2;
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
            this.txt_infoSearch.Location = new System.Drawing.Point(219, 22);
            this.txt_infoSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_infoSearch.Name = "txt_infoSearch";
            this.txt_infoSearch.PlaceholderText = "";
            this.txt_infoSearch.SelectedText = "";
            this.txt_infoSearch.Size = new System.Drawing.Size(233, 49);
            this.txt_infoSearch.TabIndex = 4;
            // 
            // guna2ContextMenuStrip1
            // 
            this.guna2ContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            this.guna2ContextMenuStrip1.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            this.guna2ContextMenuStrip1.RenderStyle.RoundedEdges = true;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2ContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
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
            this.btn_Search.Location = new System.Drawing.Point(476, 22);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(144, 49);
            this.btn_Search.TabIndex = 6;
            this.btn_Search.Text = "Tìm kiếm ";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_AddUser
            // 
            this.btn_AddUser.BorderRadius = 20;
            this.btn_AddUser.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.btn_AddUser.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_AddUser.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_AddUser.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_AddUser.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_AddUser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_AddUser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_AddUser.ForeColor = System.Drawing.Color.White;
            this.btn_AddUser.Location = new System.Drawing.Point(652, 22);
            this.btn_AddUser.Name = "btn_AddUser";
            this.btn_AddUser.Size = new System.Drawing.Size(144, 49);
            this.btn_AddUser.TabIndex = 7;
            this.btn_AddUser.Text = "Thêm nhân viên";
            this.btn_AddUser.Click += new System.EventHandler(this.guna2GradientButton2_Click);
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
            this.CB_TypeSearch.Location = new System.Drawing.Point(20, 22);
            this.CB_TypeSearch.Name = "CB_TypeSearch";
            this.CB_TypeSearch.Size = new System.Drawing.Size(172, 36);
            this.CB_TypeSearch.TabIndex = 8;
            this.CB_TypeSearch.SelectedIndexChanged += new System.EventHandler(this.CB_TypeSearch_SelectedIndexChanged);
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
            this.CB_RoleName.Location = new System.Drawing.Point(219, 22);
            this.CB_RoleName.Name = "CB_RoleName";
            this.CB_RoleName.Size = new System.Drawing.Size(233, 36);
            this.CB_RoleName.TabIndex = 9;
            this.CB_RoleName.SelectedIndexChanged += new System.EventHandler(this.CB_RoleName_SelectedIndexChanged);
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
            this.btn_Refresh.Location = new System.Drawing.Point(823, 22);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(144, 49);
            this.btn_Refresh.TabIndex = 10;
            this.btn_Refresh.Text = "Làm mới";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // UserUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.CB_RoleName);
            this.Controls.Add(this.CB_TypeSearch);
            this.Controls.Add(this.btn_AddUser);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txt_infoSearch);
            this.Controls.Add(this.dgv_Users);
            this.Name = "UserUS";
            this.Size = new System.Drawing.Size(1201, 653);
            this.Load += new System.EventHandler(this.okok_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Users)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_Users;
        private Guna.UI2.WinForms.Guna2TextBox txt_infoSearch;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip guna2ContextMenuStrip1;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Search;
        private Guna.UI2.WinForms.Guna2GradientButton btn_AddUser;
        private Guna.UI2.WinForms.Guna2ComboBox CB_TypeSearch;
        private Guna.UI2.WinForms.Guna2ComboBox CB_RoleName;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Refresh;
    }
}
