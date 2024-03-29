﻿using System;
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
            return pedidoSucursalDaoImpl.SelectBySucursalFecha(SucursalId, fechaInicio, fechaFin);
        }

        public List<PedidoSucursal> SelectBySucursalFechaEntrega(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return pedidoSucursalDaoImpl.SelectBySucursalFechaEntrega(SucursalId, fechaInicio, fechaFin);
        }

        public List<ReportePedidoSucursal> SelectReporteBySucursalFechaEntrega(int SucursalId, DateTime Fecha)
        {
            DateTime fechaInicio = Tools.DateTimeManager.AbsoluteStart(Fecha);
            DateTime fechaFin = Tools.DateTimeManager.AbsoluteEnd(Fecha);
            return pedidoSucursalDaoImpl.SelectReporteBySucursalFechaEntrega(SucursalId, fechaInicio, fechaFin);
        }
    }
}
