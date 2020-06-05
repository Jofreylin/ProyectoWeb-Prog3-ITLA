﻿using System;
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

                    UserPosterClient adminClient = new UserPosterClient();
                    var elemento = adminClient.FindCorreoContra(login.Usuario, login.Contra);
                    if (elemento != null)
                    {
                        FormsAuthentication.SetAuthCookie(Convert.ToString(elemento.Id), true);
                        return RedirectToAction("Index", "Categoria");
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


    }
}