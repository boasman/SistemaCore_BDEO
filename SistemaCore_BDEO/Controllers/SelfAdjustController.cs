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
    [RoutePrefix("api/SelfAdjust")]
    public class SelfAdjustController : ApiController
    {
        private JObject result = new JObject();

        private string BaseUrl
        {
            get { return ConfigurationManager.AppSettings["selfadjust"]; }
        }

        //Listo
        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> Selfadjust_Post(object model)
        {


            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var client = new HttpClient())
                {
                    var url = client.BaseAddress = new Uri(BaseUrl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await client.PostAsJsonAsync(url, model);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    var resultado = Validacion(cuerpo);

                    return NewMethod(respuesta, resultado);
                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }
        }


        //Listo
        [HttpPost]
        [Route("external_selfadjust")]
        public async Task<IHttpActionResult> external_selfadjust(object model)
        {
            var uri = ConfigurationManager.AppSettings["external_selfadjust"];

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var client = new HttpClient())
                {

                    var url = client.BaseAddress = new Uri(uri);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await client.PostAsJsonAsync(url, model);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    var resultado = Validacion(cuerpo);

                    return NewMethod(respuesta, resultado);
                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
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

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    var resultado = Validacion(cuerpo);

                    return NewMethod(respuesta, resultado);
                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
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

                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var respuesta = await httpCliente.PutAsJsonAsync(url, model);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    var resultado = Validacion(cuerpo);

                    return NewMethod(respuesta, resultado);

                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
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

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    var resultado = Validacion(cuerpo);

                    return NewMethod(respuesta, resultado);

                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }

        }

        public IHttpActionResult NewMethod(HttpResponseMessage respuesta, object result)
        {

            switch (respuesta.StatusCode)
            {

                case HttpStatusCode.OK:

                    return Ok(result);

                case HttpStatusCode.Unauthorized:


                    return Content(HttpStatusCode.Unauthorized, result);


                case HttpStatusCode.NoContent:


                    return Content(HttpStatusCode.NoContent, result);


                case HttpStatusCode.BadRequest:


                    return Content(HttpStatusCode.BadRequest, result);


                case HttpStatusCode.Forbidden:

                    return Content(HttpStatusCode.Forbidden, result);


                case HttpStatusCode.NotFound:

                    //result = JObject.Parse(cuerpo);
                    return Content(HttpStatusCode.NotFound, result);


                case HttpStatusCode.MethodNotAllowed:


                    return Content(HttpStatusCode.MethodNotAllowed, result);

                case HttpStatusCode.NotImplemented:


                    return Content(HttpStatusCode.NotImplemented, result);

                case HttpStatusCode.BadGateway:

                    return Content(HttpStatusCode.MethodNotAllowed, result);
                default:

                    throw new HttpResponseException(respuesta.StatusCode);

            }
        }

        public object Validacion(string cuerpo)
        {
            //int contador = 0;
            //object result = new object();



            //for (int i = 0; i < cuerpo.Length; i++)
            //{

            //    var caracter = cuerpo[i];

            //    if (caracter == '\"')
            //        contador++;
            //}



            //if (cuerpo.Contains("\"") && contador <= 2)
            //{
            //    return result = cuerpo.Replace("\"", " ").Trim();
            //}
            //else
            //{
            //    return JObject.Parse(cuerpo);
            //}

            if (cuerpo.Contains("{") )
            {

                return JObject.Parse(cuerpo);
                
            }
            else
            {
                return cuerpo.Replace("\"", " ").Trim();
            }

        }
    }
}
