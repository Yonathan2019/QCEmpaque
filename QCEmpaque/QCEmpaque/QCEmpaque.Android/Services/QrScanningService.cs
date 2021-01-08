using QCEmpaque.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(QCEmpaque.Droid.Services.QrScanningService))]

namespace QCEmpaque.Droid.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            //var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();
            var code = string.Empty;

            optionsCustom.PossibleFormats = new List<ZXing.BarcodeFormat>
            {
                ZXing.BarcodeFormat.QR_CODE,
                ZXing.BarcodeFormat.CODE_128,
                ZXing.BarcodeFormat.EAN_13
            };

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Acercar la camara al codigo",
                BottomText = "Toca la pantalla para enfocar el codigo",
                CancelButtonText = "Cancelar"                
            };            

            var scanResult = await scanner.Scan(optionsCustom);
            if (scanResult != null)
            {
                code = scanResult.Text;
            }

            return code;
        } 
        
    }
}