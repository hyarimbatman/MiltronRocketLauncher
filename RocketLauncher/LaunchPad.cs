using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketLauncher
{
    public class LaunchPad : IObserver
    {
        public Weather launchPadWeather = Commander.CheckWeather();
        public List<Rocket> launchPadRockets = Commander.GetRockets();

        public LaunchPad()
        {
            launchPadWeather.Attach(this);
            launchPadRockets.ForEach(r => r.Attach(this));
        }


        public void Update(ISubject subject)
        {
            if (subject is Weather weather)
            {
                Form1.staticForm.label3.Invoke((MethodInvoker)delegate () {
                    Form1.staticForm.label3.Text = JsonConvert.SerializeObject(weather, Formatting.Indented);
                });
            }

            if (subject is Rocket rocket)
            {
                
                Form1.staticForm.label2.Invoke((MethodInvoker)delegate () {
                    Form1.staticForm.label2.Text = JsonConvert.SerializeObject(rocket,Formatting.Indented)  ;
                });
                Form1.staticForm.rocketButtons.Where(x => x.Name == rocket.Id).First().BackColor = Form1.staticForm.getButtonColor(rocket.Status);
            }
        }

    }
}
