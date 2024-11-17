using BusinessProcessAutomation.Application.Common.DTOs;
using BusinessProcessAutomation.Application.Interface.IRepositories;
using BusinessProcessAutomation.Application.Interface.IServices;
using BusinessProcessAutomation.Infrastructure.DbContext;
using BusinessProcessAutomation.Infrastructure.Repositories;
using BusinessProcessAutomation.Infrastructure.Services;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace BusinessProcessAutomation.Test
{
    public class UserUnitTest
    {

        private readonly IUserService _userService;

        public UserUnitTest()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "TestDb"));
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _userService = serviceProvider.GetService<IUserService>();

        }
        [Fact]
        public void AddOrUpdateUserTest()
        {
            // arrange
            var newUser = new AddOrUpdateUserDTO
            {
                FirstName = "Saurabh",
                LastName = "Adhikari",
                Email = "sauravadhikari98@gmail.com",
                PhoneNumber = "+1231231230",
                GitHubUrl = null,
                LinkedInUrl = null,
                Comment = "test",
                TimeInterval = null,
            };

            // act
            _userService.AddOrUpdateUser(newUser);
            var result = _userService.GetAll().Where(p => p.Email == newUser.Email).FirstOrDefault();


            // assert
            result.ShouldNotBe(null);
            result.FirstName.ShouldMatch(newUser.FirstName);
            result.LastName.ShouldMatch(result.LastName);
            result.GitHubUrl.ShouldBe(null);
            result.LinkedInUrl.ShouldBe(null);
            result.TimeInterval.ShouldBe(null);
            result.Comment.ShouldMatch(newUser.Comment);
            result.Id.ShouldBeGreaterThan(0);

        }
    }
}