using Consultorio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Consultorio.BusinessLogic.Service
{
    public interface IUsuarioService
    {
        Task<bool> Insert(Usuario model);
        Task<bool> Update(Usuario model);
        Task<bool> Delete(int id);
        Task<Usuario> GetOne(string dni, string correo);
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> ValidarLogin(string correo, string contrasenha);
    }
}
