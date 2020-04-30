using BL.Cooperativa;
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
    public partial class FormUsuarios : Form
    {
        SeguridadBL _seguridadBL;

        public FormUsuarios()
        {
            InitializeComponent();

            _seguridadBL = new SeguridadBL();

            usuarioBindingSource.DataSource = _seguridadBL.ObtenerUsuarios();
               
        }

        private void usuarioBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            usuarioBindingSource.EndEdit();
            var usuario = (Usuario)usuarioBindingSource.Current;

            var resultado = _seguridadBL.GuardarUsuario(usuario);

            if (resultado.Exitiso == true)
            {
                usuarioBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Usuario guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButtonCancelar.Visible = !valor;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _seguridadBL.AgregarUsuario();
            usuarioBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }
            }
        }

        private void Eliminar(int id)
        {
            var resultado = _seguridadBL.EliminarUsuario(id);

            if (resultado == true)
            {
                usuarioBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el usuario");
            }
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            _seguridadBL.CancelarCambios();
            DeshabilitarHabilitarBotones(true);
        }
    }
}
