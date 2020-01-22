using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetFinder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetFinder.Controllers
{
    [Authorize(Policy = "Administrador")]
    public class HomeController : Controller
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public HomeController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Home/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginView loginView)
        {
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: loginView.Clave,
                    salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                var u = contexto.Usuarios.FirstOrDefault(x => x.Email == loginView.Email && x.Estado == 1);

                if (u != null && hashed == u.Clave)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, u.Email),
                        new Claim("FullName", u.Nombre + " " + u.Apellido),
                        new Claim(ClaimTypes.Role, "Administrador"),
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties

                    {
                        AllowRefresh = true,

                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensaje = "Correo o Contraseña Incorrectos!";
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View();
            }
        }

        // GET: Home/Logout
        public async Task<ActionResult> Logout()
        {

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // GET: Home/Account
        [AllowAnonymous]
        public async Task<ActionResult> Account(string token)
        {
            var user = contexto.Usuarios.FirstOrDefault(u => u.Confirma == token);
            if (user == null)
            {
                ViewBag.Error = "El usuario no existe!";
                return RedirectToAction("Login");
            }
            user.Confirma = "activa";
            user.Estado = 1;
            contexto.Usuarios.Update(user);
            contexto.SaveChanges();
            return View(user);
        }

        // GET: Home/StartRecovery
        [AllowAnonymous]
        public async Task<ActionResult> StartRecovery()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> StartRecovery(StarRecoveryViewModel srvm)
        {
            var user = contexto.Usuarios.FirstOrDefault(u => u.Email == srvm.Correo);
            if (user != null)
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: srvm.Correo,
                        salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));
                new Email().RestaurarClave(srvm.Correo, hashed);
                user.Confirma = hashed;
                contexto.Usuarios.Update(user);
                contexto.SaveChanges();
                ViewBag.Mensaje = "El correo se envio con exito.";                
                return View(srvm);
            }
            ViewBag.Mensaje = "El correo no existe!!!";
            return View();
        }

        // GET: Recovery/token
        [AllowAnonymous]
        public async Task<ActionResult> Recovery(string token)
        {
            var user = contexto.Usuarios.FirstOrDefault(u => u.Confirma == token);
            if (user == null)
            {
                ViewBag.Error = "Error!, No se puede recuperar su contraseña";
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Recovery(RecoveryViewModel rvm)
        {
            var user = contexto.Usuarios.FirstOrDefault(u => u.Email == rvm.Correo);
            if (user != null && rvm.NewClave==rvm.RepeatClave)
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: rvm.NewClave,
                    salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                user.Confirma = "activa";
                user.Clave = hashed;
                contexto.Usuarios.Update(user);
                contexto.SaveChanges();
                ViewBag.Mensaje = "Su contraseña se restauro con exito. Ya puede ingresar.";
                return View(rvm);
            }
            ViewBag.Mensaje = "El correo no existe o las contraseñas no son iguales!!!";
            return View();
        }

    }
}
