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
    public class CatalogoProveedoresController : Controller
    {
        //
        // GET: /CatalogoProveedores/

        public ActionResult Index()
        {
            return View();
        }

        public List<Models.CatalogoProveedorModel> RecuperarProveedores()
        {
            List<Models.CatalogoProveedorModel> catalogoProveedoresModel = new List<Models.CatalogoProveedorModel>();
            List<Proveedor> proveedores = new BOProveedor().SelectAll();
            foreach (var proveedor in proveedores)
            {
                Models.CatalogoProveedorModel catalogoProveedorModel = new Models.CatalogoProveedorModel();
                catalogoProveedorModel.Calle = proveedor.Direccion.Calle;
                catalogoProveedorModel.Ciudad = proveedor.Direccion.Ciudad;
                catalogoProveedorModel.Codigo = proveedor.Codigo;
                catalogoProveedorModel.CodigoPostal = proveedor.Direccion.CodigoPostal;
                catalogoProveedorModel.Colonia = proveedor.Direccion.Colonia;
                catalogoProveedorModel.Contacto = proveedor.Contacto;
                catalogoProveedorModel.Direccion = proveedor.Direccion;
                catalogoProveedorModel.DireccionId = proveedor.DireccionId;
                catalogoProveedorModel.Eliminado = proveedor.Eliminado;
                catalogoProveedorModel.Email = proveedor.Email;
                catalogoProveedorModel.Estado = proveedor.Direccion.Estado;
                catalogoProveedorModel.Id = proveedor.Id;
                catalogoProveedorModel.NoExterior = proveedor.Direccion.NoExterior;
                catalogoProveedorModel.NoInterior = proveedor.Direccion.NoInterior;
                catalogoProveedorModel.RazonSocial = proveedor.RazonSocial;
                catalogoProveedorModel.Referencia = proveedor.Direccion.Referencia;
                catalogoProveedorModel.Rfc = proveedor.Rfc;
                catalogoProveedorModel.Telefono = proveedor.Telefono;
                catalogoProveedoresModel.Add(catalogoProveedorModel);
            }
            return catalogoProveedoresModel;
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            return PartialView("_GridViewPartial", RecuperarProveedores());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(AIPos.WebLayer.Models.CatalogoProveedorModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    BOProveedor boProveedor = new BOProveedor();
                    BODireccion boDireccion = new BODireccion();
                    Proveedor proveedor = new Proveedor();
                    proveedor.Codigo = item.Codigo;
                    proveedor.Contacto = item.Contacto;
                    proveedor.Eliminado = item.Eliminado;
                    proveedor.Email = item.Email;
                    proveedor.RazonSocial = item.RazonSocial;
                    proveedor.Rfc = item.Rfc;
                    proveedor.Telefono = item.Telefono;
                    proveedor.Direccion = new Direccion();
                    proveedor.Direccion.Calle = item.Calle;
                    proveedor.Direccion.Ciudad = item.Ciudad;
                    proveedor.Direccion.CodigoPostal = item.CodigoPostal;
                    proveedor.Direccion.Colonia = item.Colonia;
                    proveedor.Direccion.Estado = item.Estado;
                    proveedor.Direccion.NoExterior = item.NoExterior;
                    proveedor.Direccion.NoInterior = item.NoInterior;
                    proveedor.Direccion.Referencia = item.Referencia;
                    proveedor.Direccion = boDireccion.Insert(proveedor.Direccion);
                    proveedor.DireccionId = proveedor.Direccion.Id;
                    boProveedor.Insert(proveedor);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", RecuperarProveedores());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(AIPos.WebLayer.Models.CatalogoProveedorModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOProveedor boProveedor = new BOProveedor();
                    BODireccion boDireccion = new BODireccion();
                    Proveedor proveedor = new Proveedor();
                    proveedor.Id = item.Id;
                    proveedor.DireccionId = item.DireccionId;
                    proveedor.Codigo = item.Codigo;
                    proveedor.Contacto = item.Contacto;
                    proveedor.Eliminado = item.Eliminado;
                    proveedor.Email = item.Email;
                    proveedor.RazonSocial = item.RazonSocial;
                    proveedor.Rfc = item.Rfc;
                    proveedor.Telefono = item.Telefono;
                    proveedor.Direccion = new Direccion();
                    proveedor.Direccion.Id = item.DireccionId;
                    proveedor.Direccion.Calle = item.Calle;
                    proveedor.Direccion.Ciudad = item.Ciudad;
                    proveedor.Direccion.CodigoPostal = item.CodigoPostal;
                    proveedor.Direccion.Colonia = item.Colonia;
                    proveedor.Direccion.Estado = item.Estado;
                    proveedor.Direccion.NoExterior = item.NoExterior;
                    proveedor.Direccion.NoInterior = item.NoInterior;
                    proveedor.Direccion.Referencia = item.Referencia;
                    boDireccion.Update(proveedor.Direccion);
                    boProveedor.Update(proveedor);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", RecuperarProveedores());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 Id)
        {
            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BODireccion boDireccion = new BODireccion();
                    BOProveedor boProveedor = new BOProveedor();
                    boDireccion.Delete(boProveedor.SelectById(Id).DireccionId);
                    boProveedor.Delete(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", RecuperarProveedores());
        }
    }
}
