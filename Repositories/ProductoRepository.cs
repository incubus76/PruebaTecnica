using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnica.Controllers
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly MiDbContext _context;

        public ProductoRepository(MiDbContext context)
        {
            _context = context;
        }

        public Producto Actualizar(int id, Producto producto)
        {
            var existente = _context.Productos.Find(id);
            if (existente == null) return null;

            existente.Nombre = producto.Nombre;
            existente.Precio = producto.Precio;
            _context.SaveChanges();
            return existente;
        }

        public Producto Agregar(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return producto;
        }

        public bool Eliminar(int id)
        {
            var p = _context.Productos.Find(id);
            if (p == null) return false;

            _context.Productos.Remove(p);
            _context.SaveChanges();
            return true;
        }

        public Producto ObtenerPorId(int id)
        {
            return _context.Productos.Find(id);
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            return _context.Productos.ToList();
        }
    }
}