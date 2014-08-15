using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace AIPos.WebLayer.Controllers
{
    public class ConfiguracionController : Controller
    {
        //
        // GET: /Configuracion/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ConfiguracionTicket()
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
        public static DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg" },
            MaxFileSize = 4000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                // Save uploaded file to some location
            }
        }
    }

}
