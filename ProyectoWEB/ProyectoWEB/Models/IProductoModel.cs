using ProyectoWEB.Entities;

namespace ProyectoWEB.Models
{
    public interface IProductoModel
    {
        public List<ProductoEnt>? ConsultarProductos();
        public List<ProductoEnt>? Detalle();
        public List<ProductoEnt>? Carrito();
        public List<ProductoEnt>? Platillos();
        public List<ProductoEnt>? Baterias();
        public List<ProductoEnt>? Bajos();
        public List<ProductoEnt>? Guitarras();
        public List<ProductoEnt>? AudioVideo();
        public List<ProductoEnt>? Viento();
        public List<ProductoEnt>? PianosTeclados();
        public List<ProductoEnt>? Bases();
    }
}
