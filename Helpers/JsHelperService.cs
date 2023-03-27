using Microsoft.AspNetCore.Components;
using CharityScanWebApp;
using Microsoft.JSInterop;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace CharityScanWebApp.Helpers
{
    public class JsHelperService
    {
        private readonly IJSRuntime JS;
        private static object CreateDotNetObjectRefSyncObj = new();

        public JsHelperService(IJSRuntime js_runtime)
        {
            JS = js_runtime;
        }

        #region Easier Invokes

        public async Task InvokeVoidAsync(string function)
        {
            await JS.InvokeVoidAsync(function);
        }

        public async Task InvokeVoidAsync(string function, params object[] args)
        {
            await JS.InvokeVoidAsync(function, args);
        }

        public async Task<T> InvokeAsync<T>(string function)
        {
            return await JS.InvokeAsync<T>(function);
        }

        public async Task<T> InvokeAsync<T>(string function, params object[] args)
        {
            return await JS.InvokeAsync<T>(function, args);
        }

        #endregion

        #region Userinteraction

        public async Task Alert(string message)
        {
            await JS.InvokeVoidAsync("alert", message);
        }

        public async Task<string> Prompt(string message)
        {
            return await JS.InvokeAsync<string>("prompt", message);
        }

        #endregion

        #region Callback

        public async Task InvokeCallback(string js_function, string callback, object clazz)
        {
            // Fire & Forget: ConfigureAwait(false) is telling "I'm not expecting this call to return a thing"
            await JS.InvokeAsync<object>(js_function, CreateDotNetObjectRef(clazz), callback).ConfigureAwait(false);
        }

        #endregion

        #region ObjectReference 

        // Hack to fix https://github.com/aspnet/AspNetCore/issues/11159
        public DotNetObjectReference<T> CreateDotNetObjectRef<T>(T value) where T : class
        {
            lock (CreateDotNetObjectRefSyncObj)
            {
                return DotNetObjectReference.Create(value);
            }
        }

        #endregion
    }
}
