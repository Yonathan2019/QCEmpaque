namespace QCEmpaque.Models.DM
{
    using SQLite;
    public class Estante
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEmpacadora { get; set; }
    }
}
