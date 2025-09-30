using BackEndGatoMia.Models.DTOs;


namespace BackEndGatoMia.Services
{
    public interface IUserService
    {
        Task<UserDto> AddUserAsync(CreateUserDto userDto);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(string userId, UpdateUserDto userDto);
        Task DeleteUserAsync(string userIdToDelete, string currentUserId);
    }
}