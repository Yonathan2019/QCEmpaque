namespace QCEmpaque.ModelsLocales
{
    using SQLite;
    public class MenuLocal
    {
        [PrimaryKey]
        public int IdMenu { get; set; }
        public string NameMenu { get; set; }
        public override int GetHashCode()
        {
            return IdMenu;
        }
    }
}
