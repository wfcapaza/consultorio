using Consultorio.BusinessLogic.Service;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio.AppWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarUsuario()
        {
            var listaUsuarios = _usuarioService.GetAll();
            return Json(listaUsuarios);
        }
        [HttpGet]
        public async Task<JsonResult> GetOneUser(string dni, string correo)
        {
            var usuario = await _usuarioService.GetOne(dni, correo);
            return Json(usuario);
        }
    }
}
