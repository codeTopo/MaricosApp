using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MaricosApp.Models
{
    public class ProductoRequest
    {
        [JsonPropertyName("idProducto")]
        public int idProducto { get; set; }
        [JsonPropertyName("nombre")]
        public string nombre { get; set; }
        [JsonPropertyName("desccripcion")]
        public string descripcion { get; set; }
        [JsonPropertyName("precio")]
        public decimal? precio { get; set; }
        [JsonPropertyName("ubicacion")]
        public string ubicacion { get; set; }
        [JsonPropertyName("existencia")]
        public int existencia { get; set; }
    }
    public class ApiResponse
    {
        public int exito { get; set; }
        public string mensaje { get; set; }
        public List<ProductoRequest> data { get; set; }
    }

}
