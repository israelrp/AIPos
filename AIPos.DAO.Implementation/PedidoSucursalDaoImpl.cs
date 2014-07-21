using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO.IGeneric;
using AIPos.DataContext;
using AIPos.Domain;

namespace AIPos.DAO.Implementation
{
    public class PedidoSucursalDaoImpl : PedidoSucursalDao
    {
        ConnectionManager cm = new ConnectionManager();

        public PedidoSucursal Insert(PedidoSucursal entity)
        {
            object[] parameters = new object[] { entity.UsuarioId, entity.SucursalId, entity.UnidadId, entity.Producto, entity.FechaRegistro,
                entity.FechaEntrega, entity.Cantidad };
            return cm.Database.SqlQuery<PedidoSucursal>("dbo.usp_PedidosSucursalInsert @UsuarioId={0}, @SucursalId={1}, @UnidadId={2}, @Producto={3}, "+
                "@FechaRegistro={4}, @FechaEntrega={5}, @Cantidad={6}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_PedidosSucursalDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public PedidoSucursal Update(PedidoSucursal entity)
        {
            object[] parameters = new object[] { entity.Id, entity.UsuarioId, entity.SucursalId, entity.UnidadId, entity.Producto, entity.FechaRegistro,
                entity.FechaEntrega, entity.Cantidad };
            return cm.Database.SqlQuery<PedidoSucursal>("dbo.usp_PedidosSucursalUpdate @Id={0}, @UsuarioId={1}, @SucursalId={2}, @UnidadId={3}, @Producto={4}, " +
                "@FechaRegistro={5}, @FechaEntrega={6}, @Cantidad={7}", parameters).FirstOrDefault();
        }

        public List<PedidoSucursal> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<PedidoSucursal>("dbo.usp_PedidosSucursalSelect @Id={0}", parameters).ToList();
        }

        public List<PedidoSucursal> SelectBySucursalFecha(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<PedidoSucursal>("dbo.usp_PedidosSucursalSelectBySucursalFecha @SucursalId={0}, @FechaInicio={1}, @FechaFin={2}", parameters).ToList();
        }

        public PedidoSucursal SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<PedidoSucursal>("dbo.usp_PedidosSucursalSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
