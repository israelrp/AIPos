using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.Domain;
using AIPos.BusinessLayer;
using AIPos.WebLayer.Helpers;
using DevExpress.Web.ASPxEditors;

namespace AIPos.WebLayer.Controllers
{
    public class ReporteInventariosController : Controller
    {
        //
        // GET: /ReporteInventarios/

        public ActionResult Index()
        {
            ViewData["Sucursales"] = new BOSucursal().SelectAll();
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialInventarios(int? SucursalId,  DateTime? Fecha)
        {
            List<ReporteInventarioUtilidad> model = new List<ReporteInventarioUtilidad>();
            if (SucursalId.HasValue && Fecha.HasValue)
            {
                model = new BOInventario().SelectReporteUtilidadSucursalFecha(SucursalId.Value, Fecha.Value);
            }
            return PartialView("_GridViewPartialInventarios", model);
        }

        public ActionResult ExportTo(DateTime? DateEditFecha, int? ComboBoxSucursales_VI)
        {
            foreach (string typeName in Helpers.GridViewExportHelper.ExportTypes.Keys)
            {
                if (Request.Params[typeName] != null)
                {
                    List<ReporteInventarioUtilidad> model = new List<ReporteInventarioUtilidad>();
                    if (ComboBoxSucursales_VI.HasValue && DateEditFecha.HasValue)
                    {
                        model = new BOInventario().SelectReporteUtilidadSucursalFecha(ComboBoxSucursales_VI.Value, DateEditFecha.Value);
                    }
                    ReporteInventariosExport export = new ReporteInventariosExport();
                    return Helpers.GridViewExportHelper.ExportTypes[typeName].Method(export.ExportGridViewSettings, model);
                }
            }
            return RedirectToAction("Export");
        }
    }

    public class ReporteInventariosExport : GridViewSettingExportHelper
    {
        public override GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GridViewInventarios";
            settings.CallbackRouteValues = new { Controller = "ReporteInventarios", Action = "GridViewPartialInventarios" };
            settings.ClientSideEvents.BeginCallback = "function(s,e){ e.customArgs['SucursalId']=ComboBoxSucursales.GetValue(); e.customArgs['Fecha']=DateEditFecha.GetDate().toLocaleDateString(); }";

		    settings.KeyFieldName = "Producto";

		    settings.SettingsPager.Visible = true;
		    settings.Settings.ShowGroupPanel = true;
		    settings.Settings.ShowFilterRow = true;
		    settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.KeyFieldName = "Id";
		    settings.Columns.Add("Usuario");
		    settings.Columns.Add("Producto");
		    settings.Columns.Add("CantidadReal");
		    settings.Columns.Add("CantidadSistema");
            settings.Columns.Add(c =>
            {
                c.FieldName = "PrecioUnitario";
                c.Caption = "Precio unitario";
                c.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(c =>
            {
                c.FieldName = "Monto";
                c.Caption = "Monto";
                c.PropertiesEdit.DisplayFormatString = "c";
            });
		    settings.Columns.Add("FechaRegistro");
			
            return settings;
        }
    }
}
