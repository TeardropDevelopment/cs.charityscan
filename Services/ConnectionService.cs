using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Timers;
using static System.Net.WebRequestMethods;

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
        }

        public bool IsConnectedToInternet()
        {
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(connTestUrl, 3000);
                if (reply.Status == IPStatus.Success)
                {
                    IsConnected = true;
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            return IsConnected;
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
    }
}
