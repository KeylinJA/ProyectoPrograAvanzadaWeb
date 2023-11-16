namespace ProyectoWEB.Entities
{
    public class ProductosEnt
    {
        public long IdProducto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public float precio { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
    }
}
