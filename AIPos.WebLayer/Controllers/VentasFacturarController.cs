using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.BusinessLayer;
using AIPos.Domain;

namespace AIPos.WebLayer.Controllers
{
    public class VentasFacturarController : Controller
    {
        //
        // GET: /VentasFaturar/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialVentas()
        {
            var model = new BOVenta().RecuperarVentasFacturar();
            return PartialView("_GridViewPartialVentas", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialVentasUpdate(AIPos.Domain.ReporteCorteCaja item)
        {
            List<ReporteCorteCaja> model = new List<ReporteCorteCaja>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOVenta boVenta = new BOVenta();
                    Venta venta = boVenta.SelectById(item.VentaId);
                    venta.Facturado = item.Facturado;
                    boVenta.Update(venta);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            model = new BOVenta().RecuperarVentasFacturar();
            return PartialView("_GridViewPartialVentas", model);
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialVentaDetalle(int? VentaId)
        {
            List<VentaDetalle> model = new List<VentaDetalle>();
            if (VentaId.HasValue)
            {
                model = new BOVentaDetalle().SelectByVentaId(VentaId.Value);
            }
            return PartialView("_GridViewPartialVentaDetalle", model);
        }

        [HttpPost]
        public JsonResult RecuperarDatosCliente(int VentaId)
        {
            Venta venta = new BOVenta().SelectById(VentaId);
            Cliente cliente = new BOCliente().SelectById(venta.ClienteId);
            DireccionFacturacion direccionFacturacion = new BODireccionFacturacion().SelectByClienteId(cliente.Id).FirstOrDefault();
            if(direccionFacturacion!=null)
                cliente.DireccionFacturaion = new BODireccion().SelectById(direccionFacturacion.DireccionId);
            return Json(cliente, JsonRequestBehavior.AllowGet);
        }
    }
}
