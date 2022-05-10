namespace TelegramBot.Domain.Commands
{
    public class GetCurrentDayCommand : ICommand
    {
        public string Key { get; } = "/day";

        public string Description { get; } = "Текущий день";

        public Task Invoke(CommandContext commandContext)
        {
            return commandContext.SendMessage($"Текущий день: {DateTime.Now.Day}");
        }
    }
}
