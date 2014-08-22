using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;
using AIPos.BusinessLayer;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ISVenta" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ISVenta.svc or ISVenta.svc.cs at the Solution Explorer and start debugging.
    public class ISVenta : IISVenta
    {
        public Venta Insert(Venta venta)
        {
            BOVenta boVenta = new BOVenta();
            return boVenta.Insert(venta);
        }


        public int GenerarFolioVenta(int SucursalId)
        {
            BOVenta boVenta = new BOVenta();
            return boVenta.GenerarFolioVenta(SucursalId);
        }

        public int GenerarFolioCancelado(int SucursalId, long Fecha)
        {
            BOVenta boVenta = new BOVenta();
            DateTime fechaActual = DateTime.FromFileTimeUtc(Fecha);
            return boVenta.GenerarFolioCancelado(SucursalId, fechaActual);
        }


        public int GenerarFolioCanceladoApartado(int SucursalId, long Fecha)
        {
            BOVenta boVenta = new BOVenta();
            DateTime fechaActual = DateTime.FromFileTimeUtc(Fecha);
            return boVenta.GenerarFolioCanceladoApartado(SucursalId, fechaActual);
        }

        public int GenerarFolioCanceladoDomicilio(int SucursalId, long Fecha)
        {
            BOVenta boVenta = new BOVenta();
            DateTime fechaActual = DateTime.FromFileTimeUtc(Fecha);
            return boVenta.GenerarFolioCanceladoDomicilio(SucursalId, fechaActual);
        }

        public int GenerarFolioCanceladoServicio(int SucursalId, long Fecha)
        {
            BOVenta boVenta = new BOVenta();
            DateTime fechaActual = DateTime.FromFileTimeUtc(Fecha);
            return boVenta.GenerarFolioCanceladoServicio(SucursalId, fechaActual);
        }


        public List<ReporteCorteCaja> RecuperarCorteCaja(long FechaInicio, long FechaFin, int? SucursalId)
        {
            BOVenta boVenta = new BOVenta();
            DateTime fechaInicio = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(DateTime.FromFileTimeUtc(FechaInicio));
            DateTime fechaFin = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(DateTime.FromFileTimeUtc(FechaFin));
            return boVenta.RecuperarCorteCaja(fechaInicio, fechaFin, SucursalId);
        }

        public List<VentaDetalle> RecuperarVentaDetalle(int VentaId)
        {
            return new BOVentaDetalle().SelectByVentaId(VentaId);
        }

        public Venta RecuperarVenta(int VentaId)
        {
            return new BOVenta().SelectById(VentaId);
        }

        public Venta Update(Venta venta)
        {
            return new BOVenta().Update(venta);
        }
    }
}
