using Newtonsoft.Json;

namespace QCEmpaque.Models.DM
{
    public class Clientes
    {
        [JsonProperty(PropertyName = "PK_Cliente")]
        public int Id { get; set; }
        public string CodigoSAP { get; set; }
        public string Nombre { get; set; }
    }
}
