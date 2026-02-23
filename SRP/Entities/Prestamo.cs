using System;

namespace SRP.Bad.Entities
{
    public class Prestamo
    {

        public int Id  {get;set;}
        public int ClienteId  {get;set;}
        public int VentaId  {get;set;}
        public string CreateUserId  {get;set;}
        public string UpdateUserId  {get;set;}
        public DateTime Fecha  {get;set;}
        public int EstadoId  {get;set;}
        public string Observacion  {get;set;}
        public decimal Valor  {get;set;}
        public decimal TotalAbonado { get; set; }


        public Result<int> Create(User obj)
        {
            return new Result<int>();
        }

    }
}
