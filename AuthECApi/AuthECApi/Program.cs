using AuthECApi.Controllers;
using AuthECApi.Extensions;
using AuthECApi.Models;

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

//RUNNING THE APP

app.ConfigureSwaggerExplorer()
    .ConfigureCors(config)
    .AddIdentityAuthMiddlware();

app.MapControllers();
app.MapGroup("/api")
    .MapIdentityApi<AppUser>();
app.MapGroup("/api")
    .MapIdentityUserEndpoint();

app.Run();