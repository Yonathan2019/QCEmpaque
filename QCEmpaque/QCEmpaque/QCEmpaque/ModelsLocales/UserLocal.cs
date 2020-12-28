namespace QCEmpaque.ModelsLocales
{
    using SQLite;
    public class UserLocal
    {
        [PrimaryKey]
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public string NombreUsuario { get; set; }
        public int IdFinca { get; set; }
        public string Finca { get; set; }
        public string CodeEmpleado { get; set; }
        public int IdEmpacadora { get; set; }
        public override int GetHashCode()
        {
            return IdUsuario;
        }
    }
}
