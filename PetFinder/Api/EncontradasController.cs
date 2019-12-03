using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetFinder.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetFinder.Api
{
    [Route("api/[controller]")]
    public class EncontradasController : Controller
    {
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
                var encontradas = contexto.Encontradas;
                return Ok(encontradas.ToList());
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
            contexto.Encontradas.Add(encontrada);
            contexto.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = encontrada.EncontradaId }, encontrada);
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
    }
}
