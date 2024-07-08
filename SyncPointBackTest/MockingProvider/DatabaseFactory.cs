using DocumentFormat.OpenXml.InkML;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SyncPointBack.Auth.Users;
using SyncPointBack.Model.Excel;
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

        public static async Task<SyncPointDb> CreateSyncDbContext(bool populateData)
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
            else
            {
                var databaseContext = new SyncPointDb(dbContextOptions, fakeConfiguration, fakeSyncPointDbLogger);

                return await PopulateDatabase(databaseContext);
            }
        }

        public static async Task<SyncPointDb> PopulateDatabase(SyncPointDb context)
        {
            context.Database.EnsureCreated();

            if (await context.ExcelRecords.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    context.Add(
                        new ExcelRecord()
                        {
                            UserId = (i + 10).ToString(),
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            TicketId = $"{i}",
                            ProductionTime = 1,
                            StaticPageCreation = new StaticPageCreation
                            {
                                staticPageCreation = new List<StaticPageCreationList>
                                {
                                    StaticPageCreationList.PushingPageLive,
                                    StaticPageCreationList.CopyCreation
                                }
                            },
                            StaticPageModification = new StaticPageModification
                            {
                                staticPageModification = new List<StaticPageModificationList>
                                {
                                    StaticPageModificationList.BreadcrumbUpdate
                                },
                            },
                            Other = $"Other details {i}",
                            NumOfPages = 5 + i,
                            NumOfChanges = 10 + i,
                            Description = $"Description {i}"
                        });
                    await context.SaveChangesAsync();
                }
            }

            return context;
        }

        public static async Task<AuthDbContext> CreateAuthDbContext(bool populateData)
        {
            var authDbContextOptions = new DbContextOptionsBuilder<AuthDbContext>()
               .UseInMemoryDatabase(databaseName: "TestAuthDbContext")
               .Options;

            if (!populateData)
            {
                return new AuthDbContext(authDbContextOptions);
            }
            else
            {
                var authDbContext = new AuthDbContext(authDbContextOptions);
                return await PopulateAuthDatabase(authDbContext);
            }
        }

        private static async Task<AuthDbContext> PopulateAuthDatabase(AuthDbContext authDbContext)
        {
            authDbContext.Database.EnsureCreated();

            var testUser = new ApplicationUser
            {
                UserName = "test",
                Email = "test@gmail.com"
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            testUser.PasswordHash = passwordHasher.HashPassword(testUser, "test123");

            authDbContext.Users.Add(testUser);
            await authDbContext.SaveChangesAsync();

            return authDbContext;
        }
    }
}