using BusinessProcessAutomation.Application.Common.DTOs;
using BusinessProcessAutomation.Application.Interface.IRepositories;
using BusinessProcessAutomation.Application.Interface.IServices;
using BusinessProcessAutomation.Domain.Entities;

namespace BusinessProcessAutomation.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddOrUpdateUser(AddOrUpdateUserDTO user)
        {
            var existingUser = _userRepository.FindByCondition(p => p.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.LinkedInUrl = user.LinkedInUrl;
                existingUser.GitHubUrl = user.GitHubUrl;
                existingUser.Comment = user.Comment;
                existingUser.TimeInterval = user.TimeInterval;
                existingUser.UpdatedOn = DateTimeOffset.UtcNow;

                _userRepository.Update(existingUser);
            }
            else
            {
                var newUser = new ApplicationUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    LinkedInUrl = user.LinkedInUrl,
                    GitHubUrl = user.GitHubUrl,
                    Comment = user.Comment,
                    TimeInterval = user.TimeInterval,
                    CreatedOn = DateTimeOffset.UtcNow
                };
                _userRepository.Create(newUser);
            }
            _userRepository.SaveChanges();
        }

        public void GetAll()
        {
            throw new NotImplementedException();
        }

        public void GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser()
        {
            throw new NotImplementedException();
        }
    }
}
