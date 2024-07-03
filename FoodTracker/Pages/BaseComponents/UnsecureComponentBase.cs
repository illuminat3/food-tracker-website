using FoodTracker.Data.DTOs;
using FoodTracker.Service.DataServices.Abstraction;
using Microsoft.AspNetCore.Components;

namespace FoodTracker.Pages.BaseComponents;

public class UnsecureComponentBase : ComponentBase
{
    [Inject]
    protected IUserService UserService { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }
    
    [Inject]
    protected ILogger<UnsecureComponentBase> Logger { get; set; }

    private User? CurrentUser { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("OnInitializedAsync called in UnsecureComponentBase.");
        
        CurrentUser = await UserService.GetCurrentUser();
        if (CurrentUser != null)
        {
            Logger.LogInformation("CurrentUser exists. Stopping them from viewing this page.");
            Navigation.NavigateTo("/");
        }
    }
}