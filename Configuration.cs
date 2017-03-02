using Newtonsoft.Json;
using System.IO;

namespace DiscordBot
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Configuration
    {
        [JsonProperty("token")]
        public string Token { get; set; } = "YOUR TOKEN HERE";
        [JsonProperty("prefix")]
        public string Prefix { get; set; } = "?";

        public static Configuration ReadConfig()
        {
            if (!File.Exists("config.json"))
            {
                File.WriteAllText("config.json", JsonConvert.SerializeObject(new Configuration()));
                throw new FileNotFoundException("Config File Not Found - Generating a template at 'config.json'. Put your bot token in here.");
            }

            var jsonString = File.ReadAllText("config.json");

            return JsonConvert.DeserializeObject<Configuration>(jsonString);
        }
    }
}