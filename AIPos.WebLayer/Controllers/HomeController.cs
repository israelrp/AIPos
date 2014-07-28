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

        public PartialViewResult _RecuperarPedidosSucursal()
        {
            BOPedidoSucursal boPedidoSucursal=new BOPedidoSucursal();
            List<Sucursal> sucursales = new BOSucursal().SelectAll();
            List<ReportePedidosSucursalModel> pedidosModel = new List<ReportePedidosSucursalModel>();
            foreach (Sucursal item in sucursales)
            {
                List<PedidoSucursal> pedidos = boPedidoSucursal.SelectBySucursalFechaEntrega(item.Id, DateTime.Now);
                foreach (PedidoSucursal pedido in pedidos)
                {
                    ReportePedidosSucursalModel reporte = new ReportePedidosSucursalModel();
                    reporte.Cantidad = pedido.Cantidad;
                    reporte.Producto = pedido.Producto;
                    reporte.Sucursal = item.Nombre;
                    reporte.Unidad = new BOUnidad().SelectById(pedido.UnidadId).Nombre;
                    pedidosModel.Add(reporte);
                }
            }
            return PartialView(pedidosModel);
        }

        public PartialViewResult _RecuperarSeguimientosServiciosApartados()
        {
            BOSeguimientoServicioApartado boSeguimiento = new BOSeguimientoServicioApartado();
            return PartialView(boSeguimiento.RecuperarReporteFecha(DateTime.Now));
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



    }
}
