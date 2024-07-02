using FoodTracker.Data.DTOs;
using FoodTracker.Service.DataServices.Abstraction;
using Microsoft.AspNetCore.Components;

namespace FoodTracker.Pages.BaseComponents;

public class SecureComponentBase : ComponentBase
{
    [Inject]
    protected IUserService UserService { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }
    
    [Inject]
    protected ILogger<SecureComponentBase> Logger { get; set; }

    private User? CurrentUser { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("OnInitializedAsync called in SecureComponentBase.");
        
        CurrentUser = await UserService.GetCurrentUser();
        if (CurrentUser == null)
        {
            Logger.LogInformation("CurrentUser is null, navigating to login.");
            Navigation.NavigateTo("/login");
        }
        else
        {
            Logger.LogInformation($"CurrentUser: {CurrentUser.Username}");
        }
    }
}