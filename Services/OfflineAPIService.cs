using CharityScanWebApp.Abstractions;
using CharityScanWebApp.Data;

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
                if (int.TryParse(starterNr, out var startNr))
                    await _storageService.AddLapToStorageAsync(new Data.OfflineLap(eventId, code_value, startNr));
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<List<OfflineLap>> GetLapsAsync()
        {
            try
            {
                return await _storageService.GetLapListAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }

            return null;
        }
    }
}
