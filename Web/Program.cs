using System.Reflection;
using System.Text.Json.Serialization;
using Domain;
using Domain.Helpers;
using Infra;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Infra.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Web;
using Web.Middlewares;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
//Config
//Infra
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString == null)
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));
services.AddIdentity<UserModel, IdentityRole<int>>(o => o.SignIn.RequireConfirmedEmail = false)
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

services.Configure<SecurityStampValidatorOptions>(o => o.ValidationInterval = TimeSpan.FromSeconds(10));

services.AddTransient<IStorageRepository, StorageRepository>();

services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Forbidden/";
    });

//Domain
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BaseMediatrHandler<,>).Assembly));
services.AddAutoMapper(typeof(DomainMappingProfile));

//Web
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddProblemDetails();
services.AddControllersWithViews()
    .AddJsonOptions(options => //Опція щоб віддавати enum списки не числами, а рядковими значеннями
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
services.AddRazorPages();


//Запуск програми
var app = builder.Build();
app.UseExceptionHandler(); // Should be always in first place 
//Оновлення бази даних
app.UseMigrationsEndPoint();

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

//wwwroot
app.UseStaticFiles();
//Додавання маршрутизації
app.UseRouting();

//User Auth
app.UseAuthentication();
app.UseAuthorization();

//Routing
app.MapControllerRoute("default", "{controller=Profile}/{action=Index}/{id?}");
app.MapRazorPages(); //UserIdentity // Потреба в ідентифікації користувача 

//Deleted
app.UseMiddleware<RemovedRoleMiddleware>();


app.Run();