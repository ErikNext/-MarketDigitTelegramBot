using TelegramBot.DAL.EntityFramework.Models;
using TelegramBot.Domain;

namespace TelegramBot.DAL.EntityFramework.DataAccess
{
    public static class Parser
    {
        public static List<User> ToUsers (this List<UserModel> userModel)
        {
            var users = new List<User>();
            foreach(var user in userModel)
            {
                users.Add(new User(user.ChatId, user.Username, user.RegistrationDate));
            }
            return users;
        }

        public static User ToUser(this UserModel userModel)
        {
            return new User(userModel.ChatId, userModel.Username, userModel.RegistrationDate);
        }

        public static UserModel ToModel(this User user)
        {
            return new UserModel() 
            { 
                ChatId = user.ChatId, 
                RegistrationDate = user.RegistrationDate, 
                Username = user.Username
            };
        }
    }
}
