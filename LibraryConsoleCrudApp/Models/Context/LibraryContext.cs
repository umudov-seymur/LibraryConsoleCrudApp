using LibraryConsoleCrudApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryConsoleCrudApp.Models.Context
{
    class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookGenres> BookGenres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-4IDS0G9;" +
                "Initial Catalog=LibraryDB;Integrated Security=true;");
        }
    }
}
