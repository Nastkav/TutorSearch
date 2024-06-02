using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.DatabaseAdapter;

public class DataSeed
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
        },
        new IdentityRole<int>
        {
            Id = 3,
            Name = "Removed",
            NormalizedName = "REMOVED"
        }
    ];
}