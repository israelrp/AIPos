using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BOPedidoSucursal
    {
        PedidoSucursalDaoImpl pedidoSucursalDaoImpl = new PedidoSucursalDaoImpl();

        public PedidoSucursal Insert(PedidoSucursal entity)
        {
            return pedidoSucursalDaoImpl.Insert(entity);
        }

        public PedidoSucursal Update(PedidoSucursal entity)
        {
            return pedidoSucursalDaoImpl.Update(entity);
        }

        public void Delete(int Id)
        {
            pedidoSucursalDaoImpl.Delete(Id);
        }

        public List<PedidoSucursal> SelectAll()
        {
            return pedidoSucursalDaoImpl.SelectAll();
        }

        public PedidoSucursal SelectById(int Id)
        {
            return pedidoSucursalDaoImpl.SelectByKey(Id);
        }

        public List<PedidoSucursal> SelectBySucursalFecha(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return pedidoSucursalDaoImpl.SelectAll().Where(x => x.SucursalId == SucursalId && x.FechaRegistro >= fechaInicio && x.FechaRegistro <= fechaFin).ToList();
        }
    }
}
