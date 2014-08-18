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


        public ActionResult ucLogoTicketUpload()
        {
            UploadControlExtension.GetUploadedFiles("ucLogoTicket", ConfiguracionControllerucLogoTicketSettings.ValidationSettings, ConfiguracionControllerucLogoTicketSettings.FileUploadComplete);
            return null;
        }
    }
    public class ConfiguracionControllerucLogoTicketSettings
    {
        public const string UploadDirectory = "~/Content/UploadControl/UploadFolder/";

        public static DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg" },
            MaxFileSize = 20971520
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                 string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + e.UploadedFile.FileName);
                 e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(UploadDirectory + e.UploadedFile.FileName);
                }
            }
        }
    }

}
