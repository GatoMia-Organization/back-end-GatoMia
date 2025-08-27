using BackEndGatoMia.Models;

namespace BackEndGatoMia.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(string userId);

        Task<User> GetUserByEmailAsyn(string email);


    }

}
