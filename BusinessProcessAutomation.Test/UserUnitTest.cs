using BusinessProcessAutomation.Application.Common.CustomeExceptions;
using BusinessProcessAutomation.Application.Common.DTOs;
using BusinessProcessAutomation.Application.Interface.IServices;
using BusinessProcessAutomation.Extensions;
using BusinessProcessAutomation.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.ComponentModel.DataAnnotations;

namespace BusinessProcessAutomation.Test
{
    public class UserUnitTest
    {

        private readonly IUserService _userService;

        public UserUnitTest()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "TestDb"));
            serviceCollection.ConfigureDependencies();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _userService = serviceProvider.GetService<IUserService>();

        }
        [Fact]
        public void Add_User_Success_Scenario()
        {
            // arramge
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
        [Fact]
        public void Update_User_Success_Scenario()
        {
            // arramge
            var newInformation = new AddOrUpdateUserDTO
            {
                FirstName = "Saurabh",
                LastName = "Adhikari",
                Email = "sauravadhikari98@gmail.com",
                PhoneNumber = "+0987654321",
                GitHubUrl = "https://www.github.com",
                LinkedInUrl = "https://www.linkedin.com",
                Comment = "update user information",
                TimeInterval = "After 20 min",
            };

            // act
            _userService.AddOrUpdateUser(newInformation);
            var user = _userService.GetAll().Where(p => p.Email == newInformation.Email).FirstOrDefault();

            // assert
            user.FirstName.ShouldBe(newInformation.FirstName);
            user.PhoneNumber.ShouldBe(newInformation.PhoneNumber);
            user.TimeInterval.ShouldBe(newInformation.TimeInterval);
            user.GitHubUrl.ShouldBe(newInformation.GitHubUrl);
            user.LinkedInUrl.ShouldBe(newInformation.LinkedInUrl);
        }

        [Fact]
        public void Add_User_With_Invalid_Email()
        {
            // arramge
            var newUser = new AddOrUpdateUserDTO
            {
                FirstName = "Saurabh",
                LastName = "Adhikari",
                Email = "sauravadhikar@gmail",
                PhoneNumber = "+1231231230",
                GitHubUrl = null,
                LinkedInUrl = null,
                Comment = "test",
                TimeInterval = null,
            };

            //act
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(newUser, new ValidationContext(newUser), validationResults, true);

            //assert
            isValid.ShouldBeFalse();
            validationResults.ShouldNotBeNull();
            validationResults[0].ErrorMessage.ShouldBe("Invalid email address.");
        }


        [Fact]
        public void Get_Existing_User_By_Id()
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
            _userService.AddOrUpdateUser(newUser);
            var id = _userService.GetAll().FirstOrDefault(p=>p.Email == newUser.Email).Id;

            // act
            var user = _userService.GetById(id);

            // assert
            user.ShouldNotBeNull();
            user.FirstName.ShouldBe(newUser.FirstName);
            user.LastName.ShouldBe(newUser.LastName);
            user.PhoneNumber.ShouldBe(newUser.PhoneNumber);
            user.Comment.ShouldBe(newUser.Comment);
            user.LinkedInUrl.ShouldBe(newUser.LinkedInUrl);
            user.GitHubUrl.ShouldBe(newUser.GitHubUrl);
            user.TimeInterval.ShouldBe(newUser.TimeInterval);

        }

        [Fact]
        public void Remove_Not_Existing_User()
        {
            // arramge 
            var id = 100;

            // act & assert
            Should.Throw<NotFoundException>(() => _userService.RemoveUser(id));
        }

        [Fact]
        public void Remove_Existing_User()
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
            var id = 1;
            _userService.AddOrUpdateUser(newUser);

            // act
            _userService.RemoveUser(id);

            // assert
            Should.Throw<NotFoundException>(() => _userService.GetById(id));
        }

        [Fact]
        public void Get_Not_Existing_User_By_Id()
        {
            // arrange
            var id = 100;

            // act & assert
            Should.Throw<NotFoundException>(() => _userService.GetById(id));
        }

        [Fact]
        public void Add_User_With_First_Name_Null()
        {
            // arrange
            var newUser = new AddOrUpdateUserDTO
            {
                FirstName = null,
                LastName = "Adhikari",
                Email = "sauravadhikari98@gmail.com",
                PhoneNumber = "+0987654321",
                GitHubUrl = "https://www.github.com",
                LinkedInUrl = "https://www.linkedin.com",
                Comment = "update user information",
                TimeInterval = "After 20 min",
            };

            // act
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(newUser, new ValidationContext(newUser), validationResults, true);

            //assert
            isValid.ShouldBeFalse();
            validationResults.ShouldNotBeNull();
            validationResults[0].ErrorMessage.ShouldBe("The FirstName field is required.");
        }


        [Fact]
        public void Add_User_With_Last_Name_Null()
        {
            // arrange
            var newUser = new AddOrUpdateUserDTO
            {
                FirstName = "Saurabh",
                LastName = null,
                Email = "sauravadhikari98@gmail.com",
                PhoneNumber = "+0987654321",
                GitHubUrl = "https://www.github.com",
                LinkedInUrl = "https://www.linkedin.com",
                Comment = "update user information",
                TimeInterval = "After 20 min",
            };

            // act
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(newUser, new ValidationContext(newUser), validationResults, true);

            //assert
            isValid.ShouldBeFalse();
            validationResults.ShouldNotBeNull();
            validationResults[0].ErrorMessage.ShouldBe("The LastName field is required.");
        }

        [Fact]
        public void Add_User_With_EmptyString_In_First_Name()
        {
            // arrange
            var newUser = new AddOrUpdateUserDTO
            {
                FirstName =string.Empty,
                LastName = "Adhikari",
                Email = "sauravadhikari98@gmail.com",
                PhoneNumber = "+0987654321",
                GitHubUrl = "https://www.github.com",
                LinkedInUrl = "https://www.linkedin.com",
                Comment = "update user information",
                TimeInterval = "After 20 min",
            };

            // act
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(newUser, new ValidationContext(newUser), validationResults, true);

            //assert
            isValid.ShouldBeFalse();
            validationResults.ShouldNotBeNull();
            validationResults[0].ErrorMessage.ShouldBe("The FirstName field is required.");
        }


        [Fact]
        public void Add_User_With_EmptyString_In_Last_Name()
        {
            // arrange
            var newUser = new AddOrUpdateUserDTO
            {
                FirstName = "Saurabh",
                LastName = string.Empty,
                Email = "sauravadhikari98@gmail.com",
                PhoneNumber = "+0987654321",
                GitHubUrl = "https://www.github.com",
                LinkedInUrl = "https://www.linkedin.com",
                Comment = "update user information",
                TimeInterval = "After 20 min",
            };

            // act
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(newUser, new ValidationContext(newUser), validationResults, true);

            //assert
            isValid.ShouldBeFalse();
            validationResults.ShouldNotBeNull();
            validationResults[0].ErrorMessage.ShouldBe("The LastName field is required.");
        }


    }
}