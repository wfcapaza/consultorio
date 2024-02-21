using Consultorio.DataAccess.Repository;
using Consultorio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.BusinessLogic.Service
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }
        public async Task<IEnumerable<Paciente>> GetAll(string dniUsuario)
        {
            return await _pacienteRepository.GetAll(dniUsuario);
        }
        public async Task<Paciente> GetOne(string dni, string dniUsuario)
        {
            return await _pacienteRepository.GetOne(dni, dniUsuario);
        }                
        public async Task<Respuesta> SavePaciente(Paciente paciente)
        {
            var respuesta = new Respuesta();
            var pacienteEncontrado = await _pacienteRepository.GetOne(paciente.DNI, paciente.DniUsuario);

            if(pacienteEncontrado == null)
            {
                respuesta.Resultado = await _pacienteRepository.Insert(paciente);
                return respuesta;
            }

            respuesta.Resultado = await _pacienteRepository.Update(paciente);
            return respuesta;
        }

        public Task<bool> Update(Paciente usuario)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Delete(string dni)
        {
            throw new NotImplementedException();
        }
    }
}
