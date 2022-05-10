using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Domain.Commands
{
    public interface ICommand
    {
        string Key { get; }
        string Description { get; }
        Task Invoke(CommandContext commandContext);
    }
}
