using Consultorio.BusinessLogic.Service;
using Consultorio.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio.AppWeb.Controllers
{
    [Authorize]
    public class MedicamentoController : Controller
    {
        private readonly IMedicamentoService _serviceMedicamento;

        public MedicamentoController(IMedicamentoService serviceMedicamento)
        {
            _serviceMedicamento = serviceMedicamento;
        }
        public async Task<IActionResult> Index()
        {
            var medicamentos = await _serviceMedicamento.GetAll();
            return View(medicamentos);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarMedicamento(Medicamento medicamento)
        {
            bool resultado = false;

            if (medicamento.IdMedicamento == 0)
            {
                resultado = await _serviceMedicamento.Insert(medicamento);
            }
            else
            {
                resultado = await _serviceMedicamento.Update(medicamento);
            }

            if (!resultado)
            {
                ViewData["mensaje"] = "Ocurrió un error en el guardado";
            }

            return RedirectToAction("Index", "Medicamento");
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerMedicamento(int id)
        {
            var medicamento = await _serviceMedicamento.GetOne(id);
            return Json(medicamento);
        }
        [HttpPost]
        public async Task<IActionResult> EliminarMedicamento(int id)
        {
            var resultado = await _serviceMedicamento.Delete(id);

            return Json(resultado);
        }
    }
}
