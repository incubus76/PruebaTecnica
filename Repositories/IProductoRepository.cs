using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Controllers
{
    public interface IProductoRepository
    {
        IEnumerable<Producto> ObtenerTodos();
        Producto ObtenerPorId(int id);
        Producto Agregar(Producto producto);
        Producto Actualizar(int id, Producto producto);
        bool Eliminar(int id);
    }
}
