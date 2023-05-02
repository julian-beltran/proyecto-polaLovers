using APPAdminGroup.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace APPAdminGroup.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            


            return View();
        }

        // GET: Perfil/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Perfil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfil/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfil/Edit/5
        public ActionResult Edit(int id)
        {

            personaEditPerfilViewModel persona = new personaEditPerfilViewModel();




            persona.id = (int)Session["id"];
            persona.nombre = (string)Session["nombre"];
            persona.apelllido = (string)Session["apellido"];
            persona.telefono = (string)Session["telefono"];
            persona.direccion = (string)Session["direccion"]; 
            persona.usuario1 = (string)Session["usuario"];
            persona.correo = (string)Session["correo"];
            //persona.image = (byte[])Session["imagen"];
                
        
            return View(persona);
        }

        // POST: Perfil/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, personaEditPerfilViewModel persona)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    personaEditPerfilViewModel personas = new personaEditPerfilViewModel();




                    personas.id = (int)Session["id"];
                    personas.nombre = (string)Session["nombre"];
                    personas.apelllido = (string)Session["apellido"];
                    personas.telefono = (string)Session["telefono"];
                    personas.direccion = (string)Session["direccion"];
                    personas.usuario1 = (string)Session["usuario"];
                    personas.correo = (string)Session["correo"];
                    //persona.image = (byte[])Session["imagen"];


                    return View(personas);
                }
                using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
                {
                    var user = db.Persona.Find(persona.id);
                    HttpPostedFileBase fileBase = Request.Files[0];
                    if (fileBase.ContentLength == 0)
                    {
                        persona.image = user.image;
                    }
                    else
                    {
                        WebImage image = new WebImage(fileBase.InputStream);
                        persona.image = image.GetBytes();
                    }
                    
                    user.id = persona.id;
                    user.nombre = persona.nombre;
                    user.apelllido = persona.apelllido;
                    user.telefono = persona.telefono;
                    user.direccion = persona.direccion;
                    user.Usuario.usuario1 = persona.usuario1;
                    user.Usuario.correo = persona.correo;
                    user.image = persona.image;

                    //user.Usuario.contraseña = enc.GetSha256(persona.contraseña.ToString());
                    //user.Usuario.confirmar_contraseña = enc.GetSha256(persona.confirmar_contraseña.ToString());

                    db.Entry(user).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                    Session["id"] = user.id;
                    Session["nombre"] = user.nombre;
                    Session["apellido"] = user.apelllido;
                    Session["telefono"] = user.telefono;
                    Session["direccion"] = user.direccion;
                    Session["descripcion"] = user.descripcion;
                    Session["Rol"] = user.Rol.nombre;
                    Session["usuario"] = user.Usuario.usuario1;
                    Session["correo"] = user.Usuario.correo;
                    Session["contraseña"] = user.Usuario.contraseña;
                    Session["confirmarContraseña"] = user.Usuario.confirmar_contraseña;


                }

                return RedirectToAction("Index", "Perfil");
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfil/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Perfil/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetImage (int id)
        {
            using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
            {
                var user = db.Persona.Find(id);
                byte[] imagen = user.image;
                if (imagen == null)
                {
                    return (null);
                }
                else
                {
                    MemoryStream memoryStream = new MemoryStream(imagen);
                    Image image = Image.FromStream(memoryStream);
                    memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Jpeg);
                    memoryStream.Position = 0;
                    

                    return File(memoryStream, "image/jpg");
                }
            }      
        }

        

    }
}
