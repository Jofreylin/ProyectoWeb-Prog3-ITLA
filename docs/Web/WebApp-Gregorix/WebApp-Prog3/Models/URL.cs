﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_Prog3.Models
{
    public class URL
    {
        //private readonly string direccion = "https://apiprog3.azurewebsites.net/api/";
        private readonly string direccion = "https://localhost:5001/api/";
        
        public string GetURL()
        {
            return direccion;
        }
    }
}