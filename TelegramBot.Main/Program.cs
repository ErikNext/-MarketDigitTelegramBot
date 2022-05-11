using Microsoft.Extensions.Logging;
using TelegramBot.DAL.EntityFramework.DataAccess;
using TelegramBot.Main.Core;

namespace Bot.Api 
{
    internal class Program
    {
        internal static BotManager TelegramBot;

        static void Main(string[] args)
        {
            DbUserRepository db = new DbUserRepository();
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