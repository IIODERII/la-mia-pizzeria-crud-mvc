using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_static;
using la_mia_pizzeria_static.Models;

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
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pizza data)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", data);
            }

            PizzaManager.AddPizza(data);
            return RedirectToAction("Index");
        }
    }
}
