using AuthECApi.Controllers;
using AuthECApi.Extensions;
using AuthECApi.Models;
using AuthECAPI.Controllers;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();

builder.Services.AddSwaggerExplorer()
                .InjectDbContext(config)
                .AddAppConfig(config)
                .AddIdentityHandlersAndStores()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(config);

builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));

var app = builder.Build();

// Get services from DI container
var scope = app.Services.CreateScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

// Seed roles and users
await DbInitializer.SeedRolesAsync(scope.ServiceProvider, roleManager);
await DbInitializer.SeedUsersAsync(userManager, roleManager);

//RUNNING THE APP
app.ConfigureSwaggerExplorer()
    .ConfigureCors(config)
    .AddIdentityAuthMiddlware();

app.MapControllers();
app.MapGroup("/api")
    .MapIdentityApi<AppUser>();
app.MapGroup("/api")
    .MapIdentityUserEndpoint();
app.MapGroup("/api")
.MapAccountEndpoints();

app.Run();