using Consultorio.BusinessLogic.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;
using Consultorio.Entity;

namespace Consultorio.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string contrasenha)
        {
            Usuario usuario = await _usuarioService.ValidarLogin(correo, contrasenha);

            if (usuario == null)
            {
                ViewData["mensaje"] = "Correo o contraseña están incorrectos";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("correo", usuario.Correo),
                    new Claim("dni", usuario.DNI)
                };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                //IsPersistent = model.RememberMe, // Si se marca 'Recordarme'
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Expiración de la cookie
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity), properties);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario usuario)
        {
            var usuarioEncontrado = await _usuarioService.GetOne(usuario.DNI, usuario.Correo.ToLower());

            if(usuarioEncontrado != null)
            {
                ViewData["mensaje"] = "Usuario ya se encuentra registrado";
                return View();
            }

            var respuesta = await _usuarioService.Insert(usuario);

            if (!respuesta)
            {
                ViewData["mensaje"] = "Ocurrió un error en el registro";
                return View();
            }

            return RedirectToAction("IniciarSesion", "Login");
        }
        public async Task<IActionResult> CerrarSesion() 
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Login"); 
        }
    }
}
