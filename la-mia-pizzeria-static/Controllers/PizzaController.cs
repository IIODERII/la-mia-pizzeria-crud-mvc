using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_static;
using la_mia_pizzeria_static.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Composition;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Pizza> pizze = PizzaManager.GetAllPizzas();
            //pizze = [];  //<= era per verificare che funzionasse in caso non ci fossero pizze
            return View(pizze);
        }

        [HttpGet]
        public IActionResult Show(int id)
        {
            return View(PizzaManager.GetPizzaById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            using PizzaContext db = new PizzaContext();
            {
                List<PizzaType> types = db.PizzaTypes.ToList();

                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.Types = types;
                return View(model);
            }

        }

        [HttpPost]
        public IActionResult Create(PizzaFormModel data)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", data);
            }

            Pizza pizzatoAdd = new Pizza();
            pizzatoAdd.Name = data.Pizza.Name;
            pizzatoAdd.Description = data.Pizza.Description;
            pizzatoAdd.Image = data.Pizza.Image;
            pizzatoAdd.Price = data.Pizza.Price;
            pizzatoAdd.PizzaType_Id = data.Pizza.PizzaType_Id;

            PizzaManager.AddPizza(pizzatoAdd);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id) {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToEdit = PizzaManager.GetPizzaById(id);
                if (pizzaToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    List<PizzaType> types = db.PizzaTypes.ToList();
                    PizzaFormModel model = new PizzaFormModel();
                    model.Pizza = pizzaToEdit;
                    model.Types = types;
                    return View(model);
                }
            }
        }

        [HttpPost]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                using(PizzaContext db = new PizzaContext())
                {
                    data.Types = db.PizzaTypes.ToList();
                    return View("Update", data);
                }
            }

            if (PizzaManager.UpdatePizza(data.Pizza, id))
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
             if (PizzaManager.DeletePizzaFromId(id))
             {
                 return RedirectToAction("Index");
             }

             return NotFound();
        }
    }
}
