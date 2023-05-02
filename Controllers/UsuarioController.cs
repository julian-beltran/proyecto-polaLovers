using APPAdminGroup.Filters;
using APPAdminGroup.Helpers;
using APPAdminGroup.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPAdminGroup.Controllers
{
    public class UsuarioController : Controller
    {
        

        // GET: Usuario
        //[AuthorizeUser(idOperacion:1)]
        public ActionResult Index()
        {
           
                return View();    
        }

        [HttpPost]
        public ActionResult Index(loginViewModel usuario)
        {
            Encriptador enc = new Encriptador();
            PerfilController perfil = new PerfilController();
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                string Contraseña = enc.GetSha256(usuario.contraseña.ToString());

                using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
                {

                    var oUsuario = (from d in db.Persona
                                    where d.Usuario.usuario1 == usuario.usuario1.Trim() && d.Usuario.contraseña == Contraseña.Trim()
                                    select d).FirstOrDefault();
                    
                   
                        if (oUsuario == null)
                        {
                            ViewBag.Error = "Usuario o Contraseña invalida";
                            return View();

                        } else
                        
                          if(oUsuario.Usuario.confirmarEmail == true)
                          {
                                 Session["User"] = oUsuario;
                        Session["id"] = oUsuario.id;
                        Session["nombre"] = oUsuario.nombre;
                        Session["apellido"] = oUsuario.apelllido;
                        Session["telefono"] = oUsuario.telefono;
                        Session["direccion"] = oUsuario.direccion;
                        Session["descripcion"] = oUsuario.descripcion;
                        Session["Rol"] = oUsuario.Rol.nombre;
                        Session["usuario"] = oUsuario.Usuario.usuario1;
                        Session["correo"] = oUsuario.Usuario.correo;
                        Session["contraseña"] = oUsuario.Usuario.contraseña;
                        Session["confirmarContraseña"] = oUsuario.Usuario.confirmar_contraseña;
                        




                        return RedirectToAction("Index", "Index");
                        
                          } else
                          {
                                ViewBag.Message = "Su cuenta no ha sido verificada, rebice su correo electronico";
                                return View();
                          }
                    
                }
                
              


            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();

            }
            
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Persona persona, string menCon)
        {
           
            Encriptador enc = new Encriptador();
            string token =enc.GetSha256(Guid.NewGuid().ToString());
            enviarCorreo env = new enviarCorreo();
            
           
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                using (DB_PROYECTOEntities1 Context = new DB_PROYECTOEntities1())
                {

                    var oUser = (from d in Context.Usuario
                                 where d.usuario1 == persona.Usuario.usuario1.Trim() || d.correo == persona.Usuario.correo.Trim()
                                 select d).FirstOrDefault();

                    if (oUser == null)
                    {

                        persona.Usuario.tokenCorreo = token;
                        persona.Usuario.contraseña = enc.GetSha256(persona.Usuario.contraseña.ToString());
                        persona.Usuario.confirmar_contraseña = enc.GetSha256(persona.Usuario.confirmar_contraseña.ToString());
                        persona.Usuario.confirmarEmail = false; 
                        Context.Usuario.Add(persona.Usuario);
                        Context.SaveChanges();

                        
                        //persona.id_usuario = persona.User.id;
                        persona.id_rol = 3;

                        Context.Persona.Add(persona);
                        Context.SaveChanges();
                        env.SendMail(persona.Usuario.correo,token);
                    }
                    else
                    {
                        ViewBag.Error="Ususario o email ya registrados comuniquese con el administrador";
                        return View();
                    }

                }

                
                return RedirectToAction("ConfirmarCorreo");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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

        public ActionResult ConfirmarCorreo()
        {
            return View();
        }


        public ActionResult Confirmar(string token)
        {
          
            using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
            {
                var oUser = db.Usuario.Where(d => d.tokenCorreo == token).FirstOrDefault();
                if(oUser == null)
                {
                    return RedirectToAction("Index", "Usuario");
                }
                ViewBag.Message = "Haz Confirmado Tu correo, Haz click para continuar";

                return View(db.Usuario.Where(m => m.tokenCorreo == token).FirstOrDefault());

            }
           
        }

        [HttpPost]
        public ActionResult Confirmar(string token, Usuario usuario)
        {
            try
            {

                using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
                {
                    var oUser = db.Usuario.Where(d => d.tokenCorreo == usuario.tokenCorreo).FirstOrDefault();

                    if (oUser != null)
                    {
                        oUser.confirmarEmail = true;
                        oUser.tokenCorreo = null;
                        db.Entry(oUser).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Index");

                    }
                    else
                    {
                     
                        return RedirectToAction("Index","Usuario");
                    }
                }

            }
            catch
            {
                
                return View();

            }
            
        }
        public ActionResult ContraseñaStartRecovery()
        {
            ContraseñaStartRecoveryViewModel usuario = new ContraseñaStartRecoveryViewModel();

            return View(usuario);
        }
        [HttpPost]
        public ActionResult ContraseñaStartRecovery(ContraseñaStartRecoveryViewModel usuario)
        {
            Encriptador enc = new Encriptador();
            string token = enc.GetSha256(Guid.NewGuid().ToString());
            enviarCorreo env = new enviarCorreo();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                using (DB_PROYECTOEntities1 Context = new DB_PROYECTOEntities1())
                {
                    
                    var oUser = (from d in Context.Usuario
                                 where d.correo == usuario.correo
                                 select d).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.tokenContraseña = token;
                        Context.Entry(oUser).State = System.Data.EntityState.Modified;
                        Context.SaveChanges();
                        env.SendMailContraseña(oUser.correo, token);

                        
                        return RedirectToAction("Index","Usuario");

                    }
                    else
                    {
                        return View();
                    }
                    
                }
            }
            catch
            {

                return View();
            }

        }

        public ActionResult ContraseñaRecovery(string token)
        {
            ContraseñaRecoveryViewModel usuario = new ContraseñaRecoveryViewModel();
            usuario.tokenContraseña = token;
            using (DB_PROYECTOEntities1 Context =new DB_PROYECTOEntities1())
            {
                var ouser = Context.Usuario.Where(d => d.tokenContraseña == token).FirstOrDefault();
                if(ouser == null)
                {
                    return RedirectToAction("Index", "Usuario");
                }

            }
                return View(usuario);
            
        }
        [HttpPost]
        public ActionResult ContraseñaRecovery(ContraseñaRecoveryViewModel usuario)
        {
            Encriptador enc = new Encriptador();
           
           
            try 
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                using (DB_PROYECTOEntities1 Context = new DB_PROYECTOEntities1())
                {
                    var ouser = Context.Usuario.Where(d => d.tokenContraseña == usuario.tokenContraseña).FirstOrDefault();
                    if(ouser!= null)
                    {
                        ouser.contraseña = enc.GetSha256(usuario.contraseña.ToString());
                        ouser.confirmar_contraseña = enc.GetSha256(usuario.confirmar_contraseña.ToString());
                        ouser.tokenContraseña = null;
                        Context.Entry(ouser).State = System.Data.EntityState.Modified;
                        Context.SaveChanges();
                    }
                    return RedirectToAction("Index","Usuario");
                }
            }
            catch
            {

            }
            return View();
        }

        
    }
}
