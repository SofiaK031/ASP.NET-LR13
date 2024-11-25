using LR13.Models;

namespace LR13.Services
{
    public class UserService
    {
        private List<UserModel> _users = new List<UserModel>();

        public void RegisterUser(string username, string email, string password)
        {
            _users.Add(new UserModel { Username = username, Email = email, Password = password });
        }

        public UserModel GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }
    }
}
