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
    }
}