using System.Collections.ObjectModel;

namespace SOLID.Transversal
{
    public static class FuncionesGenerales
    {
        /// <summary>
        /// Hora de Colombia tomando como ciudad Bogota
        /// </summary>
        /// <returns></returns>
        public static DateTime HoraColombia()
        {
            //Cambiar formato de fecha a la hora de Colombia
            DateTime localNow = DateTime.Now;

            ReadOnlyCollection<TimeZoneInfo> timeZoneInfoCollectio = TimeZoneInfo.GetSystemTimeZones();
            TimeZoneInfo colDate = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");//(UTC-05:00) Bogotá, Lima, Quito, Rio Branco");

            DateTime fechaCol = TimeZoneInfo.ConvertTimeFromUtc(localNow.ToUniversalTime(), colDate);

            return fechaCol;
        }
    }
}