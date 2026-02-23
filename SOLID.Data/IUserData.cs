using SOLID.Entities;

namespace SOLID.Data
{
    public interface IUserData
    {
        Result<int> Create(User user);
        Result<List<User>> GetAllUsers();
        Result<int> Update(User user);
    }
}
