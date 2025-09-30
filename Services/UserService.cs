using BackEndGatoMia.Models;
using BackEndGatoMia.Models.DTOs;
using BackEndGatoMia.Repositories;


namespace BackEndGatoMia.Services
{
        public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> AddUserAsync(CreateUserDto userDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Usuário com este email já existe.");
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Phone = userDto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                IsActive = true,
                DateRegistration = DateTime.UtcNow,
                Role = Models.Enums.UserRole.User // Padrão para novo usuário
            };

            var createdUser = await _userRepository.AddUserAsync(user);

            return new UserDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
                Phone = createdUser.Phone,
                IsActive = createdUser.IsActive,
                Role = createdUser.Role
            };
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive
            };
        }

        public async Task UpdateUserAsync(string userId, UpdateUserDto userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            if (userDto.Name != null) existingUser.Name = userDto.Name;
            if (userDto.Phone != null) existingUser.Phone = userDto.Phone;

            await _userRepository.UpdateUserAsync(existingUser);
        }

        public async Task DeleteUserAsync(string userIdToDelete, string currentUserId)
        {
            if (userIdToDelete == currentUserId)
            {
                throw new InvalidOperationException("Não é possível desativar a si mesmo.");
            }

            var user = await _userRepository.GetUserByIdAsync(userIdToDelete);
            if (user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }

            user.IsActive = false;
            await _userRepository.UpdateUserAsync(user);
        }
    }
}