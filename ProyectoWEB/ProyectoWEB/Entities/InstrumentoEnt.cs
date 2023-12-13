namespace ProyectoWEB.Entities
{
    public class InstrumentoEnt
    {
        public long IdInstrumento { get; set; }

        public string Descripcion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public long IdCategoria { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
        public string Imagen { get; set; } = string.Empty;

    }
}
