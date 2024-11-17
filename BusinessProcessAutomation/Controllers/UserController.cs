using BusinessProcessAutomation.Application.Common.DTOs;
using BusinessProcessAutomation.Application.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessProcessAutomation.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public IActionResult AddOrUpdateUser(AddOrUpdateUserDTO user)
        {
            _userService.AddOrUpdateUser(user);
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAllUsers() 
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveUser(int id)
        {
            _userService.RemoveUser(id);
            return Ok("User has been removed successfully");
        }
    }
}
