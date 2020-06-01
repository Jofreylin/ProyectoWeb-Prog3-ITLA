using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApp_Prog3.Models
{
    public class PostClient
    {
        private string originalURL = "";

        public PostClient()
        {
            URL direccion = new URL();
            originalURL = direccion.GetURL();
        }

        public IEnumerable<Post> GetAll()
        {
            try
            {
                var lista = new List<Post>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Post").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Post>>(result);
                    return lista;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public Post Get(int id)
        {
            try
            {
                var lista = new Post();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Post/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<Post>(result);
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

        public bool Add(Post model)
        {
            try
            {
                var lista = new Post();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("post", model);
                var result = response.Result;
                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Post model)
        {
            try
            {
                var lista = new Post();
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
                var lista = new Post();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(originalURL);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.DeleteAsync("post/" + id);
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