using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApp_Prog3.Models.Clients
{
    public class UserAdminClient
    {
        private string originalURL = "";

        public UserAdminClient()
        {
            URL direccion = new URL();
            originalURL = direccion.GetURL();
        }

        public IEnumerable<UserAdmin> GetAll()
        {
            try
            {
                var lista = new List<UserAdmin>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserAdmin").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<UserAdmin>>(result);
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

        public UserAdmin FindUser(UserAdmin model)
        {
            try
            {
                var lista = new UserAdmin();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserAdmin/" + model.Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<UserAdmin>(result);
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

        public bool FindRole(string adminUser)
        {
            try
            {
                var lista = new List<UserAdmin>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserAdmin").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<UserAdmin>>(result);
                    var elemento = lista.Single(x => x.Usuario == adminUser);
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

        public UserAdmin FindUserContra(string usuario, string contra)
        {
            try
            {
                var lista = new List<UserAdmin>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserAdmin").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<UserAdmin>>(result);
                    var elemento = lista.Single(x => x.Usuario == usuario && x.Contraseña == contra);
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

        public UserAdmin Get(int id)
        {
            try
            {
                var lista = new UserAdmin();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("UserAdmin/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<UserAdmin>(result);
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

        public bool Add(UserAdmin model)
        {
            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("UserAdmin", model).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(UserAdmin model)
        {
            try
            {
                var lista = new UserAdmin();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("UserAdmin", model).Result;

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
                var lista = new UserAdmin();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.DeleteAsync("UserAdmin/" + id);
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