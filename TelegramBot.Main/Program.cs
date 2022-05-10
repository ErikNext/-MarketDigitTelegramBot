using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TelegramBot.Main.Core;

namespace Bot.Api 
{
    internal class Program
    {
        internal static BotManager TelegramBot;

        static void Main(string[] args)
        {
            ILogger logger;
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            logger = loggerFactory.CreateLogger<Program>();

            TelegramBot = new BotManager(logger);
            TelegramBot.Start();
            Console.ReadLine();
        }
    }
}