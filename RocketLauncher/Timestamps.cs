using System;

namespace RocketLauncher
{
    public class Timestamps
    {
        private DateTime launched;
        private DateTime deployed;
        private DateTime failed;
        private DateTime cancelled;

        public Timestamps(String launched, String deployed, String failed, String cancelled)
        {
            if (launched != null) this.launched = DateTime.ParseExact(launched, "MM/dd/yyyy HH:mm:ss", null);
            if (deployed != null) this.deployed = DateTime.ParseExact(deployed, "MM/dd/yyyy HH:mm:ss", null);
            if (failed != null) this.failed = DateTime.ParseExact(failed, "MM/dd/yyyy HH:mm:ss", null);
            if (cancelled != null) this.cancelled = DateTime.ParseExact(cancelled, "MM/dd/yyyy HH:mm:ss", null);
        }

        public DateTime Launched { get => launched; set => launched = value; }
        public DateTime Deployed { get => deployed; set => deployed = value; }
        public DateTime Failed { get => failed; set => failed = value; }
        public DateTime Cancelled { get => cancelled; set => cancelled = value; }
    }
}