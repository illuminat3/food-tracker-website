using FoodTracker.Data.DTOs.Models;
using FoodTracker.Service.FunctionalServices.Abstraction;
using Microsoft.AspNetCore.Components;
using FoodTracker.Data.DTOs;
using FoodTracker.Data.DTOs.Transfer;

namespace FoodTracker.Pages;

public partial class Register
{
    [Inject] private IRegisterService _registerService { get; set; }
    [Inject] private ILogger<Register> _logger { get; set; }
    [Inject] private NavigationManager _navigationManager { get; set; }

    private RegisterModel registerModel = new RegisterModel();
    private Error _error = new Error(false);

    private async Task HandleValidSubmit()
    {
        _error.IsActive = false;

        if (string.IsNullOrEmpty(registerModel.Username) || 
            string.IsNullOrEmpty(registerModel.Password) || 
            string.IsNullOrEmpty(registerModel.Email) || 
            string.IsNullOrEmpty(registerModel.ConfirmPassword))
        {
            _error.IsActive = true;
            _error.Message = "All fields must have a value.";
            return;
        }
        
        
        if (registerModel.Password != registerModel.ConfirmPassword)
        {
            _error = new Error(isActive: true, message: "Passwords do not match.");
            return;
        }

        User user = new User
        {
            Username = registerModel.Username,
            Email = registerModel.Email,
            HashedPassword = registerModel.Password
        };

        var response = await _registerService.Register(user);
        
        if (response.IsSuccess)
        {
            _logger.LogInformation("User {username} registered successfully.", user.Username);
            _navigationManager.NavigateTo("/");
        }
        else
        {
            _error.IsActive = true;
            _error.Message = response.Message;
        }
    }
}