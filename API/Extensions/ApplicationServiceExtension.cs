
using System.Text;
using API.Helpers;
using Aplicacion.UnitOfWork;
using AspNetCoreRateLimit;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services){
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()    //WithOrigins("https://domini.com")
                .AllowAnyMethod()           //WithMethods(*GET", "POST")
                .AllowAnyHeader());         //WithHeaders(*accept*, "content-type")
            });
        
        public static void ConfigureRateLimiting(this IServiceCollection services){
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();
            services.Configure<IpRateLimitOptions>(options=>{
                options.EnableEndpointRateLimiting=true;
                options.StackBlockedRequests=false;
                options.HttpStatusCode=429;
                options.RealIpHeader="X-Real-IP";
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint="*",
                        Period ="20s",
                        Limit=4
                    }
                };

            });
        }

        public static void ConfigureApiVersioning(this IServiceCollection services){
            services.AddApiVersioning(options=>{
                options.DefaultApiVersion=new ApiVersion(1,0);
                options.AssumeDefaultVersionWhenUnspecified=true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("ver"),
                    new HeaderApiVersionReader("X-version")
                );
                options.ReportApiVersions=true;
            });
        }
        

    }

}