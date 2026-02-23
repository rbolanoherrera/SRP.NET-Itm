using System.Collections.Generic;

namespace SRP.Bad.Entities
{
    /// <summary>
    /// Esta clase tiene baja cohesión porque mezcla lógica de negocio, presentación y persistencia. 
    /// Tiene al menos tres razones de cambio: si cambia la lógica salarial, si cambia el formato del reporte, o si cambia la base de datos.
    /// </summary>
    public class Empleado
    {
        public int Id { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }



        public Result<int> Create(User obj)
        {
            return new Result<int>();

        }

        public Result<int> Prestamo(Prestamo obj)
        {
            return new Result<int>();

        }

        public Result<decimal> CalcularSalario(Empleado obj)
        {
            return new Result<decimal>();
        }

        public Result<IEnumerable<string>> GenerarReportesEnPDF(IEnumerable<Empleado> obj)
        {
            return new Result<IEnumerable<string>>();
        }

    }
}