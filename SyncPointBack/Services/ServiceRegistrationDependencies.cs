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

namespace SyncPointBack.Services
{
    public static class ServiceRegistrationDependencies
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddAutoMapper(typeof(MapperExcelRecord));
            services.AddScoped<IExcelDownload, ExcelDownloader>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<SyncPointDb, SyncPointDb>();
            services.AddScoped<UnitOfWork, UnitOfWork>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}