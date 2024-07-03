using Microsoft.AspNetCore.Components;

namespace FoodTracker.Pages;

public partial class Login
{
    [Inject] private NavigationManager _navigationManager {get; set;}
    
    private void NavigateToRegister()
    {
        _navigationManager.NavigateTo("/register");
    }
}