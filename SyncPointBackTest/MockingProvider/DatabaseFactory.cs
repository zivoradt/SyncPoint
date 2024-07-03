using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SyncPointBack.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPointBackTest.MockingProvider
{
    public static class DatabaseFactory
    {
        public static AuthDbContext CreateAuthDbContext()
        {
            var authDbContextOptions = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAuthDbContext")
                .Options;
            return new AuthDbContext(authDbContextOptions);
        }

        public static async SyncPointDb CreateSyncDbContext(bool populateData)
        {
            var fakeConfiguration = A.Fake<IConfiguration>();
            var fakeSyncPointDbLogger = A.Fake<ILogger<SyncPointDb>>();

            var dbContextOptions = new DbContextOptionsBuilder<SyncPointDb>()
                .UseInMemoryDatabase(databaseName: "TestSyncPointDb")
                .Options;

            if (!populateData)
            {
                return new SyncPointDb(dbContextOptions, fakeConfiguration, fakeSyncPointDbLogger);
            }
            var databaseContext = new SyncPointDb(dbContextOptions, fakeConfiguration, fakeSyncPointDbLogger);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.ExcelRecords.CountAsync() <= 0)
            {
            }

            return databaseContext;
        }
    }
}