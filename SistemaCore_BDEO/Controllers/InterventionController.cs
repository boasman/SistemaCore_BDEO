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
    [RoutePrefix("api/v1/Intervention")]
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

                    result = JObject.Parse(cuerpo);

                    return NewMethod(respuesta);

                }
            }
            catch (HttpResponseException e)            
            {


                throw new HttpResponseException(e.Response);
            }

        }

        public IHttpActionResult NewMethod(HttpResponseMessage respuesta)
        {
            switch (respuesta.StatusCode)
            {

                case HttpStatusCode.OK:

                    return Ok(result);

                case HttpStatusCode.Unauthorized:

                    /*"Access Forbidden: Access token not found"*/
                    return Content(HttpStatusCode.Unauthorized, result);


                case HttpStatusCode.NoContent:

                    return Content(HttpStatusCode.NoContent, result);


                case HttpStatusCode.BadRequest:

                    return Content(HttpStatusCode.BadRequest, result);


                case HttpStatusCode.Forbidden:

                    return Content(HttpStatusCode.Forbidden, result);


                case HttpStatusCode.NotFound:

                    return Content(HttpStatusCode.NotFound, result);


                case HttpStatusCode.MethodNotAllowed:

                    return Content(HttpStatusCode.MethodNotAllowed, result);

                case HttpStatusCode.NotImplemented:


                    return Content(HttpStatusCode.NotImplemented, result);

                default:

                    throw new HttpResponseException(respuesta.StatusCode);

            }
        }


        //TODO
        [Route("generate-agent-url")]
        [HttpPost]
        public async Task<IHttpActionResult> Generate_Url(object agent)
        {
            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            using (var client = new HttpClient())
            {
                var key = ConfigurationManager.AppSettings["gen-url"];
                var url = client.BaseAddress = new Uri(key);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("access_token", token);

                var respuesta = await client.PostAsJsonAsync(url, agent);

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

        //Listo
        [HttpPost]
        [Route()]
        public async Task<IHttpActionResult> InterventionPost(object model)
        {
            JObject result = new JObject();
            var token = Request.Headers.GetValues("access_token").FirstOrDefault();

            try
            {
                using (var client = new HttpClient())
                {
                    var url = client.BaseAddress = new Uri(BaseUrl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("access_token", token);

                    var respuesta = await client.PostAsJsonAsync(url, model);

                    switch (respuesta.StatusCode)
                    {

                        case HttpStatusCode.OK:

                            var cuerpo = await respuesta.Content.ReadAsStringAsync();

                             result = JObject.Parse(cuerpo);

                            return Ok(result);

                        case HttpStatusCode.Unauthorized:

                            return Content(HttpStatusCode.Unauthorized, "Access Forbidden: Access token not found");




                        //case HttpStatusCode.NoContent:



                        //    break;

                        case HttpStatusCode.BadRequest:

                            return BadRequest(result.ToString());
                            
                        //case HttpStatusCode.Unauthorized:
                        //    break;

                        //case HttpStatusCode.Forbidden:


                        //    break;
                        case HttpStatusCode.NotFound:

                            return NotFound();


                        //case HttpStatusCode.MethodNotAllowed:
                        //    break;

                        //case HttpStatusCode.NotImplemented:


                        //    break;

                        default:

                            throw new HttpResponseException(respuesta.StatusCode);

                    }
                   
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
        public async Task<IHttpActionResult> users()
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
