using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.BusinessLayer;

namespace AIPos.WebLayer.Controllers
{
    public class CatalogoUnidadesController : Controller
    {
        [Authorize]
        //
        // GET: /CatalogoUnidades/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = new BOUnidad().SelectAll();
            return PartialView("_GridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] AIPos.Domain.Unidad item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BOUnidad blUnidad = new BOUnidad();
                    blUnidad.Insert(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", new BOUnidad().SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] AIPos.Domain.Unidad item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BOUnidad blUnidad = new BOUnidad();
                    blUnidad.Update(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", new BOUnidad().SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 Id)
        {
            if (Id != null)
            {
                try
                {
                    BOUnidad blUnidad = new BOUnidad();
                    blUnidad.Delete(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", new BOUnidad().SelectAll());
        }
    }
}
