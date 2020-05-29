﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApp_Prog3.Models
{
    public class CategoriaClient
    {
        private string originalURL = "https://localhost:44390/api/";

        public IEnumerable<Categoria> GetAll()
        {
            try
            {
                var lista = new List<Categoria>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Categoria").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Categoria>>(result);
                    return lista;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Categoria Get(int id)
        {
            try
            {
                var lista = new Categoria();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Categoria/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<Categoria>(result);
                    return lista;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Add(Categoria model)
        {
            try
            {
                var lista = new Categoria();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("categoria", model);
                var result = response.Result;
                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Categoria model)
        {
            try
            {
                var lista = new Categoria();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PutAsJsonAsync("categoria/" + model.Id, model);
                var result = response.Result;
                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var lista = new Categoria();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.DeleteAsync("categoria/" +id);
                var result = response.Result;
                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}