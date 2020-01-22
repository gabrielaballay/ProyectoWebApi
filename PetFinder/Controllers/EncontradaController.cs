using System;
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
    public class EncontradaController : Controller
    {
        private readonly int _RegistrosPorPagina = 6;
        private PaginadorGenerico<Encontrada> _PaginadorCustomers;
        private List<Encontrada> listaMascotas = new List<Encontrada>();
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public EncontradaController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }
        // GET: Encontrada
        public ActionResult Index(int pagina = 1)
        {
            int _TotalRegistros = 0;
            using (contexto)
            {
                // Número total de registros de la tabla Customers
                _TotalRegistros = contexto.Encontradas.Count();
                // Obtenemos la 'página de registros' de la tabla Customers
                listaMascotas = contexto.Encontradas.OrderBy(x => x.Fecha)
                                                 .Include(u => u.Lugar)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
                // Número total de páginas de la tabla Customers
                var _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
                // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
                _PaginadorCustomers = new PaginadorGenerico<Encontrada>()
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

        // GET: Encontrada/Details/5
        public ActionResult Details(int id)
        {
            var model= contexto.Encontradas
                 .Include(u => u.Lugar)
                 .FirstOrDefault(e => e.EncontradaId == id);

            return View(model);
        }
        
        // GET: Encontrada/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encontrada/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Encontrada encontrada)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ubicacion = new Ubicacion
                    {
                        Latitud = encontrada.Lugar.Latitud,
                        Longitud = encontrada.Lugar.Longitud,
                        Zona = encontrada.Lugar.Zona
                    };
                    contexto.Ubicaciones.Add(ubicacion);
                    contexto.SaveChanges();
                    encontrada.UbicacionId = ubicacion.UbicacionId;

                    /************************Control de Imagen****************************/
                    var image = encontrada.ArchivoImagen;
                    var user = contexto.Usuarios.FirstOrDefault(u => u.Email == User.Identity.Name);
                    encontrada.UsuarioId = user.UsuarioId;
                    var fileName = ControlaImagen.CambiarNombre();
                    var folder = "wwwroot\\imagenesUsuarios\\"+ "encontradas\\" + user.UsuarioId + "_" + user.Apellido + "\\" ;
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

                    encontrada.Foto = "encontradas\\" + user.UsuarioId + "_" + user.Apellido + "\\" + fileName;
                    encontrada.Imagen = fileName;
                    /************************Fin de Control de Imgen***********************/
                    encontrada.Estado = 1;
                    contexto.Encontradas.Add(encontrada);
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