using Microsoft.Extensions.DependencyInjection;
using RBUkraine.DAL;

namespace RBUkraine.BLL
{
    public static class ServiceConfig
    {
        public static void AddBusinessLogicLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessLayer(connectionString);
        }
    }
}