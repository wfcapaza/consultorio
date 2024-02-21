using Consultorio.Entity;

namespace Consultorio.DataAccess.Repository
{
    public interface IUsuarioRepository
    {
        Task<bool> Insert(Usuario model);
        Task<bool> Update(Usuario model);
        Task<bool> Delete(int id);
        Task<Usuario> GetOne(string dni, string correo);
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> ValidarUsuario(string correo, string contrasenha);
    }
}
