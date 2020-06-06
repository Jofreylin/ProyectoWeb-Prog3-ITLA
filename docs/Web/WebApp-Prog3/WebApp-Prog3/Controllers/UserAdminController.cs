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
            return View();
        }
    }
}