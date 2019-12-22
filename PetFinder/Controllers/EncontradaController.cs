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
    public class EncontradaController : Controller
    {
        private List<Encontrada> listaMascotas = new List<Encontrada>();
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public EncontradaController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }
        // GET: Encontrada
        public ActionResult Index()
        {
            var todasMascotas = contexto.Encontradas;
            foreach (var m in todasMascotas)
            {
                var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == m.UsuarioId);
                var folder = user.UsuarioId + "_" + user.Apellido;
                m.Imagen = folder + "/" + m.Foto;
                listaMascotas.Add(m);
            }

            return View(listaMascotas);
        }

        // GET: Encontrada/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Encontrada/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encontrada/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Encontrada/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Encontrada/Edit/5
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

        // GET: Encontrada/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Encontrada/Delete/5
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
        }
    }
}