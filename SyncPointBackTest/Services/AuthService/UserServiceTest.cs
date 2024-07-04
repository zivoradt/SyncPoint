using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SyncPointBack.Auth.Users;
using SyncPointBack.Auth;
using SyncPointBack.Persistance;
using SyncPointBack.Services.AuthService;
using System;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SyncPointBack.Auth.Requests;
using SyncPointBackTest.MockingProvider;
using SyncPointBack.Model.Excel;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Xunit.Abstractions;
using FluentAssertions;

namespace SyncPointBackTest.Services.AuthService
{
    public class UserServiceTest
    {
        private UserService _userService;
        private IOptions<AppSettings> _fakeAppSettings;
        private UnitOfWork _fakeUnitOfWork;
        private SyncPointDb _fakeSyncPointDb;
        private AuthDbContext _fakeAuthDbContext;
        private ILogger<UserService> _fakeLogger;
        private UserManager<ApplicationUser> _fakeUserManager;
        private readonly ITestOutputHelper _output;

        public UserServiceTest(ITestOutputHelper output)
        {
            _output = output;
            PopulateDatabase();

            // Create UnitOfWork instance
            _fakeUnitOfWork = new UnitOfWork(_fakeSyncPointDb, _fakeAuthDbContext);

            // Mocking other dependencies
            _fakeLogger = A.Fake<ILogger<UserService>>();

            // Mocking UserManager
            _fakeUserManager = UserManagerFactory.CreateUserManager(_fakeAuthDbContext);

            var appSettings = new AppSettings { Secret = "test_secret" }; // Adjust as per your AppSettings structure
            _fakeAppSettings = Options.Create(appSettings);

            // Create UserService instance
            _userService = new UserService(_fakeAppSettings, _fakeUnitOfWork, _fakeLogger, _fakeUserManager);
        }

        private async Task PopulateDatabase()
        {
            _fakeSyncPointDb = await DatabaseFactory.CreateSyncDbContext(true);

            _fakeAuthDbContext = await DatabaseFactory.CreateAuthDbContext(true);
        }

        [Fact]
        public async Task RegisterUser_ValidRequest_Success()
        {
            // Arrange: Create a valid registration request
            var request = new RegistrationRequest
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Test123!"
            };

            // Act: Call the method under test
            await _userService.RegisterUser(request);

            // Assert: Verify the behavior or state changes
            A.CallTo(() => _fakeUserManager.CreateAsync(A<ApplicationUser>._, A<string>._))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetApplicationUserByEmail_ReturnsUser_WhenFound()
        {
            // Arrange: Create a test email
            var email = "test@gmail.com";
            var userToReturn = new ApplicationUser { Email = email };

            // Act: Call the method under test
            var result = await _userService.GetApplicationUserByEmail(email);

            // Assert: Verify the returned user matches the expected user
            Assert.NotNull(result);
            Assert.Equal(userToReturn.Email, result.Email);
            Assert.Equal(email, result.Email);
        }

        [Fact]
        public async Task GetApplicationUserByEmail_ReturnsNull_WhenNotFound()
        {
            // Arrange: Create a non-existent email
            var email = "nonexistent@example.com";

            // Mock UserManager behavior for FindByEmailAsync
            A.CallTo(() => _fakeUserManager.FindByEmailAsync(email))
                .Returns(Task.FromResult<ApplicationUser>(null));

            // Act: Call the method under test
            var result = await _userService.GetApplicationUserByEmail(email);

            // Assert: Verify that null is returned for non-existent user
            Assert.Null(result);
        }

        [Fact]
        public async Task CheckIfPasswordIsValid_ReturnsTrue_WhenPasswordCorrect()
        {
            var user = new ApplicationUser { UserName = "test", Email = "test@gmail.com" };
            var password = "Test123!";

            A.CallTo(() => _fakeUserManager.CheckPasswordAsync(user, password))
                .Returns(Task.FromResult(true));

            var result = await _userService.CheckIfPasswordIsValid(user, password);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldFIndUser()
        {
            var userId = 1;
            // Log information about records
            var records = await _fakeSyncPointDb.ExcelRecords.ToListAsync();

            // Act
            var record = await _fakeSyncPointDb.ExcelRecords.FirstOrDefaultAsync(r => r.StaticPageModification.ExcelRecordId == userId);

            _output.WriteLine($"ST: {record.StaticPageModification.ExcelRecordId}");
            // Assert
            Assert.NotNull(record);
            Assert.Equal(userId, record.StaticPageModification.ExcelRecordId);
        }
    }
}