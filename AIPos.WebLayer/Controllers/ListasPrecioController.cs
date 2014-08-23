using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.BusinessLayer;
using AIPos.Domain;
using AIPos.WebLayer.Models;

namespace AIPos.WebLayer.Controllers
{
    [Authorize]
    public class ListasPrecioController : Controller
    {
        BOListaPrecio blListaPrecio = new BOListaPrecio();
        //
        // GET: /ListasPrecio/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AsignarListaPrecioSucursal()
        {
            return View();
        }

        public ActionResult AsignarListaPrecioCliente()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialListasPrecio()
        {
            var model = blListaPrecio.SelectAll();
            return PartialView("_GridViewPartialListasPrecio", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialListasPrecioAddNew(AIPos.Domain.ListaPrecio item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    blListaPrecio.Insert(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialListasPrecio", blListaPrecio.SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialListasPrecioUpdate(AIPos.Domain.ListaPrecio item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    blListaPrecio.Update(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialListasPrecio", blListaPrecio.SelectAll());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialListasPrecioDelete(System.Int32 Id)
        {
            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    blListaPrecio.Delete(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialListasPrecio", blListaPrecio.SelectAll());
        }


        public ActionResult ListaPreciosProductos()
        {
            List<Tipo> tiposProductos = new List<Tipo>();
            tiposProductos = new BOTipo().SelectAll();
            tiposProductos.Add(new Tipo { Id = 0, Nombre = "Todos" });
            ViewData["tipos"] = tiposProductos;
            return View();
        }



        [HttpPost]
        public bool AumentarDisminuirDescuentoLista(int ListaPrecioIdSelected, string Codigo, string Nombre, bool Porcentaje, decimal cantidad, int? TipoId)
        {
            bool Retorno = false;
            List<ListaPrecioProductoModel> listaPreciosProductos = new List<ListaPrecioProductoModel>();
            List<Producto> productos = new List<Producto>();
            productos = new BOProducto().SelectAll();
            if (Codigo != "")
            {
                productos = productos.Where(x => x.Codigo.Contains(Codigo)).ToList();
            }
            if (Nombre != "")
            {
                productos = productos.Where(x => x.Nombre.ToLower().Contains(Nombre.ToLower())).ToList();
            }
            if (TipoId.HasValue)
            {
                if (TipoId.Value > 0)
                {
                    productos = productos.Where(x => x.TipoId == TipoId.Value).ToList();
                }
            }
            productos.OrderBy(x => x.Codigo);
            foreach (var producto in productos)
            {
                BOListaPrecioProducto boListaPrecioProducto = new BOListaPrecioProducto();
                //Se busca el precio de lista del producto existente
                ListaPrecioProducto listaPrecioProducto = new ListaPrecioProducto(); ;
                listaPrecioProducto.ListaPrecioId = ListaPrecioIdSelected;
                listaPrecioProducto.ProductoId = producto.Id;
                listaPrecioProducto = boListaPrecioProducto.SelectById(listaPrecioProducto);
                if (Porcentaje)
                {
                    listaPrecioProducto.Descuento = cantidad;
                }
                else//Se calcula
                {
                    decimal precioDescuento=listaPrecioProducto.Precio-cantidad;
                    listaPrecioProducto.Descuento = 100 - ((100 * precioDescuento) / listaPrecioProducto.Precio);
                }
                listaPrecioProducto = boListaPrecioProducto.Update(listaPrecioProducto);
            }
            Retorno = true;
            return Retorno;
        }

        [HttpPost]
        public bool AumentarDisminuirPrecioLista(int ListaPrecioIdSelected, string Codigo, string Nombre, bool Aumentar, bool Porcentaje, decimal cantidad, int? TipoId)
        {
            bool Retorno = false;
            List<ListaPrecioProductoModel> listaPreciosProductos = new List<ListaPrecioProductoModel>();
            
            List<Producto> productos = new List<Producto>();
            productos = new BOProducto().SelectAll();
            if (Codigo != "")
            {
                productos = productos.Where(x => x.Codigo.Contains(Codigo)).ToList();
            }
            if (Nombre != "")
            {
                productos = productos.Where(x => x.Nombre.ToLower().Contains(Nombre.ToLower())).ToList();
            }
            if (TipoId.HasValue)
            {
                if (TipoId.Value > 0)
                {
                    productos = productos.Where(x => x.TipoId == TipoId.Value).ToList();
                }
            }
            productos.OrderBy(x => x.Codigo);
            foreach (var producto in productos)
            {
                BOListaPrecioProducto boListaPrecioProducto = new BOListaPrecioProducto();
                //Se busca el precio de lista del producto existente
                ListaPrecioProducto listaPrecioProducto = new ListaPrecioProducto(); ;
                listaPrecioProducto.ListaPrecioId = ListaPrecioIdSelected;
                listaPrecioProducto.ProductoId = producto.Id;
                listaPrecioProducto = new BOListaPrecioProducto().SelectById(listaPrecioProducto);
                if (Aumentar)
                {
                    if (Porcentaje)
                    {
                        listaPrecioProducto.Precio += (listaPrecioProducto.Precio * (cantidad / 100));
                    }
                    else//Solo se suma
                    {
                        listaPrecioProducto.Precio += cantidad;
                    }
                }
                else //Disminuir
                {
                    if (Porcentaje)
                    {
                        listaPrecioProducto.Precio -= (listaPrecioProducto.Precio * (cantidad / 100));
                    }
                    else//Solo restar
                    {
                        listaPrecioProducto.Precio -= cantidad;
                    }
                }
                listaPrecioProducto = boListaPrecioProducto.Update(listaPrecioProducto);
            }
            Retorno = true;
            
            return Retorno;
        }

        [HttpPost]
        public bool CopiarPrecioBaseListaPrecio(int ListaPrecioIdSelected, string Codigo, string Nombre, int? TipoId)
        {
            bool Retorno = false;
            List<ListaPrecioProductoModel> listaPreciosProductos = new List<ListaPrecioProductoModel>();
            
            List<Producto> productos = new List<Producto>();
            productos = new BOProducto().SelectAll();
            if (Codigo != "")
            {
                productos = productos.Where(x => x.Codigo.Contains(Codigo)).ToList();
            }
            if (Nombre != "")
            {
                productos = productos.Where(x => x.Nombre.ToLower().Contains(Nombre.ToLower())).ToList();
            }
            if (TipoId.HasValue)
            {
                if (TipoId.Value > 0)
                {
                    productos = productos.Where(x => x.TipoId == TipoId.Value).ToList();
                }
            }
            productos.OrderBy(x => x.Codigo);
            foreach (var producto in productos)
            {
                BOListaPrecioProducto boListaPrecioProducto=new BOListaPrecioProducto();
                //Se busca el precio de lista del producto existente
                ListaPrecioProducto listaPrecioProducto = new ListaPrecioProducto(); ;
                listaPrecioProducto.ListaPrecioId = ListaPrecioIdSelected;
                listaPrecioProducto.ProductoId = producto.Id;
                listaPrecioProducto = boListaPrecioProducto.SelectById(listaPrecioProducto);
                if (listaPrecioProducto != null)
                {

                    listaPrecioProducto.Precio = producto.Precio;
                    listaPrecioProducto = boListaPrecioProducto.Update(listaPrecioProducto);
                }
                else
                {
                    listaPrecioProducto = new ListaPrecioProducto();
                    listaPrecioProducto.Descuento = 0;
                    listaPrecioProducto.ListaPrecioId = ListaPrecioIdSelected;
                    listaPrecioProducto.Precio = producto.Precio;
                    listaPrecioProducto.ProductoId = producto.Id;
                    listaPrecioProducto = boListaPrecioProducto.Insert(listaPrecioProducto);

                }
            }
            Retorno = true;
            
            return Retorno;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialListaPrecios(int? ListaPrecioIdSelected, string Codigo, string Nombre, int? TipoId)
        {
            List<ListaPrecioProductoModel> listaPreciosProductos = new List<ListaPrecioProductoModel>();
            if (ListaPrecioIdSelected.HasValue)
            {
                listaPreciosProductos = new BOListaPrecioProducto().SelectByListaPrecio(ListaPrecioIdSelected.Value, Codigo, Nombre, TipoId);
            }
            return PartialView("_GridViewPartialListaPrecios", listaPreciosProductos);
        }

        public bool ActualizarPrecio(ListaPrecioProductoModel ListaPrecioProductoModel)
        {
            bool retorno = false;
            ListaPrecioProducto listaPrecioProducto = new ListaPrecioProducto();
            listaPrecioProducto.Descuento = ListaPrecioProductoModel.Descuento;
            listaPrecioProducto.ListaPrecioId = ListaPrecioProductoModel.ListaPrecioId;
            listaPrecioProducto.Precio = ListaPrecioProductoModel.PrecioLista;
            listaPrecioProducto.ProductoId = ListaPrecioProductoModel.ProductoId;
            BOListaPrecioProducto boListaPrecioProducto = new BOListaPrecioProducto();
            if (ListaPrecioProductoModel.EsNuevo)
            {
                boListaPrecioProducto.Insert(listaPrecioProducto);
            }
            else
            {
                boListaPrecioProducto.Update(listaPrecioProducto);
            }
            return retorno;
        }

        public ActionResult ComboBoxPartialListasPrecio()
        {
            ViewData["Model"] = new BOListaPrecio().SelectAll();
            return PartialView("_ComboBoxPartialListasPrecio");
        }


        private List<Models.SucursalListaPrecioModel> RecuperarSucursalesListaPrecio()
        {
            List<Models.SucursalListaPrecioModel> sucursalesListaPrecioModel = new List<SucursalListaPrecioModel>();
            List<Sucursal> sucursales = new BOSucursal().SelectAll();
            foreach (var sucursal in sucursales)
            {
                Models.SucursalListaPrecioModel sucursalListaPrecioModel = new SucursalListaPrecioModel();
                SucursalListaPrecio sucursalListaPrecio = new BOSucursalListaPrecio().SelectBySucursal(sucursal.Id);
                if (sucursalListaPrecio != null)
                {
                    sucursalListaPrecioModel.ListaPrecioId = sucursalListaPrecio.ListaPrecioId;
                    sucursalListaPrecioModel.ListaPrecio = new BOListaPrecio().SelectById(sucursalListaPrecio.ListaPrecioId).Nombre;
                }
                else
                {
                    sucursalListaPrecioModel.ListaPrecioId = null;
                    sucursalListaPrecioModel.ListaPrecio = "PRECIO BASE";
                }
                sucursalListaPrecioModel.SucursalId = sucursal.Id;
                sucursalListaPrecioModel.Sucursal = sucursal.Nombre;
                sucursalesListaPrecioModel.Add(sucursalListaPrecioModel);
            }
            return sucursalesListaPrecioModel;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialSucursalListaPrecio()
        {
            return PartialView("_GridViewPartialSucursalListaPrecio", RecuperarSucursalesListaPrecio());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialSucursalListaPrecioUpdate(Models.SucursalListaPrecioModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOSucursalListaPrecio boSucursalListaPrecio = new BOSucursalListaPrecio();
                    SucursalListaPrecio sucursalListaPrecio = boSucursalListaPrecio.SelectBySucursal(item.SucursalId);
                    if (sucursalListaPrecio != null)
                    {
                        boSucursalListaPrecio.Delete(sucursalListaPrecio);
                    }
                    sucursalListaPrecio = new SucursalListaPrecio();
                    sucursalListaPrecio.ListaPrecioId = item.ListaPrecioId.Value;
                    sucursalListaPrecio.SucursalId = item.SucursalId;
                    boSucursalListaPrecio.Insert(sucursalListaPrecio);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialSucursalListaPrecio", RecuperarSucursalesListaPrecio());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialSucursalListaPrecioDelete(System.Int32 SucursalId)
        {
            if (SucursalId != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BOSucursalListaPrecio boSucursalListaPrecio = new BOSucursalListaPrecio();
                    SucursalListaPrecio sucursalListaPrecio = boSucursalListaPrecio.SelectBySucursal(SucursalId);
                    if (sucursalListaPrecio != null)
                    {
                        boSucursalListaPrecio.Delete(sucursalListaPrecio);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialSucursalListaPrecio", RecuperarSucursalesListaPrecio());
        }



        [ValidateInput(false)]
        public ActionResult GridViewPartialClienteListaPrecio()
        {
            return PartialView("_GridViewPartialClienteListaPrecio", new BOClienteListaPrecio().SelectAllGrid());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialClienteListaPrecioUpdate(ClienteListaPrecioModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    BOClienteListaPrecio boClienteListaPrecio = new BOClienteListaPrecio();
                    ClienteListaPrecio clienteListaPrecio = boClienteListaPrecio.SelectByCliente(item.ClienteId);
                    if (clienteListaPrecio != null)
                    {
                        boClienteListaPrecio.Delete(clienteListaPrecio);
                    }
                    clienteListaPrecio = new ClienteListaPrecio();
                    clienteListaPrecio.ListaPrecioId = item.ListaPrecioId.Value;
                    clienteListaPrecio.ClienteId = item.ClienteId;
                    boClienteListaPrecio.Insert(clienteListaPrecio);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialClienteListaPrecio", new BOClienteListaPrecio().SelectAllGrid());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialClienteListaPrecioDelete(System.Int32 ClienteId)
        {
            if (ClienteId != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    BOClienteListaPrecio boClienteListaPrecio = new BOClienteListaPrecio();
                    ClienteListaPrecio clienteListaPrecio = boClienteListaPrecio.SelectByCliente(ClienteId);
                    if (clienteListaPrecio != null)
                    {
                        boClienteListaPrecio.Delete(clienteListaPrecio);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialClienteListaPrecio", new BOClienteListaPrecio().SelectAllGrid());
        }



    }
}
