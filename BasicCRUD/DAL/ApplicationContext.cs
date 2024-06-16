using BasicCRUD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicCRUD.DAL;

public class ApplicationContext : DbContext
{
    public DbSet<Note> Notes { get; set; }
    
    public ApplicationContext() : base()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
    }
}