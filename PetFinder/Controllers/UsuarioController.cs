using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetFinder.Models;
using System;
using System.Linq;

namespace PetFinder.Controllers
{
    [Authorize(Policy = "Administrador")]
    public class UsuarioController : Controller
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public UsuarioController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        /*******************Index de usuario*******************/
        // GET: Usuario
        public ActionResult Index()
        {
            var u= User.Identity.Name;
            var usuario = contexto.Usuarios.Include(x=>x.Vive).FirstOrDefault(x => x.Email==u);
            
            if (TempData.ContainsKey("MensajeData"))
            {
                ViewBag.Mensaje = TempData["MensajeData"].ToString();
            }
            
            return View(usuario);
        }

        /******************Crea un usuario nuevo****************/
        // GET: Usuario/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                usuario.Estado = 1;
                if (ModelState.IsValid)
                {
                    contexto.Usuarios.Add(usuario);
                    contexto.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(ModelState.Values);
            }
            catch (Exception e)
            {
                ViewBag.StackTrace = e.StackTrace;
                ViewBag.Error = e.Message;
                return View();
            }
        }

        /******************Edita el usuario actual*****************/
        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            var user = contexto.Usuarios.SingleOrDefault(x => x.UsuarioId == id);
            return View(user);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario user)
        {

            try
            {
                var user_old = contexto.Usuarios.AsNoTracking().SingleOrDefault(u => u.UsuarioId == id);
                
                if (ModelState.IsValid && user_old!= null)
                {
                    user.UsuarioId = id;                    
                    user.Estado=1;
                    contexto.Usuarios.Update(user);
                    contexto.SaveChanges();
                    TempData["MensajeData"] = "Los datos se modificaron con exito!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al intentar modificar los datos!";
                    return View(user);
                }                
            }
            catch (Exception ex)
            {
                ViewBag.StackTrace = ex.StackTrace;
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Usuario/Edit/5        
        public ActionResult Details(int id)
        {
            try
            {
                if (TempData.ContainsKey("idmascota"))
                {
                    ViewBag.idmascota = TempData["idmascota"].ToString();
                }
                    
                var user = contexto.Usuarios.AsNoTracking().SingleOrDefault(u => u.UsuarioId == id);                
                return View(user);                
            }
            catch (Exception ex)
            {
                ViewBag.StackTrace = ex.StackTrace;
                ViewBag.Error = "Error no se Puede mostrar este usuario";
                return View();
            }
        }

        /******Cambia el estado del usuario cuando este elimine su cuenta******/
        // GET: Usuario/Delete/5
        /* public ActionResult Delete(int id)
         {
             return View();
         }

         // POST: Usuario/Delete/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Delete(int id, IFormCollection collection)
         {
             try
             {
                 // TODO: Add delete logic here

                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }*/

        public ActionResult CambiaClave(int id)
        {            
            return View();
        }

        // POST: Usuario/CambiaClave/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiaClave(CambioClave cambioClave)
        {

            try
            {
                var user_old = contexto.Usuarios.AsNoTracking().SingleOrDefault(u => u.Email== User.Identity.Name);

                if (ModelState.IsValid) 
                {
                    if (user_old.Clave == cambioClave.OldClave)
                    {                        
                        /*user.UsuarioId = id;
                        user.Estado = 1;
                        contexto.Usuarios.Update(user);
                        contexto.SaveChanges();
                        TempData["MensajeData"] = "Los datos se modificaron con exito!";
                        return RedirectToAction(nameof(Index));*/
                        ViewBag.Error = "Bien!";
                        return RedirectToAction("Logout", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorOldClave = "Contraseña incorrecta";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Error al intentar modificar los datos!";
                    return View();
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.StackTrace = ex.StackTrace;
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult CambiaMail()
        {
            ViewBag.mail = User.Identity.Name;
            return View();
        }

        // POST: Usuario/CambiaMail/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiaMail(CambiarCorreo model)
        {
            if (ModelState.IsValid && model.NewCorreo==model.RepeatCorreo)
            {
                var user = contexto.Usuarios.FirstOrDefault(x => x.Email == model.OldCorreo);
                user.Email = model.NewCorreo;
                contexto.Usuarios.Update(user);
                contexto.SaveChanges();

                return RedirectToAction("Logout", "Home");
            }
            else
            {
                ViewBag.mail = User.Identity.Name;
                return View(model);
            }            
        }

    }
}