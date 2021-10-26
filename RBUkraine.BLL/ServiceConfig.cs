﻿using Microsoft.Extensions.DependencyInjection;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Services;
using RBUkraine.DAL;

namespace RBUkraine.BLL
{
    public static class ServiceConfig
    {
        public static void AddBusinessLogicLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessLayer(connectionString);
            services.AddAutoMapper(typeof(Mapper));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAnimalService, AnimalService>();
            services.AddTransient<ICharityEventService, CharityEventService>();
            services.AddTransient<ICharitableOrganizationService, CharitableOrganizationService>();
        }
    }
}