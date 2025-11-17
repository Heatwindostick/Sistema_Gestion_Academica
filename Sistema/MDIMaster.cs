using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class MDIMaster : Form
    {
        public static int IdUsuario;
        private Image backgroundImageTransparent;
        private MdiClient mdiClient;

        public MDIMaster(int pIdUsuario = 0)
        {
            InitializeComponent();
            IdUsuario = pIdUsuario;
            //DEFINIMOS DISEÑO DEL FORMULARIO MDI
            this.IsMdiContainer = true;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            this.WindowState = FormWindowState.Maximized;
        }

        private void MDIMaster_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;

            if (IdUsuario == 0)
            {
                this.Close();
            }
            Configuracion.oUsuario = CD_Usuario.ObtenerDetalleUsuario(IdUsuario);

            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

            StatusStrip sttStrip = new StatusStrip();
            sttStrip.Dock = DockStyle.Top;
            sttStrip.BackColor = System.Drawing.Color.White;
            sttStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            sttStrip.Padding = new Padding(10, 10, 10, 10);

            ToolStripStatusLabel tslblUsuario = new ToolStripStatusLabel("Usuario: ");
            ToolStripStatusLabel tslblData1 = new ToolStripStatusLabel(Configuracion.oUsuario.Nombres + " " + Configuracion.oUsuario.Apellidos);
            tslblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tslblUsuario.ForeColor = Color.FromArgb(100, 116, 139);
            tslblUsuario.Spring = true;
            tslblUsuario.TextAlign = ContentAlignment.MiddleRight;
            tslblData1.ForeColor = Color.FromArgb(52, 73, 94);

            ToolStripStatusLabel tslblTipoUsuario = new ToolStripStatusLabel("Rol: ");
            ToolStripStatusLabel tslblData2 = new ToolStripStatusLabel(Configuracion.oUsuario.oRol.Descripcion);
            tslblTipoUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tslblTipoUsuario.ForeColor = Color.FromArgb(100, 116, 139);
            tslblData2.ForeColor = Color.FromArgb(52, 73, 94);

            sttStrip.Items.Add(tslblUsuario);
            sttStrip.Items.Add(tslblData1);
            sttStrip.Items.Add(tslblTipoUsuario);
            sttStrip.Items.Add(tslblData2);
            Controls.Add(sttStrip);

            MenuStrip MnuStrip = new MenuStrip();
            MnuStrip.BackColor = Color.FromArgb(52, 152, 219); // Azul moderno más claro
            MnuStrip.ForeColor = Color.White;
            MnuStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            MnuStrip.Padding = new Padding(15, 8, 0, 8);
            MnuStrip.Renderer = new CustomMenuRenderer();

            foreach (CapaModelo.Menu oMenu in Configuracion.oUsuario.oListaMenu)
            {
                ToolStripMenuItem MnuStripItem = new ToolStripMenuItem(oMenu.Nombre.ToUpper());
                MnuStripItem.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
                MnuStripItem.ForeColor = Color.White;
                MnuStripItem.Padding = new Padding(12, 10, 12, 10);
                MnuStripItem.TextImageRelation = TextImageRelation.ImageBeforeText;
                MnuStripItem.Image = null;
                MnuStripItem.ImageScaling = ToolStripItemImageScaling.None;

                if (oMenu.oSubMenu != null)
                {
                    foreach (SubMenu oSubMenu in oMenu.oSubMenu.Where(x => x.Activo == true))
                    {
                        ToolStripMenuItem SubMenuStringItem = new ToolStripMenuItem(oSubMenu.Nombre, null, ToolStripMenuItem_Click, oSubMenu.NombreFormulario);
                        SubMenuStringItem.Font = new System.Drawing.Font("Segoe UI", 10F);
                        SubMenuStringItem.ForeColor = Color.FromArgb(44, 62, 80);
                        SubMenuStringItem.Padding = new Padding(10, 8, 10, 8);
                        SubMenuStringItem.Image = null;
                        MnuStripItem.DropDownItems.Add(SubMenuStringItem);
                    }
                }
                MnuStrip.Items.Add(MnuStripItem);
            }

            ToolStripMenuItem MnuStripItemExit = new ToolStripMenuItem("SALIR", null, ToolStripMenuItemSalir_Click, "");
            MnuStripItemExit.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            MnuStripItemExit.ForeColor = Color.White;
            MnuStripItemExit.Padding = new Padding(12, 10, 12, 10);
            MnuStripItemExit.TextImageRelation = TextImageRelation.ImageBeforeText;
            MnuStripItemExit.Image = null;
            MnuStripItemExit.ImageScaling = ToolStripItemImageScaling.None;
            MnuStrip.Items.Add(MnuStripItemExit);

            this.MainMenuStrip = MnuStrip;
            Controls.Add(MnuStrip);

            // Agregar imagen de fondo semi-transparente al área MDI
            try
            {
                string imagePath = Path.Combine(projectRoot, "Iconos", "fisi.png");
                Image originalImage = Image.FromFile(imagePath);

                // Crear imagen semi-transparente
                Bitmap bmp = new Bitmap(originalImage.Width, originalImage.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Transparent);
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = 0.4f; // 40% de opacidad
                    ImageAttributes attributes = new ImageAttributes();
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    g.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                        0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, attributes);
                }

                backgroundImageTransparent = bmp;

                // Buscar el MdiClient y agregar el evento Paint
                foreach (Control control in this.Controls)
                {
                    if (control is MdiClient)
                    {
                        mdiClient = (MdiClient)control;
                        mdiClient.Paint += MdiClient_Paint;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading background image: {ex.Message}");
            }
        }

        private void MdiClient_Paint(object sender, PaintEventArgs e)
        {
            if (backgroundImageTransparent != null && mdiClient != null)
            {
                // Calcular el tamaño y posición para centrar la imagen
                int imageWidth = backgroundImageTransparent.Width;
                int imageHeight = backgroundImageTransparent.Height;
                int clientWidth = mdiClient.ClientSize.Width;
                int clientHeight = mdiClient.ClientSize.Height;

                // Calcular escala para ajustar la imagen al espacio disponible manteniendo proporción
                float scale = Math.Min((float)clientWidth / imageWidth, (float)clientHeight / imageHeight);
                scale = Math.Min(scale, 1.0f); // No agrandar la imagen, solo reducir si es necesario

                int scaledWidth = (int)(imageWidth * scale);
                int scaledHeight = (int)(imageHeight * scale);

                // Centrar la imagen
                int x = (clientWidth - scaledWidth) / 2;
                int y = (clientHeight - scaledHeight) / 2;

                // Dibujar la imagen una sola vez en el centro
                e.Graphics.DrawImage(backgroundImageTransparent, x, y, scaledWidth, scaledHeight);
            }
        }

        private void ToolStripMenuItemSalir_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                this.Close();
            }
        }

        private void ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem oMenuSelect = (ToolStripMenuItem)sender;

            if (oMenuSelect.Name != "")
            {
                Assembly asm = Assembly.GetEntryAssembly();

                Type formtype = asm.GetType(string.Format("{0}.{1}", asm.GetName().Name, oMenuSelect.Name));

                if (formtype == null)
                {
                    MessageBox.Show("Formulario no encontrado");
                }
                else
                {
                    Form formulario = (Form)Activator.CreateInstance(formtype);
                    MostrarFormulario(formulario, this);

                    formulario.WindowState = FormWindowState.Normal;
                    formulario.StartPosition = FormStartPosition.CenterScreen;
                    formulario.Activate();
                }
            }
        }

        public void MostrarFormulario(Form frmhijo, Form frmpapa)
        {
            Form FormularioEncontrado = new Form();
            bool cargado = false;
            foreach (Form Formulario in frmpapa.MdiChildren)
            {
                if (Formulario.Name == frmhijo.Name)
                {
                    FormularioEncontrado = Formulario;
                    cargado = true;
                    break;
                }
            }

            if (!cargado)
            {
                frmhijo.MdiParent = frmpapa;
                frmhijo.Show();
            }
            else
            {
                FormularioEncontrado.WindowState = FormWindowState.Normal;
                FormularioEncontrado.Activate();
            }

        }
    }

    // Renderer personalizado para el menú con diseño moderno
    public class CustomMenuRenderer : ToolStripProfessionalRenderer
    {
        public CustomMenuRenderer() : base(new CustomColorTable()) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                // Color cuando pasas el mouse por encima
                Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(41, 128, 185)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
            else if (e.Item.Pressed)
            {
                // Color cuando se presiona
                Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(31, 97, 141)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.ForeColor;
            base.OnRenderItemText(e);
        }
    }

    // Tabla de colores personalizada para el menú
    public class CustomColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(41, 128, 185); }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(41, 128, 185); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(41, 128, 185); }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(31, 97, 141); }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(31, 97, 141); }
        }

        public override Color MenuItemBorder
        {
            get { return Color.Transparent; }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return Color.White; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Color.White; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.White; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.White; }
        }

        public override Color MenuStripGradientBegin
        {
            get { return Color.FromArgb(52, 152, 219); }
        }

        public override Color MenuStripGradientEnd
        {
            get { return Color.FromArgb(52, 152, 219); }
        }

        public override Color MenuBorder
        {
            get { return Color.FromArgb(189, 195, 199); }
        }
    }
}
