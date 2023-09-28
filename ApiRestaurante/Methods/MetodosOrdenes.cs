using ApiRestaurante.Dtos;
using ApiRestaurante.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurante.Methods
{
    public class MetodosOrdenes
    {

        public readonly RestaurantContext _context;
        public MetodosOrdenes(RestaurantContext context) { _context = context; }

        public void Post(int? idMesa, List<int>? Platos)
        {
            if (idMesa != null && Platos.Count > 0)
            {
                var platos = _context.Platos.ToList();
                double subtotal = 0.0;

                foreach (var i in Platos)
                {
                    subtotal += (double)platos.FindAll(x => x.Id == i).Select(x => x.Precio).Sum();


                }
                _context.Ordenes.Add(new Ordene { MesaPertenece = idMesa, Estado = false, Subtotal = subtotal });
                _context.SaveChanges();

                var orden = _context.Ordenes.OrderBy(x => x.Id).LastOrDefault();

                foreach (var i in Platos)
                {
                    _context.OrdenPlatos.Add(new OrdenPlato { Idorden = orden.Id, Idplato = i });
                    _context.SaveChanges();
                }
            }


        }
        public ActionResult<List<OrdenesDtosGet>> Get()
        {
            var Ordenes = _context.Ordenes.ToList();
            var Platos = _context.OrdenPlatos.ToList();
            List<OrdenesDtosGet> dtos = new List<OrdenesDtosGet>();
            List<string> plto = new List<string>();

            foreach (var orden in Ordenes)
            {
                var Mesapertenece = _context.Mesas.FirstOrDefault(x => x.Id == orden.MesaPertenece).Descripcion;
                var Platofound = Platos.FindAll(x => x.Idorden == orden.Id).Select(x => x.Idplato);

                plto.Clear();

                foreach (var pl in Platofound)
                {
                    plto.Add(_context.Platos.FirstOrDefault(x => x.Id == pl).Nombre);
                }

                var OrdenFinal = new OrdenesDtosGet();
                OrdenFinal.PlatoSeleccionados.AddRange(plto);
                OrdenFinal.Subtotal = orden.Subtotal;
                OrdenFinal.Id = orden.Id;

                if (orden.Estado == true)
                {
                    OrdenFinal.Estado = "Procesada";
                }
                else
                {
                    OrdenFinal.Estado = "En Proceso";
                }

                dtos.Add(new OrdenesDtosGet
                {


                    PlatoSeleccionados = OrdenFinal.PlatoSeleccionados,
                    Id = OrdenFinal.Id,
                    Subtotal = OrdenFinal.Subtotal,
                    Estado = OrdenFinal.Estado,
                    MesaPertenece = Mesapertenece



                });

            }


            return dtos;

        }
        public OrdenesDtosGet GetbId(int id)
        {
            var e = new OrdenesDtosGet();

            var ordenes = _context.Ordenes.FirstOrDefault(x => x.Id == id);
            var idplatos = _context.OrdenPlatos.ToList().Where(x => x.Idorden == id).Select(x => x.Idplato);
            var mesapertences = _context.Mesas.FirstOrDefault(x => x.Id == ordenes.MesaPertenece).Descripcion;
            foreach (var i in idplatos)
            {
                e.PlatoSeleccionados.Add(_context.Platos.FirstOrDefault(x => x.Id == i).Nombre);
            }

            e.MesaPertenece = mesapertences;
            e.Subtotal = ordenes.Subtotal;
            e.Id = ordenes.Id;

            if (ordenes.Estado == true)
            {
                e.Estado = "Completada";

            }
            else
            {
                e.Estado = "En proceso";
            }




            return e;
        }

        public void borrar(int? id)
        {
            if (id != null)
            {
                var ordenAeliminar = _context.Ordenes.FirstOrDefault(x => x.Id == id);
                if (ordenAeliminar != null)
                {
                    var ordenAeliminar_ = _context.OrdenPlatos.ToList();

                    foreach (var o in ordenAeliminar_)
                    {
                        if (o.Idorden == id)
                        {
                            _context.OrdenPlatos.Remove(o);
                            _context.SaveChanges();

                        }
                    }

                    _context.Ordenes.Remove(ordenAeliminar);
                    _context.SaveChanges();





                }
            }
            }

        public void put(List<int> id, int idOrden)
        {
            var platos = new List<int>();
            foreach (int idPlato in id)
            {
                var iddeplato = _context.Platos.FirstOrDefault(x => x.Id == idPlato);

                if (iddeplato != null)
                {
                    var platoAremover = _context.OrdenPlatos.FirstOrDefault(x => x.Idplato == iddeplato.Id && x.Idorden == idOrden);
                    if (platoAremover != null)
                    {
                        _context.OrdenPlatos.Remove(platoAremover);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.OrdenPlatos.Add(new OrdenPlato { Idorden = idOrden, Idplato = iddeplato.Id });
                        _context.SaveChanges();
                    }

                }



            }
        }
    }
}
