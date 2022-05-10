namespace TelegramBot.Domain.Commands
{
    internal class UserIdCommand : ICommand
    {
        public string Key { get; } = "/id";

        public string Description { get; } = "Узнать свой ID";

        public Task Invoke(CommandContext context)
        {
            return context.SendMessage("Твой Id: " + context.User.ChatId.ToString());
        }
    }
}
