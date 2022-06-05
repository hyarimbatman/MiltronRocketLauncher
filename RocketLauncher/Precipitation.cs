namespace RocketLauncher
{
    public class Precipitation
    {
        private double probability;
        private bool rain;
        private bool snow;
        private bool sleet;
        private bool hail;

        public Precipitation(double probability, bool rain, bool snow, bool sleet, bool hail)
        {
            this.probability = probability;
            this.rain = rain;
            this.snow = snow;
            this.sleet = sleet;
            this.hail = hail;
        }

        public double Probability { get => probability; set => probability = value; }
        public bool Rain { get => rain; set => rain = value; }
        public bool Snow { get => snow; set => snow = value; }
        public bool Sleet { get => sleet; set => sleet = value; }
        public bool Hail { get => hail; set => hail = value; }
    }
}