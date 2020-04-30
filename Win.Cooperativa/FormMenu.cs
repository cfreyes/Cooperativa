using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Cooperativa
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            var formlogin = new FormLogin();
            formlogin.ShowDialog();

            if (Program.UsuarioLogueado !=null)
            {
                toolStripStatusLabel1.Text = "Usuario: " + Program.UsuarioLogueado.Nombre;

                if (Program.UsuarioLogueado.TipoUsuario == "Usuarios caja")
                {
                    depositosToolStripMenuItem.Visible = true;
                    clientesToolStripMenuItem.Visible = false;
                    retirosToolStripMenuItem.Visible = true;
                    facturaToolStripMenuItem.Visible = true;
                    administracionDeUsuariosToolStripMenuItem.Visible = false;
                    reporteDeTransaccionesToolStripMenuItem.Visible = false;
                    reporteDeClientesToolStripMenuItem.Visible = false;
                    reporteDeFacturasToolStripMenuItem.Visible = true;
                }

                if (Program.UsuarioLogueado.TipoUsuario == "Usuarios Transacciones")
                {
                    MessageBox.Show("Bienvenido Estimado Cliente, Porfavor registra tus datos en la seccion de Clientes. ");

                    depositosToolStripMenuItem.Visible = false;
                    clientesToolStripMenuItem.Visible = true;
                    retirosToolStripMenuItem.Visible = false;
                    facturaToolStripMenuItem.Visible = false;
                    administracionDeUsuariosToolStripMenuItem.Visible = false;
                    reporteDeTransaccionesToolStripMenuItem.Visible = false;
                    reporteDeClientesToolStripMenuItem.Visible = true;                  
                    reporteDeFacturasToolStripMenuItem.Visible = false;
                }

                if (Program.UsuarioLogueado.TipoUsuario == "Administradores")
                {
                    depositosToolStripMenuItem.Visible = true;
                    clientesToolStripMenuItem.Visible = true;
                    retirosToolStripMenuItem.Visible = true;
                    facturaToolStripMenuItem.Visible = true;
                    administracionDeUsuariosToolStripMenuItem.Visible = true;
                    reporteDeTransaccionesToolStripMenuItem.Visible = true;
                    reporteDeClientesToolStripMenuItem.Visible = true;
                    reporteDeFacturasToolStripMenuItem.Visible = true;
                }
            }
        }

        private void depositosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var formDepositos = new FormDepositos();
            formDepositos.MdiParent = this;
            formDepositos.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formClientes = new FormClientes();
            formClientes.MdiParent = this;
            formClientes.Show();
        }

        private void retirosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formRetiros = new FormRetiros();
            formRetiros.MdiParent = this;
            formRetiros.Show();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            login();
        }

        private void facturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formFactura = new FormFactura();
            formFactura.MdiParent = this;
            formFactura.Show();

        }

        private void reporteDeTransaccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formReporteDepositos = new FormReporteDepositos();
            formReporteDepositos.MdiParent = this;
            formReporteDepositos.Show();
        }

        private void reporteDeFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formReporteFacturas = new FormReporteFacturas();
            formReporteFacturas.MdiParent = this;
            formReporteFacturas.Show();
        }

        private void administracionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formUsuarios = new FormUsuarios();
            formUsuarios.MdiParent = this;
            formUsuarios.Show();
        }
    }
}
