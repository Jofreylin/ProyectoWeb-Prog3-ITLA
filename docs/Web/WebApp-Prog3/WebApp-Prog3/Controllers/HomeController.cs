﻿using System;
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
            ViewBag.listPosts = postClient.GetAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}