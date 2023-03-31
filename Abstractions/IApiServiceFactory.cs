namespace CharityScanWebApp.Abstractions
{
    public interface IApiServiceFactory
    {
        public ICharityScanApi GetApiService();
    }
}
