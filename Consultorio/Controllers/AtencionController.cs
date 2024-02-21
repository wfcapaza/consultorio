using Consultorio.BusinessLogic.Service;
using Consultorio.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Consultorio.AppWeb.Controllers
{
    public class AtencionController : Controller
    {
        private readonly IAtencionService _atencionService;
        private readonly IMedicamentoService _medicamentoService;

        public AtencionController(IAtencionService atencionService, IMedicamentoService medicamentoService)
        {
            _atencionService = atencionService;
            _medicamentoService = medicamentoService;
        }
        public async Task<IActionResult> Index()
        {
            var atenciones = await _atencionService.GetAll();
            return View(atenciones);
        }
        public IActionResult DetalleAtencion(string strAtencion)
        {
            Atencion atencion = JsonConvert.DeserializeObject<Atencion>(strAtencion);
            return View(atencion);
        }
        public async Task<IActionResult> ResumenAtencion(int idAtencion)
        {
            var atencion = await _atencionService.GetOne(idAtencion);
            return View(atencion);
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerMedicamentos()
        {
            var medicamentos = await _medicamentoService.GetAll();
            return Json(medicamentos);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarAtencion(DateTime? FechaAtencion, string DniPaciente)
        {
            int idAtencion = await _atencionService.Insert(DniPaciente, FechaAtencion);
            return Json(idAtencion);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarDiagnostico([FromBody] DiagnosticoEntity diagnostico)
        {
            bool resultado = await _atencionService.InsertDiagnostico(diagnostico);
            return Json(resultado);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarTratamiento([FromBody] Tratamiento tratamiento)
        {
            bool resultado = await _atencionService.InsertTratamiento(tratamiento);
            return Json(resultado);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarMedicamento([FromBody] MedicamentoAtencion medicamento)
        {
            bool resultado = await _atencionService.InsertMedicamento(medicamento);
            return Json(resultado);
        }

    }
}
