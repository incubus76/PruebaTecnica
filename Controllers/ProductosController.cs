using Newtonsoft.Json;
using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PruebaTecnica.Controllers
{
    public class ProductosController : ApiController
    {
        private readonly IProductoRepository _repo;

        public ProductosController(IProductoRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public IHttpActionResult Get() => Ok(_repo.ObtenerTodos());

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var producto = _repo.ObtenerPorId(id);
            return producto == null ? (IHttpActionResult)NotFound() : Ok(producto);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Producto producto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var nuevo = _repo.Agregar(producto);
            return Created($"api/productos/{nuevo.Id}", nuevo);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Producto producto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var actualizado = _repo.Actualizar(id, producto);
            return actualizado == null ? (IHttpActionResult)NotFound() : Ok(actualizado);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var eliminado = _repo.Eliminar(id);
            return !eliminado ? (IHttpActionResult)NotFound() : Ok();
        }


        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        [Route("api/llamar-external")]
        public async Task<IHttpActionResult> LlamarApiExterna()
        {
            string urlExterna = "https://api.ejemplo.com/data"; // URL de la API externa

            try
            {
                // Llamada GET
                HttpResponseMessage response = await client.GetAsync(urlExterna);

                if (!response.IsSuccessStatusCode)
                {
                    return Content(response.StatusCode, "Error al llamar a la API externa");
                }

                string resultado = await response.Content.ReadAsStringAsync();
                return Ok(resultado); // Devuelve la respuesta de la API externa
            }
            catch (HttpRequestException ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/llamar-external-post")]
        public async Task<IHttpActionResult> LlamarApiExterna([FromBody] MiRequest request)
        {
            string urlExterna = "https://api.ejemplo.com/data";

            try
            {
                // Serializar objeto a JSON
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Llamada POST
                HttpResponseMessage response = await client.PostAsync(urlExterna, content);

                if (!response.IsSuccessStatusCode)
                {
                    return Content(response.StatusCode, "Error al llamar a la API externa");
                }

                // Leer respuesta
                string resultado = await response.Content.ReadAsStringAsync();

                // Opcional: deserializar si sabes el tipo de respuesta
                var resultadoObj = JsonConvert.DeserializeObject<MiResponse>(resultado);

                return Ok(resultadoObj);
            }
            catch (HttpRequestException ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/llamar-external-bearer")]
        public async Task<IHttpActionResult> LlamarApiExternaConBearer([FromBody] MiRequest request)
        {
            string urlExterna = "https://api.ejemplo.com/data";
            string token = "TU_TOKEN_BEARER_AQUI"; // Normalmente lo obtienes dinámicamente

            try
            {
                // Serializar el objeto a JSON
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Agregar el Bearer token al header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Llamada POST
                HttpResponseMessage response = await client.PostAsync(urlExterna, content);

                if (!response.IsSuccessStatusCode)
                {
                    return Content(response.StatusCode, "Error al llamar a la API externa");
                }

                string resultado = await response.Content.ReadAsStringAsync();
                var resultadoObj = JsonConvert.DeserializeObject<MiResponse>(resultado);

                return Ok(resultadoObj);
            }
            catch (HttpRequestException ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}