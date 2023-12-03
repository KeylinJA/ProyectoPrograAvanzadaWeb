using ProyectoWEB.Entities;

namespace ProyectoWEB.Models
{
    public interface IProductoModel
    {
        public List<ProductoEnt>? ConsultarProductos();
    }
}
