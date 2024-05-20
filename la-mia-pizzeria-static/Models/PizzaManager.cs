using Microsoft.EntityFrameworkCore;
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
                Pizza p = db.Pizzas.Where(p => p.Id == id).Include(t => t.Type).FirstOrDefault();
                return p;
            }
        }
        public static void AddPizza(Pizza pizza)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza p = new Pizza();
                p.Name = pizza.Name;
                p.Description = pizza.Description;
                p.Image = pizza.Image;
                p.Price = pizza.Price;
                p.PizzaType_Id = pizza.PizzaType_Id;
                p.TypeId = pizza.PizzaType_Id;

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

        public static bool UpdatePizza(Pizza data, int id)
        {
            using(var db = new PizzaContext())
            {
                Pizza p = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

                if(p != null)
                {
                    p.Name = data.Name;
                    p.Description = data.Description;
                    p.Price = data.Price;
                    p.Image = data.Image;
                    p.PizzaType_Id = data.PizzaType_Id;
                    p.TypeId = data.PizzaType_Id;

                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public static bool DeletePizzaFromId(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza toDelete = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

                if (toDelete != null)
                {
                    db.Pizzas.Remove(toDelete);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}