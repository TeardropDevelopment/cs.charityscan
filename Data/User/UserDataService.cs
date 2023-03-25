namespace CharityScanWebApp.Data
{
    public class UserDataService
    {
        public bool IsVolunteer { get; set; } = false;
        public string? CodeValue { get; set; }

        public int AthleteId { get; set; }

        public void OnTextChange(string text)
        {
            CodeValue = text;
        }
    }
}