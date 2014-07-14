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

namespace AIPos.DekstopLayer.EntregasDomicilio
{
    /// <summary>
    /// Interaction logic for DXEnviar.xaml
    /// </summary>
    public partial class DXEnviar : DXWindow
    {
        public int VentaId { get; set; }
        

        public DXEnviar()
        {
            InitializeComponent();
            deFechaSalida.DateTime = DateTime.Now;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtRepartidor.Text.Trim().Length > 0)
            {
                ServiceServicioApartado.SServicioApartadoClient servicioApartadoClient = new ServiceServicioApartado.SServicioApartadoClient();
                ServiceSeguimientoServicioApartado.SSeguimientoServicioApartadoClient seguimientoClient = new ServiceSeguimientoServicioApartado.SSeguimientoServicioApartadoClient();
                SeguimientoServicioApartado seguimiento = seguimientoClient.SelectById(VentaId);
                ServicioApartado servicioApartado = servicioApartadoClient.SelectById(VentaId);
                if (servicioApartado != null)
                {
                    if (seguimiento == null)
                    {
                        seguimiento = new SeguimientoServicioApartado();
                        seguimiento.FechaSalidaRepartidor = deFechaSalida.DateTime;
                        seguimiento.Repartidor = txtRepartidor.Text;
                        seguimiento.VentaId = VentaId;
                        seguimiento.FechaSolicitud = DateTime.Now;
                        seguimientoClient.Insert(seguimiento);
                    }
                    else
                    {
                        seguimiento.FechaSalidaRepartidor = deFechaSalida.DateTime;
                        seguimiento.Repartidor = txtRepartidor.Text;
                        seguimiento.VentaId = VentaId;
                        seguimiento.FechaSolicitud = DateTime.Now;
                        seguimientoClient.Update(seguimiento);
                    }
                    servicioApartado.EstatusId = 5;
                    servicioApartadoClient.Update(servicioApartado);
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
