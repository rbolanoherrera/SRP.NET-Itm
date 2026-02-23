namespace SRP.Good
{
    public static class InventarioDatabaseQueries
    {
        public const string Insert_User = "INSERT INTO Users (Name, IdTipoDocumento, Documento, Nombres, Apellidos, Email, Password) VALUES ('{0}', '{1}', {2}, '{3}', '{4}', '{5}', '{6}'); SELECT CAST(SCOPE_IDENTITY() as int);";
    }
}