using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CharityScanWebApp.Services
{
    public class SessionService
    {

        private readonly ProtectedSessionStorage sessionStorage;
        public SessionService(ProtectedSessionStorage protectedSessionStorage) 
        {
            sessionStorage = protectedSessionStorage;
        }

        public async Task<bool> IsVolunteer()
        {
            return (await sessionStorage.GetAsync<bool>("IsVolunteer")).Value;
        }

        public async Task<int> GetEventId()
        {
            return (await sessionStorage.GetAsync<int>("VolunteerAtEvent")).Value;
        }

        public async Task RegisterVolunteer(int eventId)
        {
            await sessionStorage.SetAsync("IsVolunteer", true);
            await sessionStorage.SetAsync("VolunteerAtEvent", eventId);
        }
    }
}
