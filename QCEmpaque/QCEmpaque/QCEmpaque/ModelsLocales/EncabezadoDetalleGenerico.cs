namespace QCEmpaque.ModelsLocales
{
    using System;

    public class EncabezadoDetalleGenerico
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int FK_Proceso { get; set; }
        public string Guid { get; set; }
        public string Ut { get; set; }
        public string Caporal { get; set; }
        public string Jornal1 { get; set; }
        public string Jornal2 { get; set; }
        public decimal Calculo1 { get; set; }
        public decimal Calculo2 { get; set; }
        public decimal Calculo3 { get; set; }
        public decimal Valor1 { get; set; }
        public decimal Valor2 { get; set; }
        public decimal Valor3 { get; set; }
        public string Variable1 { get; set; }
        public string Variable2 { get; set; }
        public string Variable3 { get; set; }
        public int IdD { get; set; }
        public int Fila { get; set; }
        public string Valvula { get; set; }

    }
}
