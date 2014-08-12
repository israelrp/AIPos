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


namespace AIPos.DekstopLayer.CorteCaja
{
    /// <summary>
    /// Interaction logic for DXCorteCaja.xaml
    /// </summary>
    public partial class DXCorteCaja : DXWindow
    {
        public DXCorteCaja()
        {
            InitializeComponent();
            deFechaInicio.DateTime = DateTime.Now;
            ServiceUsuario.SUsuarioClient usuarioClient = new ServiceUsuario.SUsuarioClient();
            cmbUsuarios.ItemsSource = usuarioClient.SelectAll();
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUsuarios.SelectedItem != null)
            {
                ServiceVenta.ISVentaClient ventaClient = new ServiceVenta.ISVentaClient();
                Domain.Usuario usuario = (Domain.Usuario)cmbUsuarios.SelectedItem;
                RecuperarDatos();
            }
            else
            {
                MessageBox.Show("Debe de seleccionar un usuario para realizar el corte de caja.");
            }
        }

        private void RecuperarDatos()
        {
            Domain.CorteCaja corteCaja = new Domain.CorteCaja();
            ServiceVenta.ISVentaClient ventaClient = new ServiceVenta.ISVentaClient();
            Domain.Usuario usuario = (Domain.Usuario)cmbUsuarios.SelectedItem;
            List<Domain.ReporteCorteCaja> reporte = ventaClient.RecuperarCorteCaja(deFechaInicio.DateTime.ToFileTimeUtc(), deFechaInicio.DateTime.ToFileTimeUtc(), General.ConfiguracionApp.SucursalId).ToList().Where(x => x.Usuario == usuario.Nombre).ToList();
            List<Domain.RetiroDinero> retiros = new ServiceRetiroDinero.ISRetiroDineroClient().SelectAllByFechaSucursal(deFechaInicio.DateTime, General.ConfiguracionApp.SucursalId).ToList();
            corteCaja.UsuarioId = usuario.Id;
            corteCaja.SucursalId = General.ConfiguracionApp.SucursalId;
            corteCaja.Fecha = deFechaInicio.DateTime;
            corteCaja.TotalVentas = reporte.Where(x => x.Tipo.ToUpper() == "VENTA" && x.Folio > 0 && x.Cancelado==false).Sum(x => x.Total).Value;
            corteCaja.TotalMostrador = reporte.Where(x => x.Tipo.ToUpper() == "VENTA" && x.FolioCancelado > 0 && x.Cancelado == false).Sum(x => x.Total).Value;
            corteCaja.TotalDomicilio = reporte.Where(x => x.Tipo.ToUpper() == "DOMICILIO" && x.Cancelado == false).Sum(x => x.Total).Value;
            corteCaja.TotalRetiros = retiros.Sum(x => x.Monto);
            corteCaja.TotalAbonosServicios = reporte.Where(x => x.Tipo.ToUpper() == "SERVICIO" && x.Cancelado == false).Sum(x => x.Anticipo).Value;
            corteCaja.TotalAbonosApartados = reporte.Where(x => x.Tipo.ToUpper() == "APARTADO" && x.Cancelado == false).Sum(x => x.Anticipo).Value;            
            corteCaja.TotalCaja = corteCaja.TotalVentas + corteCaja.TotalMostrador + corteCaja.TotalDomicilio  - corteCaja.TotalRetiros;
            gridCorte.ItemsSource = reporte;
            decimal totalAbonos = corteCaja.TotalAbonosApartados + corteCaja.TotalAbonosServicios;
            decimal totalVentas = corteCaja.TotalMostrador + corteCaja.TotalDomicilio+ corteCaja.TotalVentas;
            lblTotalCaja.Content = "Total en ventas: "+ totalVentas.ToString("c") + " - Total de retiros: " + corteCaja.TotalRetiros.ToString("c") + " = TOTAL EN CAJA: " + corteCaja.TotalCaja.ToString("c");
        }



        private void btnImprimir_Click_1(object sender, RoutedEventArgs e)
        {
            Domain.CorteCaja corteCaja = new Domain.CorteCaja();
            ServiceVenta.ISVentaClient ventaClient = new ServiceVenta.ISVentaClient();
            Domain.Usuario usuario = (Domain.Usuario)cmbUsuarios.SelectedItem;
            List<Domain.ReporteCorteCaja> reporte = ventaClient.RecuperarCorteCaja(deFechaInicio.DateTime.ToFileTimeUtc(), deFechaInicio.DateTime.ToFileTimeUtc(), General.ConfiguracionApp.SucursalId).ToList().Where(x => x.Usuario == usuario.Nombre).ToList(); 
            List<Domain.RetiroDinero> retiros = new ServiceRetiroDinero.ISRetiroDineroClient().SelectAllByFechaSucursal(deFechaInicio.DateTime, General.ConfiguracionApp.SucursalId).ToList();
            corteCaja.UsuarioId = usuario.Id;
            corteCaja.SucursalId = General.ConfiguracionApp.SucursalId;
            corteCaja.Fecha = deFechaInicio.DateTime;
            corteCaja.TotalVentas = reporte.Where(x => x.Tipo.ToUpper() == "VENTA" && x.Folio > 0 && x.Cancelado == false).Sum(x => x.Total).Value;
            corteCaja.TotalMostrador = reporte.Where(x => x.Tipo.ToUpper() == "VENTA" && x.FolioCancelado > 0 && x.Cancelado == false).Sum(x => x.Total).Value;
            corteCaja.TotalDomicilio = reporte.Where(x => x.Tipo.ToUpper() == "DOMICILIO" && x.Cancelado == false).Sum(x => x.Total).Value;
            corteCaja.TotalServicios = reporte.Where(x => x.Tipo.ToUpper() == "SERVICIO" && x.Cancelado == false).Sum(x => x.Total).Value;
            corteCaja.TotalApartados = reporte.Where(x => x.Tipo.ToUpper() == "APARTADO" && x.Cancelado == false).Sum(x => x.Total).Value;
            corteCaja.TotalRetiros = retiros.Sum(x => x.Monto);
            corteCaja.TotalAbonosServicios = reporte.Where(x => x.Tipo.ToUpper() == "SERVICIO" && x.Cancelado == false).Sum(x => x.Anticipo).Value;
            corteCaja.TotalAbonosApartados = reporte.Where(x => x.Tipo.ToUpper() == "APARTADO" && x.Cancelado == false).Sum(x => x.Anticipo).Value;
            corteCaja.TotalCancelados = reporte.Where(x => x.Cancelado == true).Sum(x => x.Total).Value;
            corteCaja.TotalCaja = corteCaja.TotalVentas + corteCaja.TotalMostrador + corteCaja.TotalDomicilio - corteCaja.TotalRetiros;
            corteCaja.TotalCambio = reporte.Where(x=>x.Cambio>0).Sum(c => c.Cambio).Value;
            corteCaja.QuienRetira = "";
            corteCaja.CorteEntregado = 0;
            corteCaja.Diferencia = corteCaja.TotalCaja - corteCaja.CorteEntregado;
            corteCaja.TotalTickectsMostrador=reporte.Count();
            corteCaja.TotalTickectsDomicilio = reporte.Count(x => x.Tipo == "DOMICILIO");
            corteCaja.TotalTickectsMostrador = reporte.Count(x => x.Tipo == "MOSTRATOR");
            DXRealizarCorteCaja realizarCorte = new DXRealizarCorteCaja();
            realizarCorte.CorteCaja = corteCaja;
            realizarCorte.ShowDialog();
        }

        private void gridCorte_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ReporteCorteCaja corte = (ReporteCorteCaja)gridCorte.SelectedItem;
            if (corte != null)
            {
                List<VentaDetalle> ventaDetalle = new ServiceVenta.ISVentaClient().RecuperarVentaDetalle(corte.VentaId).ToList();
                if (ventaDetalle.Count() > 0)
                {
                    DXVentaDetalle dxventaDetalle = new DXVentaDetalle();
                    dxventaDetalle.gridVentaDetalle.ItemsSource = ventaDetalle;
                    dxventaDetalle.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No fue posible recuperar los detalles de la venta.");
                }
            }
        }
    }
}
