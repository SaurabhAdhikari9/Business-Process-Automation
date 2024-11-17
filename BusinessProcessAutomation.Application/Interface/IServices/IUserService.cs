using BusinessProcessAutomation.Application.Common.DTOs;

namespace BusinessProcessAutomation.Application.Interface.IServices
{
    public interface IUserService
    {
        void AddOrUpdateUser(AddOrUpdateUserDTO user);
        void RemoveUser(int id);
        UserDTO GetById(int id);
        List<UserDTO> GetAll();

    }
}
