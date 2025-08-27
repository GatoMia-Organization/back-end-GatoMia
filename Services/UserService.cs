using BCrypt.Net;
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

        var existingUser = await _userRepository.GetUserByEmailAsyn(user.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Usuário com esse email já existe.");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        await _userRepository.AddUserAsync(user);

    }
    public User GetUserById(string userId)
    {
        return null;
    }
    public void UpdateUser(User user)
    {

    }

    public void DeleteUser(string UserId)
    {

    }


}