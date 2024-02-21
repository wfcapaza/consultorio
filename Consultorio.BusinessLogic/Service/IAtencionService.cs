using Consultorio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.BusinessLogic.Service
{
    public interface IAtencionService
    {
        Task<int> Insert(string dniPaciente, DateTime? fechaAtencion);
        Task<bool> InsertDiagnostico(DiagnosticoEntity diagnostico);
        Task<bool> InsertTratamiento(Tratamiento tratamiento);
        Task<bool> InsertMedicamento(MedicamentoAtencion medicamento);
        Task<Atencion> GetOne(int idAtencion);
        Task<IEnumerable<Atencion>> GetAll();
    }
}
