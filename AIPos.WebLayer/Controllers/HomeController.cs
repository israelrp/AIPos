using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIPos.BusinessLayer;
using AIPos.DAO.Implementation.Enums;
using AIPos.Domain;
using AIPos.WebLayer.Models;

namespace AIPos.WebLayer.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewData["Sucursales"] = new BOSucursal().SelectAll();
            return View();
        }

        public PartialViewResult _ResumenVentas()
        {
            BOVenta boVenta = new BOVenta();
            ResumenVentasModel resumen = new ResumenVentasModel();
            List<ResumenVentasChartDataModel> resumenChartData = new List<ResumenVentasChartDataModel>();
            resumen.Ventas = boVenta.RecuperarResumenSemanal(TipoVenta.Venta);
            resumen.Ordenes = boVenta.RecuperarResumenSemanal(TipoVenta.Orden);
            resumen.Domiciolio = boVenta.RecuperarResumenSemanal(TipoVenta.Domicilio);
            resumen.Apartados = boVenta.RecuperarResumenSemanal(TipoVenta.Apartado);
            resumen.Servicios = boVenta.RecuperarResumenSemanal(TipoVenta.Servicio);
            foreach (ConteoVenta item in resumen.Ventas)
            {
                ResumenVentasChartDataModel resumenChartItem = new ResumenVentasChartDataModel();
                resumenChartItem.TipoVenta = "Ventas";
                resumenChartItem.Conteo = item.Conteo;
                resumenChartItem.Total = item.ImporteDia;
                resumenChartItem.Fecha = item.Fecha;
                resumenChartData.Add(resumenChartItem);
            }

            foreach (ConteoVenta item in resumen.Ordenes)
            {
                ResumenVentasChartDataModel resumenChartItem = new ResumenVentasChartDataModel();
                resumenChartItem.TipoVenta = "Ordenes";
                resumenChartItem.Conteo = item.Conteo;
                resumenChartItem.Total = item.ImporteDia;
                resumenChartItem.Fecha = item.Fecha;
                resumenChartData.Add(resumenChartItem);
            }

            foreach (ConteoVenta item in resumen.Domiciolio)
            {
                ResumenVentasChartDataModel resumenChartItem = new ResumenVentasChartDataModel();
                resumenChartItem.TipoVenta = "Domicilio";
                resumenChartItem.Conteo = item.Conteo;
                resumenChartItem.Total = item.ImporteDia;
                resumenChartItem.Fecha = item.Fecha;
                resumenChartData.Add(resumenChartItem);
            }

            foreach (ConteoVenta item in resumen.Apartados)
            {
                ResumenVentasChartDataModel resumenChartItem = new ResumenVentasChartDataModel();
                resumenChartItem.TipoVenta = "Apartados";
                resumenChartItem.Conteo = item.Conteo;
                resumenChartItem.Total = item.ImporteDia;
                resumenChartItem.Fecha = item.Fecha;
                resumenChartData.Add(resumenChartItem);
            }

            foreach (ConteoVenta item in resumen.Servicios)
            {
                ResumenVentasChartDataModel resumenChartItem = new ResumenVentasChartDataModel();
                resumenChartItem.TipoVenta = "Servicios";
                resumenChartItem.Conteo = item.Conteo;
                resumenChartItem.Total = item.ImporteDia;
                resumenChartItem.Fecha = item.Fecha;
                resumenChartData.Add(resumenChartItem);
            }
            return PartialView(resumenChartData);
        }

        public PartialViewResult _RecuperarPedidosSucursal(int? SucursalId)
        {
            List<ReportePedidoSucursal> pedidos = new List<ReportePedidoSucursal>();
            if(SucursalId.HasValue)
            {
                BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
                pedidos = boPedidoSucursal.SelectReporteBySucursalFechaEntrega(SucursalId.Value, DateTime.Now);
            }
            return PartialView(pedidos);
        }

        public PartialViewResult _RecuperarSeguimientosServiciosApartados(int? SucursalId)
        {
            List<ReporteSeguimientoServicioApartado> reporte = new List<ReporteSeguimientoServicioApartado>();
            if (SucursalId.HasValue)
            {
                BOSeguimientoServicioApartado boSeguimiento = new BOSeguimientoServicioApartado();
                reporte = boSeguimiento.RecuperarReporteFechaSucursal(DateTime.Now, SucursalId.Value);
            }
            return PartialView(reporte);
        }

        public PartialViewResult _RecuperarCortesCaja()
        {
            BOCorteCaja boCorteCaja = new BOCorteCaja();
            List<CorteCaja> cortes= boCorteCaja.SelectByFecha(DateTime.Now);
            foreach (CorteCaja corte in cortes)
            {
                corte.Sucursal = new BOSucursal().SelectById(corte.SucursalId);
                corte.Usuario = new BOUsuario().SelectById(corte.UsuarioId);
            }
            return PartialView(cortes);
        }

        public PartialViewResult _RecuperarEntradas(int? SucursalId)
        {
            List<ReporteEntrada> reporte=new List<ReporteEntrada>();
            if (SucursalId.HasValue)
            {
                BOEntrada boEntrada = new BOEntrada();
                reporte = boEntrada.SelectReporteByDay(SucursalId.Value, DateTime.Now);
            }
            return PartialView(reporte);

        }

        public PartialViewResult _RecuperarRetiros(int? SucursalId)
        {
            List<ReporteRetiroDinero> reporte = new List<ReporteRetiroDinero>();
            if (SucursalId.HasValue)
            {
                BORetiroDinero boRetiroDinero = new BORetiroDinero();
                reporte = boRetiroDinero.SelectReporteByFechaSucursal(SucursalId.Value, DateTime.Now, false);
            }
            return PartialView(reporte);
        }

    }
}
