using ApiRestaurante.Dtos;
using ApiRestaurante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiRestaurante.Methods
{
    public class MetodosMesas
    {

        private readonly RestaurantContext _context;
        public MetodosMesas( RestaurantContext context) { _context = context; }

        public void Post(int cantidadPer, string Descripcion)
        {
            if (!Descripcion.IsNullOrEmpty() && cantidadPer > 0)
            {
                _context.Mesas.Add(new Mesa { CantidadPerson = cantidadPer, Descripcion = Descripcion, IdEstado = 1});
                _context.SaveChanges();
            }
        }

        public void Put(int id, string? Descripcion, int? cantPer)
        {
            var mesassaved = _context.Mesas.ToList();
            if (id > 0)
            {
                
                foreach (var mesa in mesassaved)
                {
                    if(mesa.Id == id)
                    {
                        if (!Descripcion.IsNullOrEmpty())
                        {

                            _context.Mesas.Update(new Mesa{ Id = id, Descripcion = Descripcion, IdEstado = mesa.IdEstado, CantidadPerson = mesa.CantidadPerson});
                            _context.SaveChanges();

                        }
                        else if (cantPer > 0)
                        {
                            _context.Mesas.Update(new Mesa { Id = id, CantidadPerson = cantPer, IdEstado = mesa.IdEstado, Descripcion = mesa.Descripcion });
                            _context.SaveChanges();

                        }

                    }
                    

                }
               

            }

        }

        public ActionResult<List<MesaDto>> Get()
        {
            
            List<MesaDto> listademesas = new List<MesaDto>();
            var mesas = _context.Mesas.ToList();

            foreach (var ms in mesas)
            {
                int dt = _context.TblEstados.FirstOrDefault(x => x.Id == ms.IdEstado).Id;
                listademesas.Add(new MesaDto { Id = ms.Id, CantidadPerson = ms.CantidadPerson, Descripcion = ms.Descripcion, IdEstado = dt });
            }

            return listademesas;

        }

        public ActionResult<MesaDto> Getbyid(int? id)
        {
       
            var mesadt = new MesaDto();
            if (id>0)
            {
                var mesafound = _context.Mesas.FirstOrDefault(x => x.Id == id);
                if (mesafound != null)
                {
                    var dt = _context.TblEstados.FirstOrDefault(x => x.Id == mesafound.IdEstado).Id;
                    mesadt = new MesaDto { Id = mesafound.Id, CantidadPerson = mesafound.CantidadPerson, Descripcion = mesafound.Descripcion, IdEstado = dt };
                }

               

            }
            return mesadt;

        }

        public ActionResult<List<string>> GetTa(int? id)
        {
            var ms = new List<string>();
            if(id!= null)
            {
                ms.Add( _context.Mesas.ToList().Where(x=> x.Id == id && x.IdEstado == 2).ToString() );
                
            }

            return ms;
        }
    }
}
