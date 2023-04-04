using CharityScanWebApp.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Cryptography.X509Certificates;

namespace CharityScanWebApp.Services
{
    public class LocalStorageService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private static int lapsStored = 0;

        public LocalStorageService(ProtectedLocalStorage localStorage) 
        {
            _localStorage = localStorage;
        }

        public async Task AddLapToStorageAsync(OfflineLap lap)
        {
           await _localStorage.SetAsync("Saves a list of laps collected while being offline!", "offlineLap" + lapsStored++, lap);
        }

        public async Task<List<OfflineLap>> GetLapListAsync()
        {

            List<OfflineLap>? lapList = new List<OfflineLap>();
            try
            {
                for (int i = 0; i < lapsStored; i++)
                {
                    OfflineLap? lap = (await _localStorage.GetAsync<OfflineLap>("offlineLap" + i)).Value;
                    if(lap != null)
                        lapList.Add(lap);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            return lapList;
        }
    }
}
