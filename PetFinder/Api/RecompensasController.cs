using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetFinder.Api
{
    [Route("api/[controller]")]
    public class RecompensasController : Controller
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public RecompensasController(DataContext contexto, IConfiguration config)
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
        public async Task<IActionResult> Get(int id)
        {
            try
            {

                return Ok(contexto.Recompensas.SingleOrDefault(x => x.RecompensaId == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(Recompensa recompensa)
        {
            contexto.Recompensas.Add(recompensa);
            contexto.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = recompensa.RecompensaId }, recompensa);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Recompensa recompensa)
        {
            contexto.Recompensas.Update(recompensa);
            contexto.SaveChanges();
            return Ok(recompensa);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recompensa = contexto.Recompensas.FirstOrDefault(e => e.RecompensaId == id);
            if (recompensa != null)
            {
                recompensa.Estado = 0;
                contexto.Recompensas.Update(recompensa);
                contexto.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
