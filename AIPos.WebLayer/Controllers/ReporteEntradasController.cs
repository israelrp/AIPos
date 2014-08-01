using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIPos.Domain;
using AIPos.BusinessLayer;
using DevExpress.Web.Mvc;

namespace AIPos.WebLayer.Controllers
{
    public class ReporteEntradasController : Controller
    {
        //
        // GET: /ReporteEntradas/

        public ActionResult Index()
        {
            ViewData["Sucursales"] = new BOSucursal().SelectAll();
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialEntradas(int? SucursalId, DateTime? Fecha)
        {
            List<ReporteEntrada> model = new List<ReporteEntrada>();
            if (SucursalId.HasValue && Fecha.HasValue)
            {
                BOEntrada boEntrada = new BOEntrada();
                model = boEntrada.SelectReporteByDay(SucursalId.Value, Fecha.Value);
            }
            return PartialView("_GridViewPartialEntradas", model);
        }
    }
}
