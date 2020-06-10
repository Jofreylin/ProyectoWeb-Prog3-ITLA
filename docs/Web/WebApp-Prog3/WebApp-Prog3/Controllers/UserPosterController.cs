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
    public class UserPosterController : Controller
    {
        // GET: UserPoster
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
                if (!(String.IsNullOrEmpty(login.Usuario) || String.IsNullOrEmpty(login.Contra)))
                {

                    UserPosterClient posterClient = new UserPosterClient();
                    login.Usuario = login.Usuario.ToLower();
                    var elemento = posterClient.FindCorreoContra(login.Usuario, login.Contra);
                    if (elemento != null)
                    {
                        FormsAuthentication.SetAuthCookie(elemento.Email, true);
                        return RedirectToAction("ProfileAcc", "UserPoster");
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

        [Authorize(Roles = "Poster")]
        public ActionResult ProfileAcc()
        {
            return View();
        }

        //public ActionResult RegistrarPoster()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult RegistrarPoster(string message)
        {
            UserPoster userPoster = new UserPoster();
            ViewBag.Message = message;
            return View(userPoster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarPoster(UserPoster c)
        {
            UserPosterClient posterClient = new UserPosterClient();
            if (ModelState.IsValid)
            {
                c.Email = c.Email.ToLower();
                c.ConfirmarEmail = c.ConfirmarEmail.ToLower();
                if (c.Email == c.ConfirmarEmail)
                {
                    if (c.Contra == c.ConfirmarContra)
                    {
                        if (c.NombreCiudad == 0 || c.NombrePais == 0)
                        {
                            return RegistrarPoster("Asegurese de haber seleccionado un pais y su ciudad correspondiente.");
                        }
                        else
                        {
                            if (posterClient.FindCorreo(c))
                            {
                                return RegistrarPoster("Ya existe una cuenta registrada con ese correo");
                            }
                            else
                            {
                                posterClient.Add(c);
                                return RedirectToAction("Index");
                            }
                        }
                    }
                    else
                    {
                        return RegistrarPoster("Las Contraseñas no coinciden");
                    }
                }
                else
                {
                    return RegistrarPoster("Los Emails no coinciden");
                }
            }
            else
            {
                return View(c);
            }

        }


        public JsonResult UpdateViewBagCiudad(int id)
        {
            CiudadClient ciudadClient = new CiudadClient();
            var elemento = ciudadClient.GetAll();
            elemento.OrderBy(x => x.Nombre);
            var v = (from a in elemento
                     where a.NombrePais == id
                     select a
                     );

            SelectList selectListItems = new SelectList(v.ToList(), "Id", "Nombre");
            //ViewBag.Ciudad = selectListItems;
            List<SelectListItem> PList = new List<SelectListItem>();
            PList = v.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            }).ToList();
            ViewBag.Ciudad = selectListItems;
            return Json(selectListItems, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Poster")]
        public ActionResult Delete(int id)
        {
            UserPosterClient userPosterClient = new UserPosterClient();
            userPosterClient.Delete(id);
            return RedirectToAction("ProfileAcc");
        }

        [Authorize(Roles = "Poster")]
        public ActionResult Edit(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [Authorize(Roles = "Poster")]
        [HttpGet]
        public ActionResult Edit(int id)
        {

            UserPosterClient userPosterClient = new UserPosterClient();
            UserPoster c = new UserPoster();
            c = userPosterClient.Get(id);

            return View("Edit", c);
        }

        [Authorize(Roles = "Poster")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserPoster c)
        {
            UserPosterClient userPosterClient = new UserPosterClient();

            if (ModelState.IsValid)
            {
                c.Email = c.Email.ToLower();
                userPosterClient.Update(c);
                return RedirectToAction("ProfileAcc");
            }
            else
            {
                return View(c);
            }
        }


        public ActionResult CrearPost()
        {
            return View();
        }
    }
}