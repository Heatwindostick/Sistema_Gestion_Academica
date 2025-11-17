using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema
{
    /// <summary>
    /// Clase de utilidad para aplicar estilos modernos consistentes a los formularios
    /// </summary>
    public static class ModernStyles
    {
        // Paleta de colores moderna
        public static class Colors
        {
            // Colores primarios
            public static readonly Color Primary = Color.FromArgb(52, 152, 219);        // Azul moderno
            public static readonly Color PrimaryDark = Color.FromArgb(41, 128, 185);    // Azul oscuro
            public static readonly Color PrimaryLight = Color.FromArgb(93, 173, 226);   // Azul claro

            // Colores secundarios
            public static readonly Color Success = Color.FromArgb(46, 204, 113);        // Verde
            public static readonly Color SuccessDark = Color.FromArgb(39, 174, 96);     // Verde oscuro
            public static readonly Color Danger = Color.FromArgb(231, 76, 60);          // Rojo
            public static readonly Color DangerDark = Color.FromArgb(192, 57, 43);      // Rojo oscuro
            public static readonly Color Warning = Color.FromArgb(241, 196, 15);        // Amarillo
            public static readonly Color Info = Color.FromArgb(52, 152, 219);           // Info azul

            // Colores neutrales
            public static readonly Color Dark = Color.FromArgb(52, 73, 94);             // Gris oscuro
            public static readonly Color Light = Color.FromArgb(236, 240, 241);         // Gris claro
            public static readonly Color LightText = Color.FromArgb(100, 116, 139);     // Texto gris
            public static readonly Color Border = Color.FromArgb(189, 195, 199);        // Borde gris
            public static readonly Color Background = Color.White;                       // Fondo blanco
            public static readonly Color InputBackground = Color.FromArgb(245, 247, 250); // Fondo input

            // Colores para estados
            public static readonly Color HoverButton = Color.FromArgb(41, 128, 185);
            public static readonly Color DisabledBackground = Color.FromArgb(236, 240, 241);
            public static readonly Color DisabledText = Color.FromArgb(149, 165, 166);
        }

        // Fuentes modernas
        public static class Fonts
        {
            public static readonly Font TitleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            public static readonly Font SubtitleFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            public static readonly Font RegularFont = new Font("Segoe UI", 9F, FontStyle.Regular);
            public static readonly Font SmallFont = new Font("Segoe UI", 8F, FontStyle.Regular);
            public static readonly Font LabelFont = new Font("Segoe UI", 9F, FontStyle.Regular);
            public static readonly Font ButtonFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        }

        /// <summary>
        /// Aplica estilos modernos a un formulario
        /// </summary>
        public static void ApplyModernFormStyle(Form form)
        {
            form.BackColor = Colors.Light;
            form.Font = Fonts.RegularFont;
        }

        /// <summary>
        /// Aplica estilos modernos a un GroupBox
        /// </summary>
        public static void ApplyModernGroupBoxStyle(GroupBox groupBox)
        {
            groupBox.BackColor = Colors.Background;
            groupBox.Font = Fonts.SubtitleFont;
            groupBox.ForeColor = Colors.Dark;
        }

        /// <summary>
        /// Aplica estilos modernos a un TextBox
        /// </summary>
        public static void ApplyModernTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor = Colors.InputBackground;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = Fonts.RegularFont;
            textBox.ForeColor = Colors.Dark;
        }

        /// <summary>
        /// Aplica estilos modernos a un ComboBox
        /// </summary>
        public static void ApplyModernComboBoxStyle(ComboBox comboBox)
        {
            comboBox.BackColor = Colors.InputBackground;
            comboBox.Font = Fonts.RegularFont;
            comboBox.ForeColor = Colors.Dark;
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        /// <summary>
        /// Aplica estilos modernos a un Label
        /// </summary>
        public static void ApplyModernLabelStyle(Label label)
        {
            label.Font = Fonts.LabelFont;
            label.ForeColor = Colors.LightText;
        }

        /// <summary>
        /// Aplica estilos modernos a un botón según su tipo
        /// </summary>
        public static void ApplyModernButtonStyle(Button button, ButtonType type = ButtonType.Primary)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = Fonts.ButtonFont;
            button.Cursor = Cursors.Hand;
            button.Height = 35;

            switch (type)
            {
                case ButtonType.Primary:
                    button.BackColor = Colors.Primary;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = Colors.PrimaryDark;
                    break;

                case ButtonType.Success:
                    button.BackColor = Colors.Success;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = Colors.SuccessDark;
                    break;

                case ButtonType.Danger:
                    button.BackColor = Colors.Danger;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = Colors.DangerDark;
                    break;

                case ButtonType.Warning:
                    button.BackColor = Colors.Warning;
                    button.ForeColor = Colors.Dark;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 156, 18);
                    break;

                case ButtonType.Light:
                    button.BackColor = Colors.Light;
                    button.ForeColor = Colors.Dark;
                    button.FlatAppearance.MouseOverBackColor = Colors.Border;
                    break;

                case ButtonType.Dark:
                    button.BackColor = Colors.Dark;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(44, 62, 80);
                    break;
            }
        }

        /// <summary>
        /// Aplica estilos modernos a un DataGridView
        /// </summary>
        public static void ApplyModernDataGridViewStyle(DataGridView dgv)
        {
            dgv.BackgroundColor = Colors.Background;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = Colors.Light;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Font = Fonts.RegularFont;

            // Estilo de encabezados
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Colors.Primary;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = Fonts.SubtitleFont;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Colors.PrimaryDark;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgv.ColumnHeadersHeight = 40;

            // Estilo de celdas
            dgv.DefaultCellStyle.BackColor = Colors.Background;
            dgv.DefaultCellStyle.ForeColor = Colors.Dark;
            dgv.DefaultCellStyle.SelectionBackColor = Colors.PrimaryLight;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);

            // Estilo de filas alternadas
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            dgv.RowTemplate.Height = 35;
        }

        /// <summary>
        /// Aplica todos los estilos modernos a los controles de un formulario recursivamente
        /// </summary>
        public static void ApplyModernStylesToAllControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Form form)
                {
                    ApplyModernFormStyle(form);
                }
                else if (control is GroupBox groupBox)
                {
                    ApplyModernGroupBoxStyle(groupBox);
                }
                else if (control is TextBox textBox)
                {
                    ApplyModernTextBoxStyle(textBox);
                }
                else if (control is ComboBox comboBox)
                {
                    ApplyModernComboBoxStyle(comboBox);
                }
                else if (control is Label label)
                {
                    ApplyModernLabelStyle(label);
                }
                else if (control is DataGridView dgv)
                {
                    ApplyModernDataGridViewStyle(dgv);
                }
                else if (control is Button button)
                {
                    // Los botones se deben estilizar individualmente según su función
                    // Este es un estilo por defecto
                    if (button.Name.ToLower().Contains("guardar") || button.Name.ToLower().Contains("nuevo"))
                    {
                        ApplyModernButtonStyle(button, ButtonType.Success);
                    }
                    else if (button.Name.ToLower().Contains("eliminar"))
                    {
                        ApplyModernButtonStyle(button, ButtonType.Danger);
                    }
                    else if (button.Name.ToLower().Contains("cancelar"))
                    {
                        ApplyModernButtonStyle(button, ButtonType.Light);
                    }
                    else if (button.Name.ToLower().Contains("editar") || button.Name.ToLower().Contains("buscar"))
                    {
                        ApplyModernButtonStyle(button, ButtonType.Primary);
                    }
                    else
                    {
                        ApplyModernButtonStyle(button, ButtonType.Primary);
                    }
                }

                // Aplicar recursivamente a controles hijos
                if (control.HasChildren)
                {
                    ApplyModernStylesToAllControls(control);
                }
            }
        }

        /// <summary>
        /// Tipos de botones disponibles
        /// </summary>
        public enum ButtonType
        {
            Primary,
            Success,
            Danger,
            Warning,
            Light,
            Dark
        }
    }
}
