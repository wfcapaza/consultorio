using Consultorio.DataAccess.Repository;
using Consultorio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.BusinessLogic.Service
{
    public class AtencionService : IAtencionService
    {
        private readonly IAtencionRepository _repositoryAtencion;

        public AtencionService(IAtencionRepository repositoryAtencion)
        {
            _repositoryAtencion = repositoryAtencion;
        }
        public async Task<IEnumerable<Atencion>> GetAll()
        {
            return await _repositoryAtencion.GetAll();
        }

        public async Task<Atencion> GetOne(int idAtencion)
        {
            var atencion = await _repositoryAtencion.GetOne(idAtencion);
            atencion.Medicamentos = await _repositoryAtencion.GetMedicamentosPorAtencion(idAtencion);

            return atencion;
        }

        public async Task<int> Insert(string dniPaciente, DateTime? fechaAtencion)
        {
            return await _repositoryAtencion.Insert(dniPaciente, fechaAtencion);
        }
        public async Task<bool> InsertDiagnostico(DiagnosticoEntity diagnostico)
        {
            return await _repositoryAtencion.InsertDiagnostico(diagnostico);
        }
        public async Task<bool> InsertTratamiento(Tratamiento tratamiento)
        {
            return await _repositoryAtencion.InsertTratamiento(tratamiento);
        }
        public async Task<bool> InsertMedicamento(MedicamentoAtencion medicamento)
        {
            return await _repositoryAtencion.InsertMedicamento(medicamento);
        }
    }
}
