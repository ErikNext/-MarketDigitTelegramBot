using TelegramBot.Domain;

namespace TelegramBot.DAL
{
    public class UserRepository : IUserRepository
    {
        private List<User> Users = new List<User>
        {
            new User(5222764703, "erker")
        };

        public User GetOrCreate(long chatId, string username)
        {
            var user = GetByChatId(chatId);

            if(user == null)
            {
                Users.Add(new User(chatId, username));
            }

            return user;
        }

        public User? GetByChatId(long id)
        {
            return Users.Where(user => user.ChatId == id).FirstOrDefault();
        }

    }
}