using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Domain.Commands
{
    public class CommandContext
    {
        public ITelegramBotClient BotClient { get; }
        public Update Update { get; }
        public User User { get; }
        public string Message => Update?.Message?.Text ?? string.Empty;

        public CommandContext(ITelegramBotClient botClient, Update update, User user)
        {
            BotClient = botClient;
            Update = update;
            User = user;
        }

        public Task SendMessage(string message)
        {
            return BotClient.SendTextMessageAsync(
                      chatId: User.ChatId,
                      text: message);
        }
    }
}
