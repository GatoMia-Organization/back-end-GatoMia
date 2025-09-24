using BackEndGatoMia.Models;

namespace BackEndGatoMia.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(string userId);

        Task<User> GetUserByEmailAsync(string email);


    }

}
