using Consultorio.Entity;

namespace Consultorio.DataAccess.Repository
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAll(string dniUsuario);
        Task<Paciente> GetOne(string dni, string dniUsuario);
        Task<bool> Insert(Paciente paciente);
        Task<bool> Update(Paciente paciente);
        Task<bool> Delete(string dni);                
    }
}
