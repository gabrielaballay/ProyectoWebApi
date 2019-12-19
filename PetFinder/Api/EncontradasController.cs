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
    public class EncontradasController : Controller
    {
        private ArrayList listaMascotas = new ArrayList();

        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public EncontradasController(DataContext contexto, IConfiguration config)
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
               
                    var mascotasTodas = contexto.Encontradas.ToList();

                    foreach (var m in mascotasTodas)
                    {
                        var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == m.UsuarioId);
                        var folder = "wwwroot\\imagenesUsuarios\\encontradas\\" + user.UsuarioId + "_" + user.Apellido;
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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(Encontrada encontrada)
        {
            
            if (ModelState.IsValid)
            {
                if (encontrada.Imagen.Length > 0)
                {
                    //recupero el usuario
                    var user = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId==encontrada.UsuarioId);

                    //Guardo la imagen en el servidor
                    var fileName = CambiarNombre();
                    var folder = "wwwroot\\imagenesUsuarios\\encontradas\\" + user.UsuarioId + "_" + user.Apellido;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    var filePath = Path.Combine(folder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        var bytes = Convert.FromBase64String(encontrada.Imagen);
                        fileStream.Write(bytes, 0, bytes.Length);
                        fileStream.Flush();
                        encontrada.Foto = fileName;
                    }

                    try
                    {
                        //Guardo la mascota
                        contexto.Encontradas.Add(encontrada);
                        contexto.SaveChanges();
                        return CreatedAtAction(nameof(Get), new { id = encontrada.EncontradaId }, encontrada);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.InnerException);
                    }
                }
                return BadRequest();
            }
            return Ok("prueba");
        }                    
    

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Encontrada encontrada)
        {
            try
            {
                contexto.Encontradas.Update(encontrada);
                contexto.SaveChanges();
                return Ok(encontrada);
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
                var encontrada = contexto.Encontradas.FirstOrDefault(x => x.EncontradaId == id);
                if (encontrada != null)
                {
                    //encontrada.Estado = 0;
                    //contexto.Encontradas.Update(encontrada);
                    //contexto.SaveChanges();
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
        **** Metodo para generar un nombre al azar para las imagenes ****
        *****************************************************************/
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


        /*******************************************************************
         ***Convierte una imagen a base64 para poder enviarla como string***
         *******************************************************************/
        private string Conversor(string Path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }
    }
}
