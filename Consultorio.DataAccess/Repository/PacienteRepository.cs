using Consultorio.DataAccess.Configuration;
using Consultorio.Entity;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.DataAccess.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ConfigurationConnection _conecction;

        public PacienteRepository(IOptions<ConfigurationConnection> conecction)
        {
            _conecction = conecction.Value;
        }
        public async Task<IEnumerable<Paciente>> GetAll(string dniUsuario)
        {
            IEnumerable<Paciente> pacientes = null;
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@p_dni_usuario", dniUsuario);

                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM obtener_pacientes(@p_dni_usuario)";
                    pacientes = await conn.QueryAsync<Paciente>(sql, parametros);
                }

                return pacientes;
            }
            catch
            {
                return pacientes;
            }
        }
        public async Task<Paciente> GetOne(string dni, string dniUsuario)
        {
            var paciente = new Paciente();
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@p_dni", dni);
                parametros.Add("@p_dni_usuario", dniUsuario);

                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    string sql = "SELECT * FROM obtener_paciente(@p_dni, @p_dni_usuario)";
                    paciente = await conn.QueryFirstOrDefaultAsync<Paciente>(sql, parametros);
                }

                return paciente;
            }
            catch
            {
                return paciente;
            }
        }               
        public async Task<bool> Insert(Paciente paciente)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new
                    {
                        p_dni = paciente.DNI,
                        p_nombre = paciente.Nombre,
                        p_apellido_paterno = paciente.ApellidoPaterno,
                        p_apellido_materno = paciente.ApellidoMaterno,
                        p_correo = paciente.Correo,
                        p_fecha_nacimiento = paciente.FechaNacimiento,
                        p_fecha_registro = paciente.FechaRegistro,
                        p_dni_usuario = paciente.DniUsuario
                    };

                    await conn.ExecuteAsync("crear_paciente", parametros, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Paciente paciente)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_conecction.CadenaSQL))
                {
                    var parametros = new
                    {
                        p_dni = paciente.DNI,
                        p_nombre = paciente.Nombre,
                        p_apellido_paterno = paciente.ApellidoPaterno,
                        p_apellido_materno = paciente.ApellidoMaterno,
                        p_correo = paciente.Correo,
                        p_fecha_nacimiento = paciente.FechaNacimiento,
                        p_dni_usuario = paciente.DniUsuario
                    };

                    await conn.ExecuteAsync("actualizar_paciente", parametros, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public Task<bool> Delete(string dni)
        {
            throw new NotImplementedException();
        }

    }
}
