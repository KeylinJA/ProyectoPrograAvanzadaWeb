 using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWEB.Entities;

namespace ProyectoWEB.Models
{
	public interface IProductoModel
	{
        public List<ProductosEnt>? ConsultarProductos();

    }
}
