using ApiRestaurante.Dtos;
using ApiRestaurante.Methods;
using ApiRestaurante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesController : ControllerBase
    {
        private readonly RestaurantContext _context;
        public IngredientesController(RestaurantContext context) { _context = context; }
        // GET: api/<IngredientesController>
        [HttpGet]
        public ActionResult<List<IngredienteDtos>> List()
        {
            MetodosIngredientes metodos = new MetodosIngredientes(_context);
            return metodos.GetIngredientes();
            
        }

        // GET api/<IngredientesController>/5
        [HttpGet("{id}")]
        public ActionResult<IngredienteDtos> GetbyId(int id)
        {
            MetodosIngredientes metodos = new MetodosIngredientes(_context);    
            return metodos.GetIngredienteById(id);
        }

        // POST api/<IngredientesController>
        [HttpPost]
        public void Create(string nombre)
        {
            MetodosIngredientes metodos = new MetodosIngredientes(_context);
            if(!nombre.IsNullOrEmpty())
            {
                metodos.PostIngredientes(nombre);
            }
        }

        // PUT api/<IngredientesController>/5
        [HttpPut]
        public void Update(string nombre, int id)
        {
            MetodosIngredientes metodos = new MetodosIngredientes(_context);
            metodos.PutIngredientes(nombre, id);
        }

    }
}
