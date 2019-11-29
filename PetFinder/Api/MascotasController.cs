﻿using System;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
		public async Task<IActionResult> Post(Mascota mascota)
		{
			var image = mascota.Imagen;
			if (ModelState.IsValid) {
				if (image.Length > 0)
				{
					var user = contexto.Usuarios.FirstOrDefault(u => u.Email == User.Identity.Name);
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
				}
				contexto.Mascotas.Add(mascota);
				contexto.SaveChanges();
				return CreatedAtAction(nameof(Get), new { id = mascota.MascotaId }, mascota);
			}
			return Ok("prueba");
		}

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

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
			//agregamos un prefijo al nombre generado al azar
			name = "pet_" + name + ext;
			return name;
		}
	}
}
