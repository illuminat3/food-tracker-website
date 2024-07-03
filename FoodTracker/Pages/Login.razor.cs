using System.Text.RegularExpressions;
using FoodTracker.Data.DTOs;
using FoodTracker.Data.DTOs.Models;
using FoodTracker.Data.DTOs.Transfer;
using FoodTracker.Service.DataServices.Abstraction;
using FoodTracker.Service.DataServices.DataAccess.Abstraction;
using FoodTracker.Service.FunctionalServices.Abstraction;
using Microsoft.AspNetCore.Components;

namespace FoodTracker.Pages;

public partial class Login
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IUserService _userService { get; set; }
    [Inject] private IRegisterService _registerService { get; set; }
    [Inject] private IUserDataAccess _userDataAccess { get; set; }
    [Inject] private ILogger<Login> _logger { get; set; }
    [Inject] private IHashService _hashService { get; set; }

    private Error _error = new Error(false);
    private readonly LoginModel _loginModel = new LoginModel();
    
    private void NavigateToRegister()
    {
        _navigationManager.NavigateTo("/register");
    }

    private async Task HandleValidSubmit()
    {
        _error.IsActive = false;
        var isEmail = IsEmail(_loginModel.UsernameOrEmail);
        User? user = null;
        
        if (isEmail)
        {
            
            user = await CheckEmail(_loginModel.UsernameOrEmail) ?? await CheckUsername(_loginModel.UsernameOrEmail);
        }
        else
        {
            user = await CheckUsername(_loginModel.UsernameOrEmail) ?? await CheckEmail(_loginModel.UsernameOrEmail);
        }

        if (user == null)
        {
            _error.IsActive = true;
            _error.Message = "Invalid username or email.";
            return;
        }
        
        var password = _hashService.HashString(_loginModel.Password);
        
        if (user.HashedPassword != password)
        {
            _error.IsActive = true;
            _error.Message = "Incorrect password. Please try again.";
            return;
        }

        await _userService.SetCurrentUser(user);
        _navigationManager.NavigateTo("/");
    }
    
    private async Task<User?> CheckUsername(string username)
    {
        var user = await _userDataAccess.GetByUsername(username);
        return user;
    }
    
    private async Task<User?> CheckEmail(string email)
    {
        var user = await _userDataAccess.GetByEmail(email);
        return user;
    }
    
    private bool IsEmail(string input)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(emailPattern);
        return regex.IsMatch(input);
    }
}