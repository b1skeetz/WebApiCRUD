using BasicCRUD.DAL.Repositories;
using BasicCRUD.Domain.Interfaces;
using BasicCRUD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicCRUD.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBaseRepository<Note>, BaseRepository<Note>>();
        services.AddDbContextFactory<ApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")), 
            ServiceLifetime.Transient);
    }
}