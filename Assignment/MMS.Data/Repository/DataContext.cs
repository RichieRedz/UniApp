
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MMS.Data.Entities;

namespace MMS.Data.Repository;

public class DataContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
      
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder                              
            .UseSqlite("Filename=movies.db")
            // .LogTo(Console.WriteLine, LogLevel.Information)
            ;
    }

    public void Initialise() 
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}
