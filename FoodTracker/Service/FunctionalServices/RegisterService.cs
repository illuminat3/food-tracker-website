using System.Text.RegularExpressions;
using FoodTracker.Data.DTOs;
using FoodTracker.Data.DTOs.Transfer;
using FoodTracker.Service.DataServices.Abstraction;
using FoodTracker.Service.DataServices.DataAccess.Abstraction;
using FoodTracker.Service.FunctionalServices.Abstraction;

namespace FoodTracker.Service.FunctionalServices;

public class RegisterService : IRegisterService
{
    private readonly IUserDataAccess _userDataAccess;
    private readonly IHashService _hashService;
    private readonly IUserService _userService;
    private readonly ILogger<RegisterService> _logger;

    public RegisterService(
        IUserDataAccess userDataAccess,
        IHashService hashService,
        IUserService userService,
        ILogger<RegisterService> logger)
    {
        _userDataAccess = userDataAccess;
        _hashService = hashService;
        _userService = userService;
        _logger = logger;
    }
    
    public async Task<Response> Register(User user)
    {
        var response = await ValidateUser(user);
        if (response.IsSuccess)
        {
            user.HashedPassword = _hashService.HashString(user.HashedPassword!);
            await _userDataAccess.Insert(user);
            await _userService.SetCurrentUser(user);
        }
        
        return response;
    }

    private async Task<Response> ValidateUser(User user)
    {
        if (!ValidateEmail(user.Email))
        {
            _logger.LogError("Registration attempt failed: Invalid email format.");
            return new Response(false, "Invalid email.");
        }

        if (!ValidateUsername(user.Username))
        {
            _logger.LogError("Registration attempt failed: Invalid username format.");
            return new Response(false, "Invalid username.");
        }

        if (!ValidatePassword(user.HashedPassword))
        {
            _logger.LogError("Registration attempt failed: Invalid password format.");
            return new Response(false, "Invalid password.");
        }
        
        var response = await CheckForExistingRegistration(user);
        
        return response;
    }
    

    private bool ValidateUsername(string? username)
    {
        if (username == null)
        {
            return false;
        }
        
        return username.Length >= 3 && !username.Any(char.IsWhiteSpace);
    }

    private bool ValidateEmail(string? email)
    {
        if (email == null)
        {
            return false;
        }
        
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(emailPattern);
        return regex.IsMatch(email);
    }
    
    private bool ValidatePassword(string? password)
    {
        return password != null && password.Length <= 20 && !password.Any(char.IsWhiteSpace);
    }

    private async Task<Response> CheckForExistingRegistration(User user)
    {
        Response response = new Response(false);

        var tempUser = await _userDataAccess.GetByUsername(user.Username!);
        if (tempUser != null)
        {
            _logger.LogError("Registration attempt failed: Username already exists.");
            response.Message = "Username already exists.";
            return response;
        }

        tempUser = await _userDataAccess.GetByEmail(user.Email!);
        if (tempUser != null)
        {
            _logger.LogError("Registration attempt failed: Email already exists.");
            response.Message = "Email already exists.";
            return response;
        }

        response.IsSuccess = true;
        return response;
    }

}