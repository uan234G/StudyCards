using Microsoft.EntityFrameworkCore;

namespace StudyCards.Models {
    public class MyContext : DbContext {
        public MyContext (DbContextOptions options) : base (options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<FlashCard> FlashCards { get; set; }
    }
}