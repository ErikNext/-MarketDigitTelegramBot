using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.Logging;

namespace TelegramBot.Domain.Commands
{
    public class CommandContext
    {
        private ILogger _logger { get; }
        public ITelegramBotClient BotClient { get; }
        public IUserRepository UserRepository { get; }
        public Update Update { get; }
        public User User { get; }
        public string Message => Update?.Message?.Text ?? string.Empty;
        public string Value { get; set; }

        public CommandContext(ILogger logger, ITelegramBotClient botClient, IUserRepository userRepository, Update update, User user)
        {
            _logger = logger;
            BotClient = botClient;
            UserRepository = userRepository;
            Update = update;
            User = user;
        }

        public Task SendMessage(string message)
        {
            _logger.LogInformation($"{User.Username} recieved message: {message}");

            return BotClient.SendTextMessageAsync(
                      chatId: User.ChatId,
                      text: message);
        }

        public async Task SendMessageToAll(string message)
        {
            foreach (var user in await UserRepository.GetAllUser())
            {
                if (user.ChatId != User.ChatId)
                {
                    await BotClient.SendTextMessageAsync(
                          chatId: user.ChatId,
                          text: message);
                }
            }
        }
    }
}
