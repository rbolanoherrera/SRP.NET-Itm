using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SRP.Bad.Transversal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace SRP.Bad.Entities
{
    /// <summary>
    /// Autor: Rafael Bolaños
    /// Fecha de creación: 2026-02-22
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

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

        //code emell BASURA: constructor vacío sin implementación, se debe eliminar o implementar correctamente
        public User()
        {

        }

        public User(IConfiguration configuration)
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
                    string sQuery = string.Format(InventarioDatabaseQueries.Insert_User, obj.Name, obj.IdTipoDocumento,
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
                    string sQuery = InventarioDatabaseQueries.List_Users;

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

        
        /// <summary>
        /// TODO: Falta implementar el método Update para actualizar información del Usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Result<int> Update(User user)
        {
            return new Result<int>();
        }

        public Result<int> CearEmpleado(User user)
        {
            return new Result<int>();
        }

        public Result<int> CearProveedor(User user)
        {
            return new Result<int>();
        }

        public Result<int> CrearProducto(Producto prod)
        {
            Result<int> result = new Result<int>();
            try
            {
                using (IDbConnection conn = Connection)
                {
                    string sQuery = InventarioDatabaseQueries.Insert_Producto;

                    DynamicParameters dp = new DynamicParameters();

                    dp.Add("@Id", prod.Id, DbType.Int32, ParameterDirection.Output);

                    if (!string.IsNullOrEmpty(prod.Codigo))
                        dp.Add("@cod", prod.Codigo, DbType.String);

                    dp.Add("@Name", prod.Nombre, DbType.String);
                    dp.Add("@UserCreate", "aac7f554-1656-4e6e-bf3e-48b6090ef041", DbType.String);
                    dp.Add("@IdTipoProducto", prod.IdTipoProducto, DbType.Int32);
                    dp.Add("@ValorCompra", Convert.ToDouble(prod.ValorCompra.Replace(",", ""), CultureInfo.InvariantCulture), DbType.Double);
                    dp.Add("@ValorVenta", Convert.ToDouble(prod.ValorVenta.Replace(",", ""), CultureInfo.InvariantCulture), DbType.Double);
                    dp.Add("@ValIva", Convert.ToDecimal(prod.Iva), DbType.Int32);

                    if (prod.Imagen is null)
                        dp.Add("@Imagen", prod.Imagen, DbType.Binary);

                    dp.Add("@IdUniBase", prod.IdUnidadMedidaBase, DbType.Int32);
                    dp.Add("@IdUniMedCompra", prod.IdUnidadMedidaCompra, DbType.Int32);
                    dp.Add("@IdUniMedVenta", prod.IdUnidadMedidaVenta, DbType.Int32);
                    dp.Add("@CantEquiva", prod.CantEquivalente, DbType.Int32);

                    if (string.IsNullOrEmpty(prod.CodigoBarras))
                        dp.Add("@CodBarras", prod.CodigoBarras, DbType.String);

                    dp.Add("@cantMinimaAlert", 0, DbType.Int32);
                    dp.Add("@cantStock", 200, DbType.Int32);

                    if (prod.ProveedorId != 0)
                        dp.Add("@ProveedorId", prod.ProveedorId, DbType.Int32);

                    dp.Add("@UnidadesMedidas", string.Empty, DbType.String);
                    dp.Add("@fechaVencimiento", FuncionesGenerales.HoraColombia().AddDays(30), DbType.DateTime);
                    dp.Add("@fecha", FuncionesGenerales.HoraColombia(), DbType.DateTime);

                    if (prod.Estado != 0)
                        dp.Add("@EstadoId", prod.Estado, DbType.Int32);

                    conn.Open();
                    conn.Execute(sql: sQuery, param: dp, commandType: CommandType.StoredProcedure);

                    int id = dp.Get<int>("@Id");

                    if (id > 0)
                    {
                        result.Data = id;
                        result.StatusCode = System.Net.HttpStatusCode.OK;
                        result.Message = "Producto registrado exitosamente!";
                    }
                    else
                    {
                        result.Data = id;
                        result.StatusCode = System.Net.HttpStatusCode.OK;
                        result.Message = "No se pudo registrar el Producto!";
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //log.Error("Error en metodo CrearProducto", ex);

                result.Data = -1;
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'CU_Producto_Codigo'"))
                    result.Message = "Ya existe un Producto con ese Codigo";
                else if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'CU_Producto_Nombre'"))
                    result.Message = "Ya existe un Producto con ese Nombre";
                else
                    result.Message = "Error desconocido al crear el Producto";
            }

            return result;
        }

    }
}