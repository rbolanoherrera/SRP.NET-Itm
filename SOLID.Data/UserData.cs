using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SOLID.Entities;
using System.Data;

namespace SOLID.Data
{
    public class UserData : IUserData
    {
        private string sQuery = string.Empty;
        private string _connectionstring;

        private IDbConnection _connection;

        private IDbConnection Connection
        {
            get
            {
                if (_connection is null)
                    return new SqlConnection(_connectionstring);
                else
                    return _connection;
            }
        }

        public UserData(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("SqlInventario")!.ToString();
        }

        public Result<int> Create(User obj)
        {
            Result<int> result = new Result<int>();
            try
            {
                using (IDbConnection conn = Connection)
                {
                    sQuery = string.Format(InventarioDatabaseQueries.Insert_User, obj.Name, obj.IdTipoDocumento,
                        obj.Documento, obj.Nombres, obj.Apellidos, obj.Email, obj.Password);

                    conn.Open();
                    result.Data = conn.ExecuteScalar<int>(sQuery);
                    result.StatusCode = System.Net.HttpStatusCode.OK;
                    result.Message = "Usuario creado exitosamente!";
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                result.Data = 0;
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = "Error al crear un Usuario";
            }

            return result;
        }

        public Result<List<User>> GetAllUsers()
        {
            Result<List<User>> result = new Result<List<User>>();
            try
            {
                using (IDbConnection conn = Connection)
                {
                    sQuery = InventarioDatabaseQueries.List_Users;

                    conn.Open();
                    result.Data = conn.Query<User>(sQuery).ToList();
                    result.StatusCode = System.Net.HttpStatusCode.OK;
                    result.Message = "Usuarios Listados exitosamente!";
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                //result.Data = null;
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = "Error al listar los Usuarios";
            }

            return result;
        }

        public Result<int> Update(User user)
        {
            return new Result<int>();
        }
    }
}
