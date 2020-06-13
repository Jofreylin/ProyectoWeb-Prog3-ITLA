using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_Prog3.Models.Clients;
using WebApp_Prog3.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace WebApp_Prog3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PostClient postClient = new PostClient();
            UserPosterClient userPoster = new UserPosterClient();
            CategoriaClient categoriaClient = new CategoriaClient();
            CiudadClient ciudadClient = new CiudadClient();
            PaisClient paisClient = new PaisClient();
            TipoTrabajoClient tipoTrabajoClient = new TipoTrabajoClient();
            var elementos = postClient.GetAll();
            var i = new List<Post>();
            foreach(var e in elementos)
            {
                e.Posters = userPoster.FindPost(e.Poster);
                e.Categorias = categoriaClient.FindCategory(e.NombreCategoria);
                e.Ciudades = ciudadClient.FindCiudad(e.NombreCiudad);
                e.Paises = paisClient.FindPais(e.NombrePais);
                e.TipoTrabajos = tipoTrabajoClient.FindTipoTrabajo(e.NombreTipoTrabajo);
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
            CiudadClient ciudad = new CiudadClient();
            PaisClient pais = new PaisClient();
            TipoTrabajoClient tipoTrabajoClient = new TipoTrabajoClient();
            var elementos = postClient.GetAll();
            var e = new List<Post>();
            var j = new Post();
            foreach (var i in elementos)
            {
                i.Posters = userPoster.FindPost(i.Poster);
                i.Categorias = categoriaClient.FindCategory(i.NombreCategoria);
                i.Ciudades = ciudad.FindCiudad(i.NombreCiudad);
                i.Paises = pais.FindPais(i.NombrePais);
                i.TipoTrabajos = tipoTrabajoClient.FindTipoTrabajo(i.NombreTipoTrabajo);
                e.Add(i);
            }

            var lista = e;

            if (buscar != null)
            {
                buscar = buscar.ToUpper();
                var v = (from a in e
                         where a.Categorias.ToUpper().Contains(buscar) ||
                               a.Posters.ToUpper().Contains(buscar) ||
                               a.NombrePosicion.ToUpper().Contains(buscar) ||
                               a.Paises.ToUpper().Contains(buscar) ||
                               a.Ciudades.ToUpper().Contains(buscar)
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

        public ActionResult Categorias()
        {

            return View();
        }

        public ActionResult GetImageCategoria(int id)
        {
            CategoriaClient categoria = new CategoriaClient();
            var imagen = categoria.Get(id);
            return File(imagen.Logo, "image/jpg");
        }

        public ActionResult GetImagePost(int id)
        {
            PostClient post = new PostClient();
            var imagen = post.Get(id);
            try
            {
                return File(imagen.Logo, "image/jpg");
            }
            catch(Exception e)
            {
                return null;
            }   
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Postularme(int id, int ciudadId, int paisId, int trabajoId,int categoriaId, string pais, string ciudad, string posicion, string tipoTrabajo, string descripcion, string calle, int idPoster, string dUrl)
        {
            UserPosterClient poster = new UserPosterClient();
            var elementoPoster = poster.Get(idPoster);
            Post post = new Post();
            post.NombreCiudad = ciudadId;
            post.NombrePais = paisId;
            post.NombreTipoTrabajo = trabajoId;
            post.NombreCategoria= categoriaId;
            ViewBag.Empresa = elementoPoster.NombreEmpresa;
            post.Paises = pais;
            post.Ciudades = ciudad;
            post.TipoTrabajos = tipoTrabajo;
            post.Descripcion = descripcion;
            post.Id = id;
            ViewBag.Correo = elementoPoster.Email;
            post.NombrePosicion = posicion;
            ViewBag.Foto = GetImagePost(id);
            post.NombreCalle = calle;
            post.DireccionUrl = dUrl;
            post.Poster = idPoster;
            return View(post);
        }
    }
}