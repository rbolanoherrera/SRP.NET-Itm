using System.Data;


namespace SOLID.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name {get;set;}
        public int IdTipoDocumento {get;set;}
        public string Documento {get;set;}
        public string Nombres {get;set;}
        public string Apellidos {get;set;}
        public string Email {get;set;}
        public string Password { get; set; }
    }
}