using Discord;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args) => new Program().Run().GetAwaiter().GetResult();

        public async Task Run()
        {
            var config = Configuration.ReadConfig();

            var client = new DiscordClient();

            client.MessageReceived += async (s, e) =>
            {
                if (!e.Message.IsAuthor)
                    await e.Channel.SendMessage(e.Message.Text);
            };

            client.ExecuteAndWait(async () => {
                await client.Connect(config.Token, TokenType.Bot);
            });

            // Block this task until the program is exited.
            await Task.Delay(-1);
        }
    }
}
