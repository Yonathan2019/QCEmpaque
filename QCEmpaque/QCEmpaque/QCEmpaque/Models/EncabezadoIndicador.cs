namespace QCEmpaque.Models
{
    using SQLite;
    using System;
    public class EncabezadoIndicador
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Guid { get; set; }
        public int FK_Proceso { get; set; }
        public int IdUT { get; set; }
        public int IdVariedad { get; set; }
        public int IdCiclo { get; set; }
        public int IdEvaluador { get; set; }
        public int IdCaporal { get; set; }
        public int IdJornal1 { get; set; }
        public int IdJornal2 { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaSiembra { get; set; }
        public string Usuario { get; set; }
        public double Resultado { get; set; }
        public string Observaciones { get; set; }
        public int IdValvula { get; set; }
        public int IdTamaño { get; set; }
        public int IdCalidad { get; set; }
        public double Metros { get; set; }
        public string Bach { get; set; }
        public DateTime FechaHoraCreo { get; set; }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
