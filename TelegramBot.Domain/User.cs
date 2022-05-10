namespace TelegramBot.Domain
{
    public class User
    {
        public long ChatId { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }

        public User(long chatId, string username)
        {
            ChatId = chatId;
            Username = username;

            RegistrationDate = DateTime.Now;
        }
    }
}
