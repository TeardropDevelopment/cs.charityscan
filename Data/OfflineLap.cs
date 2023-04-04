namespace CharityScanWebApp.Data
{
    public class OfflineLap
    {
        public OfflineLap(int eventId, string? barCodeValue)
        {
            EventId = eventId;
            BarCodeValue = barCodeValue;
        }

        public OfflineLap(int eventId, string? barCodeValue, int startNr)
        {
            EventId = eventId;
            BarCodeValue = barCodeValue;
            StartNr = startNr;
        }

        public int EventId { get; set; }
        public string? BarCodeValue { get; set; }
        public int? StartNr { get; set; } = null;

        

    }
}
