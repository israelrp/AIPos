﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

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

    }
}
