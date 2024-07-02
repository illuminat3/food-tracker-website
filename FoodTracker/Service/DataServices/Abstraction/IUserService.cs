using FoodTracker.Data.DTOs;

namespace FoodTracker.Service.DataServices.Abstraction;

public interface IUserService
{
    Task<User?> GetCurrentUser();

    Task<bool> SetCurrentUser(User? user);
}