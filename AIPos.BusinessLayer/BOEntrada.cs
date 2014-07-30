using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BOEntrada
    {
        EntradaDaoImpl entradaDaoImpl = new EntradaDaoImpl();

        public  Entrada Insert(Entrada entrada)
        {
            entrada.Fecha = Tools.TimeConverter.GetDateTimeNowMexico();
            return entradaDaoImpl.Insert(entrada);
        }

        public List<Entrada> SelectAll()
        {
            return entradaDaoImpl.SelectAll();
        }

        public void Delete(int Id)
        {
            entradaDaoImpl.Delete(Id);
        }

        public List<Entrada> SelectByDay(int SucursalId, DateTime fecha)
        {
            int hours = fecha.Hour;
            int minutes = fecha.Minute;
            DateTime fechaInicio = fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            List<Entrada> entradas = entradaDaoImpl.SelectByFechaSucursal(fechaInicio, fechaFin, SucursalId);
            foreach (var entrada in entradas)
            {
                entrada.Proveedor = new BOProveedor().SelectById(entrada.ProveedorId);
                entrada.Producto = new BOProducto().SelectById(entrada.ProductoId);
                entrada.TipoProducto = new BOTipoProducto().SeectById(entrada.TipoProductoId);
            }
            return entradas;
        }
    }
}
