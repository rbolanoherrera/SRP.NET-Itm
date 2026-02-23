using Microsoft.AspNetCore.Mvc;
using SOLID.BLogic;
using SOLID.Entities;

namespace SRP.Good.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUserBLogic userLogic;

        public UserController(IConfiguration configuration, IUserBLogic userBLogic)
        {
            this.configuration = configuration;
            this.userLogic = userBLogic;
        }

        // POST: UserController/Create
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create([FromBody] User collection)
        {
            try
            {
                //return RedirectToAction(nameof(Index));
                return Ok(userLogic.Create(collection));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userLogic.GetAllUsers());
        }

    }
}
