using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Timers;

namespace CharityScanWebApp.Services
{
    public class ConnectionService
    {
        // PING GOOGLE FOR CONFIRMATION
        private static string connTestUrl = @"google.com";

        private bool _isConnected = true;
        public bool IsConnected 
        {
            get => _isConnected;
            set
            {
                if(_isConnected != value)
                {
                    _isConnected = value;
                    ConnectionStatusChanged(this, _isConnected);
                }   
            }
        }

        public event EventHandler<bool> ConnectionStatusChanged = delegate { };


        public ConnectionService()
        {
            TestConnection();

            // Start timer for periodically check if youre back online
            System.Timers.Timer timer = new(TimeSpan.FromMinutes(0.5f))
            {
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += OnTimerElapsed;
        }

        public void TestConnection()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(connTestUrl);

                switch (reply.Status)
                {
                    case IPStatus.Success:
                        IsConnected = true;
                        break;
                    default:
                        IsConnected = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                IsConnected = false;
            }
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            TestConnection();
        }
    }
}
