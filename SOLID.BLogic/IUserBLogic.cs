using SOLID.Entities;

namespace SOLID.BLogic
{
    public interface IUserBLogic
    {
        Result<int> Create(User user);
        Result<List<User>> GetAllUsers();

        Result<int> Update(User user);
    }
}