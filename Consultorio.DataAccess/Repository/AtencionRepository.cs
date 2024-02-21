using Consultorio.DataAccess.Configuration;
using Consultorio.Entity;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace Consultorio.DataAccess.Repository
{
    public class AtencionRepository : IAtencionRepository
    {
        private readonly ConfigurationConnection _conecction;

        public AtencionRepository(IOptions<ConfigurationConnection> conecction)
        {
            _conecction = conecction.Value;
        }
        public async Task<IEnumerable<MedicamentoAtencion>> GetMedicamentosPorAtencion(int idAtencion)
        {
            IEnumerable<MedicamentoAtencion> medicamentos = null;
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@p_id_atencion", idAtencion);

                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM medicamentos_por_atencion(@p_id_atencion)";
                    medicamentos = await conn.QueryAsync<MedicamentoAtencion>(sql, parametros);
                }

                return medicamentos;
            }
            catch
            {
                return medicamentos;
            }
        }
        public async Task<IEnumerable<Atencion>> GetAll()
        {
            IEnumerable<Atencion> listaAtenciones = null;
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM obtener_atenciones()";
                    listaAtenciones = await conn.QueryAsync<Atencion>(sql);
                }

                return listaAtenciones;
            }
            catch
            {
                return listaAtenciones;
            }
        }

        public async Task<Atencion> GetOne(int idAtencion)
        {
            var atencion = new Atencion();
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@p_id_atencion", idAtencion);

                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM obtener_resumen_atencion(@p_id_atencion)";
                    atencion = await conn.QueryFirstOrDefaultAsync<Atencion>(sql, parametros);
                }

                return atencion;
            }
            catch
            {
                return atencion;
            }
        }

        public async Task<int> Insert(string dniPaciente, DateTime? fechaAtencion)
        {
            try
            {
                int idAtencion = 0;
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("p_fecha_atencion", fechaAtencion);
                    parametros.Add("p_dni_paciente", dniPaciente);
                    parametros.Add("p_id_atencion", DbType.Int32, direction: ParameterDirection.Output);

                    await conn.ExecuteAsync("crear_atencion", parametros, commandType: CommandType.StoredProcedure);

                    idAtencion = parametros.Get<int>("p_id_atencion");
                }

                return idAtencion;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<bool> InsertDiagnostico(DiagnosticoEntity diagnostico)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("p_diagnostico", diagnostico.Diagnostico);
                    parametros.Add("p_descripcion", diagnostico.Descripcion);
                    parametros.Add("p_id_atencion", diagnostico.IdAtencion);

                    await conn.ExecuteAsync("crear_diagnostico", parametros, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> InsertTratamiento(Tratamiento tratamiento)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("p_descripcion", tratamiento.Descripcion);
                    parametros.Add("p_id_atencion", tratamiento.IdAtencion);

                    await conn.ExecuteAsync("crear_tratamiento", parametros, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> InsertMedicamento(MedicamentoAtencion medicamento)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("p_id_atencion", medicamento.IdAtencion);
                    parametros.Add("p_id_medicamento", medicamento.IdMedicamento);
                    parametros.Add("p_cantidad", medicamento.Cantidad);


                    await conn.ExecuteAsync("crear_medicamento_atencion", parametros, commandType: CommandType.StoredProcedure);
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
