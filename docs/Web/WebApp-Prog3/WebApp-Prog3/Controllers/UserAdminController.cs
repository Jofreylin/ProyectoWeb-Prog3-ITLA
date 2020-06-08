using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp_Prog3.Models;
using WebApp_Prog3.Models.Clients;

namespace WebApp_Prog3.Controllers
{
    public class UserAdminController : Controller
    {
        // GET: UserAdmin
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                if(!(String.IsNullOrEmpty(login.Usuario) || String.IsNullOrEmpty(login.Contra)))
                { 
                    
                    UserAdminClient adminClient = new UserAdminClient();
                    var elemento = adminClient.FindUserContra(login.Usuario, login.Contra);
                    if (elemento != null)
                    {
                        FormsAuthentication.SetAuthCookie(elemento.Usuario, true);
                        return RedirectToAction("ProfileAcc", "UserAdmin");
                    }
                    else
                    {
                        return Index("Usuario no encontrado, o contraseña no coincide.");
                    }
                }
                else
                {
                    return Index("Favor de llenar todos los campos.");
                }
            }
            else
            {
                return View(login);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "UserAdmin");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProfileAcc()
        {
            UserPosterClient poster = new UserPosterClient();
            UserAdminClient admin = new UserAdminClient();
            CategoriaClient categoria = new CategoriaClient();
            PostClient post = new PostClient();
            CiudadClient ciudad = new CiudadClient();
            var elementoCiudad = ciudad.GetAll();
            var elementoPoster = poster.GetAll();
            var elementoCategoria = categoria.GetAll();
            var elementoAdmin = admin.GetAll();
            ViewBag.Cuenta = elementoAdmin.Single(x => x.Usuario == User.Identity.Name);

            ViewBag.CountPosters = elementoPoster.Count();
            ViewBag.CountAdmins = admin.GetAll().Count();
            ViewBag.CountCategorias = elementoCategoria.Count();
            ViewBag.CountPosts = post.GetAll().Count();

            elementoCategoria = elementoCategoria.OrderByDescending(x => x.Cantidad);
            ViewBag.ListCategorias = elementoCategoria;
            ViewBag.MaxCategoria = elementoCategoria.First().Nombre;

            var v = (from a in elementoPoster
                                     where a.FechaCreacion.Day == DateTime.Now.Day
                                     select a
                                     );
            ViewBag.CountRPostersT = v.Count();
            elementoCiudad = elementoCiudad.OrderByDescending(x => x.Cantidad);
            ViewBag.MaxCityPosters = elementoCiudad.First().Nombre;

            

            return View();
        }

        [Authorize(Roles ="Admin")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult BusquedaUserPoster(string buscar, string sortOrder, string sortBy, int pageNumber = 1)
        {
            UserPosterClient userposterClient = new UserPosterClient();
            CategoriaClient categoriaClient = new CategoriaClient();
            UserPosterClient userPoster = new UserPosterClient();
            CiudadClient ciudad = new CiudadClient();
            var elementos = userposterClient.GetAll();
            var e = new List<UserPoster>();
            var j = new Post();
            foreach (var i in elementos)
            {
                i.Ciudades = ciudad.FindCiudad(i.NombreCiudad);
                e.Add(i);
            }

            var lista = e;

            if (buscar != null)
            {
                
                var v = (from a in e
                         where a.Ciudades.Contains(buscar) ||
                               a.Email.Contains(buscar) ||
                               a.NombreEmpresa.Contains(buscar) ||
                               a.Id.ToString().Equals(buscar)
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

        public List<UserPoster> ApplySorting(string sortOrder, string sortBy, List<UserPoster> e)
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

        public List<UserPoster> ApplyPagination(int pageNumber, List<UserPoster> e)
        {
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = Math.Ceiling(e.Count() / 20.0);

            e = e.Skip((pageNumber - 1) * 20).Take(20).ToList();
            return e;
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GestionUserPoster(int id)
        {
            return View();
        }
    }
}