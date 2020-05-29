using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_Prog3.Models;

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


    }
}