namespace TelegramBot.Domain.Commands
{
    public class GetCurrentTimeCommand : ICommand
    {
        public string Key { get; } = "/time";

        public string Description { get; } = "Текущее время";

        public Task Invoke(CommandContext commandContext)
        {
            return commandContext.SendMessage($"Текущее время: {DateTime.Now.ToShortTimeString()}:{DateTime.Now.TimeOfDay.Seconds}");
        }
    }
}
