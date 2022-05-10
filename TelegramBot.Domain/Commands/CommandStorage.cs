using System.Text;

namespace TelegramBot.Domain.Commands
{
    public class CommandStorage
    {
        public static Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();
        static CommandStorage()
        {
            var typeIBotCommand = typeof(ICommand);
            var typesOfCommands = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && typeIBotCommand.IsAssignableFrom(p) && p != typeof(HelpCommand));

            foreach (var typeOfCommand in typesOfCommands)
            {
                ICommand command = (ICommand)Activator.CreateInstance(typeOfCommand);
                if (Commands.ContainsKey(command.Key))
                {
                    Console.WriteLine($"Ключ для команды {command.Key} имеет дубликат");
                }
                else
                {
                    Commands.Add(command.Key, command);
                }
            }
        }

        public static string GetCommandsDescription()
        {
            var result = new StringBuilder("Доступные команды: \n");

            foreach (var command in Commands.Values)
                result.AppendLine($"{command.Key} - {command.Description}");

            return result.ToString();
        }
    }
}
