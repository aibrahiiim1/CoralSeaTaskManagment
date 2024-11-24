using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace CoralSeaTaskManagment.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSwaggerAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var securityDef = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Jwt Authorization header Using bearer schema."
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityDef);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },new string[]{}
                  }

                });
            });
            return services;
        }
    }
}
