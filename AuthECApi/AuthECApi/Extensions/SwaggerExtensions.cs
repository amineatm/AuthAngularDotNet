using Microsoft.OpenApi.Models;

namespace AuthECApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerExplorer(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Fill in the JWT token",
            });
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Name = "Api-Key",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Description = "Enter your API key",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new List<String>()
                    }
                });

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AuthEC",
                Version = "v1"
            });

        });
        return services;

    }
    public static WebApplication ConfigureSwaggerExplorer(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }
        return app;
    }
}