using ApiRestaurante.Dtos;
using ApiRestaurante.Methods;
using ApiRestaurante.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        public readonly RestaurantContext _context;
        public OrdenesController( RestaurantContext context) { _context = context; }

        // GET: api/<OrdenesController>
        [HttpGet]
        public ActionResult<List<OrdenesDtosGet>> Get()
        {
            MetodosOrdenes ordenes = new MetodosOrdenes(_context);
            return ordenes.Get();
        }

        // GET api/<OrdenesController>/5
        [HttpGet("{id}")]
        public OrdenesDtosGet GetById(int id)
        {
            MetodosOrdenes ordenes = new MetodosOrdenes(_context);
            return ordenes.GetbId(id);

        }

        // POST api/<OrdenesController>
        [HttpPost]
        public void Post(int? idMesa, List<int>? Platos)
        {
            MetodosOrdenes metodos = new MetodosOrdenes(_context);
            metodos.Post(idMesa, Platos);
            

        }

        // PUT api/<OrdenesController>/5
        [HttpPut]
        public void Put(List<int> id, int idOrden)
        {
            MetodosOrdenes ordenes = new MetodosOrdenes(_context);
            ordenes.put(id, idOrden);

        }

        // DELETE api/<OrdenesController>/5
        [HttpDelete("{id}")]
        public void Delete(int? id)
        {

            MetodosOrdenes ordenes = new MetodosOrdenes(_context);
            ordenes.borrar(id);

        }
    }
}
