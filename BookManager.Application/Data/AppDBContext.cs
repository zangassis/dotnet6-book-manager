using Microsoft.EntityFrameworkCore;

public class AppDBContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite("DataSource=books.db;Cache=Shared");
}