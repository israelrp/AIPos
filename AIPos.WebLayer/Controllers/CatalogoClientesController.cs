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
    public class CatalogoClientesController : Controller
    {
        //
        // GET: /CatalogoClientes/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialClientes()
        {
            return PartialView("_GridViewPartialClientes", new BOCliente().SelectAll());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialClientesAddNew(AIPos.Domain.Cliente item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    BOCliente blCliente = new BOCliente();
                    blCliente.Insert(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialClientes", new BOCliente().SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialClientesUpdate(AIPos.Domain.Cliente item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOCliente blCliente = new BOCliente();
                    blCliente.Update(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialClientes", new BOCliente().SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialClientesDelete(System.Int32 Id)
        {

            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BOCliente blCliente = new BOCliente();
                    blCliente.Delete(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialClientes", new BOCliente().SelectAll());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialDirecciones(int? ClienteId)
        {
            List<Direccion> model = new List<Direccion>();
            if (ClienteId.HasValue)
            {
                model = new BODireccion().SelectByClienteId(ClienteId.Value);
            }
            return PartialView("_GridViewPartialDirecciones", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDireccionesAddNew(AIPos.Domain.Direccion item, int? ClienteId)
        {
            List<Direccion> model = new List<Direccion>();
            if (ModelState.IsValid && ClienteId.HasValue)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    BODireccion boDireccion = new BODireccion();
                    item = boDireccion.Insert(item);
                    BODireccionEnvio boDireccionEnvio = new BODireccionEnvio();
                    DireccionEnvio direccionEnvio = new DireccionEnvio { ClienteId = ClienteId.Value, DireccionId = item.Id };
                    boDireccionEnvio.Insert(direccionEnvio);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
                model = new BODireccion().SelectByClienteId(ClienteId.Value);
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialDirecciones", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDireccionesUpdate(AIPos.Domain.Direccion item, int? ClienteId)
        {
            List<Direccion> model = new List<Direccion>();
            if (ModelState.IsValid && ClienteId.HasValue)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BODireccion boDireccion = new BODireccion();
                    boDireccion.Update(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
                model = new BODireccion().SelectByClienteId(ClienteId.Value);
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialDirecciones", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDireccionesDelete(System.Int32 Id, int? ClienteId)
        {
            List<Direccion> model = new List<Direccion>();
            if (Id != null && ClienteId.HasValue)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BODireccionEnvio boDireccionEnvio = new BODireccionEnvio();
                    boDireccionEnvio.Delete(Id, ClienteId.Value);
                    BODireccion boDireccion = new BODireccion();
                    boDireccion.Delete(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
                model = new BODireccion().SelectByClienteId(ClienteId.Value);
            }
            return PartialView("_GridViewPartialDirecciones", model);
        }
    }
}
