using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLauncher
{
    public class Weather : ISubject
    {
        private List<IObserver> _Observers;
        double temperature;
        double humidity;
        double pressure;
        Precipitation precipitation;
        DateTime time;
        Wind wind;

        public Weather(double temperature, double humidity, double pressure, Precipitation precipitation, DateTime time, Wind wind)
        {
            _Observers = new List<IObserver>();
            this.Temperature = temperature;
            this.Humidity = humidity;
            this.Pressure = pressure;
            this.Precipitation = precipitation;
            this.Time = time;
            this.Wind = wind;
        }

        public double Temperature { get => temperature; set { temperature = value; Notify(); } }
        public double Humidity { get => humidity; set { humidity = value; Notify(); } }
        public double Pressure { get => pressure; set { pressure = value; Notify(); } }
        public DateTime Time { get => time; set { time = value; Notify(); } }
        public Precipitation Precipitation { get => precipitation; set { precipitation = value; Notify(); } }
        public Wind Wind { get => wind; set { wind = value; Notify(); } }

        public void Attach(IObserver observer)
        {
            _Observers.Add(observer);
        }

        public void Notify()
        {
            _Observers.ForEach(o => o.Update(this));
        }

        public void Copy(Weather weather)
        {
            Temperature = weather.temperature;
            Humidity = weather.humidity;
            Pressure = weather.pressure;
            Precipitation = weather.precipitation;
            Time = weather.time;
            Wind = weather.wind;
        }
    }
}
