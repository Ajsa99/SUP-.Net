using backend.Models;

namespace backend.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> GetUserAsync(int id);
        Task<User> FindUser(int id);
        Task<User> Authenticate(string UserName, string password);
        void Register(string FirstName, string Lastname, string Email, string UserName, int Mobile, DateTime DateBirthday, string Gender,
                string MaritalStatus, string City, string Address, string Password, string Type);
        Task<bool> UserAlreadyExists(string UserName);
    }
}
