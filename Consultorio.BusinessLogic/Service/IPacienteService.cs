using Consultorio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.BusinessLogic.Service
{
    public interface IPacienteService
    {
        Task<IEnumerable<Paciente>> GetAll(string correo);
        Task<Paciente> GetOne(string dni, string dniUsuario);
        Task<Respuesta> SavePaciente(Paciente usuario);
        Task<bool> Update(Paciente usuario);
        Task<bool> Delete(string dni);
    }
}
