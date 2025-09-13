using PruebaTecnica.Controllers;
using PruebaTecnica.Models;
using System.Collections.Generic;
using System.Linq;

public class ProductoRepositoryFake : IProductoRepository
{
    private static List<Producto> _productos = new List<Producto>
    {
        new Producto { Id = 1, Nombre = "Teclado", Precio = 15 },
        new Producto { Id = 2, Nombre = "Ratón", Precio = 10 },
        new Producto { Id = 3, Nombre = "Monitor", Precio = 150 }
    };

    public IEnumerable<Producto> ObtenerTodos() => _productos;

    public Producto ObtenerPorId(int id) =>
        _productos.FirstOrDefault(p => p.Id == id);

    public Producto Agregar(Producto producto)
    {
        producto.Id = _productos.Max(p => p.Id) + 1;
        _productos.Add(producto);
        return producto;
    }

    public Producto Actualizar(int id, Producto producto)
    {
        var existente = _productos.FirstOrDefault(p => p.Id == id);
        if (existente == null) return null;

        existente.Nombre = producto.Nombre;
        existente.Precio = producto.Precio;
        return existente;
    }

    public bool Eliminar(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto == null) return false;
        _productos.Remove(producto);
        return true;
    }
}
