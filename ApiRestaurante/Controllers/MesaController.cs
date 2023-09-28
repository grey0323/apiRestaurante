using ApiRestaurante.Dtos;
using ApiRestaurante.Methods;
using ApiRestaurante.Models;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly RestaurantContext _context;
        public MesaController(RestaurantContext context) { _context = context; }

        // GET: api/<MesaController>
        [HttpGet]
        public ActionResult<List<MesaDto>> List()
        {
            MetodosMesas mesas = new MetodosMesas(_context);
            return mesas.Get();
        }

        // GET api/<MesaController>/
        [HttpGet("{id}")]
        public ActionResult<MesaDto> GetById(int? id)
        {
            MetodosMesas mesas = new MetodosMesas(_context);
            return mesas.Getbyid(id);

        }

        // POST api/<MesaController>
        [HttpPost]
        public void Create(int cantidadPer, string Descripcion)
        {
            MetodosMesas mesas = new MetodosMesas(_context);
            mesas.Post(cantidadPer, Descripcion);

        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public void Update(int id, string? Descripcion, int? cantPer)
        {
            MetodosMesas mesas = new MetodosMesas(_context);
            mesas.Put(id, Descripcion, cantPer);


        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeStatus(int id, int codigoE)
        {
            if (id > 0 && codigoE > 0)
            {
                var Mesa = _context.Mesas.Find(id);

                if (Mesa != null)
                {
                    Mesa.IdEstado = codigoE;

                    _context.Mesas.Update(Mesa);
                    await _context.SaveChangesAsync();
                }
            }

            return NoContent();

        }

        [HttpGet("[action]/{id}")]
        public ActionResult<List<OrdenesDtosGet>> GetTableOrde(int id)
        {
            
            var a = new List<OrdenesDtosGet>();
            var prb = _context.Ordenes.ToList().Where(x => x.MesaPertenece == id).ToList();

            if(prb!= null)
            {
                foreach(var p in prb)
                {
                    
                    var idsplatos = _context.OrdenPlatos.ToList().Where(x => x.Idorden == p.Id).Select(x => x.Idplato);
                    var e = new OrdenesDtosGet();
                    foreach (var i in idsplatos)
                    {

                        var platos = _context.Platos.ToList().Where(x => x.Id == i).Select(x => x.Nombre);
                        e.Id = p.Id;
                        e.PlatoSeleccionados.AddRange(platos);
                        e.MesaPertenece = "" + id;
                        e.Subtotal = p.Subtotal;


                    }

                    a.Add(new OrdenesDtosGet
                    {
                        Id = e.Id,
                        PlatoSeleccionados = e.PlatoSeleccionados,
                        MesaPertenece = e.MesaPertenece,
                        Subtotal = e.Subtotal

                    });
                    


                }

                
                
            }
            

            return a ;
        }


        // DELETE api/<MesaController>/5

    }
}
