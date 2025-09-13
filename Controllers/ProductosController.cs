using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}