namespace QCEmpaque.Models
{
    using Newtonsoft.Json;
    public class Usuario
    {
        [JsonProperty(PropertyName = "PK_Usuario")]
        public int IdUsuario { get; set; }
        public object Nombre { get; set; }

        [JsonProperty(PropertyName = "FK_Perfil")]
        public int IdPerfil { get; set; }
        public int Activo { get; set; }
        [JsonProperty(PropertyName = "Usuario")]
        public string NombreUsuario { get; set; }
        public object Password { get; set; }
        public object UsuarioCreo { get; set; }
        public object Modulo { get; set; }

        [JsonProperty(PropertyName = "Fk_Ubicacion")]
        public int IdFinca { get; set; }

        [JsonProperty(PropertyName = "Fk_Empleado")]
        public int IdEmpleado { get; set; }
        public bool Dashboard { get; set; }

        [JsonProperty(PropertyName = "Fk_Distrito")]
        public int IdDistrito { get; set; }
        public object Distrito { get; set; }
        public string Finca { get; set; }

        [JsonProperty(PropertyName = "Fk_Empacadora")]
        public int IdEmpacadora { get; set; }
        public string Empacadora { get; set; }
        public object CodigoEmpacadora { get; set; }

        [JsonProperty(PropertyName = "Codigo")]
        public string CodeEmpleado { get; set; }

    }
}
