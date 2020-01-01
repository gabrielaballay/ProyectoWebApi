using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetFinder.Models;

namespace PetFinder.Controllers
{
    [Authorize(Policy = "Administrador")]
    public class MascotaController : Controller
    {
        private List<Mascota> listaMascotas = new List<Mascota>();
        private List<Mascota> misMascotas = new List<Mascota>();
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public MascotaController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        // GET: Mascota
        public ActionResult Index()
        {
            var todasMascotas = contexto.Mascotas.Where(x => x.Estado == 1);
            foreach (var m in todasMascotas)
            {
                var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == m.UsuarioId);
                var folder = user.UsuarioId + "_" + user.Apellido;
                m.Imagen = folder + "/" + m.Foto;
                listaMascotas.Add(m);
            }
            
            return View(listaMascotas);
        }

        // GET: Mascota/Details/5
        public ActionResult Details(int id, int op)
        {
            if (op == 1) {
                var usuario = contexto.Usuarios.FirstOrDefault(x => x.Email == User.Identity.Name);
                var folder = usuario.UsuarioId + "_" + usuario.Apellido;
                var mascota = contexto.Mascotas.FirstOrDefault(x => x.MascotaId == id);
                mascota.Imagen = folder + "/" + mascota.Foto;
                ViewBag.op = op;
                return View(mascota);
            }else
            {
                var mascota = contexto.Mascotas.FirstOrDefault(x => x.MascotaId == id);
                var usuario = contexto.Usuarios.FirstOrDefault(x => x.UsuarioId==mascota.UsuarioId);
                var folder = usuario.UsuarioId + "_" + usuario.Apellido;                
                mascota.Imagen = folder + "/" + mascota.Foto;
                ViewBag.op = op;
                return View(mascota);
            }
        }

        // GET: Mascota/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mascota/Create
        [HttpPost]
        public ActionResult Create(Mascota mascota)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mascota/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mascota/Edit/5
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

        // GET: Mascota/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mascota/Delete/5
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

        // GET: Mascota/MiMascota/5
        public ActionResult MiMascota()
        {
            var usuario =contexto.Usuarios.FirstOrDefault(x=>x.Email==User.Identity.Name);
            var folder = usuario.UsuarioId + "_" + usuario.Apellido;
            var mascotas = contexto.Mascotas.Where(m => m.UsuarioId == usuario.UsuarioId);
            foreach (var m in mascotas)
            {
                m.Imagen = folder + "/" + m.Foto;
                misMascotas.Add(m);
            }
            return View(misMascotas);
        }
    }
}