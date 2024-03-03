using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaricosApp.Models
{
    public  class UsuarioRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
