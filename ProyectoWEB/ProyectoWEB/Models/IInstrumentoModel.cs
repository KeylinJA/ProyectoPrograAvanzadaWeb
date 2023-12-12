using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWEB.Entities;

namespace ProyectoWEB.Models
{
    public interface IInstrumentoModel
    {
		public List<InstrumentoEnt>? ConsultarInstrumentos();
		public List<InstrumentoEnt>? ConsultarPlatillos();
		public List<InstrumentoEnt>? ConsultarBaterias();
		public List<InstrumentoEnt>? ConsultarBajos();
		public List<InstrumentoEnt>? ConsultarGuitarras();
		public List<InstrumentoEnt>? ConsultarPianos();
		public long RegistrarInstrumento(InstrumentoEnt entidad);
		public int ActualizarEstadoInstrumento(InstrumentoEnt entidad);
        public List<SelectListItem>? ConsultarCategorias();
    }
}
