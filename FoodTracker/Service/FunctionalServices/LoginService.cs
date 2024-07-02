using FoodTracker.Data.DTOs;
using FoodTracker.Service.DataServices.Abstraction;
using FoodTracker.Service.DataServices.DataAccess.Abstraction;
using FoodTracker.Service.FunctionalServices.Abstraction;
using Microsoft.Extensions.Logging;

namespace FoodTracker.Service.FunctionalServices;
public class LoginService : ILoginService
{
    private readonly IUserDataAccess _userDataAccess;
    private readonly IHashService _hashService;
    private readonly IUserService _userService;
    private readonly ILogger<LoginService> _logger;

    public LoginService(
        IUserDataAccess userDataAccess,
        IHashService hashService,
        IUserService userService,
        ILogger<LoginService> logger)
    {
        _userDataAccess = userDataAccess;
        _hashService = hashService;
        _userService = userService;
        _logger = logger;
    }

    public async Task<Response> Login(User user)
    {
        if (IsInvalidPassword(user))
        {
            return CreateErrorResponse("Password must be provided.", "Login attempt failed: Password must be provided.");
        }

        var attemptedUser = await GetAttemptedUser(user);
        if (attemptedUser == null)
        {
            return CreateErrorResponse($"User with provided credentials doesn't exist.", $"Login attempt failed: User with provided credentials doesn't exist.");
        }

        if (IsInvalidPassword(attemptedUser, user.HashedPassword))
        {
            return CreateErrorResponse("Invalid password.", "Login attempt failed: Invalid password.");
        }

        await _userService.SetCurrentUser(attemptedUser);
        _logger.LogInformation($"User {attemptedUser.Username} logged in successfully.");
        return new Response(true);
    }

    private bool IsInvalidPassword(User user)
    {
        if (string.IsNullOrEmpty(user.HashedPassword))
        {
            _logger.LogError("Login attempt failed: Password must be provided.");
            return true;
        }
        return false;
    }

    private async Task<User?> GetAttemptedUser(User user)
    {
        User? attemptedUser = null;

        if (!string.IsNullOrEmpty(user.Username))
        {
            attemptedUser = await _userDataAccess.GetByUsername(user.Username);
        }
        else if (!string.IsNullOrEmpty(user.Email))
        {
            attemptedUser = await _userDataAccess.GetByEmail(user.Email);
        }
        else
        {
            _logger.LogError("Login attempt failed: Username or email must be provided.");
        }

        return attemptedUser;
    }

    private bool IsInvalidPassword(User attemptedUser, string providedHashedPassword)
    {
        var hashedPassword = _hashService.HashString(providedHashedPassword);
        if (attemptedUser.HashedPassword != hashedPassword)
        {
            _logger.LogWarning("Login attempt failed: Invalid password.");
            return true;
        }
        return false;
    }

    private Response CreateErrorResponse(string message, string logMessage)
    {
        _logger.LogWarning(logMessage);
        return new Response(false, message);
    }

}