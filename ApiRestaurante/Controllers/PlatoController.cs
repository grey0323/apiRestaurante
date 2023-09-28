using ApiRestaurante.Dtos;
using ApiRestaurante.Methods;
using ApiRestaurante.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoController : ControllerBase
    {
        private readonly RestaurantContext _context;
        public PlatoController(RestaurantContext context) { _context = context; }
        // GET: api/<PlatoController>
        [HttpGet]
        public ActionResult<List<PlatosDtos>> List()
        {
            MetodosPlatos metodos = new MetodosPlatos(_context);
            return metodos.Get();
        }

        // GET api/<PlatoController>/5
        [HttpGet("{id}")]
        public PlatosDtos GetById(int id)
        {

            MetodosPlatos metodos = new MetodosPlatos(_context);
            return metodos.getid(id);

        }

        // POST api/<PlatoController>
        [HttpPost]
        public void Create(PlatosDtos plato)
        {
           MetodosPlatos platos = new MetodosPlatos(_context);
           platos.Post(plato);

        }

        // PUT api/<PlatoController>/5
        [HttpPut]
        public void Put(PlatosDtos plato)
        {
            MetodosPlatos platos = new MetodosPlatos(_context);
            platos.Put(plato);
        }

        // DELETE api/<PlatoController>/5
        
    }
}
