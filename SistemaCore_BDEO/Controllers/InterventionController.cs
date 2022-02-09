using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SistemaCore_BDEO.Controllers
{
    [RoutePrefix("api/Intervention")]
    public class InterventionController : ApiController
    {

        private JObject result = new JObject();

        private string BaseUrl
        {
            get { return ConfigurationManager.AppSettings["intervention"]; }
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(object login)
        {

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    var key = ConfigurationManager.AppSettings["login"];
                    var url = httpCliente.BaseAddress = new Uri(key);

                    var respuesta = await httpCliente.PostAsJsonAsync(url, login);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    return NewMethod(respuesta, cuerpo);

                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }

        }


        //TODO
        [HttpPost]
        [Route("generate-agent-url")]       
        public async Task<IHttpActionResult> Generate_Url(object agent)
        {
            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var client = new HttpClient())
                {
                    var key = ConfigurationManager.AppSettings["gen-url"];
                    var url = client.BaseAddress = new Uri(key);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await client.PostAsJsonAsync(url, agent);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    return NewMethod(respuesta, cuerpo);


                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }

        }

        //Listo
        [HttpPost]        
        [Route()]
        public async Task<IHttpActionResult> Post(object model, [FromUri]string access_token)
        {
            
            //string token = string.Empty;

            
            //token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var client = new HttpClient())
                {
                    
                    Uri url = client.BaseAddress = new Uri(BaseUrl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("access_token", access_token);

                    var respuesta = await client.PostAsJsonAsync(url, model);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();                   

                    return NewMethod(respuesta,cuerpo);

                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }
        }

        //Listo
        [HttpPost]
        [Route]        
        public async Task<IHttpActionResult> Post(object model)
        {

            string token = string.Empty;

            token = Request.Headers.GetValues("access_token").FirstOrDefault();           

            try
            {
                using (var client = new HttpClient())
                {

                    Uri url = client.BaseAddress = new Uri(BaseUrl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await client.PostAsJsonAsync(url, model);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    return NewMethod(respuesta, cuerpo);

                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }
        }


        //Listo
        [HttpGet()]
        [Route("users")]
        public async Task<IHttpActionResult> users(string type)
        {

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var httpCliente = new HttpClient())
                {

                    var key = ConfigurationManager.AppSettings["users"];
                    var url = httpCliente.BaseAddress = new Uri(key);


                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpCliente.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await httpCliente.GetAsync(url);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();                   

                    

                    return NewMethod(respuesta,cuerpo);                    

                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }

        }

        //Listo
        [HttpGet]
        [Route("{limit:int?}/{offset:int?}/{status:int?}")]
        public async Task<IHttpActionResult> InterventionParams(int limit = 100, int offset = 0, int status = 3)
        {
            var uri = BaseUrl + "?limit=" + limit + "&offset=" + offset + "&status=" + status;

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var httpCliente = new HttpClient())
                {

                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = httpCliente.BaseAddress = new Uri(uri);

                    httpCliente.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await httpCliente.GetAsync(url);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    

                    return NewMethod(respuesta,cuerpo);

                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }

        }

        ////Listo
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Intervention_Id(string id)
        {
            var uri = BaseUrl + id;

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    var url = httpCliente.BaseAddress = new Uri(uri);

                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpCliente.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await httpCliente.GetAsync(url);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                   

                    return NewMethod(respuesta,cuerpo);

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
        public async Task<IHttpActionResult> InterventionPut(object model, string id)
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

                    
                    return NewMethod(respuesta,cuerpo);
                    

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
        public async Task<IHttpActionResult> InterventionDelete(string id)
        {
            var uri = BaseUrl + id;

            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var httpCliente = new HttpClient())
                {

                    var url = httpCliente.BaseAddress = new Uri(uri);

                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpCliente.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await httpCliente.DeleteAsync(url);

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();                   

                    return NewMethod(respuesta, cuerpo);

                }
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }

        }

        public IHttpActionResult NewMethod(HttpResponseMessage respuesta, string cuerpo)
        {
            var texto = cuerpo.Replace("\"", " ").Trim();


            switch (respuesta.StatusCode)
            {

                case HttpStatusCode.OK:

                    result = JObject.Parse(cuerpo);
                    return Ok(result);

                case HttpStatusCode.Unauthorized:

                   
                    return Content(HttpStatusCode.Unauthorized, texto);


                case HttpStatusCode.NoContent:

                    result = JObject.Parse(cuerpo);
                    return Content(HttpStatusCode.NoContent, result);


                case HttpStatusCode.BadRequest:

                    
                    result = JObject.Parse(cuerpo);
                    return Content(HttpStatusCode.BadRequest, result);


                case HttpStatusCode.Forbidden:

                    result = JObject.Parse(cuerpo);
                    return Content(HttpStatusCode.Forbidden, result);


                case HttpStatusCode.NotFound:

                    result = JObject.Parse(cuerpo);
                    return Content(HttpStatusCode.NotFound, result);


                case HttpStatusCode.MethodNotAllowed:

                    result = JObject.Parse(cuerpo);
                    return Content(HttpStatusCode.MethodNotAllowed, result);

                case HttpStatusCode.NotImplemented:

                    // TODO: ARREGLAR ESTO
                    result = JObject.Parse(cuerpo);
                    return Content(HttpStatusCode.NotImplemented, result);

                case HttpStatusCode.BadGateway:
                    result = JObject.Parse(cuerpo);
                    return Content(HttpStatusCode.MethodNotAllowed, result);

                default:

                    throw new HttpResponseException(respuesta.StatusCode);

            }
        }
    }
}
