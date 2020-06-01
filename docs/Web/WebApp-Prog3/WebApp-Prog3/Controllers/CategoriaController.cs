using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_Prog3.Models;
using WebApp_Prog3.ViewModel;

namespace WebApp_Prog3.Controllers
{
    public class CategoriaController : Controller
    {
        // GET ALL: Categoria
        public ActionResult Index()
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            ViewBag.listCategories = categoriaClient.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria c)
        {
            if (ModelState.IsValid)
            {
                CategoriaClient categoriaClient = new CategoriaClient();
                categoriaClient.Add(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }

        
        public ActionResult Delete(int id)
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            categoriaClient.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            CategoriaClient categoriaClient = new CategoriaClient();
            Categoria c = new Categoria();
            c = categoriaClient.Get(id);
            return View("Edit", c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria c)
        {
            if (ModelState.IsValid)
            {
                CategoriaClient categoriaClient = new CategoriaClient();
                categoriaClient.Update(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }


    }
}