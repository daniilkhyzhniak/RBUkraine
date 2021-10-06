using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RBUkraine.DAL.Contexts;

namespace RBUkraine.DAL
{
    public static class ServiceConfig
    {
        public static void AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}