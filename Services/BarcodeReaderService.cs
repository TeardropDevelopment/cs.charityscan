using BootstrapBlazor.Components;
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
        private readonly SwalService swalService;

        public BarcodeReaderService(JsHelperService js_helper_service, SwalService swal) 
        {
            JS = js_helper_service;
            swalService = swal;
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

        public async Task ShowSuccess(string message)
        {
            var op = new SwalOption()
            {
                Category = SwalCategory.Success,
                IsAutoHide = true,
                ShowClose = false,
                Title = message,
                Delay = 1500
            };
            await swalService.Show(op);
        }

        public async Task ShowError(string message)
        {
            var op = new SwalOption()
            {
                Category = SwalCategory.Error,
                IsAutoHide = true,
                ShowClose = false,
                Title = message,
                Delay = 1500
            };
            await swalService.Show(op);
        }

        public async Task ShowModal(bool success, string onSuccessMsg, string onErrorMsg)
        {
            if(success)
            {
                await ShowSuccess(onSuccessMsg);
                return;
            }

            await ShowError(onErrorMsg);
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
