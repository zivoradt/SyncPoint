using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SyncPointBack.Persistance;
using System;
using System.Threading.Tasks;

namespace SyncPointBack.Persistance
{
    public static class DatabaseConnectionExtensions
    {
        public static async Task<bool> UseDatabaseConnection(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<SyncPointDb>();
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger(string.Empty);

                if (await dbContext.IsDatabaseConnectedAsync())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}