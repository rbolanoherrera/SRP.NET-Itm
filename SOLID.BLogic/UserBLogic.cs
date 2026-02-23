using SOLID.Data;
using SOLID.Entities;

namespace SOLID.BLogic
{
    public class UserBLogic : IUserBLogic
    {
        private readonly IUserData userData;

        public UserBLogic(IUserData userData)
        {
            this.userData = userData;
        }

        public Result<int> Create(User user)
        {
            return userData.Create(user);
        }

        public Result<List<User>> GetAllUsers()
        {
            return userData.GetAllUsers();
        }

        public Result<int> Update(User user)
        {
            return userData.Update(user);
        }
    }
}
