using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIPos.Domain;
using AIPos.BusinessLayer;
using DevExpress.Web.Mvc;
using AIPos.WebLayer.Helpers;
using DevExpress.Web.ASPxEditors;

namespace AIPos.WebLayer.Controllers
{
    public class ReporteEntradasController : Controller
    {
        //
        // GET: /ReporteEntradas/

        public ActionResult Index()
        {
            ViewData["Sucursales"] = new BOSucursal().SelectAll();
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialEntradas(int? SucursalId, DateTime? Fecha)
        {
            List<ReporteEntradaUtilidad> model = new List<ReporteEntradaUtilidad>();
            if (SucursalId.HasValue && Fecha.HasValue)
            {
                BOEntrada boEntrada = new BOEntrada();
                model = boEntrada.SelectReporteUtilidadByDay(SucursalId.Value, Fecha.Value);
            }
            return PartialView("_GridViewPartialEntradas", model);
        }

        public ActionResult ExportTo(DateTime? DateEditFecha, int? ComboBoxSucursales_VI)
        {
            foreach (string typeName in Helpers.GridViewExportHelper.ExportTypes.Keys)
            {
                if (Request.Params[typeName] != null)
                {
                    List<ReporteEntradaUtilidad> model = new List<ReporteEntradaUtilidad>();
                    if (ComboBoxSucursales_VI.HasValue && DateEditFecha.HasValue)
                    {
                        BOEntrada boEntrada = new BOEntrada();
                        model = boEntrada.SelectReporteUtilidadByDay(ComboBoxSucursales_VI.Value, DateEditFecha.Value);
                    }
                    ReporteEntradasExport export = new ReporteEntradasExport();
                    return Helpers.GridViewExportHelper.ExportTypes[typeName].Method(export.ExportGridViewSettings, model);
                }
            }
            return RedirectToAction("Export");
        }
    }


    public class ReporteEntradasExport : GridViewSettingExportHelper
    {
        public override GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GridViewEntradas";
            settings.CallbackRouteValues = new { Controller = "ReporteEntradas", Action = "GridViewPartialEntradas" };
            settings.ClientSideEvents.BeginCallback = "function(s,e){ e.customArgs['SucursalId']=ComboBoxSucursales.GetValue(); e.customArgs['Fecha']=DateEditFecha.GetDate().toLocaleDateString(); }";

            settings.KeyFieldName = "EntradaId";

            settings.SettingsPager.Visible = true;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.Columns.Add("Fecha");
            settings.Columns.Add("Usuario");
            settings.Columns.Add("Proveedor");
            settings.Columns.Add("Producto");
            settings.Columns.Add("TotalPiezas");
            settings.Columns.Add("CantidadReal");
            settings.Columns.Add("CantidadProveedor");
            settings.Columns.Add("Diferencia");
            settings.Columns.Add("TipoProducto");
            settings.Columns.Add(c =>
            {
                c.FieldName = "PrecioUnitario";
                c.Caption = "Precio unitario";
                c.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(c =>
            {
                c.FieldName = "Importe";
                c.Caption = "Importe";
                c.PropertiesEdit.DisplayFormatString = "c";
            });
            return settings;
        }
    }
}
