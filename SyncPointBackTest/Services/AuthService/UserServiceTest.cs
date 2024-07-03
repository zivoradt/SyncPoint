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

namespace SyncPointBackTest.Services.AuthService
{
    public class UserServiceTest
    {
        private UserService _userService;
        private IOptions<AppSettings> _fakeAppSettings;
        private UnitOfWork _fakeUnitOfWork;
        private ILogger<UserService> _fakeLogger;
        private UserManager<ApplicationUser> _fakeUserManager;

        public UserServiceTest()
        {
            var fakeSyncPointDb = DatabaseFactory.CreateSyncDbContext(true);

            var fakeAuthDbContext = DatabaseFactory.CreateAuthDbContext();

            // Create UnitOfWork instance
            _fakeUnitOfWork = new UnitOfWork(fakeSyncPointDb, fakeAuthDbContext);

            // Mocking other dependencies
            _fakeLogger = A.Fake<ILogger<UserService>>();

            // Mocking UserManager
            _fakeUserManager = UserManagerFactory.CreateUserManager();

            var appSettings = new AppSettings { Secret = "test_secret" }; // Adjust as per your AppSettings structure
            _fakeAppSettings = Options.Create(appSettings);

            // Set up UserManager behavior
            A.CallTo(() => _fakeUserManager.CreateAsync(A<ApplicationUser>._, A<string>._))
                .Returns(Task.FromResult(IdentityResult.Success));

            // Create UserService instance
            _userService = new UserService(_fakeAppSettings, _fakeUnitOfWork, _fakeLogger, _fakeUserManager);
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
            var email = "test@example.com";
            var userToReturn = new ApplicationUser { Id = "1", Email = email };

            // Mock UserManager behavior for FindByEmailAsync
            A.CallTo(() => _fakeUserManager.FindByEmailAsync(email))
                .Returns(Task.FromResult(userToReturn));

            // Act: Call the method under test
            var result = await _userService.GetApplicationUserByEmail(email);

            // Assert: Verify the returned user matches the expected user
            Assert.NotNull(result);
            Assert.Equal(userToReturn.Id, result.Id);
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
    }
}