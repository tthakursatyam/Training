using AuthService.API.Entities;

namespace AuthService.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task AddUser(User user);
        Task Save();
    }
}