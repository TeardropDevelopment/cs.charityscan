namespace CharityScanWebApp.Abstractions
{
    public interface ICharityScanApi
    {
        #region LAPS

        public Task<bool> AddLapToEventAsync(int eventId, string code_value);
        public Task<bool> AddLapToEventAsync(int eventId, string code_value, string starterNr);

        #endregion
    }
}