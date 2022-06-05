using System.Text;
using Newtonsoft.Json;
using TrainClient.Models;

namespace TrainClient
{
    public class TrainClient
    {
        public async Task<string> GetTrains(string crs)
        {
            var json = await GetJson(crs);
            var result = JsonConvert.DeserializeObject<SearchResult>(json);
            var text = BuildText(result);
            return text;
        }

        private async Task<string> GetJson(string crs)
        {
            using (var client = new HttpClient())
            {
                AddBasicAuthHeader(client);
                var json = await client.GetStringAsync($"https://api.rtt.io/api/v1/json/search/{crs}");
                return json;
            }
        }

        private void AddBasicAuthHeader(HttpClient client)
        {
            var username = Environment.GetEnvironmentVariable("RTT_USER");
            var password = Environment.GetEnvironmentVariable("RTT_PASS");
            if (username == null || password == null) throw new ArgumentException("RTT_USER and/or RTT_PASS are not set.");
            client.DefaultRequestHeaders.Add($"Authorization", $"Basic {Base64Encode($"{username}:{password}")}");
        }

        private static string Base64Encode(string textToEncode)
        {
            byte[] textAsBytes = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(textAsBytes);
        }

        private string BuildText(SearchResult search)
        {
            var output = new StringBuilder();
            output.AppendLine(GetTitle(search.Location.Name));
            if (search.Services == null)
            {
                output.AppendLine(CenterString("NO SERVICES", 32));
                output.AppendLine($"{new String('-', 32)}");
                output.AppendLine(GetFooter());
                return output.ToString();
            }
            foreach (var service in search.Services.Take(20))
            {
                var platform = service.LocationDetail.Platform ?? "??";

                var status = "";
                if (service.LocationDetail.CancelReasonCode != null)
                {
                    status = "CANCELLED";
                }
                else if (service.LocationDetail.RealtimeDeparture != null)
                {
                    status = service.LocationDetail.RealtimeDeparture != service.LocationDetail.GbttBookedDeparture
                        ? $"EXPECTED {service.LocationDetail.RealtimeDeparture}"
                        : "ON TIME";
                }
                else 
                {
                    status = "ON TIME";
                }

                //var status = !service.LocationDetail.RealtimeDepartureActual ? $"EXP {service.LocationDetail.RealtimeDeparture}" : "ON TIME";
                output.AppendLine($"{service.LocationDetail.GbttBookedDeparture} {service.LocationDetail.Destination.First().Description}");
                output.AppendLine($"{service.RunningIdentity} {service.AtocName}");
                output.AppendLine($"Plat {platform.PadRight(3)}{status.PadLeft(24)}");

                if (service.LocationDetail.CancelReasonCode != null)
                {
                    output.AppendLine($"{new String('*', 32)}");
                    output.AppendLine($"Cancelled due to {service.LocationDetail.CancelReasonLongText}");
                    output.AppendLine($"{new String('*', 32)}");
                }

                output.AppendLine($"{new String('-', 32)}");
            }
            output.AppendLine(GetFooter());
            return output.ToString();
        }

        private string GetTitle(string text, int maxWidth = 32)
        {
            var output = new StringBuilder();
            output.AppendLine(new String('-', maxWidth));
            output.AppendLine(CenterString(text, maxWidth));
            output.AppendLine(new String('-', maxWidth));
            return output.ToString();
        }

        private string GetFooter(int maxWidth = 32)
        {
            var output = new StringBuilder();
            output.AppendLine();
            output.AppendLine($"Printed {DateTime.Now.ToString("")}");
            output.AppendLine("Data from Realtime Trains");
            return output.ToString();
        }

        private static string CenterString(string text, int maxWidth)
        {
            var spacesToAdd = (maxWidth - text.Length) / 2;
            return $"{new string(' ', spacesToAdd)}{text}";
        }
    }
}