using ProyectoWEB.Entities;

namespace ProyectoWEB.Models
{
    public interface ICarritoModel
    {
        public long RegistrarCarrito(CarritoEnt entidad);

        public List<CarritoEnt>? ConsultarCarrito();

        public string PagarCarrito();

        public int EliminarProductoCarrito(long q);
    }
}
