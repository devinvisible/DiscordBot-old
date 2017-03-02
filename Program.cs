using System;
using Discord;
using NLog;
using System.Threading.Tasks;
using NLog.Fluent;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args) => new Program().Run().GetAwaiter().GetResult();
        private static Logger _log = LogManager.GetLogger("Main");

        public async Task Run()
        {
            _log.Info("Starting up bot");

            _log.Info("Reading config");
            var config = Configuration.ReadConfig();

            var client = new DiscordClient();
            client.Log.Message += logging;

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

        private static void logging(Object sender, LogMessageEventArgs arg)
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
                default:
                    level = LogLevel.Off;
                    break;
            }

            if (arg.Exception == null)
                _log.Log(level, arg.Message);
            else
                _log.Log(level, arg.Exception, arg.Message);
        }
    }
}
