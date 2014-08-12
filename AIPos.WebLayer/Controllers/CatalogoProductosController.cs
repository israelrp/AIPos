using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.Domain;
using AIPos.BusinessLayer;

namespace AIPos.WebLayer.Controllers
{
    [Authorize]
    public class CatalogoProductosController : Controller
    {
        //
        // GET: /CatalogoProductos/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialProductos()
        {
            BOProducto blProducto = new BOProducto();
            return PartialView("_GridViewPartialProductos", blProducto.SelectAll());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialProductosAddNew(AIPos.Domain.Producto item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    BOProducto blProducto = new BOProducto();
                    blProducto.Insert(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialProductos", new BOProducto().SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialProductosUpdate(AIPos.Domain.Producto item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOProducto blProducto = new BOProducto();
                    blProducto.Update(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialProductos", new BOProducto().SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialProductosDelete(System.Int32 Id)
        {
            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BOProducto blProducto = new BOProducto();
                    blProducto.Delete(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialProductos", new BOProducto().SelectAll());
        }

        [HttpPost]
        public bool AumentarDisminuirPrecio(bool Aumentar, bool Porcentaje, decimal cantidad)
        {
            bool Retorno = false;
            BOProducto boProducto = new BOProducto();
            List<Producto> productos = boProducto.SelectAll();
            foreach (var producto in productos)
            {
                if (Aumentar)
                {
                    if (Porcentaje)
                    {
                        producto.Precio += (producto.Precio * (cantidad / 100));
                    }
                    else//Solo se suma
                    {
                        producto.Precio += cantidad;
                    }
                }
                else //Disminuir
                {
                    if (Porcentaje)
                    {
                        producto.Precio -= (producto.Precio * (cantidad / 100));
                    }
                    else//Solo restar
                    {
                        producto.Precio -= cantidad;
                    }
                }
                boProducto.Update(producto);
            }
            Retorno = true;

            return Retorno;
        }
    }
}
