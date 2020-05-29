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
        // GET: Categoria
        public ActionResult Index()
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            ViewBag.listCategories = categoriaClient.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Add(AllViewModel avm)
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            categoriaClient.Add(avm.Categoria);
            return RedirectToAction("Index");
        }

        [HttpDelete]
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
            AllViewModel avm = new AllViewModel();
            avm.Categoria = categoriaClient.Get(id);
            return View("Edit", avm);
        }

        [HttpPut]
        public ActionResult Update(AllViewModel avm)
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            categoriaClient.Update(avm.Categoria);
            return RedirectToAction("Index");
        }
    }
}