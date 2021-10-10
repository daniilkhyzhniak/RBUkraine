using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Entities.Enums;

namespace RBUkraine.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddSeedData(this ModelBuilder modelBuilder)
        {
            var userRole = new Role { Id = 1, Name = "User" };
            var adminRole = new Role { Id = 2, Name = "Admin" };

            modelBuilder.Entity<Role>().HasData(userRole, adminRole);

            var admin = new User
            {
                Id = 1,
                Email = "admin1@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin1@email.com")
            };

            var user = new User
            {
                Id = 2,
                Email = "user1@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("user1@email.com"),
            };

            modelBuilder.Entity<User>().HasData(admin, user);

            var userRoles = new List<UserRole>
            {
                new UserRole
                {
                    Id = 1,
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                },
                new UserRole
                {
                    Id = 2,
                    UserId = admin.Id,
                    RoleId = userRole.Id
                },
                 new UserRole
                {
                    Id = 3,
                    UserId = user.Id,
                    RoleId = userRole.Id
                },
            };

            modelBuilder.Entity<UserRole>().HasData(userRoles);

            var animal1 = new Animal
            {
                Id = 1000,
                Name = "Зубр",
                LatinName = "Bison bonasus",
                Description = "Природоохранный статус вида: Пропавший в природе.",
                Population = 200
            };

            modelBuilder.Entity<Animal>().HasData(animal1);

            var animal1Translate = new AnimalTranslate
            {
                Id = 1,
                AnimalId = animal1.Id,
                Name = "Bison",
                Description = "Conservation status of the species: Extinct in nature.",
                Language = Language.English
            };

            modelBuilder.Entity<AnimalTranslate>().HasData(animal1Translate);
        }
    }
}