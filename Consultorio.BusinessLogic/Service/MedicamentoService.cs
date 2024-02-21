using Consultorio.DataAccess.Repository;
using Consultorio.Entity;

namespace Consultorio.BusinessLogic.Service
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IMedicamentoRepository _repository;

        public MedicamentoService(IMedicamentoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Medicamento>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Medicamento> GetOne(int idMedicamento)
        {
            return await _repository.GetOne(idMedicamento);
        }               
        public async Task<bool> Insert(Medicamento medicamento)
        {
            return await _repository.Insert(medicamento);
        }

        public async Task<bool> Update(Medicamento medicamento)
        {
            return await _repository.Update(medicamento);
        }
        public async Task<bool> Delete(int idMedicamento)
        {
            return await _repository.Delete(idMedicamento);
        }
    }
}
