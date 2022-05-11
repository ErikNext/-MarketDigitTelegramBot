namespace TelegramBot.Domain.Commands
{
    internal class MessageToAllCommand : ICommand
    {
        public string Key { get; } = "/message";

        public string Description { get; } = "Отправить сообщение всем пользователям";


        public async Task Invoke(CommandContext commandContext)
        {
            if (commandContext.Value != String.Empty)
            {
                await commandContext.SendMessageToAll(commandContext.Value);
                await commandContext.SendMessage($"Сообщение \"{commandContext.Value}\" отправленно всем пользователям.");
            }
            else
            {
                await commandContext.SendMessage($"Вы не можете отправить пустое сообщение! Используйте конструкцию:\n/message какой то текст.");
            }
        }
    }
}
