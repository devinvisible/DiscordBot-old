using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NLog;
using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args) => new Program().Run().GetAwaiter().GetResult();
        private static Logger _log = LogManager.GetLogger("Main");

        public async Task Run()
        {
            _log.Info("Reading config");
            var config = Configuration.ReadConfig();

            _log.Info("Starting up bot");
            var client = new DiscordSocketClient(new DiscordSocketConfig
            {
                WebSocketProvider = Discord.Net.Providers.WS4Net.WS4NetProvider.Instance
            });

            await client.LoginAsync(TokenType.Bot, config.Token);
            await client.StartAsync();

            var map = new DependencyMap();
            map.Add(client);

            var handler = new CommandHandler();
            await handler.Install(map);

            // Block this program until it is closed.
            await Task.Delay(-1);
        }

        private static Task logging(LogMessage arg)
        {
            LogLevel level;

            switch (arg.Severity)
            {
                case LogSeverity.Debug:
                    level = LogLevel.Trace;
                    break;
                case LogSeverity.Verbose:
                    level = LogLevel.Debug;
                    break;
                case LogSeverity.Info:
                    level = LogLevel.Info;
                    break;
                case LogSeverity.Warning:
                    level = LogLevel.Warn;
                    break;
                case LogSeverity.Error:
                    level = LogLevel.Error;
                    break;
                case LogSeverity.Critical:
                    level = LogLevel.Fatal;
                    break;
                default:
                    level = LogLevel.Off;
                    break;
            }

            if (arg.Exception == null)
                _log.Log(level, arg.Message);
            else
                _log.Log(level, arg.Exception, arg.Message);
            
            return Task.CompletedTask;
        }
    }
}
