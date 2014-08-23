using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DataContext;
using AIPos.DAO.IGeneric;
using AIPos.Domain;
using AIPos.DAO.Implementation.Enums;

namespace AIPos.DAO.Implementation
{
    public class VentaDaoImpl : VentaDao
    {

        ConnectionManager cm = new ConnectionManager();

        public Venta Insert(Venta entity)
        {
            object[] parameters = new object[] { entity.ClienteId, entity.UsuarioId, entity.SucursalId, entity.Fecha, entity.Folio, entity.Recibio, entity.Cambio,
                entity.FolioCancelado, entity.Cancelado , entity.Total, entity.RequiereFactura, entity.Facturado, entity.Estatus};
            return cm.Database.SqlQuery<Venta>("dbo.usp_VentasInsert @ClienteId={0}, @UsuarioId={1}, @SucursalId={2}, @Fecha={3}, @Folio={4}, @Recibio={5}, "+
                "@Cambio={6}, @FolioCancelado={7}, @Cancelado={8}, @Total={9}, @RequiereFactura={10}, @Facturado={11}, @Estatus={12}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_VentasDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Venta Update(Venta entity)
        {
            object[] parameters = new object[] { entity.Id, entity.ClienteId, entity.UsuarioId, entity.SucursalId, entity.Fecha, entity.Folio, entity.Recibio, entity.Cambio,
                entity.FolioCancelado, entity.Cancelado , entity.Total, entity.RequiereFactura, entity.Facturado , entity.Estatus};
            return cm.Database.SqlQuery<Venta>("dbo.usp_VentasUpdate @Id={0}, @ClienteId={1}, @UsuarioId={2}, @SucursalId={3}, @Fecha={4}, @Folio={5}, @Recibio={6}, " +
                "@Cambio={7}, @FolioCancelado={8}, @Cancelado={9}, @Total={10}, @RequiereFactura={11}, @Facturado={12}, @Estatus={13}", parameters).FirstOrDefault();
        }

        public List<Venta> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Venta>("dbo.usp_VentasSelect @Id={0}", parameters).ToList();
        }

        public Venta SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Venta>("dbo.usp_VentasSelect @Id={0}", parameters).FirstOrDefault();
        }

        public Venta SelectByTipoSucursalFechaFolio(byte Tipo, int SucursalId, DateTime FechaInicio, DateTime FechaFin, int Folio)
        {
            object[] parameters = new object[] { Tipo, SucursalId, FechaInicio, FechaFin, Folio };
            return cm.Database.SqlQuery<Venta>("dbo.[usp_VentasSelectByFolioTipo] @Tipo={0}, @SucursalId={1}, @FechaInicio={2}, @FechaFin={3}, @Folio={4}", parameters).FirstOrDefault();
        }

        public Venta SelectBySucursalFolio(int SucursalId, int Folio)
        {
            object[] parameters = new object[] { SucursalId, Folio };
            return cm.Database.SqlQuery<Venta>("dbo.usp_VentasSelectFolioSucursal @SucursalId={0}, @Folio={1}", parameters).FirstOrDefault();
        }

        public Venta SelectBySucursalFolioCancelado(int SucursalId, int FolioCancelado)
        {
            object[] parameters = new object[] { SucursalId, FolioCancelado };
            return cm.Database.SqlQuery<Venta>("dbo.usp_VentasSelectFolioSucursal @SucursalId={0}, @FolioCancelado={1}", parameters).FirstOrDefault();
        }

        public int GenerarFolioVenta(int SucursalId)
        {
            object[] parameters = new object[] { SucursalId };
            return cm.Database.SqlQuery<int>("dbo.GenerarFolioVenta @SucursalId={0}", parameters).FirstOrDefault();
        }

        public int GenerarFolioCancelado(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<int>("dbo.GenerarFolioCancelado @SucursalId={0}, @FechaInicio={1}, @FechaFin={2} ", parameters).FirstOrDefault();
        }

        public int GenerarFolioCanceladoDomicilio(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<int>("dbo.GenerarFolioCanceladoDomicilio @SucursalId={0}, @FechaInicio={1}, @FechaFin={2} ", parameters).FirstOrDefault();
        }

        public int GenerarFolioCanceladoApartado(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<int>("dbo.GenerarFolioCanceladoApartado @SucursalId={0}, @FechaInicio={1}, @FechaFin={2} ", parameters).FirstOrDefault();
        }

        public int GenerarFolioCanceladoServicio(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<int>("dbo.GenerarFolioCanceladoServicio @SucursalId={0}, @FechaInicio={1}, @FechaFin={2} ", parameters).FirstOrDefault();
        }

        public List<ReporteCorteCaja> RecuperarCorteCaja(DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { FechaInicio, FechaFin };
            return cm.Database.SqlQuery<ReporteCorteCaja>("dbo.usp_CorteCaja @FechaInicio={0}, @FechaFin={1} ", parameters).ToList();
        }

        public List<ReporteCorteCaja> RecuperarVentasFacturar()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<ReporteCorteCaja>("dbo.usp_VentasFacturar", parameters).ToList();
        }

        public List<ReporteVentasProducto> RecuperarVentasProducto(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<ReporteVentasProducto>("dbo.usp_ReporteVentasProducto @SucursalId={0}, @FechaInicio={1}, @FechaFin={2}", parameters).ToList();
        }


        public ConteoVenta RecuperarResumenVenta(DateTime FechaInicio, DateTime FechaFin, TipoVenta TipoVenta)
        {
            object[] parameters = new object[] { FechaInicio, FechaFin, TipoVenta };
            return cm.Database.SqlQuery<ConteoVenta>("dbo.usp_VentasRecuperarResumen @FechaInicio={0}, @FechaFin={1}, @Tipo={2}", parameters).FirstOrDefault();
        }

        public ConteoVenta RecuperarResumenVentaBySucursal(DateTime FechaInicio, DateTime FechaFin, TipoVenta TipoVenta, string Sucursales)
        {
            object[] parameters = new object[] { FechaInicio, FechaFin, TipoVenta, Sucursales };
            return cm.Database.SqlQuery<ConteoVenta>("dbo.usp_VentasRecuperarResumenPorSucursal @FechaInicio={0}, @FechaFin={1}, @Tipo={2}, @SucursalesId={3}", parameters).FirstOrDefault();
        }

        public List<ReporteUtilidad> RecuperarUtilidadesProducto(int SucursalId, DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { SucursalId, FechaInicio, FechaFin };
            return cm.Database.SqlQuery<ReporteUtilidad>("dbo.usp_ReporteUtilidadByProducto @SucursalId={0}, @FechaInicio={1}, @FechaFin={2}", parameters).ToList();
        }
        
    }


}
