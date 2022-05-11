using System.ComponentModel.DataAnnotations;

namespace TelegramBot.DAL.EntityFramework.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public long ChatId { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
