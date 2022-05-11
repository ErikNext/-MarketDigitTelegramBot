using TelegramBot.DAL.DataAccess;
using TelegramBot.DAL.EntityFramework.Models;
using TelegramBot.Domain;

namespace TelegramBot.DAL.EntityFramework.DataAccess
{
    public class DbUserRepository : IUserRepository
    {
        public Task<IReadOnlyList<User>> GetAllUser()
        {
            using(TgBotDbContext db = new TgBotDbContext())
            {
                var allUsersModels = db.Users.ToList();
                return Task.FromResult<IReadOnlyList<User>>(allUsersModels.ToUsers());
            }
        }

        public Task<User?> GetByChatId(long id)
        {
            using (TgBotDbContext db = new TgBotDbContext())
            {
                var userModel = db.Users.Where(user => user.ChatId == id).FirstOrDefault();
                return Task.FromResult<User?>(userModel.ToUser());
            }
        }

        public Task<User> GetOrCreate(long chatId, string username)
        {
            UserModel userModel;
            using (TgBotDbContext db = new TgBotDbContext())
            {
                userModel = db.Users.Where(user => user.ChatId == chatId).FirstOrDefault();
                if (userModel == null)
                {
                    userModel = new UserModel() 
                    { 
                        ChatId = chatId, 
                        Username = username, 
                        RegistrationDate = DateTime.Now 
                    };

                    db.Add(userModel);
                    db.SaveChanges();
                }
                return Task.FromResult<User>(userModel.ToUser());
            }
        }
    }
}
