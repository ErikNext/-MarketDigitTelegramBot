namespace TelegramBot.Domain
{
    public interface IUserRepository
    {
        Task<User> GetOrCreate(long chatId, string username);
        Task<User?> GetByChatId(long id);
        Task<IReadOnlyList<User>> GetAllUser();
    }
}