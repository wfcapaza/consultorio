using Consultorio.BusinessLogic.Service;
using Consultorio.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Consultorio.AppWeb.Controllers
{
    [Authorize]
    public class PacienteController : Controller
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }
        private string ObtenerDniClaim()
        {
            var usuario = HttpContext.User;
            return usuario.FindFirstValue("dni");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string dniUsuario = ObtenerDniClaim();
            var paciente = await _pacienteService.GetAll(dniUsuario);

            return View(paciente);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarPaciente([FromBody] Paciente paciente)
        {
            string dniUsuario = ObtenerDniClaim();
            paciente.DniUsuario = dniUsuario;

            if (paciente == null)
                return Json(new Respuesta() { Mensaje = "Todos los campos son obligatorios" });

            return Json(await _pacienteService.SavePaciente(paciente));
        }
        [HttpGet]
        public async Task<IActionResult> Atencion(string dni)
        {
            string dniUsuario = ObtenerDniClaim();
            var paciente = await _pacienteService.GetOne(dni, dniUsuario);
            return View(paciente);
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerPaciente(string dniPaciente)
        {
            string dniUsuario = ObtenerDniClaim();
            var paciente = await _pacienteService.GetOne(dniPaciente, dniUsuario);
            return Json(paciente);
        }
    }
}
