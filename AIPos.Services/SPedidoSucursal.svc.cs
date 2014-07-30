using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;
using AIPos.BusinessLayer;
using AIPos.BusinessLayer.Tools;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SPedidoSucursal" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SPedidoSucursal.svc or SPedidoSucursal.svc.cs at the Solution Explorer and start debugging.
    public class SPedidoSucursal : ISPedidoSucursal
    {

        public PedidoSucursal Insert(PedidoSucursal entity, long FechaRegistro, long FechaEntrega)
        {
            BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
            entity.FechaRegistro = DateTime.FromFileTimeUtc(FechaRegistro);
            entity.FechaEntrega = DateTime.FromFileTimeUtc(FechaEntrega);
            entity.FechaRegistro = TimeConverter.GetDateTimeMexico(entity.FechaRegistro);
            entity.FechaEntrega = TimeConverter.GetDateTimeMexico(entity.FechaEntrega);
            return boPedidoSucursal.Insert(entity);
        }

        public PedidoSucursal Update(PedidoSucursal entity, long FechaRegistro, long FechaEntrega)
        {
            BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
            entity.FechaRegistro = DateTime.FromFileTimeUtc(FechaRegistro);
            entity.FechaEntrega = DateTime.FromFileTimeUtc(FechaEntrega);
            return boPedidoSucursal.Update(entity);
        }

        public void Delete(int Id)
        {
            BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
            boPedidoSucursal.Delete(Id);
        }

        public List<PedidoSucursal> SelectBySucursalFecha(int SucursalId, long Day)
        {
            BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
            DateTime fecha = DateTime.FromFileTimeUtc(Day);
            return boPedidoSucursal.SelectBySucursalFecha(SucursalId,fecha);
        }

        public List<PedidoSucursal> SelectBySucursalFechaEntrega(int SucursalId, long Day)
        {
            BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
            DateTime fecha = DateTime.FromFileTimeUtc(Day);
            List<PedidoSucursal> pedidos=boPedidoSucursal.SelectBySucursalFechaEntrega(SucursalId, fecha);
            foreach (PedidoSucursal pedido in pedidos)
            {
                pedido.Usuario = new BOUsuario().SelectById(pedido.UsuarioId);
                pedido.Unidad = new BOUnidad().SelectById(pedido.UnidadId);
                pedido.Sucursal = new BOSucursal().SelectById(pedido.SucursalId);
            }
            return pedidos;
        }
    }
}
