using Microsoft.EntityFrameworkCore;
using TelegramBot.DAL.EntityFramework.Models;

namespace TelegramBot.DAL.DataAccess
{
    public class TgBotDbContext : DbContext
    {
        public TgBotDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=TgBotDb;Trusted_Connection=True;");
        }


        public DbSet<UserModel> Users { get; set; }
    }
}
