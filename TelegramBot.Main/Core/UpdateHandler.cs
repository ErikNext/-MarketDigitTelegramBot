using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using TelegramBot.DAL;
using TelegramBot.Domain;
using TelegramBot.Domain.Commands;

namespace TelegramBot.Main.Core
{
    internal class UpdateHandler
    {
        private Dictionary<string, ICommand> _commands;
        private readonly IUserRepository _userRepository = new UserRepository();

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

            var user = _userRepository.GetOrCreate(chatId, username);

            var commandContext = new CommandContext(botClient, update, user);
            await HandleMessage(commandContext);
        }

        public async Task HandleMessage(CommandContext commandContext)
        {
            _commands.TryGetValue(commandContext.Message, out ICommand command);

            if(command is not null)
            {
                await command.Invoke(commandContext);
            }
            else
            {
                await new HelpCommand().Invoke(commandContext);
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
