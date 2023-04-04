using BootstrapBlazor.Components;
using CharityScanWebApp.Abstractions;
using CharityScanWebApp.Data;
using MySqlX.XDevAPI; 

namespace CharityScanWebApp.Services
{
    public class APIService : ICharityScanApi
    {

        // Pass the handler to httpclient(from you are calling api)
        private readonly HttpClient client = new HttpClient(new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        })
        {
            BaseAddress = new Uri("https://charityscan/api/v1/")
            //BaseAddress = new Uri("https://192.168.50.5/api/v1/")
        };

        public async Task<bool> AddLapToEventAsync(int eventId, string code_value)
        {
            var response = await client.PostAsync($"Laps?event_id={eventId}&value={code_value}", null);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddLapToEventAsync(int eventId, string code_value, string starterNr)
        {
            var response = await client.PostAsync($"Laps?event_id={eventId}&value={code_value}&starter_nr={starterNr}", null);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UploadLapsAsync(List<OfflineLap>? laps)
        {
            try
            {
                if (laps == null) return true;

                var response = await client.PostAsJsonAsync("laps/bulk", laps);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }

            return false;
        }

    }
}
