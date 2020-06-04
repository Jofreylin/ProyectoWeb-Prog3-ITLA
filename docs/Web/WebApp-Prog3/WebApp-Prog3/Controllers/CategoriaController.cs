using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_Prog3.Models;
using WebApp_Prog3.ViewModel;

namespace WebApp_Prog3.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        // GET ALL: Categoria
        [Route("[controller]")]
        public ActionResult Index()
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            ViewBag.listCategories = categoriaClient.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult Create(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria c)
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            if (categoriaClient.findNombre(c.Nombre))
            {
                return Create("Ya existe una categoria con ese nombre");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    categoriaClient.Add(c);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(c);
                }
            }
            
        }

        
        public ActionResult Delete(int id)
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            categoriaClient.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string message)
        {
            ViewBag.Message = message;
            return View();
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
            CategoriaClient categoriaClient = new CategoriaClient();
            if (categoriaClient.findNombre(c.Nombre))
            {
                return Edit("Ya existe una categoria con ese nombre");
            }
            else
            {
                if (ModelState.IsValid)
                {

                    categoriaClient.Update(c);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(c);
                }
            }
           
            
        }


    }
}