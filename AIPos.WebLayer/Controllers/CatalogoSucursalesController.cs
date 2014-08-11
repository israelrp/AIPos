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
    [Authorize]
    public class CatalogoSucursalesController : Controller
    {
        //
        // GET: /CatalogoSucursales/

        public ActionResult Index()
        {
            return View();
        }





        [ValidateInput(false)]
        public ActionResult GridViewSucursales()
        {
            return PartialView("_GridViewSucursales", ParseSurcusalModel());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewSucursalesAddNew(AIPos.WebLayer.Models.SucursalModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    BODireccion blDireccion = new BODireccion();
                    Direccion direccion = new Direccion();
                    direccion.Calle = item.Calle;
                    direccion.Ciudad = item.Ciudad;
                    direccion.CodigoPostal = item.CodigoPostal;
                    direccion.Colonia = item.Colonia;
                    direccion.Estado = item.Estado;
                    direccion.NoExterior = item.NoExterior;
                    direccion.NoInterior = item.NoInterior;
                    direccion.Referencia = item.Referencia;
                    direccion=blDireccion.Insert(direccion);
                    BOSucursal blSucursal = new BOSucursal();
                    Sucursal sucursal = new Sucursal();
                    sucursal.Nombre = item.Nombre;
                    sucursal.DireccionId = direccion.Id;
                    sucursal.Telefono = item.Telefono;
                    sucursal.FraseTicket = item.FraseTicket;
                    blSucursal.Insert(sucursal);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewSucursales", ParseSurcusalModel());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewSucursalesUpdate(AIPos.WebLayer.Models.SucursalModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOSucursal blSucursal = new BOSucursal();
                    Sucursal sucursal = blSucursal.SelectById(item.Id);

                    BODireccion blDireccion = new BODireccion();
                    Direccion direccion = new Direccion();
                    direccion.Calle = item.Calle;
                    direccion.Ciudad = item.Ciudad;
                    direccion.CodigoPostal = item.CodigoPostal;
                    direccion.Colonia = item.Colonia;
                    direccion.Estado = item.Estado;
                    direccion.NoExterior = item.NoExterior;
                    direccion.NoInterior = item.NoInterior;
                    direccion.Referencia = item.Referencia;
                    direccion.Id = sucursal.DireccionId;
                    blDireccion.Update(direccion);
                    sucursal.FraseTicket = item.FraseTicket;
                    sucursal.Nombre = item.Nombre;
                    sucursal.Telefono = item.Telefono;
                    blSucursal.Update(sucursal);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewSucursales", ParseSurcusalModel());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewSucursalesDelete(System.Int32 Id)
        {
            var model = new object[0];
            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BOSucursal blSucursal = new BOSucursal();
                    Sucursal sucursal = blSucursal.SelectById(Id);
                    BODireccion blDireccion = new BODireccion();
                    blSucursal.Delete(sucursal.Id);
                    blDireccion.Delete(sucursal.DireccionId);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewSucursales", ParseSurcusalModel());
        }

        public List<Models.SucursalModel> ParseSurcusalModel()
        {
            BOSucursal blSucursal = new BOSucursal();
            var sucursales = blSucursal.SelectAll();
            List<Models.SucursalModel> sucursalesModel = new List<Models.SucursalModel>();
            foreach (var sucursal in sucursales)
            {
                Models.SucursalModel sucursalModel = new Models.SucursalModel();
                BODireccion blDireccion = new BODireccion();
                sucursal.Direccion = blDireccion.SelectById(sucursal.DireccionId);
                sucursalModel.Calle = sucursal.Direccion.Calle;
                sucursalModel.Ciudad = sucursal.Direccion.Ciudad;
                sucursalModel.CodigoPostal = sucursal.Direccion.CodigoPostal;
                sucursalModel.Colonia = sucursal.Direccion.Colonia;
                sucursalModel.Estado = sucursal.Direccion.Estado;
                sucursalModel.NoExterior = sucursal.Direccion.NoExterior;
                sucursalModel.NoInterior = sucursal.Direccion.NoInterior;
                sucursalModel.Nombre = sucursal.Nombre;
                sucursalModel.Telefono = sucursal.Telefono;
                sucursalModel.Referencia = sucursal.Direccion.Referencia;
                sucursalModel.FraseTicket = sucursal.FraseTicket;
                sucursalModel.Id = sucursal.Id;
                sucursalesModel.Add(sucursalModel);
            }
            return sucursalesModel;
        }
    }
}
