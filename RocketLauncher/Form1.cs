using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketLauncher
{
    public partial class Form1 : Form
    {
        public LaunchPad launchPad;
        public static Form1 staticForm = null;
        Rocket _CurrentRocket;
        public List<Button> rocketButtons;
        SimpleTCP.SimpleTcpClient tcpClient = null;
        public Form1()
        {
            InitializeComponent();
            rocketButtons = new List<Button>();
            staticForm = this;
            launchPad = new LaunchPad();
            _CurrentRocket = launchPad.launchPadRockets.First();
            tcpClient = new SimpleTCP.SimpleTcpClient();
            tcpClient.StringEncoder = Encoding.UTF8;
            tcpClient.DataReceived += TcpClient_DataReceived;
        }

        private void TcpClient_DataReceived(object sender, SimpleTCP.Message e)
        {
            var message = e.Data;

            var id = string.Concat(message.Skip(1).Take(10).Select(x => (char)x));

            var altitude = BitConverter.ToSingle(message.Skip(13).Take(4).Reverse().ToArray());
            var speed = BitConverter.ToSingle(message.Skip(17).Take(4).Reverse().ToArray());
            var acceleration = BitConverter.ToSingle(message.Skip(21).Take(4).Reverse().ToArray());
            var thrust = BitConverter.ToSingle(message.Skip(25).Take(4).Reverse().ToArray());
            var temperature = BitConverter.ToSingle(message.Skip(29).Take(4).Reverse().ToArray());

            label4.Invoke((MethodInvoker)delegate ()
            {
                if (!message.Skip(13).Take(4).SequenceEqual(message.Skip(25).Take(4))) 
                {
                    label4.Text = $"Id:\t {id}\n" +
                                  $"Altitude:\t {altitude.ToString("N4")}\n" +
                                  $"Speed:\t {speed.ToString("N4")}\n" +
                                  $"Acceleration:\t {acceleration.ToString("N4")}\n" +
                                  $"Thrust:\t {thrust.ToString("N4")}\n" +
                                  $"Temperature:\t {temperature.ToString("N4")}\n";
                }
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateButtons();
            Task.Run(() => { while (true) { launchPad.launchPadWeather.Copy(Commander.CheckWeather()); Thread.Sleep(1000); } });
        }

        private void CreateButtons()
        {
            int i = 0;
            int width = 170;
            int height = 50;
            int xp = label1.Location.X + panel1.Location.X;
            int yp = label1.Location.Y + label1.Height + panel1.Location.Y;
            var rockets = launchPad.launchPadRockets;
            rockets.ForEach(x =>
            {
                ++i;
                Button button = new Button();
                button.Text = $"{x.Id}\n{x.Model}";
                button.Name = $"{x.Id}";
                button.Size = new Size(width, height);
                button.Location = new Point(xp, (yp) * (i));
                button.BackColor = getButtonColor(x.Status);
                button.ForeColor = Color.White;
                button.Click += RocketClicked;
                this.panel1.Controls.Add(button);
                rocketButtons.Add(button);
            });
            label3.Text = JsonConvert.SerializeObject(launchPad.launchPadWeather, Formatting.Indented);
            label2.Text = JsonConvert.SerializeObject(_CurrentRocket, Formatting.Indented);
            cancelButton.BackColor = Color.Red;
            deployButton.BackColor = Color.LightBlue;
            launchButton.BackColor = Color.Green;
            rocketButtons.ForEach(x => {
                if (x.Name == _CurrentRocket.Id) x.Enabled = false;
                tcpClient.Connect(_CurrentRocket.Telemetry.Host == "0.0.0.0" ? "localhost" : _CurrentRocket.Telemetry.Host, (int)_CurrentRocket.Telemetry.Port);

            });
        }

        private void RocketClicked(object sender, EventArgs e)
        {
            
            tcpClient.Disconnect();
            Button pressed = (Button)sender;
            Rocket roc = launchPad.launchPadRockets.Where(x => x.Id == pressed.Name).First();
            _CurrentRocket = roc;
            label2.Text = JsonConvert.SerializeObject(_CurrentRocket, Formatting.Indented);
            tcpClient.Connect(_CurrentRocket.Telemetry.Host == "0.0.0.0" ? "localhost" : _CurrentRocket.Telemetry.Host, (int)_CurrentRocket.Telemetry.Port);
            rocketButtons.ForEach(x => x.Enabled = true);
            pressed.Enabled = false;

        }

        public Color getButtonColor(string status)
        {
            if (status == "launched") return Color.Green;
            if (status == "cancelled") return Color.Red;
            return Color.LightBlue;
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            _CurrentRocket.Copy(Commander.LaunchRocket(_CurrentRocket));
        }

        private void deployButton_Click(object sender, EventArgs e)
        {
            _CurrentRocket.Copy(Commander.DeployRocket(_CurrentRocket));
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _CurrentRocket.Copy(Commander.CancelLaunchRocket(_CurrentRocket));
        }
    }
}
