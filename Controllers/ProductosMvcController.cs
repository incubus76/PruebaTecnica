using System.Web.Mvc;

namespace PruebaTecnica.Controllers
{
    public class ProductosMvcController : Controller
    {
        private readonly IProductoRepository _repo;

        // Usamos el mismo repositorio que tu API
        public ProductosMvcController()
        {
            _repo = new ProductoRepositoryFake(); // O tu implementación real
        }

        // Acción que llama a la vista
        public ActionResult Index()
        {
            var productos = _repo.ObtenerTodos();
            return View("~/Views/Productos/Index.cshtml", productos);
        }

        // GET: /productos/1
        public ActionResult Detalle(int id)
        {
            var producto = _repo.ObtenerPorId(id);
            if (producto == null)
                return HttpNotFound();

            return View("~/Views/Productos/Detalle.cshtml", producto);
        }
    }
}
