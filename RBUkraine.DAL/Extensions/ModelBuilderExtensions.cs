using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Entities.Base;
using RBUkraine.DAL.Entities.Enums;

namespace RBUkraine.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddSoftDeletedFilter(this ModelBuilder modelBuilder)
        {
            Expression<Func<BaseEntity, bool>> softDeleteFilter = entity => !entity.IsDeleted;

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsAssignableTo(typeof(BaseEntity)))
                {
                    var parameter = Expression.Parameter(entityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(
                        softDeleteFilter.Parameters.First(),
                        parameter, 
                        softDeleteFilter.Body);

                    entityType.SetQueryFilter(Expression.Lambda(body, parameter));
                }
            }
        }

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

            var charitableOrganization1 = new CharitableOrganization
            {
                Id = 1,
                Name = "Happy Paw",
                Description = "Happy Paw",
                Email = "happypaw@email.com",
                PhoneNumber = "12345"
            };

            modelBuilder.Entity<CharitableOrganization>().HasData(charitableOrganization1);

            var charitableOrganizationImage1 = new CharitableOrganizationImage
            {
                Id = 1,
                Title = "Happy Paw",
                Data = Convert.FromBase64String(Images.CharitableOrganization1),
                CharitableOrganizationId = charitableOrganization1.Id
            };

            modelBuilder.Entity<CharitableOrganizationImage>().HasData(charitableOrganizationImage1);

            var animal1 = new Animal
            {
                Id = 1000,
                Species = "Зубр",
                LatinSpecies = "Bison bonasus",
                CharitableOrganizationId = charitableOrganization1.Id,
                Population = 200
            };

            modelBuilder.Entity<Animal>().HasData(animal1);

            var animal1Translate = new AnimalTranslate
            {
                Id = 1,
                AnimalId = animal1.Id,
                Species = "Bison",
                Language = Language.English
            };

            modelBuilder.Entity<AnimalTranslate>().HasData(animal1Translate);

            var animalImage1 = new AnimalImage
            {
                Id = 1,
                AnimalId = animal1.Id,
                Title = "Зубр",
                Data = Convert.FromBase64String(Images.Animal1)
            };

            modelBuilder.Entity<AnimalImage>().HasData(animalImage1);
        }
    }
}