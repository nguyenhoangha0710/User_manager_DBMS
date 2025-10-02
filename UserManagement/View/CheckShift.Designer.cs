namespace UserManagement.View
{
    partial class CheckShift
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
            this.btn_Checkin = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btn_Checkout = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SuspendLayout();
            // 
            // btn_Checkin
            // 
            this.btn_Checkin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Checkin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Checkin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Checkin.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Checkin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Checkin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_Checkin.ForeColor = System.Drawing.Color.White;
            this.btn_Checkin.Location = new System.Drawing.Point(242, 276);
            this.btn_Checkin.Name = "btn_Checkin";
            this.btn_Checkin.Size = new System.Drawing.Size(246, 123);
            this.btn_Checkin.TabIndex = 0;
            this.btn_Checkin.Text = "CheckIn ";
            this.btn_Checkin.Click += new System.EventHandler(this.btn_Checkin_Click);
            // 
            // btn_Checkout
            // 
            this.btn_Checkout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Checkout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Checkout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Checkout.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Checkout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Checkout.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_Checkout.ForeColor = System.Drawing.Color.White;
            this.btn_Checkout.Location = new System.Drawing.Point(569, 276);
            this.btn_Checkout.Name = "btn_Checkout";
            this.btn_Checkout.Size = new System.Drawing.Size(246, 123);
            this.btn_Checkout.TabIndex = 1;
            this.btn_Checkout.Text = "CheckOut";
            this.btn_Checkout.Click += new System.EventHandler(this.btn_Checkout_Click);
            // 
            // CheckShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Checkout);
            this.Controls.Add(this.btn_Checkin);
            this.Name = "CheckShift";
            this.Size = new System.Drawing.Size(1201, 653);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientButton btn_Checkin;
        private Guna.UI2.WinForms.Guna2GradientButton btn_Checkout;
    }
}
