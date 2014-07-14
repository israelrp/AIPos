using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO.Implementation;
using AIPos.Domain;

namespace AIPos.BusinessLayer
{
    public class BOProveedor
    {
        ProveedoresDaoImpl proveedoresDaoImpl = new ProveedoresDaoImpl();

        public List<Proveedor> SelectAll()
        {
            List<Proveedor> proveedores=proveedoresDaoImpl.SelectAll();
            foreach (var proveedor in proveedores)
            {
                proveedor.Direccion = new BODireccion().SelectById(proveedor.DireccionId);
            }
            return proveedores;
        }

        public Proveedor SelectByCodigo(string Codigo)
        {
            Proveedor proveedor = proveedoresDaoImpl.SelectAll().Where(x => x.Codigo.ToLower() == Codigo.ToLower()).FirstOrDefault();
            return proveedor;
        }

        public Proveedor Insert(Proveedor proveedor)
        {
            Direccion direccion = new BODireccion().Insert(proveedor.Direccion);
            proveedor = proveedoresDaoImpl.Insert(proveedor);
            proveedor.Direccion = direccion;
            return proveedor;
        }

        public Proveedor Update(Proveedor proveedor)
        {
            Direccion direccion = new BODireccion().Update(proveedor.Direccion);
            proveedor = proveedoresDaoImpl.Update(proveedor);
            proveedor.Direccion = direccion;
            return proveedor;
        }

        public void Delete(int Id)
        {
            Proveedor proveedor = SelectById(Id);
            proveedoresDaoImpl.Delete(proveedor.Id);
            DireccionDaoImpl direccionDaoImpl = new DireccionDaoImpl();
            direccionDaoImpl.Delete(proveedor.DireccionId);
        }

        public Proveedor SelectById(int Id)
        {
            Proveedor proveedor = proveedoresDaoImpl.SelectByKey(Id);
            proveedor.Direccion = new BODireccion().SelectById(proveedor.DireccionId);
            return proveedor;
        }

    }
}
