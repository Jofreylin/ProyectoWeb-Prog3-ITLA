using System;
using System.Collections.Generic;
using System.IO;
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
            return RedirectToAction("Index", "UserPoster");
        }

        [Authorize(Roles = "Poster")]
        public ActionResult ProfileAcc()
        {
            UserPosterClient poster = new UserPosterClient();
            CategoriaClient categoria = new CategoriaClient();
            PostClient post = new PostClient();
            PaisClient pais = new PaisClient();
            CiudadClient ciudad = new CiudadClient();
            var elementoPais = pais.GetAll();
            var elementoCiudad = ciudad.GetAll();
            var elementoPoster = poster.GetAll();
            var elementoCategoria = categoria.GetAll();
            var elementov = elementoPoster.Single(x => x.Email == User.Identity.Name);
            var elementoposts = post.GetAll().Where(c => c.Poster == elementov.Id);
            ViewBag.Cuenta = elementov;
            ViewBag.CountPosts = elementoposts.Count();

            elementoCategoria = elementoCategoria.OrderByDescending(x => x.Cantidad);
            var e = new List<Post>();
            foreach(var i in elementoposts)
            {
                i.Categorias = elementoCategoria.Single(x => x.Id == i.NombreCategoria).Nombre;
            }

            elementov.Paises = elementoPais.Single(x => x.Id == elementov.NombrePais).Nombre;
            elementov.Ciudades = elementoCiudad.Single(x => x.Id == elementov.NombreCiudad).Nombre;

            ViewBag.ListPosts = elementoposts;
            ViewBag.Country = elementov.Paises;
            ViewBag.City = elementov.Ciudades;
            return View();
        }

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
                            return RegistrarPoster("Debes seleccionar un pais y su ciudad correspondiente.");
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
            elemento.OrderByDescending(x => x.Nombre);
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

        [Authorize(Roles = "Poster, Admin")]
        public ActionResult Delete(int id)
        {
            UserPosterClient userPosterClient = new UserPosterClient();
            userPosterClient.Delete(id);
            return RedirectToAction("ProfileAcc");
        }

        [Authorize(Roles = "Poster, Admin")]
        public ActionResult Edit(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [Authorize(Roles = "Poster, Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {

            UserPosterClient userPosterClient = new UserPosterClient();
            UserPoster c = new UserPoster();
            c = userPosterClient.Get(id);

            return View("Edit", c);
        }

        [Authorize(Roles = "Poster, Admin")]
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

        [Authorize(Roles = "Poster")]
        [HttpGet]
        public ActionResult CrearPost(string message)
        {
            Post post = new Post();
            UserPosterClient posterClient = new UserPosterClient();
            var elemento = posterClient.FindByCorreo(User.Identity.Name);
            post.Posters = elemento.NombreEmpresa;
            post.Poster = elemento.Id;
            ViewBag.Message = message;
            ViewBag.Correo = elemento.Email;
            return View(post);
        }

        [Authorize(Roles = "Poster")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPost(Post c, HttpPostedFileBase imagenSisi)
        {
            UserPosterClient posterClient = new UserPosterClient();
            var elemento = posterClient.FindByCorreo(User.Identity.Name);
            c.Poster = elemento.Id;
            PostClient postClient = new PostClient();
            if (imagenSisi != null && imagenSisi.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var bynaryImage = new BinaryReader(imagenSisi.InputStream))
                {
                    imagenData = bynaryImage.ReadBytes(imagenSisi.ContentLength);
                }
                c.Logo = imagenData;
            }

            if (c.NombreCategoria == 0)
            {
                return CrearPost("Debes seleccionar una Categoria.");
            }
            else if (c.NombrePais == 0 || c.NombreCiudad == 0)
            {
                return CrearPost("Debes seleccionar un pais y su ciudad correspondiente.");
            }
            else if (c.NombreTipoTrabajo == 0)
            {
                return CrearPost("Debes seleccionar un tipo de Trabajo.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    postClient.Add(c);
                    return RedirectToAction("ProfileAcc");
                }
                else
                {
                    return View(c);
                }
            }

        }

        [Authorize(Roles = "Poster, Admin")]
        public ActionResult EditarPost(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Poster, Admin")]
        public ActionResult EditarPost(int id, int categoria, int ciudad, int pais, int tipotrabajo, string correo, string empresa, string posicion, string descripcion, string calle, int idPoster, string dUrl)
        {
            Post post = new Post();
            post.Id = id;
            post.NombreCategoria = categoria;
            post.NombreCiudad = ciudad;
            post.NombrePais = pais;
            post.NombreTipoTrabajo = tipotrabajo;
            post.NombrePosicion = posicion;
            post.Descripcion = descripcion;
            post.NombreCalle = calle;
            post.DireccionUrl = dUrl;
            ViewBag.Empresa = empresa;
            post.Poster = idPoster;
            ViewBag.Correo = correo;
            ViewBag.Foto = GetImage(id);
            return View("EditarPost", post);
        }

        [Authorize(Roles = "Poster, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPost(Post c, HttpPostedFileBase imagenSisi)
        {
            PostClient postClient = new PostClient();
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
                var imagen = postClient.Get(c.Id);
                c.Logo = imagen.Logo;
            }

            if (c.NombreCategoria == 0)
            {
                return EditarPost("Debes seleccionar una Categoria.");
            }
            else if (c.NombrePais == 0 || c.NombreCiudad == 0)
            {
                return EditarPost("Debes seleccionar un pais y su ciudad correspondiente.");
            }
            else if (c.NombreTipoTrabajo == 0)
            {
                return EditarPost("Debes seleccionar un tipo de Trabajo.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    postClient.Update(c);
                    return RedirectToAction("ProfileAcc");
                }
                else
                {
                    return View(c);
                }
            }
        }

        [Authorize(Roles = "Poster, Admin")]
        [HttpGet]
        public ActionResult EliminarPost(int id)
        {
            PostClient postClient = new PostClient();
            postClient.Delete(id);
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("BusquedaUserPoster","UserAdmin");
            }
            else
            {
                return RedirectToAction("ProfileAcc");
            }
            
        }

        public ActionResult GetImage(int id)
        {
            PostClient post = new PostClient();
            var imagen = post.Get(id);
            try
            {
                return File(imagen.Logo, "image/jpg");
            }
            catch (Exception e)
            {
                return null;
            }
        }

        
        [Authorize(Roles = "Poster")]
        [HttpGet]
        public ActionResult InfoPost(int id, int categoria, int ciudad, int pais, int tipotrabajo, string correo, string empresa, string posicion, string descripcion, string calle, int idPoster, string dUrl)
        {
            CiudadClient ciudadClient = new CiudadClient();
            PaisClient paisClient = new PaisClient();
            CategoriaClient categoriaClient = new CategoriaClient();
            TipoTrabajoClient trabajoClient = new TipoTrabajoClient();

            Post post = new Post();
            post.Id = id;
            post.Categorias = categoriaClient.Get(categoria).Nombre;
            post.Ciudades = ciudadClient.Get(ciudad).Nombre;
            post.Paises = paisClient.Get(pais).Nombre;
            post.TipoTrabajos = trabajoClient.Get(tipotrabajo).Nombre;
            post.NombreCategoria = categoria;
            post.NombreCiudad = ciudad;
            post.NombrePais = pais;
            post.NombreTipoTrabajo = tipotrabajo;
            post.NombrePosicion = posicion;
            post.Descripcion = descripcion;
            post.NombreCalle = calle;
            post.DireccionUrl = dUrl;
            ViewBag.Empresa = empresa;
            post.Poster = idPoster;
            ViewBag.Correo = correo;
            ViewBag.Foto = GetImage(id);
            return View(post);
        }

        public ActionResult EditarPoster(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [Authorize(Roles = "Poster, Admin")]
        [HttpGet]
        public ActionResult EditarPoster(int id, int idPais, int idCiudad, string correo, string empresa, string calle)
        {
            UserPoster userPoster = new UserPoster();
            userPoster.Id = id;
            userPoster.NombrePais = idPais;
            userPoster.NombreCiudad = idCiudad;
            userPoster.Email = correo;
            userPoster.ConfirmarEmail = correo;
            userPoster.NombreCalle = calle;
            userPoster.NombreEmpresa = empresa;
            return View("EditarPoster",userPoster);
        }

        [Authorize(Roles = "Poster, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPoster(UserPoster c)
        {
            UserPosterClient posterClient = new UserPosterClient();
            var elemento = posterClient.Get(c.Id);
            if (ModelState.IsValid)
            {
                c.Email = c.Email.ToLower();
                c.ConfirmarEmail = c.ConfirmarEmail.ToLower();
                if (c.Email == c.ConfirmarEmail)
                {
                    if (elemento.Contra == c.Contra)
                    {
                        if (c.NombreCiudad == 0 || c.NombrePais == 0)
                        {
                            return EditarPoster("Debes seleccionar un pais y su ciudad correspondiente.");
                        }
                        else
                        {
                            if(elemento.Email == c.Email)
                            {
                                posterClient.Update(c);
                                if (User.IsInRole("Poster"))
                                {
                                    return RedirectToAction("Logout");
                                }
                                else
                                {
                                    return RedirectToAction("GestionUserPoster", "UserAdmin");
                                }
                            }
                            else
                            {
                                if (posterClient.FindCorreo(c))
                                {
                                    return EditarPoster("Ya existe una cuenta registrada con ese correo");
                                }
                                else
                                {
                                    posterClient.Update(c);
                                    if (User.IsInRole("Poster"))
                                    {
                                        return RedirectToAction("Logout");
                                    }
                                    else
                                    {
                                        return RedirectToAction("GestionUserPoster", "UserAdmin");
                                    }

                                }
                            }
                            
                        }
                    }
                    else
                    {
                        return EditarPoster("Las Contraseñas no coinciden");
                    }
                }
                else
                {
                    return EditarPoster("Los Emails no coinciden");
                }
            }
            else
            {
                return View(c);
            }
        }

        [Authorize(Roles = "Poster, Admin")]
        [HttpGet]
        public ActionResult EliminarPoster(int id)
        {
            UserPosterClient posterClient = new UserPosterClient();
            posterClient.Delete(id);
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("BusquedaUserPoster","UserAdmin");
            }
            else
            {
                return RedirectToAction("LogOut");
            }
            
        }
    }
}