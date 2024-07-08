using Microsoft.EntityFrameworkCore;
using SyncPointBack.Persistance;
using SyncPointBack.Services.Excel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SyncPointBack.DTO.Mapper;
using SyncPointBack.Persistance.Interface;
using System.Text.Json.Serialization;
using ExcelDownload;
using ExcelDownload.Interface;
using Microsoft.AspNetCore.Identity;
using SyncPointBack.Auth.Users;
using Microsoft.OpenApi.Models;
using SyncPointBack.Services.AuthService;

namespace SyncPointBack.Services
{
    public static class ServiceRegistrationDependencies
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.WriteIndented = true;
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
            services.AddAutoMapper(typeof(MapperExcelRecord));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExcelDownload, ExcelDownloader>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<SyncPointDb, SyncPointDb>();
            services.AddScoped<UnitOfWork, UnitOfWork>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }
    }
}