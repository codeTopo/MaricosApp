using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MaricosApp.Models
{
    public class VentaRequest
    {
        [JsonPropertyName("idcliente")]
        public long IdCliente { get; set; }
        [JsonPropertyName("conceptos")]
        public List<ConceptoRequest> Conceptos { get; set; }
        [JsonPropertyName("pago")]
        public string Pago { get; set; }
    }
    public class ConceptoRequest
    {
        [JsonPropertyName("idProducto")]
        public long IdProducto { get; set; }
        [JsonPropertyName("cantidad")]
        public long Cantidad { get; set; }
        [JsonPropertyName("detalles")]
        public string Detalles { get; set; }
    }

    public class ConceptoList
    {
        public int Contador { get; set; }
        public long IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Detalles { get; set;   }
        public decimal Precio { get; set; }
        public string Total { get; set; }
    }
    
}
