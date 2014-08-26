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
using System.ServiceModel;

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
            txtVentaId.Focus();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            int VentaId = 0;
            try
            {
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
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Ha ocurrido un problema con la conexión al servicio de principal del sistema. Detalles: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.VentaRecuperada = null;
            this.Close();
        }

        private void txtVentaId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAceptar.Focus();
            }
        }
    }
}
