using BusinessProcessAutomation.Application.Common.DTOs;

namespace BusinessProcessAutomation.Application.Interface.IServices
{
    public interface IUserService
    {
        void AddOrUpdateUser(AddOrUpdateUserDTO user);
        void RemoveUser();
        void GetById(int id);
        void GetAll();

    }
}
