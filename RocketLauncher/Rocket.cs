
using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLauncher
{
    public class Rocket : ISubject
    {
        private List<IObserver> _Observers;

        private string id;
        private string model;
        private int mass;
        private Payload payload;
        private Telemetry telemetry;
        private string status;
        private Timestamps timestamps;
        private double altitude;
        private double speed;
        private double acceleration;
        private int thrust;
        private double temperature;

        public Rocket(string id, string model, int mass, Payload payload, Telemetry telemetry, string status, Timestamps timestamps, double altitude, double speed, double acceleration, int thrust, double temperature)
        {
            _Observers = new List<IObserver>();
            this.id = id;
            this.model = model;
            this.mass = mass;
            this.payload = payload;
            this.telemetry = telemetry;
            this.status = status;
            this.timestamps = timestamps;
            this.altitude = altitude;
            this.speed = speed;
            this.acceleration = acceleration;
            this.thrust = thrust;
            this.temperature = temperature;
        }

        public string Id { get => id; set { id = value; Notify(); } }
        public string Model { get => model; set { model = value; Notify(); } }
        public int Mass { get => mass; set { mass = value; Notify(); } }
        public string Status { get => status; set { status = value; Notify(); } }
        public double Altitude { get => altitude; set { altitude = value; Notify(); } }
        public double Speed { get => speed; set { speed = value; Notify(); } }
        public double Acceleration { get => acceleration; set { acceleration = value; Notify(); } }
        public int Thrust { get => thrust; set { thrust = value; Notify(); } }
        public double Temperature { get => temperature; set { temperature = value; Notify(); } }
        public Payload Payload { get => payload; set { payload = value; Notify(); } }
        public Telemetry Telemetry { get => telemetry; set { telemetry = value; Notify(); } }
        public Timestamps Timestamps { get => timestamps; set { timestamps = value; Notify(); } }

        public void Attach(IObserver observer)
        {
            _Observers.Add(observer);
        }
        public void Notify()
        {
            _Observers.ForEach(O => O.Update(this));
        }
        public void Copy(Rocket rocket)
        {
            this.Id = rocket.id;
            this.Model = rocket.model;
            this.Mass = rocket.mass;
            this.Payload = rocket.payload;
            this.Telemetry = rocket.telemetry;
            this.Status = rocket.status;
            this.Timestamps = rocket.timestamps;
            this.Altitude = rocket.altitude;
            this.Speed = rocket.speed;
            this.Acceleration = rocket.acceleration;
            this.Thrust = rocket.thrust;
            this.Temperature = rocket.temperature;
        }
    }
}