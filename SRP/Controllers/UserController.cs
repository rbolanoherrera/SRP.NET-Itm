using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SRP.Bad.Entities;
using System;

namespace SRP.Bad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private User objUser = null;
        private readonly IConfiguration configuration;

        public UserController(IConfiguration configuration)
        {
            if(objUser is null)
                objUser = new User(configuration);

            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Create([FromBody] User collection)
        {
            try
            {
                //return RedirectToAction(nameof(Index));
                return Ok(objUser.Create(collection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(objUser.GetAllUsers());
        }

        //Code smell de INCONSISTENCIA DE NOMBRE, el método se llama Create pero el endpoint es prod y en
        //la clase User el metodo se llama Crear, lo que no tiene sentido
        [HttpPost("prod")]
        public IActionResult Create([FromBody] Producto prod)
        {
            try
            {
                return Ok(objUser.CrearProducto(prod));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
