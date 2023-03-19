using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebNotesApi.Models.AutorizationModels;
using WebNotesApi.Models.NoteModels;

namespace WebNotesApi.Services
{
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Список пользователей из бд.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Список токенов из бд.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        /// <summary>
        /// Список категорий из бд.
        /// </summary>
        public DbSet<NoteCategory> NoteCategories { get; set; }

        /// <summary>
        /// Список заметок из бд.
        /// </summary>
        public DbSet<Note> Notes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        /// <summary>
        /// Задание значений таблицы при создании бд.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            byte[] passwordHash;
            using (SHA512 shaM = new SHA512Managed())
            {
                passwordHash = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes("12345"));
            }
            modelBuilder.Entity<User>().HasData(
                  new User
                  {
                      Id = 1,
                      Name = "Андрей",
                      Email = "lapardin.andrey@mail.ru",
                      Password = passwordHash,
                      Role = UserRole.Admin.ToString(),
                      CreationTime = DateTime.Now.ToString("dd.MM.yyyy")
                  }
            );
            modelBuilder.Entity<NoteCategory>().HasData(
                  new NoteCategory() { Id = 1, CategoryName = "Work" },
                  new NoteCategory() { Id = 2, CategoryName = "Home" },
                  new NoteCategory() { Id = 3, CategoryName = "HealthAndSport" },
                  new NoteCategory() { Id = 4, CategoryName = "People" },
                  new NoteCategory() { Id = 5, CategoryName = "Documents" },
                  new NoteCategory() { Id = 6, CategoryName = "Finance" },
                  new NoteCategory() { Id = 7, CategoryName = "Various" }
            );
        }
    }
}
