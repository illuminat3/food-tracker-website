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

    private User? CurrentUser { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CurrentUser = await UserService.GetCurrentUser();
        if (CurrentUser == null)
        {
            Navigation.NavigateTo("/login");
        }
    }
}