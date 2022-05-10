using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TelegramBot.Main.Core
{
    internal sealed class MessageReciever
    {
        private Func<ITelegramBotClient, Update, CancellationToken, Task> _handleUpdateAsync;
        private Func<ITelegramBotClient, Exception, CancellationToken, Task> _handleErrorAsync;

        public MessageReciever(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync,
            Func<ITelegramBotClient, Exception, CancellationToken, Task> handleErrorAsync)
        {
            _handleUpdateAsync = handleUpdateAsync;
            _handleErrorAsync = handleErrorAsync;
        }

        public void StartRecieve(CancellationTokenSource cancellationTokenSource, ILogger logger)
        {
                #warning DELETE THIS
            var botClient = new TelegramBotClient("5297582960:AAEuDqrstcxoLMgQdqN6j0gdXcmHhnpi_IY");

            var _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            botClient.StartReceiving(_handleUpdateAsync, _handleErrorAsync, _receiverOptions, cancellationToken: cancellationTokenSource.Token);
            logger.LogInformation("Connection to telegram bot");
        }
    }
}
