using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var todasMascotas = contexto.Mascotas
                .Include(u => u.Lugar)
                .Include(r => r.Insentivo)
                .Where(m => m.Estado == 1);
            
            foreach (var m in todasMascotas)
            {
                var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == m.UsuarioId);
                var folder = user.UsuarioId + "_" + user.Apellido;
                m.Imagen = folder + "/" + m.Foto;
                listaMascotas.Add(m);
                var prueba = m.Insentivo.Monto;
            }

            return View(listaMascotas);
        }

        // GET: Mascota/Details/5
        public ActionResult Details(int id, int op)
        {
            if (op == 1)
            {
                var usuario = contexto.Usuarios.FirstOrDefault(x => x.Email == User.Identity.Name);
                var folder = usuario.UsuarioId + "_" + usuario.Apellido;
                var mascota = contexto.Mascotas
                                .Include(u => u.Lugar)
                                .Include(r => r.Insentivo)
                                .FirstOrDefault(m => m.MascotaId==id);
                
                mascota.Imagen = folder + "/" + mascota.Foto;
                ViewBag.op = op;
                return View(mascota);
            }
            else
            {
                var mascota = contexto.Mascotas
                                .Include(u => u.Lugar)
                                .Include(r => r.Insentivo)
                                .FirstOrDefault(m => m.MascotaId == id);
                var usuario = contexto.Usuarios.FirstOrDefault(x => x.UsuarioId == mascota.UsuarioId);
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
                if (ModelState.IsValid)
                {
                    var recompensa = new Recompensa
                    {
                        Monto = mascota.Insentivo.Monto,
                        Tiempo = mascota.Insentivo.Tiempo,
                        Estado = 1
                    };
                    if (recompensa.Estado == 1)
                    {
                        contexto.Recompensas.Add(recompensa);
                        contexto.SaveChanges();
                        mascota.RecompensaId = recompensa.RecompensaId;
                    }

                    var ubicacion = new Ubicacion
                    {
                        Latitud = mascota.Lugar.Latitud,
                        Longitud = mascota.Lugar.Longitud,
                        Zona = mascota.Lugar.Zona
                    };
                    contexto.Ubicaciones.Add(ubicacion);
                    contexto.SaveChanges();
                    mascota.UbicacionId = ubicacion.UbicacionId;

                    /************************Control de Imagen****************************/
                    var image = mascota.ArchivoImagen;
                    var user = contexto.Usuarios.FirstOrDefault(u => u.Email == User.Identity.Name);
                    mascota.UsuarioId = user.UsuarioId;
                    var fileName = ControlaImagen.CambiarNombre();
                    var folder = "wwwroot\\imagenesUsuarios\\" + user.UsuarioId + "_" + user.Apellido;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    var filePath = Path.Combine(folder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    using (MagickImage objMagick = new MagickImage(filePath))
                    {
                        objMagick.Resize(300, 0);
                        objMagick.Write(filePath);
                    }

                    mascota.Foto = fileName;

                    /************************Fin de Control de Imgen***********************/

                    contexto.Mascotas.Add(mascota);
                    contexto.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.InnerException;
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

            var usuario = contexto.Usuarios.FirstOrDefault(x => x.Email == User.Identity.Name);
            var folder = usuario.UsuarioId + "_" + usuario.Apellido;
            var mascotas = contexto.Mascotas
                                .Include(u => u.Lugar)
                                .Include(r => r.Insentivo)
                                .Where(m => m.Estado == 1 && m.UsuarioId == usuario.UsuarioId);
            foreach (var m in mascotas)
            {
                m.Imagen = folder + "/" + m.Foto;
                m.Imagen = folder + "/" + m.Foto;
                misMascotas.Add(m);
            }
            return View(misMascotas);
        }
    }
}