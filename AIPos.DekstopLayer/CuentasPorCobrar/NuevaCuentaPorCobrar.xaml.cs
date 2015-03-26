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
        public string Cliente { get; set; }
        public int FolioVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Tipo { get; set; }

        public NuevaCuentaPorCobrar()
        {
            InitializeComponent();
        }

        public NuevaCuentaPorCobrar(CuentaPorCobrar cuentaPorCobrar)
        {
            InitializeComponent();
            this.CuentaPorCobrar = cuentaPorCobrar;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            CuentaPorCobrar cxc = new CuentaPorCobrar();
            cxc.ClienteId = CuentaPorCobrar.ClienteId;
            cxc.VentaId = CuentaPorCobrar.VentaId;
            cxc.UsuarioId = General.UsuarioLogueado.Id;
            cxc.Descripcion = txtDescripcion.Text;
            cxc.Estatus = 0;
            cxc.Fecha = CuentaPorCobrar.Fecha;
            cxc.FechaLimite = deFechaLimite.DateTime;
            cxc.Importe = CuentaPorCobrar.Importe;
            ServiceCuentaPorCobrar.SCuentaPorCobrarClient cxcClient = new ServiceCuentaPorCobrar.SCuentaPorCobrarClient();
            cxcClient.NuevaCuentaPorCobrar(cxc);
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (CuentaPorCobrar != null)
            {
                this.txtCliente.Text = Cliente;
                this.txtFolioVenta.Text = FolioVenta.ToString();
                this.txtImporte.Text = CuentaPorCobrar.Importe.ToString();
                this.deFechaVenta.DateTime = FechaVenta;
                this.txtDescripcion.Text = "Cuenta por cobrar por "+ Tipo +" con folio " + FolioVenta.ToString();
                this.deFechaLimite.DateTime = CuentaPorCobrar.FechaLimite;
            }
        }
    }
}
