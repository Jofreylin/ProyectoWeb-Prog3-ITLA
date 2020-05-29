using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_Prog3.Models;

namespace WebApp_Prog3.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            PostClient postClient = new PostClient();
            ViewBag.listPosts = postClient.GetAll();
            return View();
        }
    }
}