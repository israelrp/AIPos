using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIPos.BusinessLayer;
using AIPos.Domain;
using DevExpress.Web.Mvc;
using AIPos.WebLayer.Helpers;
using DevExpress.Web;

namespace AIPos.WebLayer.Controllers
{
    public class ReportePedidosSucursalController : Controller
    {
        //
        // GET: /ReportePedidosSucursal/

        public ActionResult Index()
        {
            ViewData["Sucursales"] = new BOSucursal().SelectAll();
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialPedidosSucursal(int? SucursalId, DateTime? Fecha)
        {
            List<ReportePedidoSucursal> model = new List<ReportePedidoSucursal>();
            if (SucursalId.HasValue && Fecha.HasValue)
            {
                BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
                model = boPedidoSucursal.SelectReporteBySucursalFechaEntrega(SucursalId.Value, Fecha.Value);
            }
            return PartialView("_GridViewPartialPedidosSucursal", model);
        }

        public ActionResult ExportTo(DateTime? DateEditFecha, int? ComboBoxSucursales_VI)
        {
            foreach (string typeName in Helpers.GridViewExportHelper.ExportTypes.Keys)
            {
                if (Request.Params[typeName] != null)
                {
                    List<ReportePedidoSucursal> model = new List<ReportePedidoSucursal>();
                    if (ComboBoxSucursales_VI.HasValue && DateEditFecha.HasValue)
                    {
                        BOPedidoSucursal boPedidoSucursal = new BOPedidoSucursal();
                        model = boPedidoSucursal.SelectReporteBySucursalFechaEntrega(ComboBoxSucursales_VI.Value, DateEditFecha.Value);
                    }
                    ReportePedidosSucursalExport export = new ReportePedidosSucursalExport();
                    return Helpers.GridViewExportHelper.ExportTypes[typeName].Method(export.ExportGridViewSettings, model);
                }
            }
            return RedirectToAction("Export");
        }
    }

    public class ReportePedidosSucursalExport : GridViewSettingExportHelper
    {
        public override GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GridViewPedidosSucursal";
            settings.CallbackRouteValues = new { Controller = "ReportePedidosSucursal", Action = "GridViewPartialPedidosSucursal" };
            settings.ClientSideEvents.BeginCallback = "function(s,e){ e.customArgs['SucursalId']=ComboBoxSucursales.GetValue(); e.customArgs['Fecha']=DateEditFecha.GetDate().toLocaleDateString(); }";

            settings.KeyFieldName = "Id";

            settings.SettingsPager.Visible = true;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.Columns.Add("Usuario");
            settings.Columns.Add("Producto");
            settings.Columns.Add("Cantidad");
            settings.Columns.Add("Unidad");
            settings.Columns.Add("FechaRegistro");
            settings.Columns.Add(x =>
            {
                x.FieldName = "FechaEntrega";
                x.Caption = "FechaEntrega";
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
            });
            return settings;
        }
    }
}
