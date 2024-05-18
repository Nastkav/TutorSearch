using System.Reflection;
using System.Text.Json.Serialization;
using Domain;
using Domain.Helpers;
using Infra;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Infra.StorageAdapter;
using Microsoft.EntityFrameworkCore;
using Web;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var services = builder.Services;
//Infra
services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));
services.AddDefaultIdentity<UserModel>(o => o.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<AppDbContext>();
services.AddScoped<IStorage, LocalFileStorage>();
services.AddDatabaseDeveloperPageExceptionFilter();

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
app.MapRazorPages(); // Потреба в ідентифікації користувача

app.Run();