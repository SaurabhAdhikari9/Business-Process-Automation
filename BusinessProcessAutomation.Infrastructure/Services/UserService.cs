using BusinessProcessAutomation.Application.Common.CustomeExceptions;
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

        public List<UserDTO> GetAll()
        {
            return _userRepository.GetAll().Select(p => new UserDTO
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Comment = p.Comment,
                GitHubUrl = p.GitHubUrl,
                LinkedInUrl = p.LinkedInUrl,
                PhoneNumber = p.PhoneNumber,
                TimeInterval = p.TimeInterval

            }).ToList();

        }

        public UserDTO GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email  = user.Email,
                PhoneNumber = user.PhoneNumber,
                GitHubUrl = user.GitHubUrl,
                LinkedInUrl = user.GitHubUrl,
                TimeInterval = user.TimeInterval,
                Comment = user.Comment
            };
        }

        public void RemoveUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                _userRepository.Delete(user);
                _userRepository.SaveChanges();
                return;
            }
            throw new NotFoundException("user not found");
        }
    }
}
