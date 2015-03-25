using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Web.UI;
using AIPos.Domain;
using AIPos.BusinessLayer;

namespace AIPos.WebLayer.Controllers
{
    public class ConfiguracionController : Controller
    {
        //
        // GET: /Configuracion/

        public ActionResult Index()
        {
            ConfiguracionGeneral configuracion = new BOConfiguracionGeneral().ObtenerConfiguracion();
            return View(configuracion);
        }

        public ActionResult _ConfiguracionTicket()
        {
            return PartialView();
        }

        public ActionResult _ConfiguracionRedondeo()
        {
            return PartialView();
        }

        public ActionResult _ConfiguracionPOS()
        {
            return PartialView();
        }

        public void GuardarConfiguracionPOS(ConfiguracionGeneral configuracion)
        {
            BOConfiguracionGeneral boConfiguracion = new BOConfiguracionGeneral();
            ConfiguracionGeneral configuracionActual = boConfiguracion.ObtenerConfiguracion();
            configuracionActual.ActivarBascula = configuracion.ActivarBascula;
            configuracionActual.ActivarTicketPesaje = configuracion.ActivarTicketPesaje;
            configuracionActual.ActivarFotoProductoPOS = configuracion.ActivarFotoProductoPOS;
            configuracionActual.NumeroCopiasTicketVenta = configuracion.NumeroCopiasTicketVenta;
            boConfiguracion.ActualizarConfiguracion(configuracionActual);
        }

        public void GuardarConfiguracionRedondeo(ConfiguracionGeneral configuracion)
        {
            BOConfiguracionGeneral boConfiguracion = new BOConfiguracionGeneral();
            ConfiguracionGeneral configuracionActual = boConfiguracion.ObtenerConfiguracion();
            configuracionActual.DecimalesPrecioProducto = configuracion.DecimalesPrecioProducto;
            configuracionActual.DecimalesCantidad = configuracion.DecimalesCantidad;
            boConfiguracion.ActualizarConfiguracion(configuracionActual);
        }

        public void GuardarConfiguracionTicket(ConfiguracionGeneral configuracion)
        {
            BOConfiguracionGeneral boConfiguracion = new BOConfiguracionGeneral();
            ConfiguracionGeneral configuracionActual = boConfiguracion.ObtenerConfiguracion();
            configuracionActual.TituloTicket = configuracion.TituloTicket;
            configuracionActual.AgradecimientoTicket = configuracion.AgradecimientoTicket;
            configuracionActual.LeyendaFisalTicket = configuracion.LeyendaFisalTicket;
            configuracionActual.ActivarImprimirFechaHoraTicket = configuracion.ActivarImprimirFechaHoraTicket;
            if (System.Web.HttpContext.Current.Session["Logo"] != null)
            {
                configuracionActual.LogoTicket = System.Web.HttpContext.Current.Session["Logo"] as byte[];
            }
            boConfiguracion.ActualizarConfiguracion(configuracionActual);
        }

        public ActionResult ucLogoTicketUpload()
        {
            UploadControlExtension.GetUploadedFiles("ucLogoTicket", ConfiguracionControllerucLogoTicketSettings.ValidationSettings, ConfiguracionControllerucLogoTicketSettings.FileUploadComplete);
            return null;
        }
    }
    public class ConfiguracionControllerucLogoTicketSettings
    {
        public const string UploadDirectory = "~/Content/UploadControl/UploadFolder/";

        public static DevExpress.Web.UploadControlValidationSettings ValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg" },
            MaxFileSize = 20971520
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {

                string resultFilePath = HttpContext.Current.Server.MapPath(UploadDirectory + e.UploadedFile.FileName);
                 e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    HttpContext.Current.Session["Logo"] = e.UploadedFile.FileBytes;
                    e.CallbackData = urlResolver.ResolveClientUrl(UploadDirectory + e.UploadedFile.FileName);
                }
            }
        }
    }

}
