using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPAdminGroup.Controllers
{
    public class LogOutController : Controller
    {
        public ActionResult CerrarSesion()
        {

            Session["User"] = null;
            return RedirectToAction("Index","Index");
        }

       
    }
}
