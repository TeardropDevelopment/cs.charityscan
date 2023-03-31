using CharityScanWebApp.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CharityScanWebApp.Services
{
    public class LocalStorageService
    {
        private readonly ProtectedLocalStorage _localStorage;

        public LocalStorageService(ProtectedLocalStorage localStorage) 
        {
            _localStorage = localStorage;
        }

        public async Task AddLapToStorageAsync(OfflineLap lap)
        {
            var lapList = await GetLapListAsync();

            lapList?.Add(lap);

           await _localStorage.SetAsync("Saves a list of laps collected while being offline!", "offlineLaps", lapList ?? new List<OfflineLap>());
        }

        public async Task<List<OfflineLap>> GetLapListAsync()
        {
            var lapList = (await _localStorage.GetAsync<List<OfflineLap>>("offlineLaps")).Value;

            if (lapList == null)
                lapList = new List<OfflineLap>();

            return lapList;
        }
    }
}
