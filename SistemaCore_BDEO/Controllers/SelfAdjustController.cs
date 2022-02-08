using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SistemaCore_BDEO.Controllers
{
    [RoutePrefix("api/v1/SelfAdjust")]
    public class SelfAdjustController : ApiController
    {
        private string BaseUrl
        {
            get { return ConfigurationManager.AppSettings["selfadjust"]; }
        }

        //Listo
        [HttpPost]
        public async Task<IHttpActionResult> Selfadjust_Post(object model)
        {
            

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var client = new HttpClient())
                {
                    var url = client.BaseAddress = new Uri(BaseUrl);

                    client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await client.PostAsJsonAsync(url, model);

                    if (respuesta.IsSuccessStatusCode)
                    {

                        var cuerpo = await respuesta.Content.ReadAsStringAsync();

                        var result = JObject.Parse(cuerpo);

                        //var resp = JsonSerializer.Deserialize<Login>(cuerpo, jsonSerializerOptions);

                        return Ok(result);
                    }
                    else
                    {
                        throw new Exception("Error" + respuesta.StatusCode);
                    }
                }
            }
            catch (Exception e)
            {
                throw e.InnerException ?? e;
            }
        }


        //Listo
        [HttpPost]
        [Route("external_selfadjust")]
        public async Task<IHttpActionResult> external_selfadjust(object model)
        {
            var uri = BaseUrl +  "external_selfadjust";

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var client = new HttpClient())
                {

                    var url = client.BaseAddress = new Uri(uri);

                    client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await client.PostAsJsonAsync(url, model);

                    if (respuesta.IsSuccessStatusCode)
                    {

                        var cuerpo = await respuesta.Content.ReadAsStringAsync();

                        var result = JObject.Parse(cuerpo);

                        //var resp = JsonSerializer.Deserialize<Login>(cuerpo, jsonSerializerOptions);

                        return Ok(result);
                    }
                    else
                    {
                        throw new Exception("Error" + respuesta.StatusCode);
                    }
                }
            }
            catch (Exception e)
            {
                throw e.InnerException ?? e;
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> SelfAdjust_Get(string id)
        {
            var uri = BaseUrl + id;

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var httpCliente = new HttpClient())
                {

                    var url = httpCliente.BaseAddress = new Uri(uri);

                    httpCliente.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await httpCliente.GetAsync(url);

                    if (respuesta.IsSuccessStatusCode)
                    {

                        var cuerpo = await respuesta.Content.ReadAsStringAsync();

                        var result = JObject.Parse(cuerpo);

                        return Ok(result);
                    }
                    else
                    {
                        throw new Exception("Error" + respuesta.StatusCode);
                    }
                }
            }
            catch (Exception e)
            {
                throw e.InnerException ?? e;
            }

        }

        //Listo
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> SelfAdjust_Put(object model, string id)
        {
            var uri = BaseUrl + id;

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var httpCliente = new HttpClient())
                {

                    var url = httpCliente.BaseAddress = new Uri(uri);

                    httpCliente.DefaultRequestHeaders.Add("access_token", token);

                    httpCliente.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));

                    var respuesta = await httpCliente.PutAsJsonAsync(url, model);

                    if (respuesta.IsSuccessStatusCode)
                    {

                        var cuerpo = await respuesta.Content.ReadAsStringAsync();

                        var result = JObject.Parse(cuerpo);

                        //var resp = JsonSerializer.Deserialize<Login>(cuerpo, jsonSerializerOptions);

                        return Ok(result);
                    }
                    else
                    {
                        throw new Exception("Error" + respuesta.StatusCode);
                    }

                }
            }
            catch (Exception e)
            {
                throw e.InnerException ?? e;
            }

        }

        //Listo
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> SelfAdjust_Delete(string id)
        {
            var uri = BaseUrl + id;

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    var url = httpCliente.BaseAddress = new Uri(uri);


                    httpCliente.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await httpCliente.DeleteAsync(url);

                    if (respuesta.IsSuccessStatusCode)
                    {

                        var cuerpo = await respuesta.Content.ReadAsStringAsync();

                        var result = JObject.Parse(cuerpo);


                        return Ok(result);
                    }
                    else
                    {
                        throw new Exception("Error" + respuesta.StatusCode);
                    }

                }
            }
            catch (Exception e)
            {
                throw e.InnerException ?? e;
            }

        }
    }
}
