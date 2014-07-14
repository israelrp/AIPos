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
    public class AdministracionSeguridadController : Controller
    {
        //
        // GET: /AdministracionSeguridad/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsuarioSucursales()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialUsuarios()
        {
            return PartialView("_GridViewPartialUsuarios", new BOUsuario().SelectAllWithPasswordDecrypt());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUsuariosAddNew(AIPos.Domain.Usuario item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    Direccion direccion = new Direccion();
                    direccion.Calle = "";
                    direccion.Ciudad = "";
                    direccion.CodigoPostal = "";
                    direccion.Colonia = "";
                    direccion.Estado = "";
                    direccion.NoExterior = "";
                    direccion.NoInterior = "";
                    direccion.Referencia = "";
                    direccion = new BODireccion().Insert(direccion);
                    BOUsuario blUsuario = new BOUsuario();
                    item.DireccionId = direccion.Id;
                    blUsuario.Insert(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialUsuarios", new BOUsuario().SelectAllWithPasswordDecrypt());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUsuariosUpdate(AIPos.Domain.Usuario item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOUsuario blUsuario = new BOUsuario();
                    item.DireccionId = blUsuario.SelectById(item.Id).DireccionId;
                    blUsuario.Update(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialUsuarios", new BOUsuario().SelectAllWithPasswordDecrypt());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUsuariosDelete(System.Int32 Id)
        {
            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BOUsuario blUsuario = new BOUsuario();
                    Usuario usuario = blUsuario.SelectById(Id);
                    blUsuario.Delete(Id);
                    BODireccion blDireccion = new BODireccion();
                    blDireccion.Delete(usuario.DireccionId);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialUsuarios", new BOUsuario().SelectAllWithPasswordDecrypt());
        }
        public ActionResult ComboBoxPartialUsuarios()
        {
            ViewData["Usuarios"] = new BOUsuario().SelectAll();
            return PartialView("_ComboBoxPartialUsuarios");
        }

        private List<Models.UsuarioSucursalModel> RecuperarUsuarioSucursales(int UsuaurioId)
        {
            List<Models.UsuarioSucursalModel> model = new List<Models.UsuarioSucursalModel>();
            List<UsuarioSucursal> usuariosSucursal = new BOUsuarioSucursal().SelectByUsuarioId(UsuaurioId);
            foreach (var usuarioSucursal in usuariosSucursal)
            {
                Models.UsuarioSucursalModel usuarioSucursalModel = new Models.UsuarioSucursalModel();
                usuarioSucursalModel.UsuarioId = usuarioSucursal.UsuarioId;
                usuarioSucursalModel.SucursalId = usuarioSucursal.SucursalId;
                usuarioSucursalModel.Sucursal = new BOSucursal().SelectById(usuarioSucursal.SucursalId).Nombre;
                model.Add(usuarioSucursalModel);
            }
            return model;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialUsuarioSucursales(int? UsuarioIdSelected)
        {
            List<Models.UsuarioSucursalModel> model = new List<Models.UsuarioSucursalModel>();
            if (UsuarioIdSelected.HasValue)
            {
                model = RecuperarUsuarioSucursales(UsuarioIdSelected.Value);
            }
            return PartialView("_GridViewPartialUsuarioSucursales", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUsuarioSucursalesAddNew(Models.UsuarioSucursalModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    BOUsuarioSucursal boUsuarioSucursal = new BOUsuarioSucursal();
                    UsuarioSucursal usuarioSucursal = new UsuarioSucursal();
                    usuarioSucursal.SucursalId = item.SucursalId;
                    usuarioSucursal.UsuarioId = item.UsuarioId;
                    boUsuarioSucursal.Insert(usuarioSucursal);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialUsuarioSucursales", RecuperarUsuarioSucursales(item.UsuarioId));
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUsuariosDelete(Models.UsuarioSucursalModel item)
        {
            if (item != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialUsuarioSucursales", new BOUsuario().SelectAllWithPasswordDecrypt());
        }
    }
}
