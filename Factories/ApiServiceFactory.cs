using CharityScanWebApp.Abstractions;
using CharityScanWebApp.Services;
using Microsoft.AspNetCore.Authentication;

namespace CharityScanWebApp.Factories
{
    public class ApiServiceFactory : IApiServiceFactory
    {
        private readonly APIService _apiService;
        private readonly OfflineAPIService _offlineApiService;
        private readonly ConnectionService _connection;

        private bool _isConnected;

        public ApiServiceFactory(APIService apiService, OfflineAPIService offlineAPIService, ConnectionService connectionService)
        {
            _apiService = apiService;
            _offlineApiService = offlineAPIService;
            _connection = connectionService;

            _connection.ConnectionStatusChanged += OnConnectionChanged;
        }

        private async void OnConnectionChanged(object? sender, bool e)
        {
            _isConnected = e;
            if(_isConnected)
                await _apiService.UploadLapsAsync(await _offlineApiService.GetLapsAsync());
        }

        public ICharityScanApi GetApiService()
        {
            if (_connection.IsConnectedToInternet())
            {
                return _apiService;
            }
            else
            {
                return _offlineApiService;
            }
        }
    }
}