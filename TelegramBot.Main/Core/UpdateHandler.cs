using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using TelegramBot.DAL.EntityFramework.DataAccess;
using TelegramBot.Domain;
using TelegramBot.Domain.Commands;

namespace TelegramBot.Main.Core
{
    internal class UpdateHandler
    {
        private Dictionary<string, ICommand> _commands;
        private readonly IUserRepository _userRepository = new DbUserRepository();

        private readonly ILogger _logger;

        internal UpdateHandler(Dictionary<string, ICommand> commands, ILogger logger)
        {
            _commands = commands;
            _logger = logger;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        
        {
            var chatId = update.Message.From.Id;
            var username = update.Message.From.Username;

            var user = await _userRepository.GetOrCreate(chatId, username);
            var commandContext = new CommandContext(_logger, botClient, _userRepository, update, user);
            
            _logger.LogInformation($"{user.Username} send message {commandContext.Message}");

            await HandleMessage(commandContext);
        }

        public async Task HandleMessage(CommandContext commandContext)
        {
            var commandKeyAndValue = GetKeyCommandAndValue(commandContext.Message);

            _commands.TryGetValue(commandKeyAndValue.Item1, out ICommand command);
            commandContext.Value = commandKeyAndValue.Item2;

            if(command is not null)
            {
                await command.Invoke(commandContext);
            }
            else
            {
                await new HelpCommand().Invoke(commandContext);
            }
        }

        private (string, string) GetKeyCommandAndValue(string message)
        {
            if (message.Contains(' ') is false)
            {
                return (message, String.Empty);
            }
            else
            {
                var spaceIndex = message.IndexOf(' ');
                var commandKey = message.Substring(0, spaceIndex);
                var commandValue = message.Substring(spaceIndex + 1, message.Length - spaceIndex - 1);
                return (commandKey, commandValue);
            }
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
