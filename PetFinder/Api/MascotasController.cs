using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetFinder.Models;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetFinder.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MascotasController : Controller
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        private ArrayList listaMascotas = new ArrayList();
        private ArrayList misMascotas = new ArrayList();

        public MascotasController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        // GET: api/<controller>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id != -1)
                {
                    var mascotasTodas = contexto.Mascotas.Where(x => x.Estado == 1 && x.UsuarioId == id);

                    foreach (var m in mascotasTodas)
                    {
                        var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
                        var folder = "wwwroot\\imagenesUsuarios\\" + user.UsuarioId + "_" + user.Apellido;
                        m.Imagen = Conversor(folder + "\\" + m.Foto);
                        misMascotas.Add(m);
                    }

                    return Ok(misMascotas);
                }
                else
                {
                    var mascotasTodas = contexto.Mascotas.Where(x => x.Estado == 1);

                    foreach (var m in mascotasTodas)
                    {
                        var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == m.UsuarioId);
                        var folder = "wwwroot\\imagenesUsuarios\\" + user.UsuarioId + "_" + user.Apellido;
                        m.Imagen = Conversor(folder + "\\" + m.Foto);
                        listaMascotas.Add(m);
                    }

                    return Ok(listaMascotas);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> mascotasPerdidasList()
        {
            try
            {
                var mascotasTodas = contexto.Mascotas.Where(x => x.Estado == 1);

                foreach (var m in mascotasTodas)
                {
                    var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == m.UsuarioId);
                    var folder = "wwwroot\\imagenesUsuarios\\" + user.UsuarioId + "_" + user.Apellido;
                    m.Imagen = Conversor(folder + "\\" + m.Foto);
                    listaMascotas.Add(m);
                }

                return Ok(listaMascotas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /*// GET api/<controller>
        [HttpGet]
        public async Task<IActionResult> Encontradas()
        {
            try
            {
                var mascotas = contexto.Mascotas.Where(x => x.Estado == 2);

                foreach (var m in mascotas)
                {
                    var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == m.UsuarioId);
                    var folder = "wwwroot\\imagenesUsuarios\\" + user.UsuarioId + "_" + user.Apellido;
                    m.Imagen = Conversor(folder + "\\" + m.Foto);
                    encontradas.Add(m);
                }

                return Ok(encontradas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }*/

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(Mascota mascota)
        {

            var nam = mascota.NombreMascota;
            var n = mascota.UsuarioId;

            if (ModelState.IsValid)
            {
                if (mascota.Imagen.Length > 0)
                {

                    //Cargo la recompensa si existiera
                    //if (mascota.Insentivo != null)
                    //{
                    //    contexto.Recompensas.Add(mascota.Insentivo);
                    //    contexto.SaveChanges();
                    // }
                    //else
                    // {
                    mascota.RecompensaId = 1;
                    //}

                    //recupero el usuario
                    var user = contexto.Usuarios.FirstOrDefault(u => u.Email == User.Identity.Name);
                    mascota.UsuarioId = user.UsuarioId;
                    //Guardo la imagen en el servidor
                    var fileName = CambiarNombre();
                    var folder = "wwwroot\\imagenesUsuarios\\" + user.UsuarioId + "_" + user.Apellido;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    var filePath = Path.Combine(folder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        var bytes = Convert.FromBase64String(mascota.Imagen);
                        fileStream.Write(bytes, 0, bytes.Length);
                        fileStream.Flush();
                        mascota.Foto = fileName;
                    }
                    try
                    {
                        //Guardo la mascota
                        contexto.Mascotas.Add(mascota);
                        contexto.SaveChanges();
                        return CreatedAtAction(nameof(Get), new { id = mascota.MascotaId }, mascota);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                return BadRequest();
            }
            return Ok("prueba");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Mascota mascota)
        {
            try
            {
                contexto.Mascotas.Update(mascota);
                contexto.SaveChanges();
                return Ok(mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Cambio el estado de la moscota
                var user = contexto.Usuarios.FirstOrDefault(e => e.Email == User.Identity.Name);
                var mascota = contexto.Mascotas.FirstOrDefault(m => m.MascotaId == id);
                if (user != null)
                {
                    mascota.Estado = 0;
                    contexto.Mascotas.Update(mascota);
                    contexto.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /****************************************************************
        **** Metodo para generar un nombre al azar para las imagenes****
        * **************************************************************/
        private string CambiarNombre()
        {
            //caracteres para el nombre nuevo
            string chars = "23456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            //crean un generador al azar
            Random rnd = new Random();
            string name = string.Empty;
            while (name.Length < 8)
            {
                name += chars.Substring(rnd.Next(chars.Length), 1);
            }
            //agregamos un prefijo al nombre generado al azar y la extension del mismo
            name = "pet_" + name + ".jpg";
            return name;
        }

        private string Conversor(string Path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

    }

}
