using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using AIPos.Domain;

namespace AIPos.DekstopLayer.CuentasPorCobrar
{
    /// <summary>
    /// Interaction logic for NuevaCuentaPorCobrar.xaml
    /// </summary>
    public partial class NuevaCuentaPorCobrar : DXWindow
    {
        public Venta venta { get; set; }
        public CuentaPorCobrar CuentaPorCobrar { get; set; }
        public NuevaCuentaPorCobrar()
        {
            InitializeComponent();
        }

        public NuevaCuentaPorCobrar(CuentaPorCobrar cuentaPorCobrar)
        {
            InitializeComponent();

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            CuentaPorCobrar cxc = new CuentaPorCobrar();
            cxc.ClienteId = venta.ClienteId;
            cxc.VentaId = venta.Id;
            cxc.UsuarioId = General.UsuarioLogueado.Id;
            cxc.Descripcion = txtDescripcion.Text;
            cxc.Estatus = 0;
            cxc.Fecha = DateTime.Now;
            cxc.FechaLimite = deFechaLimite.DateTime;
            cxc.Importe = venta.Total;
            ServiceCuentaPorCobrar.SCuentaPorCobrarClient cxcClient = new ServiceCuentaPorCobrar.SCuentaPorCobrarClient();
            cxcClient.NuevaCuentaPorCobrar(cxc);
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
