using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace RocketLauncher
{
    public class Commander : HttpClient
    {
        private static HttpClient _Commander;
        private static string _LaunchSite = "http://localhost:5000";
        private Commander()
        {
            _Commander = new HttpClient();
        }
        private static HttpClient GetCommander()
        {
            if (_Commander == null)
            {
                _Commander = new Commander();
                _Commander.DefaultRequestHeaders.TryAddWithoutValidation("X-API-Key", "API_KEY_1");
            }
            return _Commander;
        }
        private static bool CheckResponse(HttpResponseMessage resp)
        {
            if (resp.StatusCode == HttpStatusCode.ServiceUnavailable) return false;
            return true;
        }
        public static Weather CheckWeather()
        {
            var url = "http://localhost:5000/weather";
            var commander = Commander.GetCommander();
            var resp = commander.GetAsync(url).Result;

            while (!Commander.CheckResponse(resp)) resp = commander.GetAsync(url).Result;

            var result = resp.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<JObject>(result).ToObject<Weather>();
        }
        public static List<Rocket> GetRockets()
        {
            var url = $"{Commander._LaunchSite}/rockets";
            var commander = Commander.GetCommander();
            var resp = commander.GetAsync(url).Result;
            while (!Commander.CheckResponse(resp)) resp = commander.GetAsync(url).Result;
            var result = resp.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<JArray>(result).Select(x => x.ToObject<Rocket>()).ToList();
        }
        public static Rocket LaunchRocket(Rocket rocket)
        {
            var url = $"{Commander._LaunchSite}/rocket/{rocket.Id}/status/launched";
            var commander = Commander.GetCommander();
            var resp = commander.PutAsync(url, null).Result;

            while (!Commander.CheckResponse(resp)) resp = commander.PutAsync(url, null).Result;
            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                rocket.Timestamps.Failed = DateTime.UtcNow;
                return rocket;
            }
            var result = resp.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(result)) rocket = JsonConvert.DeserializeObject<JObject>(result).ToObject<Rocket>();
            return rocket;
        }
        public static Rocket DeployRocket(Rocket rocket)
        {
            var url = $"{Commander._LaunchSite}/rocket/{rocket.Id}/status/deployed";
            var commander = Commander.GetCommander();
            var resp = commander.PutAsync(url, null).Result;
            while (!Commander.CheckResponse(resp)) resp = commander.PutAsync(url, null).Result;
            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                rocket.Timestamps.Failed = DateTime.UtcNow;
                return rocket;
            }
            var result = resp.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(result)) rocket = JsonConvert.DeserializeObject<JObject>(result).ToObject<Rocket>();
            return rocket;
        }
        public static Rocket CancelLaunchRocket(Rocket rocket)
        {
            var url = $"{Commander._LaunchSite}/rocket/{rocket.Id}/status/launched";
            var commander = Commander.GetCommander();
            var resp = commander.DeleteAsync(url).Result;
            while (!Commander.CheckResponse(resp)) resp = commander.PutAsync(url, null).Result;
            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                rocket.Timestamps.Failed = DateTime.UtcNow;
                return rocket;
            }
            var result = resp.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(result)) rocket = JsonConvert.DeserializeObject<JObject>(result).ToObject<Rocket>();
            return rocket;
        }
    }
}