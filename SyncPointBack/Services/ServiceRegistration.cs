using Microsoft.EntityFrameworkCore;
using SyncPointBack.Persistance;
using SyncPointBack.Services.Excel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SyncPointBack.DTO.Mapper;

namespace SyncPointBack.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(MapperExcelRecord));
            services.AddScoped<IExcelService, ExcelService>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}