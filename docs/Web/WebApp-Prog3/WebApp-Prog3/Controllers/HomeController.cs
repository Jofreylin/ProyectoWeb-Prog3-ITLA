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

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Busqueda(string buscar,string sortOrder, string sortBy, int pageNumber = 1)
        {
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

            var lista = e;

            if (buscar != null)
            {
                buscar = buscar.ToUpper();
                var v = (from a in e
                         where a.Categorias.Contains(buscar) ||
                               a.Posters.Contains(buscar) ||
                               a.NombrePosicion.Contains(buscar)
                         select a);
                lista = v.ToList();
                lista = ApplySorting(sortOrder, sortBy, lista);
                lista = ApplyPagination(pageNumber, lista);
                
            }
            else
            {
                lista = ApplySorting(sortOrder, sortBy, lista);
                lista = ApplyPagination(pageNumber, lista);
            }

            ViewBag.Buscar = buscar;

            return View(lista);

        }

        public List<Post> ApplySorting(string sortOrder, string sortBy, List<Post> e)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.SortBy = sortBy;

            switch (sortBy)
            {
                case "Fecha":
                    switch (sortOrder)
                    {
                        case "Asc":
                            e = e.OrderBy(x => x.FechaCreacion).ToList();
                            break;

                        case "Desc":
                            e = e.OrderByDescending(x => x.FechaCreacion).ToList();
                            break;
                        default:
                            e = e.OrderBy(x => x.FechaCreacion).ToList();
                            break;


                    }
                    break;
                default:
                    e = e.OrderBy(x => x.FechaCreacion).ToList();
                    break;
            }

            return e;
        }

        public List<Post> ApplyPagination(int pageNumber,List<Post> e)
        {
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = Math.Ceiling(e.Count() / 20.0);

            e = e.Skip((pageNumber - 1) * 20).Take(20).ToList();
            return e;
        }

    }
}