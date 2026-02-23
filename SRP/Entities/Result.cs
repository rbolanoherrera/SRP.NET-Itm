using System.Net;

namespace SRP.Bad.Entities
{
    public partial class Result<T>
    {
        /// <summary>
        /// Status Code of a logic process.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        /// <summary>
        /// Message of a logic process.
        /// </summary>
        public string Message { get; set; }

        public string JWTToken { get; set; }

        /// <summary>
        /// Data of a logic process.
        /// </summary>
        public T Data { get; set; }
    }
}