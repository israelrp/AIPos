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

namespace AIPos.DekstopLayer.EntregasDomicilio
{
    /// <summary>
    /// Interaction logic for DXEnviar.xaml
    /// </summary>
    public partial class DXEnviar : DXWindow
    {
        public List<int> ventaIds=new List<int>();
        public List<int> VentaIds
        {
            get
            {
                return ventaIds;
            }
            set
            {
                ventaIds = value;
                lblVentas.Content = "Ha seleccionado " + ventaIds.Count().ToString() + " ventas para enviar.";
            }
        }
        

        public DXEnviar()
        {
            InitializeComponent();
            deFechaSalida.DateTime = DateTime.Now;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtRepartidor.Text.Trim().Length > 0)
            {
                try
                {
                    ServiceServicioApartado.SServicioApartadoClient servicioApartadoClient = new ServiceServicioApartado.SServicioApartadoClient();
                    ServiceSeguimientoServicioApartado.SSeguimientoServicioApartadoClient seguimientoClient = new ServiceSeguimientoServicioApartado.SSeguimientoServicioApartadoClient();
                    int folio = seguimientoClient.GenerarNuevoFolioEnvio();
                    foreach (int ventaId in VentaIds)
                    {
                        SeguimientoServicioApartado seguimiento = seguimientoClient.SelectById(ventaId);
                        ServicioApartado servicioApartado = servicioApartadoClient.SelectById(ventaId);
                        if (servicioApartado != null)
                        {
                            if (seguimiento == null)
                            {
                                seguimiento = new SeguimientoServicioApartado();
                                seguimiento.FechaSalidaRepartidor = deFechaSalida.DateTime;
                                seguimiento.Repartidor = txtRepartidor.Text;
                                seguimiento.VentaId = ventaId;
                                seguimiento.FechaSolicitud = DateTime.Now;
                                seguimiento.Folio = folio;
                                seguimientoClient.Insert(seguimiento);
                            }
                            else
                            {
                                seguimiento.FechaSalidaRepartidor = deFechaSalida.DateTime;
                                seguimiento.Repartidor = txtRepartidor.Text;
                                seguimiento.VentaId = ventaId;
                                seguimiento.FechaSolicitud = DateTime.Now;
                                seguimiento.Folio = folio;
                                seguimientoClient.Update(seguimiento);
                            }
                            servicioApartado.EstatusId = 5;
                            servicioApartadoClient.Update(servicioApartado);
                        }
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
            else
            {
                MessageBox.Show("Debes capturar el nombre del repartidor");
            }
            this.Close();
        }
    }
}
