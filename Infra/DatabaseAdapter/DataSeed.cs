using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Identity;

namespace Infra.DatabaseAdapter;

public static class DataSeed
{
    public static SubjectModel[] Subjects
    {
        get
        {
            var iSubject = 1;
            var objSubject = File.ReadAllLines("../Infra/Data/subjects_list.csv")
                .Select(line => line.Split(','))
                .Select(x => new SubjectModel
                {
                    Id = iSubject++,
                    Name = x[0]
                })
                .ToArray();
            return objSubject;
        }
    }

    public static CityModel[] Cities
    {
        get
        {
            var iCity = 1;
            var objCity = File.ReadAllLines("../Infra/Data/ukr_cities.csv")
                .Select(line => line.Split(','))
                .Select(x => new CityModel
                {
                    Id = iCity++,
                    Name = x[0],
                    Region = x[1],
                    CreatedAt = DateTime.Parse("2024-01-01")
                })
                .ToArray();
            return objCity;
        }
    }

    public static UserModel[] Users
    {
        get
        {
            var hash = new PasswordHasher<IdentityUser>();

            UserModel[] users =
            [
                new UserModel
                {
                    Id = 1,
                    BirthDate = DateTime.Today.AddYears(-20),
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    Name = "Administrator",
                    Surname = "None",
                    Patronymic = "None",
                    PasswordHash =
                        "AQAAAAIAAYagAAAAED/JcnGip5yrSrBHQH+LmlC7r6Pf1nzvsaAZgxa0Pc25cvBsvI1hAD7lJ+61BoGihQ==",
                    SecurityStamp = "482ea3c3-a1c3-4c83-aced-72e0e5b8808c"
                },
                new UserModel
                {
                    Id = 2,
                    BirthDate = DateTime.Today.AddYears(-25),
                    UserName = "tutor@example.com",
                    NormalizedUserName = "TUTOR@EXAMPLE.COM",
                    Email = "tutor@example.com",
                    NormalizedEmail = "TUTOR@EXAMPLE.COM",
                    EmailConfirmed = true,
                    Name = "Ірина",
                    Surname = "Мельник",
                    Patronymic = "Миколаївна",
                    PasswordHash =
                        "AQAAAAIAAYagAAAAEMfkHhl9MnIURCj0Kd8zbGKDK9t+NX29GB2ZJ7L2iwIYn7j1jbN2yDErYirY8PsRNA==",
                    SecurityStamp = "9cbd20e0-3497-4bbb-95f7-da2f2710e420"
                }
            ];
            return users;
        }
    }

    public static IdentityRole<int>[] Roles =>
    [
        new IdentityRole<int>
        {
            Id = 1,
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR"
        },
        new IdentityRole<int>
        {
            Id = 2,
            Name = "User",
            NormalizedName = "USER"
        }
    ];

    public static IdentityUserRole<int>[] UserRoles =>
    [
        new IdentityUserRole<int>()
        {
            RoleId = Roles[0].Id,
            UserId = Users[0].Id
        },
        new IdentityUserRole<int>()
        {
            RoleId = Roles[1].Id,
            UserId = Users[1].Id
        }
    ];
}