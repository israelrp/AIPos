using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace AIPos.WebLayer.Controllers
{
    public class ReporteCorteCajaController : Controller
    {
        [Authorize]
        //
        // GET: /ReporteCorteCaja/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult _GridViewPartialReporteCorteCaja(DateTime? FechaInicio, DateTime? FechaFin, int? SucursalId_VI)
        {
            List<Domain.ReporteCorteCaja> model = new List<Domain.ReporteCorteCaja>();
            if(FechaInicio.HasValue && FechaFin.HasValue)
                model = new BusinessLayer.BOVenta().RecuperarCorteCaja(FechaInicio.Value, FechaFin.Value, SucursalId_VI);
            return PartialView("_GridViewPartialReporteCorteCaja", model);
        }


    }
}
