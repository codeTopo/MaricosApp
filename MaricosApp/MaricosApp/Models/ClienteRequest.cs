using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MaricosApp.Models
{
    public class RespuestaAPi
    {
        [JsonPropertyName("exito")]
        public int Exito { get; set; }

        [JsonPropertyName("mensaje")]
        public string Mensaje { get; set; }

        [JsonPropertyName("data")]
        public ClienteRequest Data { get; set; }
    }
    public class ClienteResponse
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public List<ClienteRequest> Data { get; set; }
    }
    public class ClienteRequest
    {
        [JsonPropertyName("IdCliente")]
        public long IdCliente { get; set; }
        [JsonPropertyName("Nombre")]
        public string nombre { get; set; }
        [JsonPropertyName("Apellido")]
        public string apellido { get; set; }
        [JsonPropertyName("Calle")]
        public string calle { get; set; }
        [JsonPropertyName("Colonia")]
        public string colonia { get; set; }
        [JsonPropertyName("Numero")]
        public string numero { get; set; }
        [JsonPropertyName("Telefono")]
        public string telefono { get; set; }
        [JsonPropertyName("Referencia")]
        public string referencia { get; set; }
    }
}
