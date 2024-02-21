using Consultorio.Entity;

namespace Consultorio.BusinessLogic.Service
{
    public interface IMedicamentoService
    {
        Task<IEnumerable<Medicamento>> GetAll();
        Task<Medicamento> GetOne(int idMedicamento);
        Task<bool> Insert(Medicamento medicamento);
        Task<bool> Update(Medicamento medicamento);
        Task<bool> Delete(int idMedicamento);
    }
}
