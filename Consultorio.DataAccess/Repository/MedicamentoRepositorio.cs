using Consultorio.DataAccess.Configuration;
using Consultorio.Entity;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Net;

namespace Consultorio.DataAccess.Repository
{
    public class MedicamentoRepositorio : IMedicamentoRepository
    {
        private readonly ConfigurationConnection _conecction;

        public MedicamentoRepositorio(IOptions<ConfigurationConnection> conecction)
        {
            _conecction = conecction.Value;
        }
        public async Task<IEnumerable<Medicamento>> GetAll()
        {
            IEnumerable<Medicamento> listaMedicamentos = null;
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM obtener_medicamentos()";
                    listaMedicamentos = await conn.QueryAsync<Medicamento>(sql);
                }

                return listaMedicamentos;
            }
            catch
            {
                return listaMedicamentos;
            }
        }
        public async Task<Medicamento> GetOne(int idMedicamento)
        {
            Medicamento medicamento = null;
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@p_id_medicamento", idMedicamento);

                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM obtener_medicamento(@p_id_medicamento)";
                    medicamento = await conn.QuerySingleOrDefaultAsync<Medicamento>(sql, parametros);
                }

                return medicamento;
            }
            catch
            {
                return medicamento;
            }
        }          
        public async Task<bool> Insert(Medicamento medicamento)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new
                    {
                        p_nombre = medicamento.Nombre,
                        p_categoria = medicamento.Categoria,
                        p_descripcion = medicamento.Descripcion
                    };

                    await conn.ExecuteAsync("crear_medicamento", parametros, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Update(Medicamento medicamento)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new
                    {
                        p_id_medicamento = medicamento.IdMedicamento,
                        p_nombre = medicamento.Nombre,
                        p_categoria = medicamento.Categoria,
                        p_descripcion = medicamento.Descripcion
                    };

                    await conn.ExecuteAsync("actualizar_medicamento", parametros, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(int idMedicamento)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new
                    {
                        p_id_medicamento = idMedicamento
                    };

                    await conn.ExecuteAsync("eliminar_medicamento", parametros, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
