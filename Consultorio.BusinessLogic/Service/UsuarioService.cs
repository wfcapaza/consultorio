using Consultorio.DataAccess.Repository;
using Consultorio.Entity;

namespace Consultorio.BusinessLogic.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Usuario> GetOne(string dni, string correo)
        {
            return await _repository.GetOne(dni, correo);
        }
        public async Task<bool> Insert(Usuario model)
        {
            model.FechaRegistro = DateTime.Now;
            return await _repository.Insert(model);
        }
        public async Task<bool> Update(Usuario model)
        {
            return await _repository.Update(model);
        }
        public async Task<Usuario> ValidarLogin(string correo, string contrasenha)
        {
            return await _repository.ValidarUsuario(correo, contrasenha);
        }
    }
}
