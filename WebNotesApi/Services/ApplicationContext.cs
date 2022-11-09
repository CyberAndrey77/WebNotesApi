using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebNotesApi.Models;

namespace WebNotesApi.Services
{
    public class ApplicationContext : DbContext
    {
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
