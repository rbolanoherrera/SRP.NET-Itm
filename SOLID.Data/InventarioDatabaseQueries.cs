namespace SOLID.Data
{
    public static class InventarioDatabaseQueries
    {
        public const string Insert_User = "INSERT INTO Users (Name, IdTipoDocumento, Documento, Nombres, Apellidos, Email, Password) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}'); SELECT CAST(SCOPE_IDENTITY() as int);";
        public const string List_Users = "select Name, IdTipoDocumento, Documento, Nombres, Apellidos, Email, Password from Users order by name";
        public const string Insert_Producto = "SP_CREATE_PRODUCT";
    }
}