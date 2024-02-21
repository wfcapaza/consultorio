using Consultorio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.DataAccess.Repository
{
    public interface IMedicamentoRepository
    {
        Task<IEnumerable<Medicamento>> GetAll();
        Task<Medicamento> GetOne(int idMedicamento);
        Task<bool> Insert(Medicamento medicamento);
        Task<bool> Update(Medicamento medicamento);
        Task<bool> Delete(int idMedicamento);
    }
}
