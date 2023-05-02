using APPAdminGroup.Controllers;
using APPAdminGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace APPAdminGroup.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        public Persona oUsuario;
        
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            base.OnActionExecuting(filterContext);
            oUsuario = (Persona)HttpContext.Current.Session["User"];
            if (oUsuario == null)
            { 
                if(filterContext.Controller is UsuarioController == false)
                {
                    //filterContext.HttpContext.Response.Redirect("~/Index/Index");
                }

            }
            else
            {
                if (filterContext.Controller is UsuarioController == true)
                {
                   filterContext.HttpContext.Response.Redirect("~/Index/Index");
                }

            }
            

        }
    }
}