using System.Reflection;
using System.Text.Json.Serialization;
using Domain;
using Domain.Helpers;
using Domain.Port.Driving;
using Infra;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Infra.Ports;
using Microsoft.EntityFrameworkCore;
using Web;
using Web.Helpers;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var services = builder.Services;
//Infra
services.AddInfra(connectionString);
services.AddApplication();
services.AddAutoMapper(typeof(DomainMappingProfile));
services.AddDatabaseDeveloperPageExceptionFilter();
services.AddDefaultIdentity<UserModel>(o => o.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<AppDbContext>();
services.AddDbContext<TemplateDbContext>(options => options.UseMySQL(connectionString));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddProblemDetails();
services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
services.AddRazorPages();
var app = builder.Build();
app.Logger.LogInformation($"EnvironmentName: {app.Environment.EnvironmentName}");

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// app.UseExceptionHandler();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // need for identity user 

app.Run();