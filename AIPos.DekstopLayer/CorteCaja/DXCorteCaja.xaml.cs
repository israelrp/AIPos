﻿using System;
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
            ServiceVenta.ISVentaClient ventaClient = new ServiceVenta.ISVentaClient();
            Domain.Usuario usuario = (Domain.Usuario)cmbUsuarios.SelectedItem;
            gridCorte.ItemsSource = ventaClient.RecuperarCorteCaja(deFechaInicio.DateTime.ToFileTimeUtc(), deFechaInicio.DateTime.ToFileTimeUtc(), General.ConfiguracionApp.SucursalId).ToList().Where(x=>x.Usuario==usuario.Nombre);
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
            corteCaja.TotalMostrador = reporte.Where(x => x.Tipo.ToUpper() == "VENTA" && x.FolioCancelado>0).Sum(x => x.Total).Value;
            corteCaja.TotalDomicilio = reporte.Where(x => x.Tipo.ToUpper() == "DOMICILIO").Sum(x => x.Total).Value;
            corteCaja.TotalServicios = reporte.Where(x => x.Tipo.ToUpper() == "SERVICIO").Sum(x => x.Total).Value;
            corteCaja.TotalApartados = reporte.Where(x => x.Tipo.ToUpper() == "APARTADO").Sum(x => x.Total).Value;
            corteCaja.TotalRetiros = retiros.Sum(x => x.Monto);
            corteCaja.TotalAbonosServicios = reporte.Where(x => x.Tipo.ToUpper() == "SERVICIO").Sum(x => x.Anticipo).Value;
            corteCaja.TotalAbonosApartados = reporte.Where(x => x.Tipo.ToUpper() == "APARTADO").Sum(x => x.Anticipo).Value;
            corteCaja.TotalCancelados = 0;
            corteCaja.TotalCaja = corteCaja.TotalMostrador + corteCaja.TotalDomicilio + corteCaja.TotalAbonosApartados + corteCaja.TotalAbonosServicios - corteCaja.TotalRetiros;
            corteCaja.TotalCambio = reporte.Where(x=>x.Cambio>0).Sum(c => c.Cambio).Value;
            corteCaja.QuienRetira = "";
            corteCaja.CorteEntregado = 0;
            corteCaja.Diferencia = corteCaja.TotalCaja - corteCaja.CorteEntregado;
            corteCaja.TotalTickectsMostrados=reporte.Count();
            corteCaja.TotalTickectsDomicilio = reporte.Count(x => x.Tipo == "DOMICILIO");
            corteCaja.TotalTickectsMostrador = reporte.Count(x => x.Tipo == "MOSTRATOR");
            DXRealizarCorteCaja realizarCorte = new DXRealizarCorteCaja();
            realizarCorte.CorteCaja = corteCaja;
            realizarCorte.ShowDialog();
        }
    }
}
