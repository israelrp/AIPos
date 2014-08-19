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

namespace AIPos.DekstopLayer.Ventas
{
    /// <summary>
    /// Interaction logic for DXVentaId.xaml
    /// </summary>
    public partial class DXVentaId : DXWindow
    {
        public Venta VentaRecuperada { get; set; }

        public DXVentaId()
        {
            this.VentaRecuperada = null;
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            int VentaId = 0;
            if (int.TryParse(txtVentaId.Text, out VentaId))
            {
                ServiceVenta.ISVentaClient ventaClient = new ServiceVenta.ISVentaClient();
                Venta venta = ventaClient.RecuperarVenta(VentaId);
                if (venta != null)
                {
                    if (venta.Estatus == 0)
                    {
                        venta.VentasDetalle = ventaClient.RecuperarVentaDetalle(VentaId).ToList();
                        this.VentaRecuperada = venta;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Esta venta ya fue cerrada, para volver a cobrarla necesitas cancelar y volver a hacerla.");
                    }
                }
                else
                {
                    MessageBox.Show("La venta con el Id " + VentaId.ToString() + " no fue enontrado.");
                }
            }
            else
            {
                MessageBox.Show("El Id de la venta debe ser números enteros.");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.VentaRecuperada = null;
            this.Close();
        }
    }
}
