using GroupInvest.Common.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GroupInvest.Common.Infrastructure.DataAccess.Repositorios
{
    public abstract class HttpRepositorio<T> : IHttpRepositorio<T>
    {
        #region Atributos
        protected readonly string UrlBase;
        #endregion

        #region Propriedades
        public string Token { get; set; }
        #endregion

        #region Construtor
        public HttpRepositorio(string urlBase)
        {
            UrlBase = urlBase;
        }
        #endregion

        #region Métodos
        protected HttpResponseMessage Get(string uri)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {Token}");
            
            var response = client.GetAsync($"{UrlBase}/{uri}").Result;

            return response;
        }

        protected HttpResponseMessage Post(string uri, object content)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {Token}");

            var serializedContent = JsonConvert.SerializeObject(content);
            var httpContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");

            var response = client.PostAsync($"{UrlBase}/{uri}", httpContent).Result;

            return response;
        }
        #endregion
    }
}
