using FoodTracker.Data.DTOs;
using FoodTracker.Service.DataServices.Abstraction;
using Microsoft.AspNetCore.Components;

namespace FoodTracker.Pages;

public partial class SignOut
{
    [Inject] private NavigationManager _navigationManager { get; set; }
    [Inject] private IUserService _userService { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            User? nullUser = null;
            await _userService.SetCurrentUser(nullUser);
            _navigationManager.NavigateTo("/login");
        }
    }
}