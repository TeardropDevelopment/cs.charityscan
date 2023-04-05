using CharityScanWebApp.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Cryptography.X509Certificates;

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
            int lapsStored = await GetLapCounterAsync() + 1;
            await _localStorage.SetAsync("Saves a list of laps collected while being offline!", "offlineLap" + lapsStored, lap);
            await SetLapCounterAsync(lapsStored);
        }

        public async Task<List<OfflineLap>> GetLapListAsync()
        {

            List<OfflineLap>? lapList = new List<OfflineLap>();
            int lapsStored = await GetLapCounterAsync();
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

        #region Store Lapscounter for individuals

        private async Task SetLapCounterAsync(int n)
        {
            await _localStorage.SetAsync("lapCounter", n);
        }

        private async Task<int> GetLapCounterAsync()
        {
            return (await _localStorage.GetAsync<int>("lapCounter")).Value;
        }

        #endregion

    }
}
