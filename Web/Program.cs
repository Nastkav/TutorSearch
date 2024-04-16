using System.Reflection;
using System.Text.Json.Serialization;
using Domain;
using Domain.Port.Driving;
using Infra;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using Infra.Ports;
using Microsoft.EntityFrameworkCore;
using Web.Helpers;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var services = builder.Services;
//Infra
services.AddInfra(connectionString);
services.AddApplication();
services.AddAutoMapper(typeof(WebMappingProfile));
services.AddDatabaseDeveloperPageExceptionFilter();
services.AddDefaultIdentity<UserModel>(o => o.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
services.AddRazorPages();
var app = builder.Build();

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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // need for identity user 

app.Run();