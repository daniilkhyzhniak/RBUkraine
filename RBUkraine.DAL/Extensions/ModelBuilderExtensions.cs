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

            var hearts = new CharitableOrganization
            {
                Id = 123,
                Name = "Маленькі серця",
                Description = "Маленькі серця",
                Email = "bf_zah_tv_RBU@gmail.com",
                PhoneNumber = "+380 (66)-232-47-12",
                Founders = "Зінченко Владислав Олегович, Кузнець Марина Геннадіївна",
                Stockholders = "Зінченко Владислав Олегович, Кузнець Марина Геннадіївна",
                FoundationDate = new DateTimeOffset(2009, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            var happyPaw = new CharitableOrganization
            {
                Id = 12312,
                Name = "Happy Paw",
                Description = "Happy Paw",
                Email = "bf_zah_tv_RBU@gmail.com",
                PhoneNumber = "+380 (66)-232-47-12",
                Founders = "Зінченко Владислав Олегович, Кузнець Марина Геннадіївна",
                Stockholders = "Зінченко Владислав Олегович, Кузнець Марина Геннадіївна",
                FoundationDate = new DateTimeOffset(2009, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            var cf = new CharitableOrganization
            {
                Id = 2344,
                Name = "БФ “Захист тварин Червоної книги України”",
                Description = "БФ “Захист тварин Червоної книги України",
                Email = "bf_zah_tv_RBU@gmail.com",
                PhoneNumber = "+380 (66)-232-47-12",
                Founders = "Зінченко Владислав Олегович, Кузнець Марина Геннадіївна",
                Stockholders = "Зінченко Владислав Олегович, Кузнець Марина Геннадіївна",
                FoundationDate = new DateTimeOffset(2009, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            modelBuilder.Entity<CharitableOrganization>().HasData(
                hearts, happyPaw, cf);

            var charitableOrganizationImage1 = new CharitableOrganizationImage
            {
                Id = 124,
                Title = "Happy Paw",
                Data = Convert.FromBase64String(Images.HappyPaw),
                CharitableOrganizationId = happyPaw.Id
            };

            var charitableOrganizationImage2 = new CharitableOrganizationImage
            {
                Id = 12412,
                Title = "БФ “Захист тварин Червоної книги України",
                Data = Convert.FromBase64String(Images.CharitableOrganization2),
                CharitableOrganizationId = cf.Id
            };

            var charitableOrganizationImage3 = new CharitableOrganizationImage
            {
                Id = 4444,
                Title = "Маленькі серця",
                Data = Convert.FromBase64String(Images.CharitableOrganization1),
                CharitableOrganizationId = hearts.Id
            };

            modelBuilder.Entity<CharitableOrganizationImage>().HasData(
                charitableOrganizationImage1, charitableOrganizationImage2, charitableOrganizationImage3);

            var animal1 = new Animal
            {
                Id = 1000,
                Species = "Зубр",
                LatinSpecies = "Bison bonasus",
                CharitableOrganizationId = happyPaw.Id,
                Population = 200
            };

            var animal2 = new Animal
            {
                Id = 1001,
                Species = "Бурий ведмідь",
                LatinSpecies = "Ursus arctos",
                CharitableOrganizationId = hearts.Id,
                Population = 300
            };

            var animal3 = new Animal
            {
                Id = 1002,
                Species = "Рись",
                LatinSpecies = "Lynx",
                CharitableOrganizationId = cf.Id,
                Population = 400
            };

            modelBuilder.Entity<Animal>().HasData(animal1, animal2, animal3);

            var animal1Translate = new AnimalTranslate
            {
                Id = 1,
                AnimalId = animal1.Id,
                Species = "Bison",
                Language = Language.English
            };

            var animal2Translate = new AnimalTranslate
            {
                Id = 2,
                AnimalId = animal2.Id,
                Species = "Brown bear",
                Language = Language.English
            };

            modelBuilder.Entity<AnimalTranslate>().HasData(
                animal1Translate, animal2Translate);

            var animalImage1 = new AnimalImage
            {
                Id = 1,
                AnimalId = animal1.Id,
                Title = "Зубр",
                Data = Convert.FromBase64String(Images.Animal1)
            };

            var animalImage2 = new AnimalImage
            {
                Id = 2,
                AnimalId = animal2.Id,
                Title = "Бурий ведмідь",
                Data = Convert.FromBase64String(Images.Animal2)
            };

            var animalImage3 = new AnimalImage
            {
                Id = 3,
                AnimalId = animal3.Id,
                Title = "Рись",
                Data = Convert.FromBase64String(Images.Lynx)
            };

            modelBuilder.Entity<AnimalImage>().HasData(
                animalImage1, animalImage2, animalImage3);

            var charityEvent1 = new CharityEvent
            {
                Id = 1,
                Name = "Благодійний захід присвячений 30 річниці фонду \"Маленькі серця\"",
                DateTime = new DateTimeOffset(2021, 11, 14, 13, 0, 0, TimeSpan.Zero),
                Description =
                    "Захід присвячено підтримці виду Бурого медведя від Благодійних фондів “Маленькі серця” та “Happy Paw”.",
                Organizer = "Зінченко Владислав Олегович",
                Price = 113
            };

            var charityEvent2 = new CharityEvent
            {
                Id = 2,
                Name = "Благодійний захід присвячений підтримці виду Бурий ведмідь",
                DateTime = new DateTimeOffset(2021, 12, 24, 17, 0, 0, TimeSpan.Zero),
                Description =
                    "Захід присвячено підтримці виду Бурого медведя від Благодійних фондів.",
                Organizer = "Зінченко Владислав Олегович",
                Price = 0
            };

            modelBuilder.Entity<CharityEvent>().HasData(
                charityEvent1, charityEvent2);
        }
    }
}