using CharityScanWebApp.Abstractions;

namespace CharityScanWebApp.Services
{
    public class OfflineAPIService : ICharityScanApi
    {

        private readonly LocalStorageService _storageService;

        public OfflineAPIService(LocalStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<bool> AddLapToEventAsync(int eventId, string code_value)
        {
            try
            {
                await _storageService.AddLapToStorageAsync(new Data.OfflineLap(eventId, code_value));
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddLapToEventAsync(int eventId, string code_value, string starterNr)
        {
            try
            {
                await _storageService.AddLapToStorageAsync(new Data.OfflineLap(eventId, code_value, starterNr));
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
