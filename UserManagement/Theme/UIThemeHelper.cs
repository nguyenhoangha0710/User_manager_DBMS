using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace UserManagement.Theme
{
    public static class UIThemeHelper
    {
        // Light Modern palette
        private static readonly Color Background = ColorTranslator.FromHtml("#F5F7FB");
        private static readonly Color Surface = ColorTranslator.FromHtml("#FFFFFF");
        private static readonly Color Primary = ColorTranslator.FromHtml("#3A61AB");
        private static readonly Color Primary2 = ColorTranslator.FromHtml("#5894E4");
        private static readonly Color Accent = ColorTranslator.FromHtml("#22C55E");
        private static readonly Color Text = ColorTranslator.FromHtml("#111827");
        private static readonly Color TextSecondary = ColorTranslator.FromHtml("#6B7280");

        public static void ApplyTheme(Control root)
        {
            if (root == null) return;

            try
            {
                // Root background & base font
                root.BackColor = Background;
                root.Font = new Font("Segoe UI", Math.Max(9f, root.Font.Size));

                ApplyThemeToControl(root);

                foreach (Control child in root.Controls)
                {
                    ApplyThemeRecursive(child);
                }
            }
            catch { /* theming must never break app */ }
        }

        private static void ApplyThemeRecursive(Control control)
        {
            ApplyThemeToControl(control);
            foreach (Control child in control.Controls)
            {
                ApplyThemeRecursive(child);
            }
        }

        private static void ApplyThemeToControl(Control c)
        {
            try
            {
                switch (c)
                {
                    case Guna2GradientButton btn:
                        btn.FillColor = Primary;
                        btn.FillColor2 = Primary2;
                        btn.ForeColor = Color.White;
                        btn.BorderRadius = Math.Max(12, btn.BorderRadius);
                        btn.Height = Math.Max(44, btn.Height);
                        btn.Font = new Font("Segoe UI", 11f, FontStyle.Regular);
                        break;

                    case Guna2Button btn2:
                        btn2.FillColor = Primary;
                        btn2.ForeColor = Color.White;
                        btn2.BorderRadius = Math.Max(10, btn2.BorderRadius);
                        btn2.Height = Math.Max(42, btn2.Height);
                        btn2.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
                        break;

                    case Guna2TextBox tb:
                        tb.BorderRadius = Math.Max(8, tb.BorderRadius);
                        tb.BorderColor = Color.FromArgb(210, 214, 220);
                        tb.FocusedState.BorderColor = Primary2;
                        tb.HoverState.BorderColor = Primary2;
                        tb.FillColor = Surface;
                        tb.ForeColor = Text;
                        tb.PlaceholderForeColor = TextSecondary;
                        break;

                    case Guna2DateTimePicker dtp:
                        dtp.BorderRadius = Math.Max(8, dtp.BorderRadius);
                        dtp.FillColor = Surface;
                        dtp.ForeColor = Text;
                        break;

                    case Guna2Panel pnl2:
                        pnl2.FillColor = Surface;
                        break;

                    case Panel pnl:
                        pnl.BackColor = Surface;
                        break;

                    case Label lbl:
                        lbl.ForeColor = Text;
                        if (lbl.Font.Size < 10)
                            lbl.Font = new Font("Segoe UI", 10f, lbl.Font.Style);
                        break;

                    case Guna2HtmlLabel hl:
                        hl.ForeColor = Text;
                        if (hl.Font.Size < 10)
                            hl.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
                        break;

                    case DataGridView dgv:
                        StyleDataGridView(dgv);
                        break;
                }
            }
            catch { /* never fail on theming */ }
        }

        private static void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BackgroundColor = Surface;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Header
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5f, FontStyle.SemiBold);
            dgv.ColumnHeadersHeight = Math.Max(36, dgv.ColumnHeadersHeight);

            // Rows
            dgv.DefaultCellStyle.BackColor = Surface;
            dgv.DefaultCellStyle.ForeColor = Text;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 241, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Text;
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = Math.Max(28, dgv.RowTemplate.Height);

            // Alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
        }
    }
}
