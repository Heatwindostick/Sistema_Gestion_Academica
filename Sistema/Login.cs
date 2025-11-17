using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            int IdUsuario = CD_Usuario.LoginUsuario(txtUsuario.Text, txtClave.Text);
            if (IdUsuario != 0)
            {
                MDIMaster frm = new MDIMaster(IdUsuario);
                frm.Show();
                this.Hide();
                frm.FormClosing += Frm_Closing;
            }
            else
            {
                MessageBox.Show("No se econtraron coincidencias del usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            txtUsuario.Text = "";
            txtClave.Text = "";
            txtUsuario.Focus();
            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            try
            {
                string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string projectRoot = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(assemblyPath), "..", "..", ".."));
                string imagePath = Path.Combine(projectRoot, "Iconos", "logo.png");
                this.BackgroundImage = Image.FromFile(imagePath);

                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading background image:\nException: {ex.ToString()}");
            }

            // Centrar el panel de login
            CentrarPanelLogin();

            // Agregar evento para centrar cuando se redimensiona
            this.Resize += Login_Resize;
        }

        private void Login_Resize(object sender, EventArgs e)
        {
            CentrarPanelLogin();
        }

        private void CentrarPanelLogin()
        {
            // Centrar panelLogin en el formulario
            panelLogin.Left = (this.ClientSize.Width - panelLogin.Width) / 2;
            panelLogin.Top = (this.ClientSize.Height - panelLogin.Height) / 2;
        }
    }
}
