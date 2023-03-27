using CharityScanWebApp.Helpers;
using Microsoft.JSInterop;

namespace CharityScanWebApp.Services
{
    /// <summary>
    /// Proxy between C# and JavaScript for the barcode reader in javascript
    /// </summary>
    public class BarcodeReaderService
    {
        private readonly JsHelperService JS;
        private readonly string htmlElement = "html5QrCode";

        public BarcodeReaderService(JsHelperService js_helper_service) 
        {
            JS = js_helper_service;
        }

        public async Task StartAsync(string callback, object clazz)
        {
            await InvokeProxyAsync("scannerProxy", callback, clazz);
        }

        public async Task StopAsync()
        {
            await InvokeAsync("stop");
        }

        public async Task PauseAsync()
        {
            await InvokeAsync("pause");
        }

        public async Task ResumeAsync()
        {
            await InvokeAsync("resume");
        }


        #region private

        private async Task InvokeAsync(string methodName) 
        {
            await JS.InvokeVoidAsync(methodName);
        }

        private async Task InvokeProxyAsync(string proxy, string callback, object clazz)
        {
            await JS.InvokeCallback(proxy, callback, clazz);
        }

        #endregion
    }
}
