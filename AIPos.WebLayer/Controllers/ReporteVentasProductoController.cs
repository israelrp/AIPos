using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace AIPos.WebLayer.Controllers
{
    public class ReporteVentasProductoController : Controller
    {
        //
        // GET: /ReporteVentasProducto/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialVentasProducto(int? SucursalId_VI)
        {
            List<Domain.ReporteVentasProducto> model = new List<Domain.ReporteVentasProducto>();
            if(SucursalId_VI.HasValue)
                model = new BusinessLayer.BOVenta().RecuperarVentasProducto(SucursalId_VI.Value);
            return PartialView("_GridViewPartialVentasProducto", model);
        }
    }
}
