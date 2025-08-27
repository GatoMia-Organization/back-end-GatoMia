using BackEndGatoMia.Models;
using BackEndGatoMia.Repositories;

public class UserService
{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }
    public async Task AddUser(User user)
    {
        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
        {
            throw new ArgumentException("Email e senha requeridos.");
        }

        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Usuário com esse email já existe.");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        await _userRepository.AddUserAsync(user);

    }
    public async Task<User> GetUserById(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user != null)
        {
            user.PasswordHash = null;
        }

        return user;
    }
    public async Task UpdateUser(User user)
    {
        if (string.IsNullOrEmpty(user.Id))
        {
            throw new ArgumentException("Id de usuário não encontratado.");
        }
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUser(string userIdToDelete, string currentUserId)
    {
        if (userIdToDelete == currentUserId)
        {
            throw new InvalidOperationException("Não é possível deletar a si mesmo.");
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