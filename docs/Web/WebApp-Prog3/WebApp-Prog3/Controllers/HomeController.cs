using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_Prog3.Models.Clients;
using WebApp_Prog3.Models;

namespace WebApp_Prog3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PostClient postClient = new PostClient();
            UserPosterClient userPoster = new UserPosterClient();
            CategoriaClient categoriaClient = new CategoriaClient();
            var elementos = postClient.GetAll();
            var i = new List<Post>();
            foreach(var e in elementos)
            {
                e.Posters = userPoster.FindPost(e.Poster);
                e.Categorias = categoriaClient.FindCategory(e.NombreCategoria);
                i.Add(e);
            }
            var v = (from a in i
                     
                     select a);

            ViewBag.ListCategories = categoriaClient.GetAll().OrderBy(p => p.Nombre);
            v = v.OrderBy(p => p.Categorias);
            return View(v);
        }

        public ActionResult Busqueda()
        {
            PostClient postClient = new PostClient();
            CategoriaClient categoriaClient = new CategoriaClient();
            UserPosterClient userPoster = new UserPosterClient();
            var elementos = postClient.GetAll();
            var e = new List<Post>();
            foreach (var i in elementos)
            {
                i.Posters = userPoster.FindPost(i.Poster);
                i.Categorias = categoriaClient.FindCategory(i.NombreCategoria);
                e.Add(i);
            }
            
            return View(e);
        }

        [HttpPost]
        public ActionResult Busqueda(string buscar)
        {
            buscar = buscar.ToUpper();
            PostClient postClient = new PostClient();
            CategoriaClient categoriaClient = new CategoriaClient();
            UserPosterClient userPoster = new UserPosterClient();
            var elementos = postClient.GetAll();
            var e = new List<Post>();
            var j = new Post();
            foreach (var i in elementos)
            {
                i.Posters = userPoster.FindPost(i.Poster);
                i.Categorias = categoriaClient.FindCategory(i.NombreCategoria);
                e.Add(i);
            }
            var v = (from a in e
                     where a.Categorias.Contains(buscar) || 
                           a.Posters.Contains(buscar) || 
                           a.NombrePosicion.Contains(buscar) 
                     select a);
            return View(v.ToList());
        }

        //[HttpPost]
        //public ActionResult Busqueda(int page = 1, string buscar = "")
        //{
        //    int pageSize = 20;
        //    int totalRecord = 0;
        //    if(page < 1)
        //    {
        //        page = 1;
        //    }
        //    int skip = (page * pageSize) - pageSize;
        //    var data = FiltrarPosts(buscar, pageSize,skip, totalRecord);
        //    ViewBag.TotalRows = totalRecord;
        //    return View(data);
        //}



        public List<Post> FiltrarPosts(string buscar, int pageSize, int skip, int totalRecord)
        {
            PostClient postClient = new PostClient();
            CategoriaClient categoriaClient = new CategoriaClient();
            var elementos = postClient.GetAll();
            var e = new List<Post>();
            foreach(var i in elementos)
            {
                i.NombreCategoriaNavigation.Nombre = categoriaClient.Get(i.NombreCategoria).ToString();
                e.Add(i);
            }
            var v = (from a in e
                     where a.NombreCategoriaNavigation.Nombre.Contains(buscar)
                     select a);
            totalRecord = v.Count();
            if(pageSize > 0)
            {
                v = v.Skip(skip).Take(pageSize);
            }
            
            return v.ToList();
        }
    }
}