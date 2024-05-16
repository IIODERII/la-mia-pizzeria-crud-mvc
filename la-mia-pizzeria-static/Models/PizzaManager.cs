using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Models
{
    public static class PizzaManager
    {
        public static List<Pizza> GetAllPizzas()
        {
            List<Pizza> pizze = new List<Pizza>();
            using (PizzaContext db = new PizzaContext())
            {
                pizze = db.Pizzas.ToList();
            }
            return pizze;
        }
        public static Pizza GetPizzaById(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza p = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                return p;
            }
        }
        public static void AddPizza(Pizza pizza)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza p = new Pizza();
                p.Id = GetUniquePizzaId();
                p.Name = pizza.Name;
                p.Description = pizza.Description;
                p.Image = pizza.Image;
                p.Price = pizza.Price;

                db.Pizzas.Add(p);
                db.SaveChanges();
            }
        }

        private static int GetUniquePizzaId()
        {
            using (var db = new PizzaContext())
            {
                // Trova l'ID massimo attualmente presente nella tabella delle pizze
                int maxId = db.Pizzas.Max(p => p.Id);

                // Incrementa di uno il massimo ID trovato
                return maxId + 1;
            }
        }
    }
}