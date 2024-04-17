using SyncPointBack.Services.Excel;
using SyncPointBack.Services.ExcelInitiation;

namespace SyncPointBack.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureService(this IServiceCollection service)
        {
            service.AddControllers();
            service.AddScoped<IExcelService, ExcelService>();
            service.AddScoped<ExcelApp>();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();

            return service;
        }
    }
}