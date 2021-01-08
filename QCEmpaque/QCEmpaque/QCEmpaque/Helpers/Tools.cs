namespace QCEmpaque.Helpers
{
    using QCEmpaque.Models;
    using QCEmpaque.Models.DM;
    using QCEmpaque.Services;
    using System.Collections.Generic;
    using System.Linq;

    public class Tools
    {
        private DataService dataService;

        private readonly static Tools _tools = new Tools();
        public Tools()
        {
            dataService = new DataService();
        }
        public static Tools GetInstance
        {
            get { return _tools; }
        }

        public EncabezadoIndicador GetIndicadorByBach(string bach)
        {
            var data = dataService.Get<EncabezadoIndicador>(false);
            return data.AsEnumerable().FirstOrDefault(l => l.Bach == bach);             
        }
        public List<Estante> GetEstanteByEmpacadora()
        {
            return dataService.Get<Estante>(false).Where(e => e.IdEmpacadora == Settings.IdEmpacadora).ToList();
        }
    }
}
