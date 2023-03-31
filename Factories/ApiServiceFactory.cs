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
        public ApiServiceFactory(APIService apiService, OfflineAPIService offlineAPIService, ConnectionService connectionService)
        {
            _apiService = apiService;
            _offlineApiService = offlineAPIService;
            _connection = connectionService;
        }
        public ICharityScanApi GetApiService()
        {
            if (_connection.IsConnected)
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