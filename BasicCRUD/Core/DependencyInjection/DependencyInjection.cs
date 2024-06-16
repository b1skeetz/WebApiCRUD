using BasicCRUD.Core.Services;
using BasicCRUD.Domain.Interfaces.Services;

namespace BasicCRUD.Core.DependencyInjection;

public static class DependencyInjection
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
    }
}