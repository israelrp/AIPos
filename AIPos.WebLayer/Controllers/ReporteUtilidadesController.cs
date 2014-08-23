using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace AIPos.WebLayer.Controllers
{
    public class ReporteUtilidadesController : Controller
    {
        //
        // GET: /ReporteUtilidades/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialUtilidad(DateTime? Fecha, int? SucursalId)
        {
            List<Domain.ReporteUtilidad> model = new List<Domain.ReporteUtilidad>();
            if (SucursalId.HasValue && Fecha.HasValue)
                model = new BusinessLayer.BOVenta().RecuperarUtilidadesProducto(SucursalId.Value, Fecha.Value);
            return PartialView("_GridViewPartialUtilidad", model);
        }
    }
}
