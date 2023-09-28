using ApiRestaurante.Dtos;
using ApiRestaurante.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiRestaurante.Methods
{
    public class MetodosIngredientes
    {
        private readonly RestaurantContext _context;
        public MetodosIngredientes(RestaurantContext context) { _context = context; }   

        public List<IngredienteDtos> GetIngredientes()
        {
            var listIngredientes = _context.Ingredientes.ToList();
            List<IngredienteDtos> dt = new List<IngredienteDtos>();

            foreach(var ingrediente in listIngredientes)
            {
                dt.Add(new IngredienteDtos
                {
                    Id = ingrediente.Id,
                    Nombre = ingrediente.Nombre,

                });
            }

            return dt;
        }

        public IngredienteDtos GetIngredienteById(int? id)
        {
            var ingrediente = _context.Ingredientes.FirstOrDefault(x => x.Id == id);

            

            var dt = new IngredienteDtos { Id = ingrediente.Id, Nombre = ingrediente.Nombre};

            

            return dt;

        }

        public void PostIngredientes(string ingrediente)
        {
            if(!ingrediente.IsNullOrEmpty())
            {
               _context.Ingredientes.Add(new Ingrediente { Nombre = ingrediente });
                _context.SaveChanges();

            }
            
        }

        public void PutIngredientes(string nombre, int  id)
        {
            if(id != null)
            {
                var dt = new Ingrediente { Nombre = nombre, Id = id };
                _context.Ingredientes.Update(dt);
                _context.SaveChanges();

            }

        }

        

    }
}
