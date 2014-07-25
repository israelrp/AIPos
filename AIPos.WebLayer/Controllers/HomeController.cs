using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIPos.BusinessLayer;
using AIPos.DAO.Implementation.Enums;
using AIPos.Domain;

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
            resumen.Ventas = boVenta.RecuperarResumenSemanal(TipoVenta.Venta);
            resumen.Ordenes = boVenta.RecuperarResumenSemanal(TipoVenta.Orden);
            resumen.Domiciolio = boVenta.RecuperarResumenSemanal(TipoVenta.Domicilio);
            resumen.Apartados = boVenta.RecuperarResumenSemanal(TipoVenta.Apartado);
            resumen.Servicios = boVenta.RecuperarResumenSemanal(TipoVenta.Servicio);
            return PartialView(resumen);
        }

        

    }
}
