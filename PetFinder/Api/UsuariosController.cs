using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetFinder.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetFinder.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuariosController : Controller
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public UsuariosController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

		//***************************USUARIO LOGEADO*******************************
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario = User.Identity.Name;

                return Ok(contexto.Usuarios.SingleOrDefault(x => x.Email == usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

		//*************************USUARIO X ID********************************************
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {

                return Ok(contexto.Usuarios.SingleOrDefault(x => x.UsuarioId == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

		//******************************REGISTRARSE*******************************************
        // POST api/<controller>
        [HttpPost("Registrarse")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrarse(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var a=usuario.Telefono;
                    var e = usuario.Clave;
                    /*usuario.Clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                password: usuario.Clave,
                                                salt: System.Text.Encoding.ASCII.GetBytes("SALADA"),
                                                prf: KeyDerivationPrf.HMACSHA1,
                                                iterationCount: 1000,
                                                numBytesRequested: 256 / 8));*/
                    contexto.Usuarios.Add(usuario);
                    contexto.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = usuario.UsuarioId }, usuario);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

		//*************************************UPDATE***********************************
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            
            try
            {
                //edita solo el propietario logeado
                if (ModelState.IsValid && contexto.Usuarios.AsNoTracking().SingleOrDefault(u => u.UsuarioId == id && u.Email == User.Identity.Name) != null)
                {
                    usuario.UsuarioId = id;
					/*if (usuario.Clave.Length <9)
					{
						usuario.Clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
													password: usuario.Clave,
													salt: System.Text.Encoding.ASCII.GetBytes("SALADA"),
													prf: KeyDerivationPrf.HMACSHA1,
													iterationCount: 1000,
													numBytesRequested: 256 / 8));
					}*/
                    contexto.Usuarios.Update(usuario);
                    contexto.SaveChanges();
                    return Ok(usuario);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

		//***************************************DELETE****************************************
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Borra el usuario actual
                var user = contexto.Usuarios.FirstOrDefault(e => e.UsuarioId == id && e.Email == User.Identity.Name);
                if (user != null)
                {
                    user.Estado = 0;
					contexto.Usuarios.Update(user);
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

		//***************************************LOGIN********************************
        // GET api/<controller>/5
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginView loginView)
        {
            try
            {
               /* string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: loginView.Password,
                    salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));*/
                var u = contexto.Usuarios.FirstOrDefault(x => x.Email == loginView.Email);
                if (u == null || u.Clave != loginView.Clave)//hashed)
                {
                    return BadRequest("Nombre de usuario o password incorrecto");
                }
                else
                {
                     var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
                     var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                     var claims = new List<Claim>
                     {
                         new Claim(ClaimTypes.Name, u.Email),
                         new Claim("FullName", u.Nombre + " " + u.Apellido),
                         new Claim(ClaimTypes.Role, "Usuario"),
                     };

                     var token = new JwtSecurityToken(
                         issuer: config["TokenAuthentication:Issuer"],
                         audience: config["TokenAuthentication:Audience"],
                         claims: claims,
                         expires: DateTime.Now.AddMinutes(60),
                         signingCredentials: credenciales
                     );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
