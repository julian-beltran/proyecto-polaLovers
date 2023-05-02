using APPAdminGroup.Filters;
using APPAdminGroup.Helpers;
using APPAdminGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPAdminGroup.Controllers
{
    public class PersonaController : Controller
    {
        [AuthorizeUser(idOperacion: 1)]
        // GET: Persona
        public ActionResult Listar()
        {
            List<PersonaViewModel> lst = null;
            using(DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
            {

                lst = (from d in db.Persona
                       where d.id_rol == 2 || d.id_rol == 3
                       orderby d.nombre
                       select new PersonaViewModel
                       {
                           id = d.id,
                           nombre = d.nombre,
                           apelllido =d.apelllido,
                           telefono = d.telefono,
                           direccion = d.direccion,
                           descripcion =d.descripcion, 
                           rol=d.Rol,
                           usuario=d.Usuario,
                          
                          
                       }).ToList();
            }
            return View(lst);
        }

        // GET: Persona/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Persona/Create
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Create()
        {
            List<Rol> lista = null;
            using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
            {
                 lista = db.Rol.Where(d=>d.id !=1 ).ToList();
                
            }
            ViewBag.lista = lista;
            return View();
        }

        // POST: Persona/Create
        [HttpPost]
        public ActionResult Create(PersonaCreateViewModel personaCrear)
        {
            Encriptador enc = new Encriptador();
            string token = enc.GetSha256(Guid.NewGuid().ToString());
            enviarCorreo env = new enviarCorreo();


            try
            {
                if (!ModelState.IsValid)
                {
                    List<Rol> lista = null;
                    using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
                    {
                        lista = db.Rol.Where(d => d.id != 1).ToList();

                    }
                    ViewBag.lista = lista;
                    return View();
                }
                using (DB_PROYECTOEntities1 Context = new DB_PROYECTOEntities1())
                {

                    var oUser = (from d in Context.Usuario
                                 where d.usuario1 == personaCrear.Usuario.usuario1.Trim() || d.correo == personaCrear.Usuario.correo.Trim()
                                 select d).FirstOrDefault();

                    if (oUser == null)
                    {
                        
                        personaCrear.Usuario.tokenCorreo = token;
                        personaCrear.Usuario.contraseña = enc.GetSha256(personaCrear.Usuario.contraseña.ToString());
                        personaCrear.Usuario.confirmar_contraseña = enc.GetSha256(personaCrear.Usuario.confirmar_contraseña.ToString());
                        personaCrear.Usuario.confirmarEmail = false;
                        Context.Usuario.Add(personaCrear.Usuario);
                        Context.SaveChanges();

                        Persona persona = new Persona();
                        
                        
                        persona.nombre = personaCrear.nombre;
                        persona.apelllido = personaCrear.apelllido;
                        persona.telefono = personaCrear.telefono;
                        persona.direccion = personaCrear.direccion;
                        persona.descripcion = personaCrear.descripcion;
                        persona.id_rol = personaCrear.id_rol;
                        persona.id_usuario = personaCrear.Usuario.id;



                        Context.Persona.Add(persona);
                        Context.SaveChanges();
                        env.SendMail(personaCrear.Usuario.correo, token);
                    }
                    else
                    {
                        ViewBag.Error = "Ususario o email ya registrados comuniquese con el administrador";
                        return View();
                    }

                }


                return RedirectToAction("Listar","Persona");

            }
            catch
            {
                return View();
            }
        }

        [AuthorizeUser(idOperacion: 3)]
        // GET: Persona/Edit/5
        public ActionResult Edit(int id)
        {
            List<Rol> lista = null;
            using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
            {
                lista = db.Rol.Where(d => d.id != 1).ToList();

            }
            ViewBag.lista = lista;
            PersonaEditViewModel persona = new PersonaEditViewModel();
            using(DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
            {
                var user = db.Persona.Find(id);
                persona.id = user.id;
                persona.nombre = user.nombre;
                persona.apelllido = user.apelllido;
                persona.telefono = user.telefono;
                persona.direccion = user.direccion;
                persona.descripcion = user.descripcion;
                persona.id_rol = user.id_rol;
                persona.usuario1 = user.Usuario.usuario1;
                persona.correo = user.Usuario.correo;
                //persona.contraseña = user.Usuario.contraseña;
                //persona.confirmar_contraseña = user.Usuario.confirmar_contraseña;
            }
            return View(persona);
        }

        // POST: Persona/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PersonaEditViewModel persona)
        {
            Encriptador enc = new Encriptador();
          

            try
            {
                if (!ModelState.IsValid)
                {
                    List<Rol> lista = null;
                    using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
                    {
                        lista = db.Rol.Where(d => d.id != 1).ToList();

                    }
                    ViewBag.lista = lista;
                    return View();
                }
                using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
                {
                    
                        var user = db.Persona.Find(persona.id);
                        user.id = persona.id;
                        user.nombre = persona.nombre;
                        user.apelllido = persona.apelllido;
                        user.telefono = persona.telefono;
                        user.direccion = persona.direccion;
                        user.descripcion = persona.descripcion;
                        user.id_rol = persona.id_rol;
                        user.Usuario.usuario1 = persona.usuario1;
                        user.Usuario.correo = persona.correo;
                        //user.Usuario.contraseña = enc.GetSha256(persona.contraseña.ToString());
                        //user.Usuario.confirmar_contraseña = enc.GetSha256(persona.confirmar_contraseña.ToString());

                        db.Entry(user).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                   

                }

                return RedirectToAction("Listar","Persona");
            }
            catch
            {
                return View();
            }
        }


        [AuthorizeUser(idOperacion: 4)]
        // POST: Persona/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
               
                using (DB_PROYECTOEntities1 db = new DB_PROYECTOEntities1())
                {
                    

                    var user = db.Persona.Find(id);

                    var ouser = db.Usuario.Where(d => d.id == user.Usuario.id).FirstOrDefault();
                    if (ouser == null)
                    {
                        return View();
                    }
                    else
                    {
                        db.Usuario.Remove(ouser);

                        db.Persona.Remove(user);
                        db.SaveChanges();
                    }

                }

                return RedirectToAction("Listar", "Persona");
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

                        ViewBag.Message = "Se ha enviado un Correo Electronico al usuario para que complete el proceso de cambio de contraseña";
                        return View();

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
    }
}
