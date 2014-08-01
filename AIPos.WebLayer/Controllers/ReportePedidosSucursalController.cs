using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIPos.BusinessLayer;
using AIPos.Domain;
using DevExpress.Web.Mvc;

namespace AIPos.WebLayer.Controllers
{
    public class ReportePedidosSucursalController : Controller
    {
        //
        // GET: /ReportePedidosSucursal/

        public ActionResult Index()
        {
            ViewData["Sucursales"] = new BOSucursal().SelectAll();
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialPedidosSucursal(int? SucursalId, DateTime? Fecha)
        {
            List<ReportePedidoSucursal> model = new List<ReportePedidoSucursal>();
            if (SucursalId.HasValue && Fecha.HasValue)
            {
                BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
                model = boPedidoSucursal.SelectReporteBySucursalFechaEntrega(SucursalId.Value, Fecha.Value);
            }
            return PartialView("_GridViewPartialPedidosSucursal", model);
        }
    }
}
