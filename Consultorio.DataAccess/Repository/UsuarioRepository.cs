using Consultorio.DataAccess.Configuration;
using Consultorio.Entity;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.PostgresTypes;
using System.Data;
using System.Net;

namespace Consultorio.DataAccess.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConfigurationConnection _conecction;

        public UsuarioRepository(IOptions<ConfigurationConnection> conecction)
        {
            _conecction = conecction.Value;
            //Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            //_conecction = "Server=localhost;Port=5432;Database=Consultorio;User Id=postgres;Password=Fr@nc1sc0321;";
        }
        public async Task<IEnumerable<Usuario>> GetAll()
        {
            IEnumerable<Usuario> listaUsuarios = null;
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM obtener_lista_usuarios()";
                    listaUsuarios = await conn.QueryAsync<Usuario>(sql);
                }

                return listaUsuarios;
            }
            catch
            {
                return listaUsuarios;
            }
        }

        public async Task<Usuario> GetOne(string dni, string correo)
        {
            Usuario usuario = null;
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@p_dni", dni);
                parametros.Add("@p_correo", correo);

                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM obtener_usuario(@p_dni, @p_correo)";
                    usuario = await conn.QuerySingleOrDefaultAsync<Usuario>(sql, parametros);
                }

                return usuario;
            }
            catch
            {
                return usuario;
            }
        }            
        public async Task<bool> Insert(Usuario usuario)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new
                    { 
                        p_dni = usuario.DNI,
                        p_nombre = usuario.Nombre,
                        p_apellido_paterno = usuario.ApellidoPaterno,
                        p_apellido_materno = usuario.ApellidoMaterno,
                        p_correo = usuario.Correo,
                        p_contrasenha = usuario.Contrasenha,
                        p_fecha_nacimiento = usuario.FechaNacimiento,
                        p_fecha_registro = usuario.FechaRegistro
                    };
                    
                    await conn.ExecuteAsync("crear_usuario", parametros, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Update(Usuario model)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> ValidarUsuario(string correo, string contrasenha)
        {
            Usuario usuario = null;
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@p_correo", correo);
                parametros.Add("@p_contrasenha", contrasenha);

                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM validar_usuario(@p_correo, @p_contrasenha)";
                    usuario = await conn.QuerySingleOrDefaultAsync<Usuario>(sql, parametros);
                }

                return usuario;
            }
            catch
            {
                return usuario;
            }
        }
    }
}
