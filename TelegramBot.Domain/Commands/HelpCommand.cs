namespace TelegramBot.Domain.Commands
{
    public class HelpCommand : ICommand
    {
        public string Key { get; } = "/help";

        public string Description { get; } = "Помощь";

        public Task Invoke(CommandContext context)
        {
            return context.SendMessage(CommandStorage.GetCommandsDescription());
        }
    }
}
