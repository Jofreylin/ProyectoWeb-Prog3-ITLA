using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApp_Prog3.Models;
using WebApp_Prog3.ViewModel;

namespace WebApp_Prog3.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult Create(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria c, HttpPostedFileBase imagenSisi)
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            c.Nombre = c.Nombre.ToUpper();
            if (categoriaClient.findNombre(c.Nombre))
            {
                return Create("Ya existe una categoria con ese nombre");
            }
            else
            {
                if (imagenSisi != null && imagenSisi.ContentLength > 0)
                {
                    byte[] imagenData = null;
                    using (var bynaryImage = new BinaryReader(imagenSisi.InputStream))
                    {
                        imagenData = bynaryImage.ReadBytes(imagenSisi.ContentLength);
                    }
                    c.Logo = imagenData;
                }
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
        public ActionResult Edit(Categoria c, HttpPostedFileBase imagenSisi)
        {
            CategoriaClient categoriaClient = new CategoriaClient();
            if (imagenSisi != null && imagenSisi.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var bynaryImage = new BinaryReader(imagenSisi.InputStream))
                {
                    imagenData = bynaryImage.ReadBytes(imagenSisi.ContentLength);
                }
                c.Logo = imagenData;
            }
            else
            {
                var imagen = categoriaClient.Get(c.Id);
                c.Logo = imagen.Logo;
            }

            if (ModelState.IsValid)
            {
                c.Nombre = c.Nombre.ToUpper();
                categoriaClient.Update(c);
                return RedirectToAction("Index");
            }
            else
            {
                return View(c);
            }
        }

        public ActionResult GetImage(int id)
        {
            CategoriaClient categoria = new CategoriaClient();
            var imagen = categoria.Get(id);
            return File(imagen.Logo, "image/jpg");
        }


    }
}