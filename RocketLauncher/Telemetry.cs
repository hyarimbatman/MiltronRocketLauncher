namespace RocketLauncher
{
    public class Telemetry
    {
        private string host;
        private int port;


        public Telemetry(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public string Host { get => host; set => host = value; }
        public int Port { get => port; set => port = value; }
    }
}