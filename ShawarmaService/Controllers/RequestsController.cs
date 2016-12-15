using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ORM;
using DAL;

namespace ShawarmaService.Controllers
{
    public class RequestsController : Controller
    {
        private ShawarmaModel db = new ShawarmaModel();
        // GET: Requests
        public ActionResult Index()
        {
            return View();
        }

        //GET
        [HttpGet]
        public ViewResult AddIngradient()
        {
            ViewBag.Categories = new SelectList
                (db.IngradientCategory, "CategoryName", "CategoryName");
            return View();
        }

        [HttpPost]
        public ViewResult AddIngradient
            (string ingradientName, string categoryName, string totalWeight)
        {
            int weight;

            if (int.TryParse(totalWeight, out weight) && 
                DataOperations.AddIngredient(ingradientName, categoryName, weight))
            {
                return View("Success");
            }
            return View("Failed");
        }
    }
}