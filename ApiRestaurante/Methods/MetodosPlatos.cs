using ApiRestaurante.Dtos;
using ApiRestaurante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiRestaurante.Methods
{
    public class MetodosPlatos
    {
        private readonly RestaurantContext _context;
        public MetodosPlatos(RestaurantContext context) { _context = context; }

        public void Post( PlatosDtos plato)
        {
            bool added = false;
            if (plato != null)
            {
                //Obtener lista de Platos
                var platos = _context.Platos.ToList();
                //Optener contedo de cantidad de platos
                var platosnumero = _context.Platos.Count();
                int cont = 0;
                //Foreach para iterar los platos
                foreach (var item in platos)
                {
                    cont++;
                    //Si los nombres de los platos guardados y el nombre del nuevo plato a agregar son diferentes...
                    if (item.Nombre != plato.Nombre)
                    {
                        //Si el contado y el numero del plato son lo mismo quiere decir que esta es la ultima vuelta del bucle
                        if (cont == platosnumero)
                        {
                            var dt = new Plato
                            {
                                Categoria = plato.Categoria,
                                NumeroPersonas = plato.NumeroPersonas,
                                Precio = plato.Precio,
                                Nombre = plato.Nombre,


                            };
                            _context.Platos.Add(dt);
                            _context.SaveChanges();
                            added = true;
                            cont = 0;



                        }


                    }
                    else
                    {
                        //Si son iguales se rompera el foreach
                        break;
                        cont = 0;
                    }

                }

            }

            if (added == true)
            {
                //Optenenos el numero de ingredientes que tenemos guardados
                var ingredientescontados = _context.Ingredientes.Count();
                //Optenemos todos los ingredientes
                var ingredientes = _context.Ingredientes.ToList();
                //Un contador para contar la cantidad de vueltas de un foreach
                int contador = 0;
                foreach (var ingrediente in plato.ingredientes) //== 2 ingredientes 
                {

                    foreach (var item in ingredientes) //== 5 ingredientes guardados
                    {
                        contador++; // == 5
                                    //Si los nombres son diferentes entrarar a la siguiente condicional
                        if (ingrediente != item.Nombre)
                        {
                            //Para verificar cual es la ultima vuelta del foreach el contador tiene que ser igual a los ingredientes contados
                            if (contador == ingredientescontados)
                            {
                                _context.Ingredientes.Add(new Ingrediente { Nombre = ingrediente });
                                _context.SaveChanges();
                                contador = 0;
                            }
                        }
                        else
                        {
                            contador = 0;
                            break;
                        }

                    }





                }
            }




            var idplato = _context.Platos.FirstOrDefault(x => x.Nombre == plato.Nombre).Id;

            if (idplato != null)
            {
                foreach (var ingrediente in plato.ingredientes)
                {
                    var idingre = _context.Ingredientes.FirstOrDefault(x => x.Nombre == ingrediente).Id;
                    _context.IngredientePlatos.Add(new IngredientePlato { Idplato = idplato, Idingrediente = idingre });
                    _context.SaveChanges();


                }
            }

        }

        //En este metodo put nos enfocamos en realizar una logica que cosiste en que si los ingredientes
        //enviados por el usuario ya se encuentran la base de datos quiere decir que el usuario quiere eliminar este ingrediente
        // del plato, y si no se encuenta agregado quiere decir que lo quiere agregar
        public void Put(PlatosDtos plato)
        {
            if (plato.Id != null)
            {
                
                var platoupdate = new Plato
                {
                    Id = plato.Id,
                    Nombre = plato.Nombre,
                    Precio = plato.Precio,
                    NumeroPersonas = plato.NumeroPersonas,
                    Categoria = plato.Categoria

                };

                _context.Platos.Update(platoupdate);
                _context.SaveChanges();

                var ingredienteplato = _context.IngredientePlatos.ToList();

                //Foreach para recorrer todos los ingredientes obtenidos del usuario

                foreach (var ing in plato.ingredientes)
                {
                    bool opcion = false;
                    foreach (var ingpl in ingredienteplato)
                    {
                        //mediante el id de la conexion ingredienteplato obtenemos los nombres de los ingredientes
                        //Para luego comparar para saber si el ingrediente ya se encuentra agregado
                        var ingredi = _context.Ingredientes.FirstOrDefault(x => x.Id == ingpl.Idingrediente).Nombre;

                        if (ing == ingredi)
                        {
                            //Si se encuentra agregado quiere decir que queremos eliminar este ingrediente del plato
                            var ig = _context.IngredientePlatos.FirstOrDefault(x => x.Idingrediente == ingpl.Idingrediente);
                            _context.IngredientePlatos.Remove(ig);
                            _context.SaveChanges();
                            //Este se interpreta como una bandera que indica que el ingrediente fue eliminado
                            opcion = true;
                        }
                    }

                    //Si opcion es falso significa que el ingrediente no esta agregado y es necesario agregarlo
                    if (opcion == false)
                    {
                        _context.Ingredientes.Add(new Ingrediente { Nombre = ing });
                        _context.SaveChanges();

                        var idplato = _context.Ingredientes.FirstOrDefault(x => x.Nombre == ing).Id;

                        _context.IngredientePlatos.Add(new IngredientePlato { Idingrediente = idplato, Idplato = plato.Id });
                        _context.SaveChanges();
                    }


                }


            }
        }

        public ActionResult<List<PlatosDtos>> Get()
        {
            var Listplatos = _context.Platos.ToList();// Lista de platos
            var ingrePlato = _context.IngredientePlatos.ToList();//Lista de ingredientes con sus platos
            var ingrediente = _context.Ingredientes.ToList();// Lista de ingredientes

            List<PlatosDtos> pl = new List<PlatosDtos>();
            List<string> dl = new List<string>();

            var ed = new List<string>();

            foreach (var plato in Listplatos)
            {
                //Optenemos una lista de los id de los ingredientes
                var idde = ingrePlato.FindAll(x => x.Idplato == plato.Id).Select(x => x.Idingrediente);
                dl.Clear();

                //Recorremos todos los id de los ingredientes para buscar sus nombre y irlos agregando a una lista
                foreach (var i in idde)
                {
                    dl.Add(ingrediente.FirstOrDefault(x => x.Id == i).Nombre);
                }

                //Creamos un platodto para ir agregando los datos y luego pasarlos PL
                var newPlat = new PlatosDtos();
                newPlat.Nombre = plato.Nombre;
                newPlat.Id = plato.Id;

                //USamos addrange para que por cada ingredientes que haya en la lista dl
                //Se haga un add
                newPlat.ingredientes.AddRange(dl);
                newPlat.Precio = plato.Precio;
                newPlat.NumeroPersonas = plato.NumeroPersonas;
                newPlat.Categoria = plato.Categoria;

                pl.Add(newPlat);

            }

            return pl;
        }

        public PlatosDtos getid(int? id)
        {
            var e = new PlatosDtos();
            var Plato = _context.Platos.FirstOrDefault(x => x.Id == id);

            var idingredientes = _context.IngredientePlatos.ToList().Where(x => x.Idplato == Plato.Id).Select(x => x.Idingrediente);

            foreach (var ingrediente in idingredientes)
            {
                e.ingredientes.Add(_context.Ingredientes.FirstOrDefault(x => x.Id == ingrediente).Nombre);

            }

            e.Nombre = Plato.Nombre;
            e.Precio = Plato.Precio;
            e.Categoria = Plato.Categoria;
            e.NumeroPersonas = Plato.NumeroPersonas;
            e.Id = Plato.Id;



            return e;
        }

        
    }
}
