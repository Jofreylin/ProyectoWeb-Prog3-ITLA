using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApp_Prog3.Models.Clients
{
    
        public class PaisClient
        {
            private string originalURL = "";

            public PaisClient()
            {
                URL direccion = new URL();
                originalURL = direccion.GetURL();
            }

            public IEnumerable<Pais> GetAll()
            {
                try
                {
                    var lista = new List<Pais>();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(originalURL);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("Pais").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        lista = JsonConvert.DeserializeObject<List<Pais>>(result);
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

            public bool FindNombre(string nombre)
            {
                try
                {
                    var lista = new List<Pais>();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(originalURL);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("Pais").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        lista = JsonConvert.DeserializeObject<List<Pais>>(result);
                        var elemento = lista.Single(x => x.Nombre == nombre);
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            public Pais Get(int id)
            {
                try
                {
                    var lista = new Pais();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(originalURL);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("Pais/" + id).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        lista = JsonConvert.DeserializeObject<Pais>(result);
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

            public string FindPais(int id)
            {
                try
                {
                    var lista = new List<Pais>();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(originalURL);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("Pais").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        lista = JsonConvert.DeserializeObject<List<Pais>>(result);
                        var elemento = lista.Single(x => x.Id == id);
                        string Pais = elemento.Nombre;
                        return Pais;

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
            public bool Add(Pais model)
            {
                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(originalURL);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync("Pais", model).Result;
                    return response.IsSuccessStatusCode;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            public bool Update(Pais model)
            {
                try
                {
                    var lista = new Pais();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(originalURL);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PutAsJsonAsync("Pais", model).Result;

                    return response.IsSuccessStatusCode;
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
                    var lista = new Pais();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(originalURL);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.DeleteAsync("Pais/" + id);
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