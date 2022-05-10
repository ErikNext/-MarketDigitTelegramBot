using Microsoft.Extensions.Logging;
using TelegramBot.Domain.Commands;

namespace TelegramBot.Main.Core
{
    internal sealed class BotManager
    {
        private UpdateHandler _messageHandler;
        private MessageReciever _messageReciever;
        private ILogger _logger;

        public BotManager(ILogger logger)
        {
            _logger = logger;
            _messageHandler = new UpdateHandler(CommandStorage.Commands, logger);
            _messageReciever = new MessageReciever(_messageHandler.HandleUpdateAsync, _messageHandler.HandleErrorAsync);
        }

        public void Start()
        {
            _messageReciever.StartRecieve(new CancellationTokenSource(), _logger);
        }
    }
}
