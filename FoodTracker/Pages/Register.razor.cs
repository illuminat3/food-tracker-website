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

    private RegisterModel _registerModel = new RegisterModel();
    private Error _error = new Error(false);

    private async Task HandleValidSubmit()
    {
        _error.IsActive = false;

        if (string.IsNullOrEmpty(_registerModel.Username) || 
            string.IsNullOrEmpty(_registerModel.Password) || 
            string.IsNullOrEmpty(_registerModel.Email) || 
            string.IsNullOrEmpty(_registerModel.ConfirmPassword))
        {
            _error.IsActive = true;
            _error.Message = "All fields must have a value.";
            return;
        }
        
        
        if (_registerModel.Password != _registerModel.ConfirmPassword)
        {
            _error = new Error(isActive: true, message: "Passwords do not match.");
            return;
        }

        User user = new User
        {
            Username = _registerModel.Username,
            Email = _registerModel.Email,
            HashedPassword = _registerModel.Password
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

    private void NavigateToLogin()
    {
        _navigationManager.NavigateTo("/login");
    }
    
}