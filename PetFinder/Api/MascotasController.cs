using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetFinder.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetFinder.Api
{
    [Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class MascotasController : Controller
    {
		private readonly DataContext contexto;
		private readonly IConfiguration config;

		public MascotasController(DataContext contexto, IConfiguration config)
		{
			this.contexto = contexto;
			this.config = config;
		}
		// GET: api/<controller>
		[HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var mascotas = contexto.Mascotas.Where(x => x.Estado == 1);
                return Ok(mascotas.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var mascotas = contexto.Mascotas.Where(x => x.Estado == 1 && x.UsuarioId==id);
            return Ok(mascotas.ToList());
        }

        // POST api/<controller>
        [HttpPost]
		public async Task<IActionResult> Post(Mascota mascota)
		{
			var image = mascota.Imagen;
			if (ModelState.IsValid) {
				if (image.Length > 0)
				{
                    //Cargo de ubicacion de la mascota
                    contexto.Localizaciones.Add(mascota.ubicacion);
                    contexto.SaveChanges();

                    //Cargo la recompensa si existiera
                    if (mascota.Insentivo != null)
                    {
                        contexto.Recompensas.Add(mascota.Insentivo);
                        contexto.SaveChanges();
                    }

                    //recupero el usuario
					var user = contexto.Usuarios.FirstOrDefault(u => u.Email == User.Identity.Name);

                    //Guardo la imagen en el servidor
                    var fileName = CambiarNombre(Path.GetExtension(image.FileName));
					var folder = "wwwroot/imagenesUser/" + user.UsuarioId + "_" + user.Apellido + user.Nombre;
					if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}
					var filePath = Path.Combine(folder, fileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						image.CopyTo(fileStream);
						mascota.Foto = fileName;
					}

                    //Guardo la mascota
                    mascota.LocalizaId = mascota.ubicacion.LocalizacionId;
                    contexto.Mascotas.Add(mascota);
                    contexto.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = mascota.MascotaId }, mascota);
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
        private string CambiarNombre(string ext)
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
            name = "pet_" + name + ext;
            return name;
        }
    }
}
