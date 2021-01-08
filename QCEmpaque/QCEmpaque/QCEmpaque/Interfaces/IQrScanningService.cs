namespace QCEmpaque.Interfaces
{
    using System.Threading.Tasks;
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
