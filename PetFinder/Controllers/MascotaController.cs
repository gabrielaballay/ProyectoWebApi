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
        private readonly int _RegistrosPorPagina =6;
        private PaginadorGenerico<Mascota> _PaginadorCustomers;
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
        public ActionResult Index(int pagina = 1)
        {
            var user = contexto.Usuarios.FirstOrDefault(u => u.Email == User.Identity.Name);
            int _TotalRegistros = 0;
            using (contexto)
            {
                // Número total de registros de la tabla Mascotas
                _TotalRegistros = contexto.Mascotas.Where(m => m.UsuarioId != user.UsuarioId).Count();

                // Obtenemos la 'página de registros' de la tabla Mascotas
                listaMascotas = contexto.Mascotas.OrderBy(x => x.Fecha)
                                                 .Where(x=>x.UsuarioId!=user.UsuarioId)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
                
                // Número total de páginas de la tabla Mascotas
                var _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
                // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
                _PaginadorCustomers = new PaginadorGenerico<Mascota>()
                {
                    RegistrosPorPagina = _RegistrosPorPagina,
                    TotalRegistros = _TotalRegistros,
                    TotalPaginas = _TotalPaginas,
                    PaginaActual = pagina,
                    Resultado = listaMascotas
                };
                // Enviamos a la Vista la 'Clase de paginación'
                return View(_PaginadorCustomers);
            }            
        }

        // GET: Mascota/Details/5
        public ActionResult Details(int id)
        {
            var mascota = contexto.Mascotas
                            .Include(u => u.Lugar)
                            .Include(r => r.Insentivo)
                            .FirstOrDefault(m => m.MascotaId == id);
            var usuario = contexto.Usuarios.FirstOrDefault(x => x.UsuarioId == mascota.UsuarioId);
            
            mascota.Imagen =  mascota.Foto;
            if (usuario.Email == User.Identity.Name)
            {
                ViewBag.op = "MiMascota";
            }
            else
            {
                ViewBag.op = "Index";
                TempData["idmascota"] = mascota.MascotaId;
            }

            return View(mascota);

        }

        // GET: Mascota/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mascota/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    if (Convert.ToInt32(recompensa.Monto) > 1)
                    {
                        if (recompensa.Estado == 1)
                        {
                            contexto.Recompensas.Add(recompensa);
                            contexto.SaveChanges();
                            mascota.RecompensaId = recompensa.RecompensaId;
                        }
                    }
                    else
                    {
                        mascota.RecompensaId = 1;
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
                    var folder = "wwwroot\\imagenesUsuarios\\"+ user.UsuarioId + "_" + user.Apellido + "\\";
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

                    mascota.Foto = user.UsuarioId + "_" + user.Apellido + "\\"+fileName;
                    mascota.Imagen = fileName;
                    /************************Fin de Control de Imgen***********************/
                    mascota.Estado = 1;
                    contexto.Mascotas.Add(mascota);
                    contexto.SaveChanges();
                    ViewBag.Mensaje = "Su mascota se cargo con exito!, precione Volver...";
                    ViewBag.Ok = "ok";
                }

                return View();
            }
            catch (Exception e)
            {
                ViewBag.error = e.InnerException;
                return View();
            }
        }

        // GET: Mascota/Delete/5
        public ActionResult Delete(int id)
        {
            var mascota = contexto.Mascotas.FirstOrDefault(m => m.MascotaId == id);
            mascota.Estado = 0;
            contexto.Update(mascota);
            contexto.SaveChanges();
            return RedirectToAction("MiMascota");
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
            
            return View(mascotas);
        }
    }
}