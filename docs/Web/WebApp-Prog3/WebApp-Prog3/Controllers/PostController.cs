using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_Prog3.Models.Clients;
using WebApp_Prog3.Models;
using System.IO;

namespace WebApp_Prog3.Controllers
{
    [Authorize(Roles = "Poster")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }


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

        public ActionResult EditarPost(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
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

        [HttpGet]
        public ActionResult EliminarPost(int id)
        {
            PostClient postClient = new PostClient();
            postClient.Delete(id);
            return RedirectToAction("ProfileAcc");
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
    }
}