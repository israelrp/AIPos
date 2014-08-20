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
            ViewData["Semanas"] = new BusinessLayer.Tools.SemanaManager().RecuperarSemanaAnio();
            int añoInicio=DateTime.Now.Year-10;
            int añoFin=DateTime.Now.Year+10;
            List<int> años = new List<int>();
            for (int i = añoInicio; i <= añoFin; i++)
            {
                años.Add(i);
            }
            ViewData["Anios"] = años;
            ViewData["AnioActual"] = 10;
            ViewData["SemanaActual"] =new BusinessLayer.Tools.SemanaManager().SemanaActual();
            return View();
        }

        private PartialViewResult ArmarGraficar(ResumenVentasModel resumen)
        {
            List<ResumenVentasChartDataModel> resumenChartData = new List<ResumenVentasChartDataModel>();
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
            return PartialView("_ResumenVentas", resumenChartData);
        }

        public PartialViewResult ResumenVentasFiltrado(int Semana, int Año, string SucursalId)
        {
            BOVenta boVenta = new BOVenta();
            ResumenVentasModel resumen = new ResumenVentasModel();
            resumen.Ventas = boVenta.RecuperarResumenSemanal(TipoVenta.Venta, Semana, Año, SucursalId);
            resumen.Ordenes = boVenta.RecuperarResumenSemanal(TipoVenta.Orden, Semana, Año,SucursalId);
            resumen.Domiciolio = boVenta.RecuperarResumenSemanal(TipoVenta.Domicilio, Semana, Año, SucursalId);
            resumen.Apartados = boVenta.RecuperarResumenSemanal(TipoVenta.Apartado, Semana, Año, SucursalId);
            resumen.Servicios = boVenta.RecuperarResumenSemanal(TipoVenta.Servicio, Semana, Año, SucursalId);
            return ArmarGraficar(resumen);
        }

        public PartialViewResult _ResumenVentas()
        {
            BOVenta boVenta = new BOVenta();
            ResumenVentasModel resumen = new ResumenVentasModel();
            resumen.Ventas = boVenta.RecuperarResumenSemanal(TipoVenta.Venta);
            resumen.Ordenes = boVenta.RecuperarResumenSemanal(TipoVenta.Orden);
            resumen.Domiciolio = boVenta.RecuperarResumenSemanal(TipoVenta.Domicilio);
            resumen.Apartados = boVenta.RecuperarResumenSemanal(TipoVenta.Apartado);
            resumen.Servicios = boVenta.RecuperarResumenSemanal(TipoVenta.Servicio);
            return ArmarGraficar(resumen);
        }

        public PartialViewResult _RecuperarPedidosSucursal(int? SucursalId)
        {
            List<ReportePedidoSucursal> pedidos = new List<ReportePedidoSucursal>();
            if(SucursalId.HasValue)
            {
                BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
                pedidos = boPedidoSucursal.SelectReporteBySucursalFechaEntrega(SucursalId.Value, BusinessLayer.Tools.TimeConverter.GetDateTimeNowMexico());
            }
            return PartialView(pedidos);
        }

        public PartialViewResult _RecuperarSeguimientosServiciosApartados(int? SucursalId)
        {
            List<ReporteSeguimientoServicioApartado> reporte = new List<ReporteSeguimientoServicioApartado>();
            if (SucursalId.HasValue)
            {
                BOSeguimientoServicioApartado boSeguimiento = new BOSeguimientoServicioApartado();
                reporte = boSeguimiento.RecuperarReporteFechaSucursal(BusinessLayer.Tools.TimeConverter.GetDateTimeNowMexico(), SucursalId.Value);
            }
            return PartialView(reporte);
        }

        public PartialViewResult _RecuperarCortesCaja()
        {
            BOCorteCaja boCorteCaja = new BOCorteCaja();
            List<CorteCaja> cortes = boCorteCaja.SelectByFecha(BusinessLayer.Tools.TimeConverter.GetDateTimeNowMexico());
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
                reporte = boEntrada.SelectReporteByDay(SucursalId.Value, BusinessLayer.Tools.TimeConverter.GetDateTimeNowMexico());
            }
            return PartialView(reporte);

        }

        public PartialViewResult _RecuperarRetiros(int? SucursalId)
        {
            List<ReporteRetiroDinero> reporte = new List<ReporteRetiroDinero>();
            if (SucursalId.HasValue)
            {
                BORetiroDinero boRetiroDinero = new BORetiroDinero();
                reporte = boRetiroDinero.SelectReporteByFechaSucursal(SucursalId.Value, BusinessLayer.Tools.TimeConverter.GetDateTimeNowMexico(), false);
            }
            return PartialView(reporte);
        }

    }


}
