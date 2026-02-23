using Microsoft.AspNetCore.Mvc;
using SOLID.BLogic;
using SOLID.Entities;

namespace SRP_Bad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private readonly IProductoBLogic prodBLogic;

        public ProductoController(IProductoBLogic productoBLogic)
        {
            this.prodBLogic = productoBLogic;
        }

        [HttpPost()]
        public IActionResult Create([FromBody] Producto collection)
        {
            try
            {
                return Ok(prodBLogic.Create(collection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
