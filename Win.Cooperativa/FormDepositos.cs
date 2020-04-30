using BL.Cooperativa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Cooperativa
{
    public partial class FormDepositos : Form
    {
        DepositosBL _depositos;
        TiposBL _tiposBL;

        public FormDepositos()
        {
            InitializeComponent();

            _depositos = new DepositosBL();
            listaDepositosBindingSource.DataSource = _depositos.ObtenerDeposito();

            _tiposBL = new TiposBL();
            listaTiposBindingSource.DataSource = _tiposBL.ObtenerTipos();

        }

        private void listaDepositosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaDepositosBindingSource.EndEdit();
            var deposito = (Deposito)listaDepositosBindingSource.Current;

            if (fotoPictureBox.Image != null)
            {
                deposito.Foto = Program.ImageToByteArray(fotoPictureBox.Image);
            }
            else
            {
                deposito.Foto = null;
            }

            var resultado = _depositos.GuardarDeposito(deposito);

            if (resultado.Exitiso == true)
            {
                listaDepositosBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Deposito Guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _depositos.AgregarDeposito();

            listaDepositosBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
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
            
            var resultado = _depositos.EliminarDeposito(id);

            if (resultado == true)
            {
                listaDepositosBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el producto");
            }
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            _depositos.CancelarCambios();
            DeshabilitarHabilitarBotones(true);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var deposito = (Deposito)listaDepositosBindingSource.Current;

            if (deposito != null)
            {
                openFileDialog1.ShowDialog();
                var archivo = openFileDialog1.FileName;

                if (archivo != "")
                {
                    var fileInfo = new FileInfo(archivo);
                    var fileStream = fileInfo.OpenRead();

                    fotoPictureBox.Image = Image.FromStream(fileStream);
                }

            }
            else
            {
                MessageBox.Show("Cree un deposito antes de asignar una imagen!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var buscar = textBox1.Text;

            if (string.IsNullOrEmpty(buscar) == true)
            {
                listaDepositosBindingSource.DataSource =
                    _depositos.ObtenerDeposito();
            }
            else
            {
                listaDepositosBindingSource.DataSource =
                    _depositos.ObtenerDepositos(buscar);
            }


            listaDepositosBindingSource.ResetBindings(false);
        }
    }
}
