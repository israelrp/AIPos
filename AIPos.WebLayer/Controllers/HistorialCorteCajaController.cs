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
            settings.Name = "GridViewReporteCorteCaja";
            settings.CallbackRouteValues = new { Controller = "ReporteCorteCaja", Action = "_GridViewPartialReporteCorteCaja" };
            settings.ClientSideEvents.BeginCallback = "function(s,e){ e.customArgs['FechaInicio']=FechaInicio.GetDate().toLocaleDateString(); e.customArgs['FechaFin']=FechaFin.GetDate().toLocaleDateString(); e.customArgs['SucursalId_VI']=SucursalId.GetValue(); }";
            settings.KeyFieldName = "Folio";

            settings.SettingsPager.Visible = true;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;

            settings.Columns.Add("Cliente");
            settings.Columns.Add(x =>
            {
                x.FieldName = "Sucursal";
                x.Caption = "Sucursal";
                x.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = x.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = new AIPos.BusinessLayer.BOSucursal().SelectAll();
                comboBoxProperties.TextField = "Nombre";
                comboBoxProperties.ValueField = "Id";

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Fecha";
                x.Caption = "Fecha";
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Total";
                x.Caption = "Total";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Recibio";
                x.Caption = "Recibio";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Cambio";
                x.Caption = "Cambio";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Anticipo";
                x.Caption = "Anticipo";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add("FolioCancelado");
            settings.Columns.Add("Folio");
            settings.Columns.Add("Usuario");
            settings.Columns.Add("Estatus");
            settings.Columns.Add("Tipo");
            settings.Columns.Add(x =>
            {
                x.FieldName = "Cancelado";
                x.Caption = "Cancelado";
                x.ColumnType = MVCxGridViewColumnType.CheckBox;
            });
            return settings;
        }
    }
}
