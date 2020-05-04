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
    public partial class FormReporteDepositos : Form
    {
        public FormReporteDepositos()
        {
            InitializeComponent();

            var _depositoBL = new DepositosBL();
            var _tiposBL = new TiposBL();


            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = _depositoBL.ObtenerDeposito();

            var bindingSource2 = new BindingSource();
            bindingSource2.DataSource = _tiposBL.ObtenerTipos();

            var reporte = new ReporteDepositos();
            reporte.SetDataSource(bindingSource1);

            crystalReportViewer1.ReportSource = reporte ;
            crystalReportViewer1.RefreshReport();


        }
    }
}
