namespace QCEmpaque.Models.DM
{
    using Newtonsoft.Json;
    public class Empacadora
    {
        [JsonProperty(PropertyName = "PK_Empacadora")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Empacadora")]
        public string Nombre { get; set; }
    }
}
