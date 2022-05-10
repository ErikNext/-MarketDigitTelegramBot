namespace TelegramBot.Domain
{
    public interface IUserRepository
    {
        User GetOrCreate(long chatId, string username);
        User? GetByChatId(long id);
    }
}