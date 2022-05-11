namespace TelegramBot.Domain.Commands
{
    public class GetRegistrateDateCommand : ICommand
    {
        public string Key { get; } = "/regdate";

        public string Description { get; } = "Дата регистрации";

        public Task Invoke(CommandContext commandContext)
        {
            return commandContext.SendMessage($"Дата регистрации: {commandContext.User.RegistrationDate.ToShortDateString()}");
        }
    }
}
