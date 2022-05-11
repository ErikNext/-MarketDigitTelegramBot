using TelegramBot.Domain;

namespace TelegramBot.DAL
{
    public class MemoryUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>();

        public Task<User> GetOrCreate(long chatId, string username)
        {
            var user = Users.Where(user => user.ChatId == chatId).FirstOrDefault();

            if(user == null)
            {
                user = new User(chatId, username, DateTime.Now);
                Users.Add(user);
            }

            return Task.FromResult<User>(user);
        }

        public Task<User> GetByChatId(long id)
        {
            return Task.FromResult<User>(Users.Where(user => user.ChatId == id).FirstOrDefault());
        }

        public Task<IReadOnlyList<User>> GetAllUser()
        {
            return Task.FromResult<IReadOnlyList<User>>(Users);
        }
    }
}