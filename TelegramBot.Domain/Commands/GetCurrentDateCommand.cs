namespace TelegramBot.Domain.Commands
{
    public class GetCurrentDateCommand : ICommand
    {
        public string Key { get; } = "/date";

        public string Description { get; } = "Текущая дата";

        public Task Invoke(CommandContext commandContext)
        {
            return commandContext.SendMessage($"Текущая дата: {DateTime.Now.Date.ToShortDateString()}");
        }
    }
}
