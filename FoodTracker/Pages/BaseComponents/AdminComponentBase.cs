using FoodTracker.Data.DTOs;
using FoodTracker.Service.DataServices.Abstraction;
using Microsoft.AspNetCore.Components;

namespace FoodTracker.Pages.BaseComponents;

public class AdminComponentBase : ComponentBase
{
    [Inject]
    protected IUserService UserService { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }
    
    [Inject]
    protected ILogger<AdminComponentBase> Logger { get; set; }

    private User? CurrentUser { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("OnInitializedAsync called in AdminComponentBase.");
        
        CurrentUser = await UserService.GetCurrentUser();
        if (CurrentUser == null)
        {
            Logger.LogInformation("CurrentUser is null, navigating to login.");
            Navigation.NavigateTo("/login");
        }
        else if (!CurrentUser.IsAdmin)
        {
            Logger.LogInformation($"CurrentUser is not admin: {CurrentUser.Username}");
            Navigation.NavigateTo("/");
        }
        else
        {
            Logger.LogInformation($"CurrentUser is admin: {CurrentUser.Username}");
        }
    }
}