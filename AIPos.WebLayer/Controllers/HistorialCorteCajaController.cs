using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.WebLayer.Helpers;
using DevExpress.Web.ASPxEditors;

namespace AIPos.WebLayer.Controllers
{
    public class HistorialCorteCajaController : Controller
    {
        [Authorize]
        //
        // GET: /HistorialCorteCaja/

        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialHistorialCorteCaja(int? SucursalId)
        {
            List<Domain.CorteCaja> corteCaja = new List<Domain.CorteCaja>();
            if (SucursalId.HasValue)
            {
                corteCaja = new BusinessLayer.BOCorteCaja().SelectAll().Where(x => x.SucursalId == SucursalId.Value).ToList();
            }
            return PartialView("_GridViewPartialHistorialCorteCaja", corteCaja);
        }

        public ActionResult ExportTo(int? SucursalId_VI)
        {
            foreach (string typeName in Helpers.GridViewExportHelper.ExportTypes.Keys)
            {
                if (Request.Params[typeName] != null)
                {
                    List<Domain.CorteCaja> model     = new List<Domain.CorteCaja>();
                    if (SucursalId_VI.HasValue)
                    {
                        model = new BusinessLayer.BOCorteCaja().SelectAll().Where(x => x.SucursalId == SucursalId_VI.Value).ToList();
                    }
                    ReporteHistorialCorteCajaExport export = new ReporteHistorialCorteCajaExport();
                    return Helpers.GridViewExportHelper.ExportTypes[typeName].Method(export.ExportGridViewSettings, model);
                }
            }
            return RedirectToAction("Export");
        }

    }

    public class ReporteHistorialCorteCajaExport : GridViewSettingExportHelper
    {
        public override GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GridViewHistorialCorteCaja";
            settings.CallbackRouteValues = new { Controller = "HistorialCorteCaja", Action = "GridViewPartialHistorialCorteCaja" };
            settings.ClientSideEvents.BeginCallback = "function(s,e){ e.customArgs['SucursalId']=SucursalId.GetValue(); }";

            settings.KeyFieldName = "Id";

            settings.SettingsPager.Visible = true;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;

            settings.Columns.Add(x =>
            {
                x.FieldName = "Fecha";
                x.Caption = "Fecha";
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalVentas";
                x.Caption = "Total Ventas";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalMostrador";
                x.Caption = "Total Mostrador";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalDomicilio";
                x.Caption = "Total Domicilio";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalServicios";
                x.Caption = "Total Servicios";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalApartados";
                x.Caption = "Total Apartados";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalRetiros";
                x.Caption = "Total Retiros";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalAbonosServicios";
                x.Caption = "Total abonos servicios";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalAbonosApartados";
                x.Caption = "Total Abonos Apartados";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalCancelados";
                x.Caption = "Total Cancelados";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalCaja";
                x.Caption = "Total Caja";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalCambio";
                x.Caption = "Total Cambio";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add("QuienRetira");
            settings.Columns.Add(x =>
            {
                x.FieldName = "CorteEntregado";
                x.Caption = "Corte Entregado";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Diferencia";
                x.Caption = "Diferencia";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add("TotalTickectsDomicilio");
            settings.Columns.Add("TotalTickectsMostrador");
            return settings;
        }
    }
}
