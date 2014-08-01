using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.Domain;
using AIPos.BusinessLayer;

namespace AIPos.WebLayer.Controllers
{
    public class ReporteInventariosController : Controller
    {
        //
        // GET: /ReporteInventarios/

        public ActionResult Index()
        {
            ViewData["Sucursales"] = new BOSucursal().SelectAll();
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialInventarios(int? SucursalId, DateTime? Fecha)
        {
            List<ReporteInventario> model = new List<ReporteInventario>();
            if (SucursalId.HasValue && Fecha.HasValue)
            {
                model = new BOInventario().SelectReporteSucursalFecha(SucursalId.Value, Fecha.Value);
            }
            return PartialView("_GridViewPartialInventarios", model);
        }
    }
}
