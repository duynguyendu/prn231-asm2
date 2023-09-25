using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessModel;

public class EBookStoreDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var options = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var str = options.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(str);
    }
}