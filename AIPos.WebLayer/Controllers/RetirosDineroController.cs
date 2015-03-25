using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using AIPos.WebLayer.Helpers;
using DevExpress.Web;

namespace AIPos.WebLayer.Controllers
{
    public class RetirosDineroController : Controller
    {
        [Authorize]
        //
        // GET: /RetirosDinero/

        public ActionResult Reporte()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartialRetirosDinero(DateTime? Fecha, int? SucursalId)
        {
            List<Domain.RetiroDinero> model = new List<Domain.RetiroDinero>();
            if (SucursalId.HasValue && Fecha.HasValue)
                model = new BusinessLayer.BORetiroDinero().SelectAllByFechaSucursal(SucursalId.Value, Fecha.Value);
            return PartialView("_GridViewPartialRetirosDinero", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialRetirosDineroAddNew(AIPos.Domain.RetiroDinero item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialRetirosDinero", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialRetirosDineroUpdate(AIPos.Domain.RetiroDinero item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartialRetirosDinero", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialRetirosDineroDelete(System.Int32 Id, int SucursalId, string Fecha)
        {
            List<Domain.RetiroDinero> model = new List<Domain.RetiroDinero>();
            if (Id != null)
            {
                try
                {
                    BusinessLayer.BORetiroDinero boRetiroDinero = new BusinessLayer.BORetiroDinero();
                    boRetiroDinero.Delete(Id);
                    //var fecha = DateTime.ParseExact(Fecha, "ddd MMM dd yyyy HH-mm-ss", CultureInfo.CurrentCulture);
                    //model = new BusinessLayer.BORetiroDinero().SelectAllByFechaSucursal(SucursalId, da);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return RedirectToAction("Reporte");
        }

        public ActionResult ExportTo(DateTime Fecha, int? SucursalId_VI)
        {
            foreach (string typeName in Helpers.GridViewExportHelper.ExportTypes.Keys)
            {
                if (Request.Params[typeName] != null)
                {
                    List<Domain.RetiroDinero> model = new List<Domain.RetiroDinero>();
                    if (SucursalId_VI.HasValue)
                        model = new BusinessLayer.BORetiroDinero().SelectAllByFechaSucursal(SucursalId_VI.Value, Fecha);
                    ReporteRetirosDineroExport export = new ReporteRetirosDineroExport();
                    return Helpers.GridViewExportHelper.ExportTypes[typeName].Method(export.ExportGridViewSettings, model);
                }
            }
            return RedirectToAction("Export");
        }
    }

    public class ReporteRetirosDineroExport : GridViewSettingExportHelper
    {
        public override GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "GridViewRetirosDinero";
            settings.CallbackRouteValues = new { Controller = "RetirosDinero", Action = "GridViewPartialRetirosDinero" };

            settings.SettingsEditing.AddNewRowRouteValues = new { Controller = "RetirosDinero", Action = "GridViewPartialRetirosDineroAddNew" };
            settings.SettingsEditing.UpdateRowRouteValues = new { Controller = "RetirosDinero", Action = "GridViewPartialRetirosDineroUpdate" };
            settings.SettingsEditing.DeleteRowRouteValues = new { Controller = "RetirosDinero", Action = "GridViewPartialRetirosDineroDelete" };
            settings.ClientSideEvents.BeginCallback = "function(s,e){ e.customArgs['SucursalId']=SucursalId.GetValue(); e.customArgs['Fecha']=Fecha.GetDate().toString();  }";

            settings.SettingsBehavior.ConfirmDelete = true;

            settings.CommandColumn.Visible = true;

            settings.KeyFieldName = "Id";

            settings.SettingsPager.Visible = true;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;

            settings.Columns.Add(c =>
            {
                c.FieldName = "UsuarioId";
                c.Caption = "Usuario";
                c.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = c.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = new AIPos.BusinessLayer.BOUsuario().SelectAll();
                comboBoxProperties.TextField = "Nombre";
                comboBoxProperties.ValueField = "Id";
            });

            settings.Columns.Add(c =>
            {
                c.FieldName = "SucursalId";
                c.Caption = "Sucursal";
                c.ColumnType = MVCxGridViewColumnType.ComboBox;
                var comboBoxProperties = c.PropertiesEdit as ComboBoxProperties;
                comboBoxProperties.DataSource = new AIPos.BusinessLayer.BOSucursal().SelectAll();
                comboBoxProperties.TextField = "Nombre";
                comboBoxProperties.ValueField = "Id";
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "Monto";
                x.Caption = "Monto";
                x.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add("Descripcion");
            settings.Columns.Add(x =>
            {
                x.FieldName = "Fecha";
                x.Caption = "Fecha";
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "EsCorteCaja";
                x.Caption = "Corte caja";
                x.ColumnType = MVCxGridViewColumnType.CheckBox;
            });
            return settings;
        }
    }
}
