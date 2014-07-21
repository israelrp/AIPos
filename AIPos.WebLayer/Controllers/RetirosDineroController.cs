using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;


namespace AIPos.WebLayer.Controllers
{
    public class RetirosDineroController : Controller
    {
        [Authorize]
        //
        // GET: /RetirosDinero/

        public ActionResult Reporte()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialRetirosDinero(DateTime Fecha, int? SucursalId_VI)
        {
            List<Domain.RetiroDinero> model = new List<Domain.RetiroDinero>();
            if(SucursalId_VI.HasValue)
                model = new BusinessLayer.BORetiroDinero().SelectAllByFechaSucursal(SucursalId_VI.Value, Fecha);
            return PartialView("_GridViewPartialRetirosDinero", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialRetirosDineroAddNew(AIPos.Domain.RetiroDinero item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialRetirosDinero", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialRetirosDineroUpdate(AIPos.Domain.RetiroDinero item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialRetirosDinero", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialRetirosDineroDelete(System.Int32 Id, int SucursalId, string Fecha)
        {
            List<Domain.RetiroDinero> model = new List<Domain.RetiroDinero>();
            if (Id != null)
            {
                try
                {
                    BusinessLayer.BORetiroDinero boRetiroDinero = new BusinessLayer.BORetiroDinero();
                    boRetiroDinero.Delete(Id);
                    //var fecha = DateTime.ParseExact(Fecha, "ddd MMM dd yyyy HH-mm-ss", CultureInfo.CurrentCulture);
                    //model = new BusinessLayer.BORetiroDinero().SelectAllByFechaSucursal(SucursalId, da);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return RedirectToAction("Reporte");
        }
    }
}
