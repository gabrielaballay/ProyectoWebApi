using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetFinder.Models;

namespace PetFinder.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public UsuarioController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }
        // GET: Usuario
        public ActionResult Index(int id)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(x => x.UsuarioId == 12);
            return View(usuario);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuario = contexto.Usuarios.SingleOrDefault(x => x.UsuarioId == id);
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    contexto.Usuarios.Add(usuario);
                    contexto.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ViewBag.StackTrace = e.StackTrace;
                ViewBag.Error = e.Message;
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

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

    }
}