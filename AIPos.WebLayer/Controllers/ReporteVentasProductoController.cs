﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.WebLayer.Helpers;
using DevExpress.Web;

namespace AIPos.WebLayer.Controllers
{
    public class ReporteVentasProductoController : Controller
    {
        [Authorize]
        //
        // GET: /ReporteVentasProducto/

        public ActionResult Index()
        {
            int añoInicio = DateTime.Now.Year - 10;
            int añoFin = DateTime.Now.Year + 10;
            List<int> años = new List<int>();
            for (int i = añoInicio; i <= añoFin; i++)
            {
                años.Add(i);
            }
            ViewData["Anios"] = años;
            ViewData["AnioActual"] = 10;
            ViewData["SemanaActual"] = new BusinessLayer.Tools.SemanaManager().SemanaActual();
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialVentasProducto(int? SucursalId, int? Semana, int? Año)
        {
            List<Domain.ReporteVentasProducto> model = new List<Domain.ReporteVentasProducto>();
            if (SucursalId.HasValue && Semana.HasValue && Año.HasValue)
                model = new BusinessLayer.BOVenta().RecuperarVentasProducto(SucursalId.Value,Semana.Value,Año.Value);
            return PartialView("_GridViewPartialVentasProducto", model);
        }

        public ActionResult ExportTo(int? SucursalId_VI, int? Semana_VI, int? Año_VI)
        {
            foreach (string typeName in Helpers.GridViewExportHelper.ExportTypes.Keys)
            {
                if (Request.Params[typeName] != null)
                {
                    List<Domain.ReporteVentasProducto> model = new List<Domain.ReporteVentasProducto>();
                    if (SucursalId_VI.HasValue && Semana_VI.HasValue && Año_VI.HasValue)
                        model = new BusinessLayer.BOVenta().RecuperarVentasProducto(SucursalId_VI.Value, Semana_VI.Value, Año_VI.Value);
                    ReporteVentasProductoExport export = new ReporteVentasProductoExport();
                    return Helpers.GridViewExportHelper.ExportTypes[typeName].Method(export.ExportGridViewSettings, model);
                }
            }
            return RedirectToAction("Export");
        }
    }

    public class ReporteVentasProductoExport : GridViewSettingExportHelper
    {
        public override GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GridViewVentasProducto";
            settings.CallbackRouteValues = new { Controller = "ReporteVentasProducto", Action = "GridViewPartialVentasProducto" };


            settings.KeyFieldName = "ProductoId";

            settings.SettingsPager.Visible = false;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;

            settings.Columns.Add("Tipo");
            settings.Columns.Add("Producto");
            settings.Columns.Add("Cantidad");
            settings.Columns.Add(c =>
            {
                c.FieldName = "ImporteTotal";
                c.Caption = "Importe";
                c.PropertiesEdit.DisplayFormatString = "c";
            });
            return settings;
        }
    }
}
