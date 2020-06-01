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
    }
}