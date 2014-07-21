using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.BusinessLayer;

namespace AIPos.WebLayer.Controllers
{
    [Authorize]
    public class CatalogoTiposController : Controller
    {
        //
        // GET: /CatalogoTipos/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialTipos()
        {
            var model = new BOTipo().SelectAll();
            return PartialView("_GridViewPartialTipos", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialTiposAddNew(AIPos.Domain.Tipo item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    BOTipo blTipo = new BOTipo();
                    blTipo.Insert(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialTipos", new BOTipo().SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialTiposUpdate(AIPos.Domain.Tipo item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOTipo blTipo = new BOTipo();
                    blTipo.Update(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialTipos", new BOTipo().SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialTiposDelete(System.Int32 Id)
        {
            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BOTipo blTipo = new BOTipo();
                    blTipo.Delete(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialTipos", new BOTipo().SelectAll());
        }
    }
}
