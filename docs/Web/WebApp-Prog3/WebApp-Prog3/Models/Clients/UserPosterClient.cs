using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WebApp_Prog3.Models
{
    public class UserPosterClient
    {
        private string originalURL = "";

        public UserPosterClient()
        {
            URL direccion = new URL();
            originalURL = direccion.GetURL();
        }

        public IEnumerable<UserPoster> GetAll()
        {
            try
            {
                var lista = new List<UserPoster>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserPoster").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<UserPoster>>(result);
                    return lista;
                }
                else
                {
                    return null;
                }
            }catch(Exception e)
            {
                return null;
            }
        }

        public UserPoster FindCorreo(UserPoster model)
        {
            try
            {
                var lista = new UserPoster();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserPoster/" + model.Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<UserPoster>(result);
                    return lista ;

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

        public bool FindRole(string posterEmail)
        {
            try
            {
                var lista = new List<UserPoster>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserPoster").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<UserPoster>>(result);
                    var elemento = lista.Single(x => x.Email == posterEmail);
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

        public string FindPost(int id)
        {
            try
            {
                var lista = new List<UserPoster>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserPoster").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<UserPoster>>(result);
                    var elemento = lista.Single(x => x.Id == id);
                    string poster = elemento.NombreEmpresa;
                    return poster;

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

        public UserPoster FindCorreoContra(string email, string contra)
        {
            try
            {
                var lista = new List<UserPoster>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserPoster").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<UserPoster>>(result);
                    var elemento = lista.Single(x => x.Email == email && x.Contra == contra);
                    return elemento;

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

        public UserPoster Get(int id)
        {
            try
            {
                var lista = new UserPoster();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Userposter/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<UserPoster>(result);
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

        public bool Add(UserPoster model)
        {
            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("userposter", model).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(UserPoster model)
        {
            try
            {
                var lista = new UserPoster();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("userposter", model).Result;

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
                var lista = new UserPoster();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.DeleteAsync("userposter/" + id);
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