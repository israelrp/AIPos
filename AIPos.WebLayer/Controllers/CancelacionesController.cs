using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.BusinessLayer;
using AIPos.DAO.Implementation.Enums;
using AIPos.Domain;

namespace AIPos.WebLayer.Controllers
{
    public class CancelacionesController : Controller
    {
        //
        // GET: /Cancelaciones/

        public ActionResult Index()
        {
            ViewData["Sucursales"] = new BOSucursal().SelectAll();
            return View();
        }

        public JsonResult RecuperarVenta(TipoVenta Tipo, int SucursalId, DateTime Fecha, int Folio)
        {
            Venta venta = new BOVenta().SelectByTipoFolio(Tipo, SucursalId, Fecha, Folio);
            if (venta != null)
            {
                venta.Cliente = new BOCliente().SelectById(venta.ClienteId);
                venta.Usuario = new BOUsuario().SelectById(venta.UsuarioId);
            }
            return Json(venta, JsonRequestBehavior.AllowGet);
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialVentaDetalle(int? VentaId)
        {
            List<VentaDetalle> model=new List<VentaDetalle>();
            if (VentaId.HasValue)
                model = new BOVentaDetalle().SelectByVentaId(VentaId.Value);
            return PartialView("_GridViewPartialVentaDetalle", model);
        }
    }
}
